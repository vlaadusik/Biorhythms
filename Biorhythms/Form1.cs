using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biorhythms
{
    public partial class Form1 : Form
    {
        public double BioPh, BioIntel, BioEmo, BioGeneral;
        public double BioPhTest, BioIntelTest, BioEmoTest;
        private double t, n1, n2;
        private string k;
        private int n;
        private bool painting = false;
        private DateTime birth;
        private DateTime today;
        public int height;
        Nullable<int> clicklPos = null;
        int stepforday = 17;
        int lam = 0;

        private string colorPh = "Red";
        private string colorIntel = "Blue";
        private string colorEmo = "Green";

        public Form1()
        {
            InitializeComponent();

            height = pictureBox1.Height - 24;
        }
        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
        private void BirthDate(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
        private void TodayDate(object sender, EventArgs e)
        {
            dateTimePicker2.Invalidate();
            pictureBox1.Invalidate();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Invalidate();
        }
        private void Texts()
        {
            textBox4.Text = null;
            textBox8.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox3.Text = null;
            textBox3.Invalidate();

            Biorithms();
            textBox8.ForeColor = Color.FromName(colorPh);
            if (colorPh == "White" || colorPh == "Yellow" || colorPh == "Pink") textBox8.BackColor = Color.Black;
            else textBox8.BackColor = Color.White;
            textBox8.Text = textBox8.Text + $"Физический: {BioPh:#.##}%";

            textBox6.ForeColor = Color.FromName(colorIntel);
            if (colorIntel == "White" || colorIntel == "Yellow" || colorIntel == "Pink") textBox6.BackColor = Color.Black;
            else textBox6.BackColor = Color.White;
            textBox6.Text = textBox6.Text + $"Интеллектуальный: {BioIntel:#.##}%";

            textBox5.ForeColor = Color.FromName(colorEmo);
            if (colorEmo == "White" || colorEmo == "Yellow" || colorEmo == "Pink") textBox5.BackColor = Color.Black;
            else textBox5.BackColor = Color.White;
            textBox5.Text = textBox5.Text + $"Эмоциональный: {BioEmo:#.##}%";
            textBox4.Text = textBox4.Text + $"Общий: {BioGeneral:#.##}%";

            if (t % 10 == 1) textBox3.Text += $"Вы прожили {t} день";
            else if (t % 10 >= 2 && t % 10 <= 4) textBox3.Text += $"Вы прожили {t} дня";
            else textBox3.Text += $"Вы прожили {t} дней";
        }
        private void PассчитатьБиоритмыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox4.SelectedItem == null) toolStripComboBox4.SelectedItem = colorPh;
            if (toolStripComboBox1.SelectedItem == null) toolStripComboBox1.SelectedItem = colorIntel;
            if (toolStripComboBox2.SelectedItem == null) toolStripComboBox2.SelectedItem = colorEmo;
            colorPh = toolStripComboBox4.SelectedItem.ToString();
            colorIntel = toolStripComboBox1.SelectedItem.ToString();
            colorEmo = toolStripComboBox2.SelectedItem.ToString();

            Texts();
            Biorithms();
            k = textBox7.Text;
            if (k == "") n1 = 31;
            else n1 = Convert.ToDouble(k);

            n = Convert.ToInt32(n1);
            if (n <= 1) MessageBox.Show("Введите значение не меньше 2");

            else
            {
                textBox7.Text = n.ToString();
                painting = true;
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (painting == true)
            {
                n1 = n;
                Graphics gr = e.Graphics;
                Pen g = new Pen(Color.Black);
                int i;
                n2 = n1 - 1;
                height = pictureBox1.Height - 24;

                Font drawFont = new Font("Arial", 8);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                Pen p = new Pen(Color.Black, 1);
                Pen p2 = new Pen(Color.Black, 2);
                Pen p_net = new Pen(Color.Gray, 1);
                p_net.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;

                double koef2 = (double)height / 8;
                for (i = 0; i <= pictureBox1.Width; i++)
                {// горизонтальная сетка
                    gr.DrawLine(p_net, 0, (height / 2) + (i * (int)koef2), pictureBox1.Width, height / 2 + (i * (int)koef2));
                    gr.DrawLine(p_net, 0, (height / 2) + (i * -(int)koef2), pictureBox1.Width, height / 2 + (i * -(int)koef2));
                }
                double koef = Convert.ToDouble(pictureBox1.Width) / n;


                gr.DrawLine(p, 0, height / 2, pictureBox1.Width, height / 2);
                gr.DrawLine(p, 0, 0, 0, pictureBox1.Height);

                // стрелочки
                gr.DrawLine(p2, 0, 0, 0 - 5, 10);
                gr.DrawLine(p2, 0, 0, 0 + 5, 10);
                gr.DrawLine(p2, pictureBox1.Width - 3, height / 2, pictureBox1.Width - 13, (height / 2) - 5);
                gr.DrawLine(p2, pictureBox1.Width - 3, height / 2, pictureBox1.Width - 13, (height / 2) + 5);

                Brush br = new SolidBrush(Color.Black);
                gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                int k = 100;
                for (int j = 0; j <= height; j++)
                {// проценты на оси у
                    gr.DrawString(k.ToString(), Font, br, 3 + 0, 0 + (j * (float)koef2));
                    k -= 25;
                }

                stepforday = Convert.ToInt32(koef / 17 > 1.0 ? 1 : Math.Ceiling(15 / koef + 0.5));
                i = 0;
                string day, month;
                today = Convert.ToDateTime(dateTimePicker2.Value.Date.ToString());
                while (i < n)
                {
                    day = today.Day.ToString();
                    month = today.Month.ToString();

                    gr.DrawString(day, drawFont, drawBrush, (float)(i * koef), pictureBox1.Height - 24);  //days
                    gr.DrawString(month, drawFont, drawBrush, (float)(i * koef), pictureBox1.Height - 12);  //months

                    today = today.AddDays(stepforday);
                    i += stepforday;
                }
                for (i = 0; i <= n; i++)
                {
                    gr.DrawLine(g, (float)(i * koef), pictureBox1.Height / 2 - 9, (float)(i * koef), pictureBox1.Height / 2 - 15);  //разметка на оси х
                    gr.DrawLine(p_net, 0 + (i * (float)koef), 0, 0 + (i * (float)koef), pictureBox1.Height); //вертикальная сетка
                }
                Point[] pointsPh = new Point[n];
                Point[] pointsIntel = new Point[n];
                Point[] pointsEmo = new Point[n];
                BioTest();
                double stepPh, stepIntel, stepEmo;
                stepPh = 2.0 * Math.PI / 23.0;
                stepEmo = 2.0 * Math.PI / 28.0;
                stepIntel = 2.0 * Math.PI / 33.0;

                double aPh, aIntel, aEmo;
                //MessageBox.Show(BioPhTest.ToString() + " " + BioPh.ToString());
                if (BioPhTest < BioPh) aPh = Math.Asin(BioPh / 100);   // начальный угол
                else aPh = Math.PI - Math.Asin(BioPh / 100);
                if (BioIntelTest < BioIntel) aIntel = Math.Asin(BioIntel / 100);
                else aIntel = Math.PI - Math.Asin(BioIntel / 100);
                if (BioEmoTest < BioEmo) aEmo = Math.Asin(BioEmo / 100);
                else aEmo = Math.PI - Math.Asin(BioEmo / 100);

                for (i = 0; i < n; i++)
                {
                    pointsPh[i] = new Point(Convert.ToInt32(i * pictureBox1.Width / n2), Convert.ToInt32(-(Math.Sin(aPh) * 100.0 - 100.0) * height / 200.0));
                    aPh += stepPh;

                    pointsIntel[i] = new Point(Convert.ToInt32(i * pictureBox1.Width / n2), Convert.ToInt32(-(Math.Sin(aIntel) * 100.0 - 100.0) * height / 200.0));
                    aIntel += stepIntel;

                    pointsEmo[i] = new Point(Convert.ToInt32(i * pictureBox1.Width / n2), Convert.ToInt32(-(Math.Sin(aEmo) * 100.0 - 100.0) * height / 200.0));
                    aEmo += stepEmo;
                }
                Pen Ph = new Pen(Color.FromName(colorPh), 2f);
                Pen Intel = new Pen(Color.FromName(colorIntel), 2f);
                Pen Emo = new Pen(Color.FromName(colorEmo), 2f);
                gr.DrawLines(Ph, pointsPh);
                gr.DrawLines(Intel, pointsIntel);
                gr.DrawLines(Emo, pointsEmo);
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicklPos != null)
            {
                dateTimePicker2.Value = dateTimePicker2.Value.Date.AddDays((int)((e.X - clicklPos) / stepforday));
                if (dateTimePicker2.Value < birth) { dateTimePicker2.Value = birth; }
                else { clicklPos = e.X; }
                dateTimePicker2.Invalidate();
                Biorithms();
                textBox3.Invalidate();
                Texts();
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            lam = e.Delta;
            n += lam * 3 / 120; //провернули на  дня
            if (n <= 1) n = 2;
            textBox7.Text = n.ToString();
            textBox7.Invalidate();
            pictureBox1.Invalidate();
        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            clicklPos = null;
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            clicklPos = null;
        }
        private void Invalidate(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            clicklPos = e.X;
        }
        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = null;
            pictureBox1.Image = null;
            painting = false;
            dateTimePicker2.Value = DateTime.Today;
            pictureBox1.Invalidate();
            textBox4.Text = null;
            textBox8.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox3.Text = null;
            textBox7.Text = null;
        }
        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {
            k = textBox7.Text;

            textBox7.Invalidate();
            PассчитатьБиоритмыToolStripMenuItem_Click(sender, e);
            pictureBox1.Invalidate();
        }
        private void Biorithms()
        {
            //B = (sin(2pi * t / P)) * 100 %, где P = {23 Ph, 28 Emo, 33 Intel}
            //B — состояния биоритма в % либо может выражаться как состояние относительно нуля, а также состояния нарастания или спадания.
            //pi — число π, принимаем равным 3,14 {Math.PI}
            //t — количество дней, прошедших с даты рождения до текущего момента.
            //P — фаза биоритма.
            // РАССЧИТЫВАЕМ КОЛ-ВО ДНЕЙ
            today = Convert.ToDateTime(dateTimePicker2.Value.Date.ToString()); //принимаем значение даты расчета
            //today = today.AddDays(lam * 3 / 120);
            birth = Convert.ToDateTime(dateTimePicker1.Value.Date.ToString()); //принимаем значение даты рождения
            TimeSpan ok = (today - birth); //считаем кол-во дней
            t = ok.TotalDays; //оставляем только дни
            BioPh = Math.Sin(2 * Math.PI * t / 23) * 100;
            BioIntel = Math.Sin(2 * Math.PI * t / 33) * 100;
            BioEmo = Math.Sin(2 * Math.PI * t / 28) * 100;
            BioGeneral = (BioPh + BioIntel + BioEmo) / 3;
        }
        private void BioTest()
        {
            double ti = t - 1;
            BioPhTest = Math.Sin(2 * Math.PI * ti / 23) * 100;
            BioIntelTest = Math.Sin(2 * Math.PI * ti / 33) * 100;
            BioEmoTest = Math.Sin(2 * Math.PI * ti / 28) * 100;
        }
        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
    }
}
