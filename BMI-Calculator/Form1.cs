using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts.Wpf;
using LiveCharts;

namespace BMI_Calculator
{
    public partial class Form1 : Form
    {
        private List<double> riwayatBMI = new List<double>();
        private int hitungKe = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Man");
            comboBox1.Items.Add("Woman");
            comboBox1.SelectedIndex = 0;
            panel1.Visible = true;
            panel2.Visible = false;
            cartesianChart1.Series = new SeriesCollection
    {
        new LineSeries
        {
            Title = "BMI",
            Values = new ChartValues<double>()
        }
    };

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Waktu",
                Labels = new List<string>()
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "BMI",
                LabelFormatter = value => value.ToString("F1")
            });
        }


        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double tinggi = double.Parse(textBox1.Text) / 100; // dari cm ke meter
                double berat = double.Parse(textBox2.Text);
                int usia = int.Parse(textBox3.Text);
                string gender = comboBox1.SelectedItem.ToString();

                // Hitung BMI
                double bmi = berat / (tinggi * tinggi);

                // Kategori BMI
                string kategori;
                if (bmi < 18.5)
                    kategori = "Skinny";
                else if (bmi < 25)
                    kategori = "Normal";
                else if (bmi < 30)
                    kategori = "Fat";
                else
                    kategori = "Obesity";

                // Hitung Body Fat
                double bodyFat;
                if (gender == "Man")
                    bodyFat = (1.20 * bmi) + (0.23 * usia) - 16.2;
                else
                    bodyFat = (1.20 * bmi) + (0.23 * usia) - 5.4;

                // Tampilkan hasil
                label5.Text = $"BMI Anda: {bmi:F2} ({kategori})";
                label6.Text = $"Body Fat: {bodyFat:F2}%";

                // Tambahkan ke listBox
                string waktu = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                string log = $"{bmi:F1} - {kategori} - {waktu}";
                listBox1.Items.Add(log);

                // Tambahkan ke grafik
                var lineSeries = cartesianChart1.Series[0] as LineSeries;
                lineSeries.Values.Add(bmi);

                string timeLabel = hitungKe.ToString();
                cartesianChart1.AxisX[0].Labels.Add(timeLabel);
                hitungKe++;

                // Simpan ke list
                riwayatBMI.Add(bmi);
            }
            catch
            {
                MessageBox.Show("Input a valid number.");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }
    }
}
