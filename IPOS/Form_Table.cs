using IPOS.DB;
using IPOS.DB_Access;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void LoadBanButtonsFromButton1()
        {
            Table_Details_Access tda = new Table_Details_Access();
            List<Table_Details> danhSachBan = tda.GetAllTables();

            flowLayoutPanel1.Controls.Clear();

            foreach (var ban in danhSachBan)
            {
                Button btn = new Button();

              
                btn.Size = button1.Size;
                btn.Font = button1.Font;
                btn.Margin = button1.Margin;
                btn.Image = button1.Image;
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.ForeColor = button1.ForeColor;

                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Cỡ, kiểu chữ
                btn.ForeColor = ban.Status == "free" ? Color.Green : Color.Red;
                btn.TextAlign = ContentAlignment.BottomCenter; // Căn chữ


                btn.FlatStyle = button1.FlatStyle;

             
                btn.Text = ban.nameTable;
                btn.Tag = ban;
                btn.Name = $"btnBan{ban.ID}";

               
                btn.Click += Btn_Click;

           
                if (ban.IsTakeAway)
                {
                    flowLayoutPanel2.Controls.Add(btn);
                }
                else
                {
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }

            button1.Visible = false;
        }


        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Table_Details ban = btn.Tag as Table_Details;

            Order ord = new Order();
            ord.Show();

        }
    }
}
