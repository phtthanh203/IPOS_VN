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
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        private void Payment_Load(object sender, EventArgs e)
        {

        }

        private void ThemSo(string so)
        {
            // Lấy nội dung hiện tại và xóa dấu phẩy
            string raw = textBox1.Text.Replace(",", "").Replace(".", "");

            // Ghép thêm số vừa nhấn
            raw += so;

            if (long.TryParse(raw, out long number))
            {
                textBox1.Text = number.ToString("#,##0");
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void themso(String so)
        {
            string raw = textBox1.Text.Replace(",", "").Replace(",", "");
            raw += so;
            if (long.TryParse(raw, out long number))
            {
                textBox1.Text = number.ToString("#,##0");
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ThemSo("1");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ThemSo("2");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ThemSo("3");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ThemSo("4");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ThemSo("5");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ThemSo("6");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ThemSo("7");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ThemSo("8");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ThemSo("9");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ThemSo("0");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ThemSo("000");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            xoaSo();
        }

        private void xoaSo()
        {
            string raw = textBox1.Text.Replace(",", "").Replace(".", "");
            if (raw.Length > 0)
            {
                raw = raw.Substring(0, raw.Length - 1);
                if(raw.Length > 0 && long.TryParse(raw, out long number))
                {
                    textBox1.Text = number.ToString("#,##0");
                }
                else
                {
                    textBox1.Text = "";
                }
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
