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
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;

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

            listBox1.Items.Clear();
            listBox1.Items.Add("Bmi          Date                  Time");

            LoadDataFromDatabase();
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
                double tinggi = double.Parse(textBox1.Text) / 100; 
                double berat = double.Parse(textBox2.Text);
                int usia = int.Parse(textBox3.Text);
                string gender = comboBox1.SelectedItem.ToString();

                double bmi = berat / (tinggi * tinggi);

                string kategori;
                if (bmi < 18.5)
                    kategori = "Skinny";
                else if (bmi < 25)
                    kategori = "Normal";
                else if (bmi < 30)
                    kategori = "Fat";
                else
                    kategori = "Obesity";

                double bodyFat;
                if (gender == "Man")
                    bodyFat = (1.20 * bmi) + (0.23 * usia) - 16.2;
                else
                    bodyFat = (1.20 * bmi) + (0.23 * usia) - 5.4;

                label5.Text = $"BMI Anda: {bmi:F2} ({kategori})";
                label6.Text = $"Body Fat: {bodyFat:F2}%";

                DateTime now = DateTime.Now;

                string formattedDate = now.ToString("dd/MM/yyyy");
                string formattedTime = now.ToString("HH:mm:ss");

                string logItem = string.Format("{0,-12}{1,-12}{2}",
                                              bmi.ToString("F1"),
                                              formattedDate,
                                              formattedTime);

                if (listBox1.Items.Count == 0)
                {
                    listBox1.Items.Add("Bmi          Date                  Time");
                }

                listBox1.Items.Insert(1, logItem);

                var lineSeries = cartesianChart1.Series[0] as LineSeries;
                lineSeries.Values.Add(bmi);

                string timeLabel = hitungKe.ToString();
                cartesianChart1.AxisX[0].Labels.Add(timeLabel);
                hitungKe++;

                riwayatBMI.Add(bmi);

                switch (kategori)
                {
                    case "Skinny":
                        label8.Text = "Saran: Tambahkan asupan kalori, konsumsi protein tinggi (daging, telur, kacang-kacangan).\nOlahraga: Weight lifting ringan, yoga, hindari cardio berlebih.";
                        break;
                    case "Normal":
                        label8.Text = "Saran: Pertahankan pola makan seimbang.\nOlahraga: Campuran cardio dan strength training 3-4x seminggu.";
                        break;
                    case "Fat":
                        label8.Text = "Saran: Kurangi makanan berlemak, perbanyak sayur dan buah.\nOlahraga: Fokus pada cardio seperti lari, bersepeda, minimal 30 menit per hari.";
                        break;
                    case "Obesity":
                        label8.Text = "Saran: Konsultasi ke dokter gizi.\nMulai dari diet rendah kalori, banyak minum air putih.\nOlahraga: Jalan cepat, berenang, jangan terlalu berat di awal.";
                        break;
                    default:
                        label8.Text = "Saran: Data tidak dikenali.";
                        break;
                }

                string dateForDB = now.ToString("yyyy-MM-dd");
                float weight = float.Parse(textBox2.Text);
                float height = float.Parse(textBox1.Text);
                float bmiValue = (float)bmi;
                float bodyFatValue = (float)bodyFat;

                SaveToDatabase(dateForDB, weight, height, bmiValue, bodyFatValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Input a valid number. Error: " + ex.Message);
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
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

            label5.Text = "";
            label6.Text = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            label8.AutoSize = false;
            label8.MaximumSize = new Size(400, 0); 
            label8.Size = new Size(400, label8.PreferredHeight);
            label8.TextAlign = ContentAlignment.TopLeft;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex <= 0)
            {
                MessageBox.Show("Please select a BMI record to delete.");
                return;
            }

            string selectedItem = listBox1.SelectedItem.ToString();

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete this entry?\n\n{selectedItem}",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string[] parts = selectedItem.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 1)
                    {
                        string bmiStr = parts[0].Trim();

                        float bmiValue;
                        if (!float.TryParse(bmiStr.Replace(',', '.'), System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture, out bmiValue))
                        {
                            MessageBox.Show("Error parsing BMI value: " + bmiStr);
                            return;
                        }

                        string dateInfo = "";
                        if (parts.Length >= 2)
                        {
                            dateInfo = parts[1];
                        }

                        Console.WriteLine($"Attempting to delete BMI record with value: {bmiValue}");

                        DeleteFromDatabase(dateInfo, bmiValue);

                        listBox1.Items.RemoveAt(selectedIndex);

                        int dataIndex = selectedIndex - 1;
                        var lineSeries = cartesianChart1.Series[0] as LiveCharts.Wpf.LineSeries;
                        if (lineSeries != null && dataIndex < lineSeries.Values.Count)
                        {
                            lineSeries.Values.RemoveAt(dataIndex);
                            cartesianChart1.AxisX[0].Labels.RemoveAt(dataIndex);
                        }

                        if (dataIndex < riwayatBMI.Count)
                        {
                            riwayatBMI.RemoveAt(dataIndex);
                        }

                        for (int i = 0; i < cartesianChart1.AxisX[0].Labels.Count; i++)
                        {
                            cartesianChart1.AxisX[0].Labels[i] = (i + 1).ToString();
                        }

                        hitungKe = cartesianChart1.AxisX[0].Labels.Count + 1;
                    }
                    else
                    {
                        MessageBox.Show("Data format not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error processing data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string inputDate = Microsoft.VisualBasic.Interaction.InputBox(
        "Enter the date to search (format: dd/MM/yyyy):",
        "Search BMI by Date",
        DateTime.Now.ToString("dd/MM/yyyy")
    );

            if (string.IsNullOrWhiteSpace(inputDate)) return;

            StringBuilder result = new StringBuilder();
            for (int i = 1; i < listBox1.Items.Count; i++)
            {
                string item = listBox1.Items[i].ToString();
                if (item.Contains(inputDate))
                {
                    result.AppendLine(item);
                }
            }

            if (result.Length > 0)
            {
                MessageBox.Show(result.ToString(), $"Results for {inputDate}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No data found for that date.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void SaveToDatabase(string date, float weight, float height, float bmi, float bodyFat)
        {
            try
            {
                string connectionString = "Server=localhost;Database=bmi_calculator;Uid=root;Pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO bmi_records (Date, Weight, Height, BMI, BodyFat) VALUES (@Date, @Weight, @Height, @BMI, @BodyFat)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Date", date);
                        cmd.Parameters.AddWithValue("@Weight", weight);
                        cmd.Parameters.AddWithValue("@Height", height);
                        cmd.Parameters.AddWithValue("@BMI", bmi);
                        cmd.Parameters.AddWithValue("@BodyFat", bodyFat);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data successfully saved to database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteFromDatabase(string datetimeFull, float bmi)
        {
            try
            {
                string connectionString = "Server=localhost;Database=bmi_calculator;Uid=root;Pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string selectQuery = "SELECT * FROM bmi_records WHERE BMI BETWEEN @BMIMin AND @BMIMax";
                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BMIMin", bmi - 0.1);
                        cmd.Parameters.AddWithValue("@BMIMax", bmi + 0.1);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            bool dataFound = false;
                            int matchId = -1;

                            while (reader.Read())
                            {
                                dataFound = true;
                                matchId = reader.GetInt32("ID");
                                break;
                            }

                            reader.Close();

                            if (dataFound && matchId > 0)
                            {
                                string deleteQuery = "DELETE FROM bmi_records WHERE ID = @ID";
                                using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                                {
                                    deleteCmd.Parameters.AddWithValue("@ID", matchId);
                                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Data succesfully deleted!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to delete data by ID " + matchId, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("BMi data like " + bmi + " not found in database.",
                                               "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDataFromDatabase()
        {
            try
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Bmi          Date                  Time");

                var lineSeries = cartesianChart1.Series[0] as LiveCharts.Wpf.LineSeries;
                lineSeries.Values.Clear();
                cartesianChart1.AxisX[0].Labels.Clear();
                riwayatBMI.Clear();

                int recordCount = 0;

                string connectionString = "Server=localhost;Database=bmi_calculator;Uid=root;Pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // First, count total records
                    string countQuery = "SELECT COUNT(*) FROM bmi_records";
                    using (MySqlCommand countCmd = new MySqlCommand(countQuery, conn))
                    {
                        recordCount = Convert.ToInt32(countCmd.ExecuteScalar());
                        Console.WriteLine($"Total records in database: {recordCount}");
                    }

                    if (recordCount == 0)
                    {
                        MessageBox.Show("No BMI records found in the database.",
                                       "Database Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        hitungKe = 1;
                        return;
                    }

                    string query = "SELECT * FROM bmi_records ORDER BY Date DESC";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int counter = 1;

                            Console.WriteLine("Starting to read database records...");

                            while (reader.Read())
                            {
                                Console.WriteLine($"Reading record {counter} of {recordCount}");

                                DateTime date = reader.GetDateTime("Date");
                                float bmi = reader.GetFloat("BMI");

                                string formattedDate = date.ToString("dd/MM/yyyy");
                                string formattedTime = date.ToString("HH:mm:ss");

                                string logItem = string.Format("{0,-12}{1,-12}{2}",
                                                              bmi.ToString("F1"),
                                                              formattedDate,
                                                              formattedTime);

                                listBox1.Items.Add(logItem);
                                Console.WriteLine($"Added to listBox1: {logItem}");

                                lineSeries.Values.Add(bmi);
                                cartesianChart1.AxisX[0].Labels.Add(counter.ToString());

                                riwayatBMI.Add(bmi);

                                counter++;
                            }

                            hitungKe = counter;
                            Console.WriteLine($"Finished loading {counter - 1} records");

                            if (counter > 1)
                            {
                                MessageBox.Show($"Successfully loaded {counter - 1} BMI records from database.",
                                               "Data Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }

                listBox1.Refresh();
                cartesianChart1.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from database: " + ex.Message + "\n\nStack Trace: " + ex.StackTrace,
                               "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Database error: " + ex.Message);
                Console.WriteLine("Stack trace: " + ex.StackTrace);
            }
        }
    }
}