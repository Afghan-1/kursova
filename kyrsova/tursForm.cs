using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static kyrsova.LoginForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kyrsova
{
    public partial class tursForm : Form
    {
        public tursForm()
        {
            InitializeComponent();
        }

        private User currentUser;

        public tursForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            CheckUserRole();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSize = true;
            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            textBox3.ForeColor = Color.Gray;
            textBox4.ForeColor = Color.Gray;
            textBox5.ForeColor = Color.Gray;
            textBox6.ForeColor = Color.Gray;
            textBox11.ForeColor = Color.Gray;
            textBox7.ForeColor = Color.Gray;
            textBox8.ForeColor = Color.Gray;
            textBox9.ForeColor = Color.Gray;
            textBox10.ForeColor = Color.Gray;
            textBox12.ForeColor = Color.Gray;
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBox1.Items.AddRange(new string[] { "tur_ID", "name", "hotel_name", "airline_name"});

        }

        private void CheckUserRole()
        {
            if (currentUser.Role == "user")
            {
                // Приховуємо кнопки, які не повинні бути доступні для користувача з роллю "user"
                button2.Visible = false;
                button2.Enabled = false;
                button3.Visible = false;
                button3.Enabled = false;
                button4.Visible = false;
                button4.Enabled = false;
                button13.Visible = false;
                button13.Enabled = false;

                textBox1.Visible = false;
                textBox1.Enabled = false;
                textBox2.Visible = false;
                textBox2.Enabled = false;
                textBox3.Visible = false;
                textBox3.Enabled = false;
                textBox4.Visible = false;
                textBox4.Enabled = false;
                textBox5.Visible = false;
                textBox5.Enabled = false;
                textBox6.Visible = false;
                textBox6.Enabled = false;
                textBox11.Visible = false;
                textBox11.Enabled = false;
                textBox7.Visible = false;
                textBox7.Enabled = false;
                textBox8.Visible = false;
                textBox8.Enabled = false;
                textBox9.Visible = false;
                textBox9.Enabled = false;
                textBox10.Visible = false;
                textBox10.Enabled = false;



            }
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            clientForm clientsForm = new clientForm(currentUser);
            clientsForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm MainsForm = new MainForm(currentUser);
            MainsForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            reservForm reservsForm = new reservForm(currentUser);
            reservsForm.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            workersForm workerssForm = new workersForm(currentUser);
            workerssForm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            hotelsForm hotelssForm = new hotelsForm(currentUser);
            hotelssForm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            airlinesForm airlinessForm = new airlinesForm(currentUser);
            airlinessForm.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            tursForm hotelssForm = new tursForm(currentUser);
            hotelssForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT tur_ID, name, hotel_name, airline_name, overview FROM turs", db.getConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("Користувача не знайдено.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка бази даних: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);


            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Image Files (.jpg;.jpeg;.png;.gif;.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (.)|*.*";
            openFileDialog1.Title = "Виберіть файл зображення";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при завантаженні зображення: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            try
            {
                db.openConnection();

                // Знаходимо рядок в DataGridView за tur_ID
                int turIdToFind = Convert.ToInt32(textBox1.Text);
                DataGridViewRow selectedRow = null;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow && Convert.ToInt32(row.Cells["tur_ID"].Value) == turIdToFind)
                    {
                        selectedRow = row;
                        break;
                    }
                }

                byte[] imageData = null;
                if (pictureBox1.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        imageData = ms.ToArray();
                    }
                }
                if (selectedRow != null)
                {
                    // Оновлюємо дані в базі даних
                    MySqlCommand command = new MySqlCommand("UPDATE turs SET name = @name, hotel_name = @hotelName, airline_name = @airlineName, overview = @overview WHERE tur_ID = @id", db.getConnection());
                    command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox2.Text;
                    command.Parameters.Add("@hotelName", MySqlDbType.VarChar).Value = textBox3.Text;
                    command.Parameters.Add("@airlineName", MySqlDbType.VarChar).Value = textBox4.Text;
                    command.Parameters.Add("@overview", MySqlDbType.VarChar).Value = textBox5.Text;
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = turIdToFind;

                    command.ExecuteNonQuery();

                    MessageBox.Show("Запис успішно оновлено.");
                }
                else
                {
                    MessageBox.Show("Запис з tur_ID " + turIdToFind + " не знайдено.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректні числові значення.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка при оновленні запису: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM turs WHERE tur_ID = @id", db.getConnection());
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToInt32(textBox6.Text);
                command.ExecuteNonQuery();

                MessageBox.Show("Запис успішно видалено.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка при видаленні запису: " + ex.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректний ID.");
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                // Отримання максимального ID
                MySqlCommand maxIdCommand = new MySqlCommand("SELECT MAX(tur_ID) FROM turs", db.getConnection());
                object maxIdObject = maxIdCommand.ExecuteScalar();
                int maxId = (maxIdObject == DBNull.Value) ? 0 : Convert.ToInt32(maxIdObject);

                // Перетворення зображення на масив байтів
                byte[] imageData = null;
                if (pictureBox1.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        imageData = ms.ToArray();
                    }
                }

                // Запит INSERT з параметром для зображення
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO turs (tur_ID, name, hotel_name, airline_name, overview, image) VALUES (@turID, @Name, @HotelName, @AirlineName, @Overview, @Image)", db.getConnection());
                insertCommand.Parameters.Add("turID", MySqlDbType.Int32).Value = textBox11.Text;
                insertCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = textBox7.Text;
                insertCommand.Parameters.Add("@HotelName", MySqlDbType.VarChar).Value = textBox8.Text;
                insertCommand.Parameters.Add("@AirlineName", MySqlDbType.VarChar).Value = textBox9.Text;
                insertCommand.Parameters.Add("@Overview", MySqlDbType.VarChar).Value = textBox10.Text;
                insertCommand.Parameters.Add("@Image", MySqlDbType.Blob).Value = imageData;

                insertCommand.ExecuteNonQuery();

                MessageBox.Show("Запис успішно додано.");
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректні числові значення.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка при додаванні запису: " + ex.Message);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            string columnName = "";
            string searchValue = textBox12.Text;

            // Визначаємо, за яким стовпцем шукати
            switch (comboBox1.SelectedIndex)
            {
                case 0: // tur_ID
                    columnName = "tur_ID";
                    break;
                case 1: // name
                    columnName = "name";
                    break;
                case 2: // hotel_name
                    columnName = "hotel_name";
                    break;
                case 3: // airline_name
                    columnName = "airline_name";
                    break;
                case 4: // overview
                    columnName = "overview";
                    break;
                case 5: // image
                    columnName = "image";
                    break;
                default:
                    MessageBox.Show("Будь ласка, виберіть критерій пошуку.");
                    return;
            }

            try
            {
                db.openConnection();

                // Формуємо SQL-запит для пошуку (без стовпця image)
                string query = $"SELECT tur_ID, name, hotel_name, airline_name, overview FROM turs WHERE {columnName} = @searchValue";
                MySqlCommand command = new MySqlCommand(query, db.getConnection());

                // Перевірка типу даних для tur_ID
                if (columnName == "tur_ID")
                {
                    command.Parameters.AddWithValue("@searchValue", Convert.ToInt32(searchValue));
                }
                else
                {
                    command.Parameters.AddWithValue("@searchValue", searchValue);
                }

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    dataGridView1.DataSource = table;

                    // Відображення зображення в pictureBox1 (якщо вибрано image)
                    if (columnName == "image" && table.Rows[0]["image"] != DBNull.Value)
                    {
                        byte[] imageData = (byte[])table.Rows[0]["image"];
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                    else if (columnName == "image")
                    {
                        pictureBox1.Image = null; // Очищення pictureBox1, якщо зображення немає
                    }
                    else
                    {
                        // Якщо вибрано інший стовпець, відображаємо перше зображення з результатів пошуку
                        query = $"SELECT image FROM turs WHERE tur_ID = {table.Rows[0]["tur_ID"]}";
                        command = new MySqlCommand(query, db.getConnection());
                        object imageObject = command.ExecuteScalar();

                        if (imageObject != DBNull.Value)
                        {
                            byte[] imageData = (byte[])imageObject;
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                        }
                        else
                        {
                            pictureBox1.Image = null; // Очищення pictureBox1, якщо зображення немає
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Записів не знайдено.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка бази даних: " + ex.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильний формат даних.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Введіть айді")
                textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Введіть айді";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Введіть назву тура")
                textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Введіть назву тура";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Введіть назву готеля")
                textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Введіть назву готеля";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Введіть назву авіакомпанії")
                textBox4.Text = "";
            textBox4.ForeColor = Color.Black;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Введіть назву авіакомпанії";
                textBox4.ForeColor = Color.Gray;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Введіть опис")
                textBox5.Text = "";
            textBox5.ForeColor = Color.Black;
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Введіть опис";
                textBox5.ForeColor = Color.Gray;
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Введіть айді")
                textBox6.Text = "";
            textBox6.ForeColor = Color.Black;
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Введіть айді";
                textBox6.ForeColor = Color.Gray;
            }
        }

        private void textBox11_Enter(object sender, EventArgs e)
        {
            if (textBox11.Text == "Введіть айді")
                textBox11.Text = "";
            textBox11.ForeColor = Color.Black;
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            if (textBox11.Text == "")
            {
                textBox11.Text = "Введіть айді";
                textBox11.ForeColor = Color.Gray;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Введіть назву тура")
                textBox7.Text = "";
            textBox7.ForeColor = Color.Black;
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "Введіть назву тура";
                textBox7.ForeColor = Color.Gray;
            }
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text == "Введіть назву готеля")
                textBox8.Text = "";
            textBox8.ForeColor = Color.Black;
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
            {
                textBox8.Text = "Введіть назву готеля";
                textBox8.ForeColor = Color.Gray;
            }
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            if (textBox9.Text == "Введіть назву авіакомпанії")
                textBox9.Text = "";
            textBox9.ForeColor = Color.Black;
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                textBox9.Text = "Введіть назву авіакомпанії";
                textBox9.ForeColor = Color.Gray;
            }
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            if (textBox10.Text == "Введіть опис")
                textBox10.Text = "";
            textBox10.ForeColor = Color.Black;
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if (textBox10.Text == "")
            {
                textBox10.Text = "Введіть опис";
                textBox10.ForeColor = Color.Gray;
            }

        }

        private void textBox12_Enter(object sender, EventArgs e)
        {
            if (textBox12.Text == "Напишіть значення")
                textBox12.Text = "";
            textBox12.ForeColor = Color.Black;
        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
            if (textBox12.Text == "")
            {
                textBox12.Text = "Напишіть значення";
                textBox12.ForeColor = Color.Gray;
            }
        }
    }
}
    



