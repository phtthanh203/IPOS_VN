using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IPOS
{
    public static class Session
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUserFullName { get; set; }
        public static string CurrentUserRole { get; set; }
    }

    public partial class Form_SignIn : Form
    {
        public Form_SignIn()
        {
            InitializeComponent();
            SetPlaceholder();
            this.Load += Form_SignIn_Load;
            this.Resize += Form_SignIn_Load;
        }

        public static class NativeMethods
        {
            [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
            public static extern IntPtr CreateRoundRectRgn(
                int nLeftRect, int nTopRect,
                int nRightRect, int nBottomRect,
                int nWidthEllipse, int nHeightEllipse);
        }

        private void Form_SignIn_Load(object sender, EventArgs e)
        {
            panelContainer.Location = new Point(
                (this.ClientSize.Width - panelContainer.Width) / 2,
                (this.ClientSize.Height - panelContainer.Height) / 2
            );

            panelContainer.Region = Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, panelContainer.Width, panelContainer.Height, 20, 20)
            );
        }

        private void SetPlaceholder()
        {
            textBoxUsername.Text = "Tên đăng nhập";
            textBoxUsername.ForeColor = Color.Gray;

            textBoxPassword.Text = "Mật khẩu";
            textBoxPassword.ForeColor = Color.Gray;
            textBoxPassword.UseSystemPasswordChar = false;
        }

        private void textBoxUsername_Enter(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "Tên đăng nhập")
            {
                textBoxUsername.Text = "";
                textBoxUsername.ForeColor = Color.Black;
            }
        }

        private void textBoxUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                textBoxUsername.Text = "Tên đăng nhập";
                textBoxUsername.ForeColor = Color.Gray;
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "Mật khẩu")
            {
                textBoxPassword.Text = "";
                textBoxPassword.ForeColor = Color.Black;
                textBoxPassword.UseSystemPasswordChar = true;
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                textBoxPassword.Text = "Mật khẩu";
                textBoxPassword.ForeColor = Color.Gray;
                textBoxPassword.UseSystemPasswordChar = false;
            }
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            if (username == "" || username == "Tên đăng nhập" ||
                password == "" || password == "Mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Users WHERE Username = @username AND PasswordHash = @password AND IsActive = 1";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password); // Thực tế nên mã hóa

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int userId = Convert.ToInt32(reader["UserId"]);
                        string fullname = reader["FullName"].ToString();
                        string role = reader["Role"].ToString();

                        // Lưu vào session
                        Session.CurrentUserId = userId;
                        Session.CurrentUserFullName = fullname;
                        Session.CurrentUserRole = role;

                        MessageBox.Show($"Đăng nhập thành công!\nXin chào: {fullname}", "IPOS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Form_Table mainForm = new Form_Table();
                        mainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối DB: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                buttonSignIn.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
