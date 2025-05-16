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
        private bool isLoggedIn = false;
        private string loggedInEmail = "";
        private int lastButtonClicked = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;

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
            textBox5.PasswordChar = '*';
        }

        private void ShowPanel(System.Windows.Forms.Panel activePanel)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;

            activePanel.Visible = true;
            activePanel.BringToFront();
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

                label5.Text = $"BMI : {bmi:F2} ({kategori})";
                label16.Text = $"BMI : {bmi:F2}";
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
                        label8.Text = "Dietary Recommendations:\n• Increase caloric intake by 300-500 calories above BMR\n• Consume high protein (2g/kg body weight): beef, chicken, fish, eggs, dairy\n• Focus on complex carbohydrates: brown rice, whole grain bread, oatmeal, quinoa\n• Include healthy fats: avocados, olive oil, nuts, fatty fish\n• Eat 5-6 moderate-sized meals daily\n• Consider high-calorie nutritional smoothies with milk, fruits, protein, and nut butter";
                        label15.Text = "Exercise Program:\n• Resistance training 3-4 times weekly focusing on compound movements\n• Perform squats, deadlifts, bench press with moderate weight (8-12 repetitions)\n• Allow adequate rest between sets (60-90 seconds)\n• Limit cardio to maximum 20 minutes, twice weekly\n• Core strengthening exercises twice weekly\n• Rest muscle groups for minimum 48 hours before retraining\n• Implement progressive overload by gradually increasing weights";
                        break;
                    case "Normal":
                        label8.Text = "Dietary Recommendations:\n• Maintain balanced nutrition with optimal macronutrient ratio (50% carbs, 30% protein, 20% fat)\n• Consume protein at 1.6-1.8g/kg body weight to preserve muscle mass\n• Diversify nutrient sources: fish, lean meat, eggs, legumes, seeds\n• Prioritize whole foods while minimizing processed options\n• Use the plate method: ½ vegetables, ¼ protein, ¼ carbohydrates\n• Ensure minimum 2 liters of water daily";
                        label15.Text = "Exercise Program:\n• Combine cardio and strength training 4-5 times weekly\n• Include HIIT sessions twice weekly (20-30 minutes per session)\n• Perform weight training 2-3 times weekly targeting all major muscle groups\n• Engage in recreational activities like cycling or swimming once weekly\n• Incorporate flexibility and mobility exercises twice weekly\n• Maintain exercise intensity at 70-85% of maximum heart rate\n• Diversify training routines to prevent plateaus";
                        break;
                    case "Fat":
                        label8.Text = "Dietary Recommendations:\n• Create caloric deficit of 300-500 calories below BMR\n• Limit foods high in saturated fats and added sugars\n• Increase consumption of green vegetables, fresh fruits, and fiber (25-30g daily)\n• Choose lean protein sources: skinless poultry, fish, tofu, tempeh\n• Utilize healthy cooking methods: steaming, baking, boiling\n• Practice mindful eating and recognize satiety signals\n• Consider intermittent fasting with 16:8 protocol\n• Maintain a food journal to increase caloric awareness";
                        label15.Text = "Exercise Program:\n• Emphasize cardiovascular training 4-5 times weekly for 45-60 minutes\n• Alternate between low-intensity steady state (LISS) and high-intensity interval training (HIIT)\n• Include jogging, cycling, swimming, and elliptical training in cardio routine\n• Add resistance training 2-3 times weekly to preserve muscle mass\n• Implement circuit-based workouts with minimal rest periods\n• Aim for 10,000 steps daily\n• Gradually increase intensity every 2 weeks";
                        break;
                    case "Obesity":
                        label8.Text = "Dietary Recommendations:\n• Consult with a registered dietitian for a safe weight reduction program\n• Create gradual caloric deficit of 500-750 calories daily\n• Eliminate processed foods, fast food, and sugary beverages\n• Prioritize high-protein and high-fiber foods for improved satiety\n• Follow plate method: ½ non-starchy vegetables, ¼ lean protein, ¼ complex carbohydrates\n• Consume smaller portions more frequently (4-5 meals daily)\n• Drink minimum 2.5-3 liters of water daily\n• Restrict sodium intake (<2300mg daily) to reduce water retention";
                        label15.Text = "Exercise Program:\n• Begin with light activity: 15-20 minute walks, three times weekly\n• Progressively increase duration to 30-45 minutes, five times weekly\n• Focus on non-weight bearing activities such as swimming and stationary cycling\n• Incorporate strength training using resistance bands or bodyweight exercises\n• Emphasize functional movements for daily living activities\n• Include flexibility and mobility exercises to prevent injuries\n• Monitor heart rate: maintain at 50-70% of maximum\n• Consider consulting with certified fitness professional for personalized programming";
                        break;
                    default:
                        label8.Text = "Dietary recommendations: Data unavailable.";
                        label15.Text = "Exercise recommendations: Data unavailable.";
                        break;
                }

                // Only save to database if user is logged in
                if (isLoggedIn)
                {
                    string dateForDB = now.ToString("yyyy-MM-dd");
                    float weight = float.Parse(textBox2.Text);
                    float height = float.Parse(textBox1.Text);
                    float bmiValue = (float)bmi;
                    float bodyFatValue = (float)bodyFat;

                    SaveToDatabase(dateForDB, weight, height, bmiValue, bodyFatValue);
                }

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

            label5.Text = "BMI";
            label16.Text = "BMI Anda :";
            label6.Text = "Body Fat";
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
                    string query = "INSERT INTO bmi_records (Date, Weight, Height, BMI, BodyFat, UserEmail) VALUES (@Date, @Weight, @Height, @BMI, @BodyFat, @UserEmail)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Date", date);
                        cmd.Parameters.AddWithValue("@Weight", weight);
                        cmd.Parameters.AddWithValue("@Height", height);
                        cmd.Parameters.AddWithValue("@BMI", bmi);
                        cmd.Parameters.AddWithValue("@BodyFat", bodyFat);
                        cmd.Parameters.AddWithValue("@UserEmail", isLoggedIn ? loggedInEmail : "guest"); // Save as guest if not logged in

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

                string connectionString = "Server=localhost;Database=bmi_calculator;Uid=root;Pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query;
                    MySqlCommand cmd;

                    // If logged in, only show user's data
                    if (isLoggedIn)
                    {
                        query = "SELECT * FROM bmi_records WHERE UserEmail = @UserEmail ORDER BY Date DESC";
                        cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@UserEmail", loggedInEmail);
                    }
                    else
                    {
                        // Show all data without user filtering when not logged in
                        query = "SELECT * FROM bmi_records WHERE UserEmail = 'guest' OR UserEmail IS NULL ORDER BY Date DESC";
                        cmd = new MySqlCommand(query, conn);
                    }

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int counter = 1;

                        while (reader.Read())
                        {
                            DateTime date = reader.GetDateTime("Date");
                            float bmi = reader.GetFloat("BMI");

                            string formattedDate = date.ToString("dd/MM/yyyy");
                            string formattedTime = date.ToString("HH:mm:ss");

                            string logItem = string.Format("{0,-12}{1,-12}{2}",
                                                          bmi.ToString("F1"),
                                                          formattedDate,
                                                          formattedTime);

                            listBox1.Items.Add(logItem);

                            lineSeries.Values.Add(bmi);
                            cartesianChart1.AxisX[0].Labels.Add(counter.ToString());

                            riwayatBMI.Add(bmi);

                            counter++;
                        }

                        hitungKe = counter;
                    }
                }

                listBox1.Refresh();
                cartesianChart1.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from database: " + ex.Message,
                               "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                ShowPanel(panel2);
            }
            else
            {
                lastButtonClicked = 6;
                ShowPanel(panel5);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowPanel(panel1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                ShowPanel(panel3);
            }
            else
            {
                lastButtonClicked = 7;
                ShowPanel(panel5); 
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                ShowPanel(panel4);
            }
            else
            {
                lastButtonClicked = 8;
                ShowPanel(panel5);
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string email = textBox4.Text.Trim();
            string password = textBox5.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AuthenticateUser(email, password))
            {
                isLoggedIn = true;
                loggedInEmail = email;
                label18.Text = $"Welcome {email}";

                switch (lastButtonClicked)
                {
                    case 6:
                        ShowPanel(panel2);
                        break;
                    case 7:
                        ShowPanel(panel3);
                        break;
                    case 8:
                        ShowPanel(panel4);
                        break;
                    default:
                        ShowPanel(panel1); 
                        break;
                }
            }
            else
            {
                MessageBox.Show("Invalid email or password. Please try again.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!isLoggedIn)
            {
                MessageBox.Show("You must be logged in to change your email.", "Not Logged In",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newEmail = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter your new email address:",
                "Change Email",
                loggedInEmail);

            if (string.IsNullOrWhiteSpace(newEmail))
            {
                return;
            }

            // Validate email format
            if (!IsValidEmail(newEmail))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the new email already exists (except for current user)
            if (EmailExistsForOtherUser(newEmail, loggedInEmail))
            {
                MessageBox.Show("This email is already in use by another account.", "Email Exists",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm with current password for security
            string currentPassword = Microsoft.VisualBasic.Interaction.InputBox(
                "Please enter your current password to confirm:",
                "Confirm Password",
                "");

            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                return;
            }

            if (UpdateUserEmail(loggedInEmail, newEmail, currentPassword))
            {
                string oldEmail = loggedInEmail;
                loggedInEmail = newEmail;
                label18.Text = $"Welcome {newEmail}";

                MessageBox.Show("Your email has been updated successfully.", "Email Updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!isLoggedIn)
            {
                MessageBox.Show("You must be logged in to change your password.", "Not Logged In",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verify current password
            string currentPassword = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter your current password:",
                "Verify Current Password",
                "");

            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                return;
            }

            if (!VerifyCurrentPassword(loggedInEmail, currentPassword))
            {
                MessageBox.Show("Current password is incorrect. Please try again.", "Authentication Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get new password
            string newPassword = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter your new password:",
                "New Password",
                "");

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                return;
            }

            // Confirm new password
            string confirmPassword = Microsoft.VisualBasic.Interaction.InputBox(
                "Confirm your new password:",
                "Confirm Password",
                "");

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Password Mismatch",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UpdateUserPassword(loggedInEmail, newPassword))
            {
                MessageBox.Show("Your password has been updated successfully.", "Password Updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            isLoggedIn = false;
            loggedInEmail = "";
            label18.Text = "Please login to continue";

            MessageBox.Show("You have been logged out successfully.", "Logout Successful",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowPanel(panel5);
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
        private bool AuthenticateUser(string email, string password)
        {
            try
            {
                string connectionString = "Server=localhost;Database=bmi_calculator;Uid=root;Pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM users WHERE Email = @Email AND Password = @Password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            return reader.HasRows; // Returns true if user exists with matching credentials
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Authentication Error: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // User registration method
        private bool RegisterUser(string email, string password)
        {
            try
            {
                string connectionString = "Server=localhost;Database=bmi_calculator;Uid=root;Pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if email already exists
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE Email = @Email";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Email", email);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Email already registered. Please use a different email or login.",
                                "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // Insert new user
                    string insertQuery = "INSERT INTO users (Email, Password) VALUES (@Email, @Password)";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration Error: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string email = textBox4.Text.Trim();
            string password = textBox5.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password", "Registration Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (RegisterUser(email, password))
            {
                MessageBox.Show("Registration successful! You can now login.", "Registration Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the fields
                textBox4.Clear();
                textBox5.Clear();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ShowPanel(panel1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            isLoggedIn = false;
            loggedInEmail = "";
            label17.Text = "Please login to continue";

            MessageBox.Show("You have been logged out successfully.", "Logout Successful",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool EmailExistsForOtherUser(string newEmail, string currentEmail)
        {
            try
            {
                string connectionString = "Server=localhost;Database=bmi_calculator;Uid=root;Pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE Email = @Email AND Email != @CurrentEmail";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", newEmail);
                        cmd.Parameters.AddWithValue("@CurrentEmail", currentEmail);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // Assume exists to prevent update on error
            }
        }

        private bool UpdateUserEmail(string oldEmail, string newEmail, string password)
        {
            try
            {
                string connectionString = "Server=localhost;Database=bmi_calculator;Uid=root;Pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // First verify the password is correct
                    string verifyQuery = "SELECT COUNT(*) FROM users WHERE Email = @Email AND Password = @Password";
                    using (MySqlCommand verifyCmd = new MySqlCommand(verifyQuery, conn))
                    {
                        verifyCmd.Parameters.AddWithValue("@Email", oldEmail);
                        verifyCmd.Parameters.AddWithValue("@Password", password);
                        int count = Convert.ToInt32(verifyCmd.ExecuteScalar());

                        if (count == 0)
                        {
                            MessageBox.Show("Incorrect password. Email change failed.", "Authentication Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    // Update email in users table
                    string updateUserQuery = "UPDATE users SET Email = @NewEmail WHERE Email = @OldEmail";
                    using (MySqlCommand updateUserCmd = new MySqlCommand(updateUserQuery, conn))
                    {
                        updateUserCmd.Parameters.AddWithValue("@NewEmail", newEmail);
                        updateUserCmd.Parameters.AddWithValue("@OldEmail", oldEmail);
                        int userRowsAffected = updateUserCmd.ExecuteNonQuery();

                        if (userRowsAffected == 0)
                        {
                            MessageBox.Show("Failed to update email in users table.", "Update Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    // Update UserEmail in bmi_records table
                    string updateRecordsQuery = "UPDATE bmi_records SET UserEmail = @NewEmail WHERE UserEmail = @OldEmail";
                    using (MySqlCommand updateRecordsCmd = new MySqlCommand(updateRecordsQuery, conn))
                    {
                        updateRecordsCmd.Parameters.AddWithValue("@NewEmail", newEmail);
                        updateRecordsCmd.Parameters.AddWithValue("@OldEmail", oldEmail);
                        updateRecordsCmd.ExecuteNonQuery();
                        // We don't check rows affected here as there might not be any BMI records
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool VerifyCurrentPassword(string email, string password)
        {
            try
            {
                string connectionString = "Server=localhost;Database=bmi_calculator;Uid=root;Pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE Email = @Email AND Password = @Password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool UpdateUserPassword(string email, string newPassword)
        {
            try
            {
                string connectionString = "Server=localhost;Database=bmi_calculator;Uid=root;Pwd=;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE users SET Password = @NewPassword WHERE Email = @Email";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                        cmd.Parameters.AddWithValue("@Email", email);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}