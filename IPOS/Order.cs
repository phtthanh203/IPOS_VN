using IPOS.DB;
using IPOS.DB_Access;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPOS
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void Order_Load(object sender, EventArgs e)
        {
            LoadProductFromButton1();
            LoadCategoryButtonsFromButton2();
            DateTime now = DateTime.Now;  // Lấy ngày giờ hiện tại theo hệ thống

            string currentTime = now.ToString("dd/MM/yyyy HH:mm:ss");  // Định dạng: 30/05/2025 14:45:12

            label2.Text = currentTime;
        }
        private void LoadCategoryButtonsFromButton2()
        {

            Category_Access cate = new Category_Access();
            List<Category> danhsachmuc = cate.GetAllTables();

            foreach(var muc in danhsachmuc)
            {
                Button btn = new Button();
                btn.Size = button2.Size;
                btn.Font = button2.Font;
                btn.Margin = button2.Margin;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.ForeColor = button1.ForeColor;
                btn.Text = muc.CategoryName;
                btn.Tag = muc;
                btn.Click += Btn_ClickDanhMuc;
                flowLayoutPanel2.Controls.Add(btn);
            }
            button2.Visible = false;
        }



        private void LoadProductFromButton1()
        {
            ProductDetails_Access pro = new ProductDetails_Access();
            List<ProductDetails> sanpham = pro.getAllTables();

            foreach(var s in sanpham)
            {

                Button btn = new Button();
                btn.Size = button1.Size;
                btn.Font = button1.Font;
                btn.Margin = button1.Margin;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.ForeColor = button1.ForeColor;
                btn.Text = s.ProductName + " "+ s.Price;
                btn.Tag = s;
                btn.Image = Image.FromFile(s.ImagePath);
                
                btn.Click += Btn_ClickSanPham;
                flowLayoutPanel1.Controls.Add(btn);
            }
            button1.Visible = false;
        }

        private void Btn_ClickDanhMuc(object sender, EventArgs e)
        {
            

        }

        private void Btn_ClickSanPham(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is ProductDetails sp)
            {
            
                {

                }
            }
           

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Payment pay = new Payment();
            pay.Show();
        }
    }
}
