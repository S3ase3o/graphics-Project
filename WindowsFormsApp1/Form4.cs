using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        int transx1 = 0, transy1 = 0;
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ob = new Form1();
            ob.Show();
        }

        private void Circal(int x1,int y1,int r)
        {
            int p0 = 1 - r;
            int x = 0, y = r;
            Bitmap pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            while (x <= y)
            {
                pic.SetPixel(x1 + x, y1 + y, Color.Black);
                pic.SetPixel(x1 - x, y1 + y, Color.Black);
                pic.SetPixel(x1 + x, y1 - y, Color.Black);
                pic.SetPixel(x1 - x, y1 - y, Color.Black);
                pic.SetPixel(x1 + y, y1 + x, Color.Black);
                pic.SetPixel(x1 - y, y1 + x, Color.Black);
                pic.SetPixel(x1 + y, y1 - x, Color.Black);
                pic.SetPixel(x1 - y, y1 - x, Color.Black);
                x++;
                if (p0 < 0)
                {
                    p0 += 2 * x + 1;
                }
                else
                {
                    y--;
                    p0 += 2 * (x - y) + 1;
                }
            }
            pictureBox1.Image = pic;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int x1 = int.Parse(textBox1.Text);
            int y1 = int.Parse(textBox2.Text);
            int r = int.Parse(textBox3.Text);
            transx1 = x1;
            transy1 = y1;
            Circal(x1, y1, r);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox8.Text);
            int y = int.Parse(textBox9.Text);
            int r = int.Parse(textBox3.Text);
            Circal(transx1, transy1, r * x * y);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox4.Text);
            int y = int.Parse(textBox5.Text);
            int r = int.Parse(textBox3.Text);
            transx1 += x;
            transy1 += y;
            Circal(transx1, transy1, r);
        }
    }
}
