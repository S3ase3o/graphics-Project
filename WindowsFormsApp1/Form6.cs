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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        public int complutecode(int x, int y, int xmin, int ymin, int xmax, int ymax)
        {
            int inside = 0;
            int top = 8;
            int bottom = 4;
            int right = 2;
            int left = 1;

            int code = inside;
            if (x < xmin)
                code = code | left;
            if (y < ymin)
                code = code | bottom;
            if (x > xmax)
                code = code | right;
            if (y > ymax)
                code = code | top;
            return code;
        }

        private void DDA(int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int steps = 0;
            if (Math.Abs(dx) > Math.Abs(dy))
                steps = (int)Math.Abs(dx);
            else
                steps = (int)Math.Abs(dy);
            float xinc = dx / steps;
            float yinc = dy / steps;
            float x = x1, y = y1;
            Bitmap pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int i = 0; i <= steps; i++)
            {
                int newx = (int)Math.Round(x);
                int newy = (int)Math.Round(y);
                pic.SetPixel(newx, newy, Color.Black);
                x += xinc;
                y += yinc;
            }
            pictureBox1.Image = pic;
        }

        public bool cohen(int x1, int y1, int x2, int y2, int xmin, int ymin, int xmax, int ymax)
        {

            int inside = 0;
            int top = 8;
            int bottom = 4;
            int right = 2;
            int left = 1;

            int code1 = complutecode(x1, y1, xmin, ymin, xmax, ymax);
            int code2 = complutecode(x2, y2, xmin, ymin, xmax, ymax);

            bool flag = false;

            while (true)
            {
                if (code1 == 0 & code2 == 0)
                {
                    flag = true;
                    break;
                }
                else if ((code1 & code2) != 0)
                {
                    break;
                }
                else
                {
                    int codeout;
                    int x = 0, y = 0;

                    if (code1 == 0)
                        codeout = code1;
                    else
                        codeout = code2;
                    if ((codeout & top) == top)
                    {
                        y = xmax;
                        x = x1 + (x2 - x1) * (ymax - y1) / (y2 - y1);
                    }
                    else if ((codeout & bottom) == bottom)
                    {
                        y = ymin;
                        x = x1 + (x2 - x1) * (ymax - y1) / (y2 - y1);
                    }
                    else if ((codeout & right) == right)
                    {
                        x = xmax;
                        y = y1 + (y2 - y1) * (xmax - x1) / (x2 - x1);
                    }
                    else if ((codeout & left) == left)
                    {
                        x = xmin;
                        y = y1 + (y2 - y1) * (xmin - x1) / (x2 - x1);
                    }


                    if (codeout == code1)
                    {
                        x1 = x;
                        y1 = y;
                        code1 = complutecode(x1, y1, xmin, ymin, xmax, ymax);
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;
                        code2 = code2 = complutecode(x2, y2, xmin, ymin, xmax, ymax);
                    }
                }
            }
            if (flag == true)
            {
                DDA(x1, y1, x2, y2);
                return true;
            }
            else
            {

                return false;
            }
        }

        public void barsky(int x1, int y1, int x2, int y2, int xmin, int ymin, int xmax, int ymax)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int[] p = new int[4];
            int[] q = new int[4];
            int t1 = 0;
            int t2 = 1;
            int xx1, xx2, yy1, yy2;

            p[0] = -dx;
            p[1] = dx;
            p[2] = -dy;
            p[3] = dy;

            q[0] = x1 - xmin;
            q[1] = xmax - x1;
            q[2] = y1 - ymin;
            q[3] = ymax - y1;

            for (int i = 0; i < 4; i++)
            {
                if (p[i] == 0)
                {
                    if (q[i] >= 0)
                    {
                        if (i < 2)
                        {
                            if (y1 < ymin)
                                y1 = ymin;
                            if (y2 > ymax)
                                y2 = ymax;
                            DDA(x1, y1, x2, y2);
                        }
                        if (i > 1)
                        {
                            if (x1 < xmin)
                                x1 = xmin;
                            if (x2 > xmax)
                                x2 = xmax;
                            DDA(x1, y1, x2, y2);
                        }
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                double u = q[i] / (double)p[i];
                if (p[i] < 0)
                {
                    if (t1 <= u)
                        t1 = (int)u;
                }
                else
                {
                    if (t2 > u)
                        t2 = (int)u;
                }
            }
            if (t1 < t2)
            {
                xx1 = x1 + t1 * p[1];
                xx2 = x1 + t2 * p[1];
                yy1 = y1 + t1 * p[3];
                yy2 = y1 + t2 * p[3];
                DDA(xx1, yy1, xx2, yy2);
            }
            DDA(x1, y1, x2, y2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x1 = int.Parse(textBox1.Text);
            int y1 = int.Parse(textBox2.Text);
            int x2 = int.Parse(textBox3.Text);
            int y2 = int.Parse(textBox4.Text);
            int xmin = int.Parse(textBox5.Text);
            int ymin = int.Parse(textBox6.Text);
            int xmax = int.Parse(textBox7.Text);
            int ymax = int.Parse(textBox7.Text);
            cohen(x1, y1, x2, y2, xmin, ymin, xmax, ymax);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x1 = int.Parse(textBox1.Text);
            int y1 = int.Parse(textBox2.Text);
            int x2 = int.Parse(textBox3.Text);
            int y2 = int.Parse(textBox4.Text);
            int xmin = int.Parse(textBox5.Text);
            int ymin = int.Parse(textBox6.Text);
            int xmax = int.Parse(textBox7.Text);
            int ymax = int.Parse(textBox7.Text);
            barsky(x1, y1, x2, y2, xmin, ymin, xmax, ymax);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ob = new Form1();
            ob.Show();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
