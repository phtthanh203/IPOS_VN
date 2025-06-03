using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IPOS
{
    partial class Order
    {
        private System.ComponentModel.IContainer components = null;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int left, int top, int right, int bottom, int width, int height);

        private Panel panelHeader;
        private Label label1;
        private Label label2;
        private Label label6;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private TextBox txtSearch;
        private Button button2;
        private FlowLayoutPanel flowLayoutPanel3;
        private Panel panel4;
        private Label label3;
        private PictureBox pictureBox1;
        private Label labelImageDescription;
        private Button button13;
        private Button button14;
        private Button button15;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 2
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F));  // Danh mục
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Sản phẩm
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 360F)); // Chi tiết

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));        // Header
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));        // Nội dung

            // ========== HEADER ==========
            panelHeader = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            label6 = new Label
            {
                Text = "Danh mục",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.MediumSlateBlue,
                Location = new Point(15, 14),
                AutoSize = true
            };

            label1 = new Label
            {
                Text = "Thời gian:",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(860, 18),
                AutoSize = true
            };

            label2 = new Label
            {
                Text = "00/00/0000 00:00",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.Black,
                Location = new Point(960, 18),
                AutoSize = true
            };

            panelHeader.Controls.Add(label6);
            panelHeader.Controls.Add(label1);
            panelHeader.Controls.Add(label2);
            layout.Controls.Add(panelHeader, 0, 0);
            layout.SetColumnSpan(panelHeader, 3);

            // ========== DANH MỤC ==========
            flowLayoutPanel2 = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(10),
                AutoScroll = false,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown
            };

            txtSearch = new TextBox
            {
                Font = new Font("Segoe UI", 12F),
                Width = 200,
                Margin = new Padding(5),
                Text = "Tìm kiếm sản phẩm...",
                ForeColor = Color.Gray
            };

            button2 = new Button
            {
                Text = "Tất cả sản phẩm",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Size = new Size(200, 50),
                Margin = new Padding(5),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.MediumSlateBlue
            };

            flowLayoutPanel2.Controls.Add(txtSearch);
            flowLayoutPanel2.Controls.Add(button2);
            layout.Controls.Add(flowLayoutPanel2, 0, 1);

            // ========== DANH SÁCH SẢN PHẨM ==========
            flowLayoutPanel1 = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(5),
                WrapContents = true,
                AutoScroll = false,
                FlowDirection = FlowDirection.LeftToRight
            };
            layout.Controls.Add(flowLayoutPanel1, 1, 1);

            // ========== THÔNG TIN CHI TIẾT ==========
            flowLayoutPanel3 = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(5),
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = false,
                WrapContents = false
            };

            panel4 = new Panel
            {
                BackColor = Color.White,
                Size = new Size(330, 320),
                Margin = new Padding(0),
                BorderStyle = BorderStyle.FixedSingle
            };

            label3 = new Label
            {
                Text = "Tên sản phẩm",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(310, 80),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pictureBox1 = new PictureBox
            {
                Location = new Point(10, 100),
                Size = new Size(310, 200),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };


            panel4.Controls.Add(label3);
            panel4.Controls.Add(pictureBox1);
            panel4.Controls.Add(labelImageDescription);

            button13 = new Button
            {
                Text = "🛒 Thêm vào giỏ",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Size = new Size(330, 40),
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 10, 0, 5)
            };

            button14 = new Button
            {
                Text = "🎁 Mã giảm giá",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Size = new Size(330, 40),
                BackColor = Color.DeepSkyBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 0, 0, 5)
            };

            flowLayoutPanel3.Controls.Add(panel4);
            flowLayoutPanel3.Controls.Add(button13);
            flowLayoutPanel3.Controls.Add(button14);
            layout.Controls.Add(flowLayoutPanel3, 2, 1);

            // ========== NÚT THANH TOÁN ==========
            button15 = new Button
            {
                Text = "💰 Thanh toán (0 đ)",
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.ForestGreen,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            // ========== TỔNG ==========
            this.Controls.Add(layout);
            this.Controls.Add(button15);
            this.Text = "Đặt món";
            this.Font = new Font("Segoe UI", 10F);
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.WhiteSmoke;
            this.ResumeLayout(false);
        }
    }
}