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
    public partial class Form2 : Form
    {
        int transx1 = 0, transy1 = 0, transx2 = 0, transy2 = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ob = new Form1();
            ob.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox5.Text);
            int y = int.Parse(textBox6.Text);
            transx1 += x;
            transy1 += y;
            transx2 += x;
            transy2 += y;
            DDA(transx1, transy1, transx2, transy2);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        double[,] trasformation(double x, double y)
        {
            double[,] trans = { { 1, 0, -1 * x }, { 0, 1, -1 * y }, { 0, 0, 1 } };
            return trans;
        }

        double[,] matrix3_X_matrix3(double[,] arr1, double[,] arr2)
        {
            double[,] line = new double[3, 3];
            for (int i = 0; i < 9; i++)
            {
                for (int col = 0; col < 3; col++)
                {
                    line[i / 3, i % 3] += arr1[i / 3, col] * arr2[col, i % 3];
                }
            }
            return line;
        }

        double[] matrix3_X_matrix1(double[,] arr1, double[] arr2)
        {
            double[] line = new double[3];
            for (int i = 0; i < 9; i++)
            {
                line[i / 3] += arr1[i / 3, i % 3] * arr2[i % 3];
            }
            return line;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double ceta = double.Parse(textBox7.Text) * Math.PI / 180;
            double[,] rotate = { { Math.Cos(ceta), Math.Sin(ceta), 0 }, { Math.Sin(ceta) * -1, Math.Cos(ceta), 0 }, { 0, 0, 1 } };
            double[,] trasformation_to_zero = trasformation((transx1 + transx2) / 2, (transy1 + transy2) / 2);
            double newtransx1 = transx1 + trasformation_to_zero[0, 2];
            double newtransx2 = transx2 + trasformation_to_zero[0, 2];
            double newtransy1 = transy1 + trasformation_to_zero[1, 2];
            double newtransy2 = transy2 + trasformation_to_zero[1, 2];
            double[,] line = trasformation((transx1 + transx2) / -2, (transy1 + transy2) / -2);
            double[,] line1 = matrix3_X_matrix3(line, rotate);
            double[,] line2 = matrix3_X_matrix3(line1, trasformation_to_zero);
            double[] rotate1__before = { transx1, transy1, 1 };
            double[] rotate2__before = { transx2, transy2, 1 };
            double[] rotate1__after = matrix3_X_matrix1(line2, rotate1__before);
            double[] rotate2__after = matrix3_X_matrix1(line2, rotate2__before);
            DDA((int)rotate1__after[0], (int)rotate1__after[1], (int)rotate2__after[0], (int)rotate2__after[1]);
            transx1 = (int)rotate1__after[0];
            transy1 = (int)rotate2__after[0];
            transx2 = (int)rotate1__after[1];
            transy2 = (int)rotate2__after[1];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox8.Text);
            int y = int.Parse(textBox9.Text);
            transx1 *= x;
            transy1 *= y;
            transx2 *= x;
            transy2 *= y;
            DDA(transx1, transy1, transx2, transy2);
        }

        private void DDA(int x1,int y1,int x2,int y2)
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
        private void button2_Click(object sender, EventArgs e)
        {
            int x1 = int.Parse(textBox1.Text);
            int y1 = int.Parse(textBox2.Text);
            int x2 = int.Parse(textBox3.Text);
            int y2 = int.Parse(textBox4.Text);
            transx1 = x1;
            transy1 = y1;
            transx2 = x2;
            transy2 = y2;
            DDA(x1, y1, x2, y2);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
