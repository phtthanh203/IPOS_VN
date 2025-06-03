using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace IPOS
{
    public partial class Shift_Manager : Form
    {
        private int currentUserId;
        private int shiftId;

        public Shift_Manager(int userId)
        {
            InitializeComponent();
            currentUserId = userId;

            var userInfo = GetUserInfo(currentUserId);
            if (userInfo == null)
            {
                MessageBox.Show("Không tìm thấy người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            string fullName = userInfo.Item1;
            string email = userInfo.Item2;

            if (!CheckWorkSchedule(currentUserId, out DateTime startTime))
            {
                MessageBox.Show("Không có lịch làm việc hôm nay cho nhân viên này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int late = (int)(DateTime.Now - startTime).TotalMinutes;
            if (late > 0)
                MessageBox.Show($"Bạn đến trễ {late} phút!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Bạn đến đúng giờ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string shiftCode = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            LoadShiftInfo(shiftCode, fullName, DateTime.Now);
            SaveShiftToDatabase(shiftCode, DateTime.Now);
            LoadOrdersFromDatabase();
        }

        private Tuple<string, string> GetUserInfo(int userId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT FullName, Email FROM Users WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Tuple.Create(
                            reader["FullName"].ToString(),
                            reader["Email"].ToString()
                        );
                    }
                }
            }
            return null;
        }

        private bool CheckWorkSchedule(int userId, out DateTime scheduledStart)
        {
            scheduledStart = DateTime.MinValue;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT StartTime 
                    FROM WorkSchedule 
                    WHERE UserId = @UserId AND WorkDate = CAST(GETDATE() AS DATE)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                object result = cmd.ExecuteScalar();
                if (result != null && TimeSpan.TryParse(result.ToString(), out TimeSpan startTime))
                {
                    scheduledStart = DateTime.Today.Add(startTime);
                    return true;
                }
            }
            return false;
        }

        private void SaveShiftToDatabase(string shiftCode, DateTime openTime)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Shifts (UserId, ShiftCode, OpenTime) 
                                 OUTPUT INSERTED.ShiftId
                                 VALUES (@UserId, @ShiftCode, @OpenTime)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", currentUserId);
                cmd.Parameters.AddWithValue("@ShiftCode", shiftCode);
                cmd.Parameters.AddWithValue("@OpenTime", openTime);

                shiftId = (int)cmd.ExecuteScalar();
            }
        }

        private void LoadShiftInfo(string shiftCode, string employee, DateTime openTime)
        {
            lblShiftCode.Text = $"Mã ca: {shiftCode}";
            lblEmployee.Text = $"Nhân viên: {employee}";
            lblOpenTime.Text = $"Giờ mở ca: {openTime:dd/MM/yyyy, HH:mm}";
            lblStatus.Text = "Trạng thái: Đang mở";
        }

        private void LoadOrdersFromDatabase()
        {
            flowOrders.Controls.Clear();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                SELECT o.OrderCode, o.OrderId, 
                       CASE WHEN o.IsTakeAway = 1 THEN N'Mang về' ELSE N'Đơn tại chỗ' END AS OrderType,
                       CONCAT(o.TotalAmount, ' đ') AS Amount,
                       ISNULL(t.nameTable, 'GRABFOOD') AS Channel,
                       FORMAT(o.OrderTime, 'HH:mm') AS Time
                FROM Orders o
                LEFT JOIN Table_Details t ON o.TableId = t.ID
                WHERE o.Status IN ('New', 'Preparing')
                ORDER BY o.OrderTime DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Panel card = new Panel
                    {
                        Size = new Size(380, 220),
                        Margin = new Padding(12),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White
                    };

                    string orderId = reader["OrderId"].ToString();

                    var title = new Label
                    {
                        Text = $"📝 {reader["OrderCode"]}   |   HĐ: {orderId}",
                        Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                        Dock = DockStyle.Top,
                        Height = 36,
                        Padding = new Padding(12, 6, 0, 0),
                        ForeColor = Color.Black
                    };

                    var typeAndChannel = new Label
                    {
                        Text = $"📦 {reader["OrderType"]}       📍 {reader["Channel"]}",
                        Font = new Font("Segoe UI", 12F),
                        Dock = DockStyle.Top,
                        Height = 32,
                        Padding = new Padding(12, 4, 0, 0),
                        ForeColor = Color.DimGray
                    };

                    var amountAndTime = new Label
                    {
                        Text = $"💰 {reader["Amount"]}       ⏰ {reader["Time"]}",
                        Font = new Font("Segoe UI", 12F),
                        Dock = DockStyle.Top,
                        Height = 32,
                        Padding = new Padding(12, 4, 0, 0),
                        ForeColor = Color.DimGray
                    };

                    List<string> products = new List<string>();
                    using (SqlCommand productCmd = new SqlCommand(@"
                        SELECT P.ProductName, OD.Quantity 
                        FROM OrderDetails OD 
                        JOIN Products P ON OD.ProductId = P.ProductId 
                        WHERE OD.OrderId = @OrderId", conn))
                    {
                        productCmd.Parameters.AddWithValue("@OrderId", orderId);
                        using (SqlDataReader prodReader = productCmd.ExecuteReader())
                        {
                            while (prodReader.Read())
                            {
                                products.Add($"- {prodReader["ProductName"]} x{prodReader["Quantity"]}");
                            }
                        }
                    }

                    var productLabel = new Label
                    {
                        Text = string.Join("\n", products),
                        Font = new Font("Segoe UI", 11F),
                        ForeColor = Color.Black,
                        Dock = DockStyle.Fill,
                        Padding = new Padding(12, 0, 0, 0)
                    };

                    var completeButton = new Button
                    {
                        Text = "✅ Hoàn thành",
                        BackColor = Color.LightGreen,
                        ForeColor = Color.Black,
                        Dock = DockStyle.Bottom,
                        Height = 40,
                        Tag = orderId,
                        FlatStyle = FlatStyle.Flat
                    };
                    completeButton.Click += (s, e) =>
                    {
                        string oid = ((Button)s).Tag.ToString();
                        using (SqlConnection conn2 = new SqlConnection(connectionString))
                        {
                            conn2.Open();
                            SqlCommand doneCmd = new SqlCommand("UPDATE Orders SET Status = 'Served' WHERE OrderId = @OrderId", conn2);
                            doneCmd.Parameters.AddWithValue("@OrderId", oid);
                            doneCmd.ExecuteNonQuery();
                        }
                        MessageBox.Show($"Đã hoàn thành đơn {oid}!");
                        LoadOrdersFromDatabase();
                    };

                    card.Controls.Add(completeButton);
                    card.Controls.Add(productLabel);
                    card.Controls.Add(amountAndTime);
                    card.Controls.Add(typeAndChannel);
                    card.Controls.Add(title);

                    flowOrders.Controls.Add(card);
                }
            }
        }

        private void btnCloseShift_Click(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Shifts SET CloseTime = @CloseTime WHERE ShiftId = @ShiftId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CloseTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@ShiftId", shiftId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Đã đóng ca!");
            lblStatus.Text = "Trạng thái: Đã đóng";

            // 👉 Quay lại form chọn bàn
            this.Hide();
            Form_Table tableForm = new Form_Table();
            tableForm.Show();
        }

        private void btnCloseShift_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form_Table tableForm = new Form_Table();
            tableForm.Show();
        }
    }
}
