using IPOS.DB;
using IPOS.DB_Access;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IPOS
{
    public partial class Order : Form
    {
        private List<ProductDetails> cart = new List<ProductDetails>();
        private ProductDetails selectedProduct = null;
        private FlowLayoutPanel cartPanel;
        private int? selectedTableId;
        private Button buttonLeave;

        public Order(int tableId)
        {
            InitializeComponent();
            selectedTableId = tableId;
            this.Load += Order_Load;
            button13.Click += buttonAddToCart_Click;
            button15.Click += buttonThanhToan_Click;
        }

        private void Order_Load(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            SetupSearchBoxEvents();
            LoadCategoryButtonsFromButton2();
            LoadProductFromButton1();

            cartPanel = new FlowLayoutPanel
            {
                Width = 324,
                Height = 250,
                AutoScroll = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 10, 0, 10)
            };
            flowLayoutPanel3.Controls.Add(cartPanel);

            button15.Text = "💰 Thanh toán (0 đ)";

            buttonLeave = new Button
            {
                Text = "❌ Rời đi",
                Size = new Size(324, 50),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            buttonLeave.Click += ButtonLeave_Click;
            flowLayoutPanel3.Controls.Add(buttonLeave);
        }

        private void ButtonLeave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn rời đi?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                new Form_Table().Show();
            }
        }

        private void SetupSearchBoxEvents()
        {
            txtSearch.TextChanged += (s, ev) =>
            {
                if (txtSearch.Text == "Tìm kiếm sản phẩm...") return;
                UpdateProductList(GetFilteredProducts(txtSearch.Text.ToLower()));
            };
        }

        private void LoadCategoryButtonsFromButton2()
        {
            var cateAccess = new Category_Access();
            var categories = cateAccess.GetAllTables();

            var toRemove = flowLayoutPanel2.Controls
                .OfType<Control>()
                .Where(c => c is Button && c != button2)
                .ToList();

            foreach (var ctrl in toRemove)
                flowLayoutPanel2.Controls.Remove(ctrl);

            foreach (var cat in categories)
                flowLayoutPanel2.Controls.Add(CreateCategoryButton(cat));

            flowLayoutPanel2.Controls.SetChildIndex(txtSearch, 0);
            button2.Visible = false;
        }

        private Button CreateCategoryButton(Category category)
        {
            var btn = new Button
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
            var proAccess = new ProductDetails_Access();
            var products = proAccess.getAllTables();

            flowLayoutPanel1.Controls.Clear();
            foreach (var product in products)
                flowLayoutPanel1.Controls.Add(CreateProductButton(product));
        }

        private Button CreateProductButton(ProductDetails product)
        {
            var btn = new Button
            {
                Size = new Size(150, 130),
                Font = new Font("Segoe UI", 10F),
                Margin = new Padding(10),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DarkSlateGray,
                Text = $"{product.ProductName}\n{product.Price:N0} đ",
                Tag = product
            };

            if (System.IO.File.Exists(product.ImagePath))
            {
                using (var original = Image.FromFile(product.ImagePath))
                {
                    btn.Image = new Bitmap(original, new Size(100, 60));
                    btn.ImageAlign = ContentAlignment.TopCenter;
                    btn.TextImageRelation = TextImageRelation.ImageAboveText;
                }
            }

            btn.Click += Btn_ClickSanPham;
            return btn;
        }

        private void Btn_ClickDanhMuc(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Category category)
            {
                var proAccess = new ProductDetails_Access();
                var products = proAccess.getAllTables()
                    .Where(p => p.CategoryId == category.CategoryId)
                    .ToList();

                UpdateProductList(products);
            }
        }

        private void Btn_ClickSanPham(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is ProductDetails product)
            {
                selectedProduct = product;
                label3.Text = $"Sản phẩm: {product.ProductName}\nGiá: {product.Price:N0} đ";

                if (System.IO.File.Exists(product.ImagePath))
                {
                    pictureBox1.Image = Image.FromFile(product.ImagePath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pictureBox1.Image = null;
                }

            }
        }

        private void buttonAddToCart_Click(object sender, EventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Hãy chọn một món trước khi thêm vào giỏ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cart.Add(selectedProduct);
            UpdateCartUI();
            MessageBox.Show($"Đã thêm {selectedProduct.ProductName} vào giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonThanhToan_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào trong giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int tongTien = cart.Sum(p => p.Price);
            string maDon = "ORD" + DateTime.Now.Ticks;

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString))
            {
                conn.Open();

                var insertOrder = new SqlCommand(@"
                    INSERT INTO Orders (OrderCode, TotalAmount, Status, OrderTime, IsTakeAway, TableId)
                    OUTPUT INSERTED.OrderId
                    VALUES (@code, @total, 'New', GETDATE(), 0, @tableId)", conn);

                insertOrder.Parameters.AddWithValue("@code", maDon);
                insertOrder.Parameters.AddWithValue("@total", tongTien);
                insertOrder.Parameters.AddWithValue("@tableId", selectedTableId.HasValue ? (object)selectedTableId.Value : DBNull.Value);

                int orderId = (int)insertOrder.ExecuteScalar();

                foreach (var item in cart)
                {
                    var insertDetail = new SqlCommand(@"
                        INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice)
                        VALUES (@orderId, @productId, 1, @unitPrice)", conn);

                    insertDetail.Parameters.AddWithValue("@orderId", orderId);
                    insertDetail.Parameters.AddWithValue("@productId", item.ProductId);
                    insertDetail.Parameters.AddWithValue("@unitPrice", item.Price);
                    insertDetail.ExecuteNonQuery();
                }

                MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cart.Clear();
                UpdateCartUI();
                this.Hide();
                new Form_Table().Show();
            }
        }

        private void UpdateCartUI()
        {
            cartPanel.Controls.Clear();

            for (int i = 0; i < cart.Count; i++)
            {
                var item = cart[i];

                var itemPanel = new Panel
                {
                    Width = cartPanel.Width - 20,
                    Height = 40,
                    Margin = new Padding(5),
                    BackColor = Color.FromArgb(245, 245, 245)
                };

                var label = new Label
                {
                    Text = $"- {item.ProductName} ({item.Price:N0} đ)",
                    AutoSize = false,
                    Width = itemPanel.Width - 40,
                    Height = 40,
                    Font = new Font("Segoe UI", 11F),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(5)
                };

                var removeBtn = new Button
                {
                    Text = "❌",
                    Width = 30,
                    Height = 30,
                    BackColor = Color.Red,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(itemPanel.Width - 35, 5),
                    Tag = i
                };
                removeBtn.Click += (s, e) =>
                {
                    cart.RemoveAt((int)((Button)s).Tag);
                    UpdateCartUI();
                };

                itemPanel.Controls.Add(label);
                itemPanel.Controls.Add(removeBtn);
                cartPanel.Controls.Add(itemPanel);
            }

            int tongTien = cart.Sum(p => p.Price);
            button15.Text = $"💰 Thanh toán ({tongTien:N0} đ)";
        }

        private List<ProductDetails> GetFilteredProducts(string keyword)
        {
            var proAccess = new ProductDetails_Access();
            return proAccess.getAllTables()
                .Where(p => p.ProductName.ToLower().Contains(keyword))
                .ToList();
        }

        private void UpdateProductList(List<ProductDetails> products)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var product in products)
                flowLayoutPanel1.Controls.Add(CreateProductButton(product));
        }
    }
}
