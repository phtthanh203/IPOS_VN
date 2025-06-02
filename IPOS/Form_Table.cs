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

        private void LoadBanButtonsFromButton1()
        {
            Table_Details_Access tda = new Table_Details_Access();
            List<Table_Details> danhSachBan = tda.GetAllTables();

            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();

            ToolTip tooltip = new ToolTip();

            foreach (var ban in danhSachBan)
            {
                Button btn = new Button
                {
                    Size = button1.Size,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Margin = button1.Margin,
                    ForeColor = ban.Status == "free" ? Color.Green : Color.Red,
                    TextAlign = ContentAlignment.BottomCenter,
                    FlatStyle = button1.FlatStyle,
                    Text = ban.nameTable,
                    Tag = ban,
                    Name = $"btnBan{ban.ID}",
                    ImageAlign = ContentAlignment.TopCenter
                };

                btn.Image = ban.Status == "free" ? Properties.Resources.Table_Free : Properties.Resources.Table_Occupied;
                tooltip.SetToolTip(btn, $"Bàn: {ban.nameTable}\nTrạng thái: {ban.Status}");
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
                Order ord = new Order();
                ord.Show();
            }
        }
    }
}
