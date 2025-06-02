using System;
using System.Drawing;
using System.IO;
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
            var layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 3;
            layout.RowCount = 2;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 360F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this.panelHeader = new Panel();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label6 = new Label();

            this.panelHeader.Dock = DockStyle.Fill;
            this.panelHeader.BackColor = Color.White;
            this.panelHeader.BorderStyle = BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Controls.Add(this.label2);
            this.panelHeader.Controls.Add(this.label6);

            this.label6.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.label6.ForeColor = Color.MediumSlateBlue;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(25, 12);
            this.label6.Text = "Danh mục";

            this.label1.AutoSize = true;
            this.label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.label1.ForeColor = Color.Gray;
            this.label1.Location = new Point(950, 18);
            this.label1.Text = "Thời gian:";

            this.label2.AutoSize = true;
            this.label2.Font = new Font("Segoe UI", 14F);
            this.label2.ForeColor = Color.Black;
            this.label2.Location = new Point(1060, 18);
            this.label2.Text = "00/00/0000 00:00";

            layout.Controls.Add(this.panelHeader, 0, 0);
            layout.SetColumnSpan(this.panelHeader, 3);

            this.flowLayoutPanel2 = new FlowLayoutPanel();
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.BackColor = Color.WhiteSmoke;
            this.flowLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Dock = DockStyle.Fill;
            this.flowLayoutPanel2.Padding = new Padding(10);

            this.txtSearch = new TextBox();
            this.txtSearch.Font = new Font("Segoe UI", 14F);
            this.txtSearch.Width = 220;
            this.txtSearch.Margin = new Padding(10);
            this.txtSearch.Text = "Tìm kiếm sản phẩm...";
            this.txtSearch.ForeColor = Color.Gray;
            this.txtSearch.Enter += (s, e) => {
                if (txtSearch.Text == "Tìm kiếm sản phẩm...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };
            this.txtSearch.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Tìm kiếm sản phẩm...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            this.button2 = new Button();
            this.button2.Text = "Danh mục";
            this.button2.FlatStyle = FlatStyle.Flat;
            this.button2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.button2.ForeColor = Color.MediumSlateBlue;
            this.button2.Size = new Size(220, 80);
            this.button2.Margin = new Padding(10);

            this.flowLayoutPanel2.Controls.Add(this.txtSearch);
            this.flowLayoutPanel2.Controls.Add(this.button2);
            layout.Controls.Add(this.flowLayoutPanel2, 0, 1);

            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = Color.WhiteSmoke;
            this.flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Dock = DockStyle.Fill;
            this.flowLayoutPanel1.Padding = new Padding(20);
            this.flowLayoutPanel1.WrapContents = true;
            this.flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            layout.Controls.Add(this.flowLayoutPanel1, 1, 1);

            this.flowLayoutPanel3 = new FlowLayoutPanel();
            this.flowLayoutPanel3.BackColor = Color.WhiteSmoke;
            this.flowLayoutPanel3.BorderStyle = BorderStyle.FixedSingle;
            this.flowLayoutPanel3.Dock = DockStyle.Fill;
            this.flowLayoutPanel3.Padding = new Padding(10);
            this.flowLayoutPanel3.FlowDirection = FlowDirection.TopDown;

            this.panel4 = new Panel();
            this.panel4.BackColor = Color.White;
            this.panel4.BorderStyle = BorderStyle.FixedSingle;
            this.panel4.Size = new Size(324, 400);

            this.label3 = new Label();
            this.label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.label3.ForeColor = Color.Black;
            this.label3.Size = new Size(304, 70);
            this.label3.Location = new Point(10, 10);
            this.label3.Text = "Tên sản phẩm";
            this.label3.TextAlign = ContentAlignment.MiddleCenter;

            this.pictureBox1 = new PictureBox();
            this.pictureBox1.Location = new Point(10, 90);
            this.pictureBox1.Size = new Size(304, 200);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;

            this.labelImageDescription = new Label();
            this.labelImageDescription.Font = new Font("Segoe UI", 14F);
            this.labelImageDescription.ForeColor = Color.Black;
            this.labelImageDescription.Location = new Point(10, 300);
            this.labelImageDescription.Size = new Size(304, 70);
            this.labelImageDescription.Text = "Thông tin / mô tả hình ảnh";
            this.labelImageDescription.TextAlign = ContentAlignment.MiddleCenter;

            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.labelImageDescription);

            this.button13 = new Button();
            this.button13.Text = "Thêm Sản Phẩm vào giỏ";
            this.button13.FlatStyle = FlatStyle.Flat;
            this.button13.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.button13.ForeColor = Color.MediumSlateBlue;
            this.button13.Size = new Size(324, 60);
            this.button13.Cursor = Cursors.Hand;

            this.button14 = new Button();
            this.button14.Text = "Thêm mã giảm giá";
            this.button14.FlatStyle = FlatStyle.Flat;
            this.button14.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.button14.ForeColor = Color.MediumSlateBlue;
            this.button14.Size = new Size(324, 60);
            this.button14.Cursor = Cursors.Hand;

            this.flowLayoutPanel3.Controls.Add(this.panel4);
            this.flowLayoutPanel3.Controls.Add(this.button13);
            this.flowLayoutPanel3.Controls.Add(this.button14);
            layout.Controls.Add(this.flowLayoutPanel3, 2, 1);

            this.button15 = new Button();
            this.button15.Text = "THANH TOÁN";
            this.button15.Dock = DockStyle.Bottom;
            this.button15.Height = 65;
            this.button15.BackColor = Color.MediumSlateBlue;
            this.button15.ForeColor = Color.White;
            this.button15.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.button15.FlatStyle = FlatStyle.Flat;
            this.button15.Cursor = Cursors.Hand;

            this.Controls.Add(layout);
            this.Controls.Add(this.button15);
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Đặt món";
            this.BackColor = Color.Gainsboro;
            this.Font = new Font("Segoe UI", 10F);
            this.ResumeLayout(false);
        }
    }
}
