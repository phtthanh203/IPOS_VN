namespace IPOS
{
    partial class Form_SignIn
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubTitle;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonSignIn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelContainer = new System.Windows.Forms.Panel();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSubTitle = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonSignIn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();

            // panelContainer
            this.panelContainer.BackColor = System.Drawing.Color.White;
            this.panelContainer.Size = new System.Drawing.Size(600, 460);
            this.panelContainer.Padding = new System.Windows.Forms.Padding(10);
            this.panelContainer.Location = new System.Drawing.Point(300, 70);
            this.panelContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelContainer.Controls.Add(this.logoBox);
            this.panelContainer.Controls.Add(this.labelTitle);
            this.panelContainer.Controls.Add(this.labelSubTitle);
            this.panelContainer.Controls.Add(this.labelUsername);
            this.panelContainer.Controls.Add(this.textBoxUsername);
            this.panelContainer.Controls.Add(this.labelPassword);
            this.panelContainer.Controls.Add(this.textBoxPassword);
            this.panelContainer.Controls.Add(this.buttonSignIn);

            // logoBox
            this.logoBox.Size = new System.Drawing.Size(80, 80);
            this.logoBox.Location = new System.Drawing.Point(260, 10);
            this.logoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoBox.Image = global::IPOS.Properties.Resources.logo_placeholder;

            // labelTitle
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.labelTitle.Location = new System.Drawing.Point(190, 100);
            this.labelTitle.Text = "Quản Lý IPOS";

            // labelSubTitle
            this.labelSubTitle.AutoSize = true;
            this.labelSubTitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelSubTitle.ForeColor = System.Drawing.Color.DimGray;
            this.labelSubTitle.Location = new System.Drawing.Point(165, 135);
            this.labelSubTitle.Text = "Giải pháp quản lý quán cà phê hiện đại";

            // labelUsername
            this.labelUsername.Location = new System.Drawing.Point(50, 190);
            this.labelUsername.Size = new System.Drawing.Size(100, 25);
            this.labelUsername.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelUsername.Text = "Tài khoản:";

            // textBoxUsername
            this.textBoxUsername.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.textBoxUsername.Size = new System.Drawing.Size(400, 32);
            this.textBoxUsername.Location = new System.Drawing.Point(150, 190);
            this.textBoxUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUsername.Enter += new System.EventHandler(this.textBoxUsername_Enter);
            this.textBoxUsername.Leave += new System.EventHandler(this.textBoxUsername_Leave);

            // labelPassword
            this.labelPassword.Location = new System.Drawing.Point(50, 240);
            this.labelPassword.Size = new System.Drawing.Size(100, 25);
            this.labelPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.labelPassword.Text = "Mật khẩu:";

            // textBoxPassword
            this.textBoxPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.textBoxPassword.Size = new System.Drawing.Size(400, 32);
            this.textBoxPassword.Location = new System.Drawing.Point(150, 240);
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassword.Enter += new System.EventHandler(this.textBoxPassword_Enter);
            this.textBoxPassword.Leave += new System.EventHandler(this.textBoxPassword_Leave);

            // buttonSignIn
            this.buttonSignIn.Text = "Đăng nhập";
            this.buttonSignIn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonSignIn.BackColor = System.Drawing.Color.SeaGreen;
            this.buttonSignIn.ForeColor = System.Drawing.Color.White;
            this.buttonSignIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSignIn.FlatAppearance.BorderSize = 0;
            this.buttonSignIn.Size = new System.Drawing.Size(220, 45);
            this.buttonSignIn.Location = new System.Drawing.Point(190, 310);
            this.buttonSignIn.Click += new System.EventHandler(this.buttonSignIn_Click);

            // Form_SignIn
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.panelContainer);
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form_SignIn";
            this.Text = "Đăng nhập hệ thống IPOS";

            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
