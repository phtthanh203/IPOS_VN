using IPOS.DB;
using IPOS.DB_Access;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace IPOS
{
    public partial class Form_Table : Form
    {
        public Form_Table()
        {
            InitializeComponent();
        }

        private void Form_Table_Load(object sender, EventArgs e)
        {
            LoadBanButtonsFromButton1();
        }

        private bool CheckWorkSchedule(int userId)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString))
            {
                string query = @"
                    SELECT DATEDIFF(MINUTE, StartTime, CAST(GETDATE() AS TIME)) AS LateMinutes
                    FROM WorkSchedule
                    WHERE UserId = @UserId AND WorkDate = CAST(GETDATE() AS DATE)";

                var cmd = new System.Data.SqlClient.SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int lateMinutes = Convert.ToInt32(reader["LateMinutes"]);
                    if (lateMinutes < 0)
                    {
                        MessageBox.Show("Bạn đến sớm hơn giờ làm việc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else if (lateMinutes > 0)
                    {
                        MessageBox.Show($"Bạn đã trễ {lateMinutes} phút!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return true;
                }
                else
                {
                    return false; // Không có ca hôm nay
                }
            }
        }

        private void LoadBanButtonsFromButton1()
        {
            Table_Details_Access tda = new Table_Details_Access();
            List<Table_Details> danhSachBan = tda.GetAllTables();

            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();

            ToolTip tooltip = new ToolTip();

            foreach (var ban in danhSachBan)
            {
                bool isOccupied = IsTableOccupied(ban.ID); // kiểm tra đơn đang xử lý

                Button btn = new Button
                {
                    Size = button1.Size,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Margin = button1.Margin,
                    ForeColor = !isOccupied ? Color.Green : Color.Red,
                    TextAlign = ContentAlignment.BottomCenter,
                    FlatStyle = button1.FlatStyle,
                    Text = ban.nameTable,
                    Tag = ban,
                    Name = $"btnBan{ban.ID}",
                    ImageAlign = ContentAlignment.TopCenter,
                    Image = !isOccupied ? Properties.Resources.Table_Free : Properties.Resources.Table_Occupied
                };

                tooltip.SetToolTip(btn, $"Bàn: {ban.nameTable}\nTrạng thái: {(isOccupied ? "Đang phục vụ" : "Trống")}");
                btn.Click += Btn_Click;

                if (ban.IsTakeAway)
                    flowLayoutPanel2.Controls.Add(btn);
                else
                    flowLayoutPanel1.Controls.Add(btn);
            }

            button1.Visible = false;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Table_Details ban)
            {
                if (IsTableOccupied(ban.ID))
                {
                    MessageBox.Show("Bàn này đang có khách hoặc đang xử lý đơn hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Order ord = new Order(ban.ID); // truyền ID bàn
                ord.Show();
                this.Hide();
            }
        }

        private bool IsTableOccupied(int tableId)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT COUNT(*) 
                    FROM Orders 
                    WHERE TableId = @TableId AND Status IN ('New', 'Preparing')";

                var cmd = new System.Data.SqlClient.SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TableId", tableId);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int userId = Session.CurrentUserId;

            if (!CheckWorkSchedule(userId))
            {
                MessageBox.Show("Bạn không được phép truy cập vào ca làm chưa đến giờ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            Shift_Manager shiftManagerForm = new Shift_Manager(userId);
            shiftManagerForm.Show();
            this.Hide();
        }
    }
}
