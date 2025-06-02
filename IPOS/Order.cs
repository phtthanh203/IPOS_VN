using IPOS.DB;
using IPOS.DB_Access;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IPOS
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
            this.Load += Order_Load;
        }

        private void Order_Load(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            SetupSearchBoxEvents();
            LoadCategoryButtonsFromButton2();
            LoadProductFromButton1();
        }

        private void SetupSearchBoxEvents()
        {
            txtSearch.Text = "Tìm kiếm sản phẩm...";
            txtSearch.ForeColor = Color.Gray;

            txtSearch.Enter += (sender, e) =>
            {
                if (txtSearch.Text == "Tìm kiếm sản phẩm...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Tìm kiếm sản phẩm...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            txtSearch.TextChanged += (sender, e) =>
            {
                if (txtSearch.Text == "Tìm kiếm sản phẩm...") return;

                string searchTerm = txtSearch.Text.ToLower();
                List<ProductDetails> filteredProducts = GetFilteredProducts(searchTerm);
                UpdateProductList(filteredProducts);
            };
        }

        private void LoadCategoryButtonsFromButton2()
        {
            Category_Access cate = new Category_Access();
            List<Category> danhsachmuc = cate.GetAllTables();

            var itemsToRemove = flowLayoutPanel2.Controls
                .OfType<Control>()
                .Where(c => c is Button && c != button2)
                .ToList();

            foreach (var ctrl in itemsToRemove)
                flowLayoutPanel2.Controls.Remove(ctrl);

            foreach (var muc in danhsachmuc)
            {
                Button btn = CreateCategoryButton(muc);
                flowLayoutPanel2.Controls.Add(btn);
            }

            flowLayoutPanel2.Controls.SetChildIndex(txtSearch, 0);
            button2.Visible = false;
        }

        private Button CreateCategoryButton(Category category)
        {
            Button btn = new Button
            {
                Size = button2.Size,
                Font = button2.Font,
                Margin = button2.Margin,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = button2.ForeColor,
                Text = category.CategoryName,
                Tag = category
            };
            btn.Click += Btn_ClickDanhMuc;
            return btn;
        }

        private void LoadProductFromButton1()
        {
            ProductDetails_Access pro = new ProductDetails_Access();
            List<ProductDetails> sanpham = pro.getAllTables();

            flowLayoutPanel1.Controls.Clear();

            foreach (var s in sanpham)
            {
                Button btn = CreateProductButton(s);
                flowLayoutPanel1.Controls.Add(btn);
            }
        }

        private Button CreateProductButton(ProductDetails product)
        {
            Button btn = new Button
            {
                Size = new Size(150, 130),
                Font = new Font("Segoe UI", 10F),
                Margin = new Padding(10),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DarkSlateGray,
                Text = $"{product.ProductName}\n{product.Price}",
                Tag = product
            };

            if (System.IO.File.Exists(product.ImagePath))
            {
                Image originalImage = Image.FromFile(product.ImagePath);
                btn.Image = new Bitmap(originalImage, new Size(100, 60));
                btn.ImageAlign = ContentAlignment.TopCenter;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;
            }

            btn.Click += Btn_ClickSanPham;
            return btn;
        }

        private void Btn_ClickDanhMuc(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is Category selectedCategory)
            {
                ProductDetails_Access pro = new ProductDetails_Access();
                List<ProductDetails> products = pro.getAllTables()
                    .Where(p => p.CategoryId == selectedCategory.CategoryId).ToList();

                flowLayoutPanel1.Controls.Clear();
                foreach (var s in products)
                {
                    Button btnProduct = CreateProductButton(s);
                    flowLayoutPanel1.Controls.Add(btnProduct);
                }
            }
        }

        private void Btn_ClickSanPham(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is ProductDetails product)
            {
                label3.Text = $"Product: {product.ProductName}\nPrice: {product.Price}";

                if (System.IO.File.Exists(product.ImagePath))
                {
                    pictureBox1.Image = Image.FromFile(product.ImagePath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pictureBox1.Image = null;
                }

                labelImageDescription.Text = $"Image: {product.ImagePath}";
            }
        }

        private List<ProductDetails> GetFilteredProducts(string searchTerm)
        {
            ProductDetails_Access pro = new ProductDetails_Access();
            List<ProductDetails> allProducts = pro.getAllTables();

            return allProducts.Where(p => p.ProductName.ToLower().Contains(searchTerm)).ToList();
        }

        private void UpdateProductList(List<ProductDetails> filteredProducts)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var product in filteredProducts)
            {
                Button btn = CreateProductButton(product);
                flowLayoutPanel1.Controls.Add(btn);
            }
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            Payment pay = new Payment();
            pay.Show();
        }
    }
}
