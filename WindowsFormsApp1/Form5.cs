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
    public partial class Form5 : Form
    {
        int transx1 = 0, transy1 = 0;
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ob = new Form1();
            ob.Show();
        }

        private void Elipse(int Xcenter, int Ycenter, int a, int b)
        {
            int d = b * b - a * a * b + a * a / 4;
            int x = 0;
            int y = b;
            Bitmap pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            while (2 * b * b * x < 2 * a * a * y)
            {
                pic.SetPixel(Xcenter + x, Ycenter + y, Color.Black);
                pic.SetPixel(Xcenter - x, Ycenter + y, Color.Black);
                pic.SetPixel(Xcenter + x, Ycenter - y, Color.Black);
                pic.SetPixel(Xcenter - x, Ycenter - y, Color.Black);
                x++;
                if (d < 0)
                {
                    d += 2 * b * b * x + b * b;

                }
                else
                {
                    y--;

                    d += 2 * b * b * x - 2 * a * a * y + b * b;
                }
            }
            double d2 = b * b * (x + 0.5) * (x + 0.5) + a * a * (y - 1) * (y - 1) - a * a * b * b;
            while (y >= 0)
            {
                pic.SetPixel(Xcenter + x, Ycenter + y, Color.Black);
                pic.SetPixel(Xcenter - x, Ycenter + y, Color.Black);
                pic.SetPixel(Xcenter + x, Ycenter - y, Color.Black);
                pic.SetPixel(Xcenter - x, Ycenter - y, Color.Black);
                y--;
                if (d2 < 0)
                {
                    x++;
                    d2 += 2 * b * b * x - 2 * a * a * y + a * a;
                }
                else
                {
                    d2 -= 2 * a * a * y + a * a;
                }
            }
            pictureBox1.Image = pic;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int Xcenter = int.Parse(textBox1.Text);
            int Ycenter = int.Parse(textBox2.Text);
            int a = int.Parse(textBox3.Text);
            int b = int.Parse(textBox4.Text);
            transx1 = Xcenter;
            transy1 = Ycenter;
            Elipse(Xcenter, Ycenter, a, b);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox8.Text);
            int y = int.Parse(textBox9.Text);
            int a = int.Parse(textBox3.Text);
            int b = int.Parse(textBox4.Text);
            Elipse(transx1, transy1, a * x, b * y);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox5.Text);
            int y = int.Parse(textBox6.Text);
            int a = int.Parse(textBox3.Text);
            int b = int.Parse(textBox4.Text);
            transx1 += x;
            transy1 += y;
            Elipse(transx1, transy1, a, b);
        }
    }
}
