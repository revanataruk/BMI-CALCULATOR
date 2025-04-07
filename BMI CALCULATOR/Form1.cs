namespace BMI_CALCULATOR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Ambil input dari TextBox
                double tinggi = double.Parse(textBox1.Text) / 100; // cm ke meter
                double berat = double.Parse(textBox2.Text);

                // Hitung BMI
                double bmi = berat / (tinggi * tinggi);

                // Kategori
                string kategori;
                if (bmi < 18.5)
                {
                    kategori = "Kurus";
                }
                else if (bmi < 25)
                {
                    kategori = "Normal";
                }
                else if (bmi < 30)
                {
                    kategori = "Gemuk";
                }
                else
                {
                    kategori = "Obesitas";
                }

                // Tampilkan hasil
                label4.Text = $"BMI Anda: {bmi:F2} ({kategori})";
            }
            catch
            {
                MessageBox.Show("Mohon masukkan angka yang valid di kedua kotak.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Ambil input dari TextBox
                double tinggi = double.Parse(textBox1.Text) / 100; // cm ke meter
                double berat = double.Parse(textBox2.Text);

                // Hitung BMI
                double bmi = berat / (tinggi * tinggi);

                // Kategori
                string kategori;
                if (bmi < 18.5)
                {
                    kategori = "Kurus";
                }
                else if (bmi < 25)
                {
                    kategori = "Normal";
                }
                else if (bmi < 30)
                {
                    kategori = "Gemuk";
                }
                else
                {
                    kategori = "Obesitas";
                }

                // Tampilkan hasil
                label4.Text = $"BMI Anda: {bmi:F2} ({kategori})";
            }
            catch
            {
                MessageBox.Show("Mohon masukkan angka yang valid di kedua kotak.");
            }
        }
    }
}
