using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static kyrsova.LoginForm;

namespace kyrsova
{
    public partial class clientForm: Form
    {

        public clientForm()
        {
            InitializeComponent();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSize = true;
            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            textBox3.ForeColor = Color.Gray;
            textBox4.ForeColor = Color.Gray;
            textBox5.ForeColor = Color.Gray;
            textBox13.ForeColor = Color.Gray;
            textBox14.ForeColor = Color.Gray;
            textBox6.ForeColor = Color.Gray;
            textBox11.ForeColor = Color.Gray;
            textBox8.ForeColor = Color.Gray;
            textBox7.ForeColor = Color.Gray;
            textBox9.ForeColor = Color.Gray;
            textBox10.ForeColor = Color.Gray;
            textBox16.ForeColor = Color.Gray;
            textBox15.ForeColor = Color.Gray;
            textBox12.ForeColor = Color.Gray;
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBox1.Items.AddRange(new string[] { "client_ID", "name", "surname", "client_adress", "email", "contact_information", "order_ID" });
        }

        private User currentUser;

        public clientForm(User user)
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
            textBox13.ForeColor = Color.Gray;
            textBox14.ForeColor = Color.Gray;
            textBox6.ForeColor = Color.Gray;
            textBox11.ForeColor = Color.Gray;
            textBox8.ForeColor = Color.Gray;
            textBox7.ForeColor = Color.Gray;
            textBox9.ForeColor = Color.Gray;
            textBox10.ForeColor = Color.Gray;
            textBox16.ForeColor = Color.Gray;
            textBox15.ForeColor = Color.Gray;
            textBox12.ForeColor = Color.Gray;
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBox1.Items.AddRange(new string[] { "client_ID", "name", "surname", "client_adress", "email", "contact_information", "order_ID" });
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
                textBox13.Visible = false;
                textBox13.Enabled = false;
                textBox14.Visible = false;
                textBox14.Enabled = false;
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
                textBox15.Visible = false;
                textBox15.Enabled = false;
                textBox16.Visible = false;
                textBox16.Enabled = false;

                
            }
        }

 
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM clients", db.getConnection());

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

        private void button2_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            try
            {
                db.openConnection();

                // Знаходимо рядок в DataGridView за client_ID
                int clientIdToFind = Convert.ToInt32(textBox1.Text);
                DataGridViewRow selectedRow = null;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow && Convert.ToInt32(row.Cells["client_ID"].Value) == clientIdToFind)
                    {
                        selectedRow = row;
                        break;
                    }
                }

                if (selectedRow != null)
                {
                    // Оновлюємо дані в базі даних
                    MySqlCommand command = new MySqlCommand("UPDATE clients SET name = @name, surname = @surname, client_adress = @address, email = @email, contact_information = @contact, order_ID = @orderId WHERE client_ID = @id", db.getConnection());
                    command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox2.Text;
                    command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = textBox3.Text;
                    command.Parameters.Add("@address", MySqlDbType.VarChar).Value = textBox4.Text;
                    command.Parameters.Add("@email", MySqlDbType.VarChar).Value = textBox5.Text;
                    command.Parameters.Add("@contact", MySqlDbType.VarChar).Value = textBox13.Text;
                    command.Parameters.Add("@orderId", MySqlDbType.Int32).Value = Convert.ToInt32(textBox14.Text); 
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = clientIdToFind;

                    command.ExecuteNonQuery();

                    MessageBox.Show("Запис успішно оновлено.");
                }
                else
                {
                    MessageBox.Show("Запис з client_ID " + clientIdToFind + " не знайдено.");
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
            if (textBox2.Text == "Введіть ім'я")
                textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Введіть ім'я";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Введіть прізвище")
                textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Введіть прізвище";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Введіть адресу")
                textBox4.Text = "";
            textBox4.ForeColor = Color.Black;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Введіть адресу";
                textBox4.ForeColor = Color.Gray;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Email")
                textBox5.Text = "";
            textBox5.ForeColor = Color.Black;
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Email";
                textBox5.ForeColor = Color.Gray;
            }
        }

        private void textBox13_Enter(object sender, EventArgs e)
        {
            if (textBox13.Text == "Контактна інформація")
                textBox13.Text = "";
            textBox13.ForeColor = Color.Black;
        }

        private void textBox13_Leave(object sender, EventArgs e)
        {
            if (textBox13.Text == "")
            {
                textBox13.Text = "Контактна інформація";
                textBox13.ForeColor = Color.Gray;
            }
        }

        private void textBox14_Enter(object sender, EventArgs e)
        {
            if (textBox14.Text == "Айді замовлення")
                textBox14.Text = "";
            textBox14.ForeColor = Color.Black;
        }

        private void textBox14_Leave(object sender, EventArgs e)
        {
            if (textBox14.Text == "")
            {
                textBox14.Text = "Айді замовлення";
                textBox14.ForeColor = Color.Gray;
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
            if (textBox11.Text == "Айді клієнта")
                textBox11.Text = "";
            textBox11.ForeColor = Color.Black;
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            if (textBox11.Text == "")
            {
                textBox11.Text = "Айді клієнта";
                textBox11.ForeColor = Color.Gray;
            }
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text == "Введіть ім'я")
                textBox8.Text = "";
            textBox8.ForeColor = Color.Black;
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
            {
                textBox8.Text = "Введіть ім'я";
                textBox8.ForeColor = Color.Gray;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Введіть прізвище")
                textBox7.Text = "";
            textBox7.ForeColor = Color.Black;
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "Введіть прізвище";
                textBox7.ForeColor = Color.Gray;
            }
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            if (textBox9.Text == "Введіть адресу")
                textBox9.Text = "";
            textBox9.ForeColor = Color.Black;
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                textBox9.Text = "Введіть адресу";
                textBox9.ForeColor = Color.Gray;
            }
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            if (textBox10.Text == "Email")
                textBox10.Text = "";
            textBox10.ForeColor = Color.Black;
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if (textBox10.Text == "")
            {
                textBox10.Text = "Email";
                textBox10.ForeColor = Color.Gray;
            }
        }

        private void textBox16_Enter(object sender, EventArgs e)
        {
            if (textBox16.Text == "Контактна інформація")
                textBox16.Text = "";
            textBox16.ForeColor = Color.Black;
        }

        private void textBox16_Leave(object sender, EventArgs e)
        {
            if (textBox16.Text == "")
            {
                textBox16.Text = "Контактна інформація";
                textBox16.ForeColor = Color.Gray;
            }
        }

        private void textBox15_Enter(object sender, EventArgs e)
        {
            if (textBox15.Text == "Айді замовлення")
                textBox15.Text = "";
            textBox15.ForeColor = Color.Black;
        }

        private void textBox15_Leave(object sender, EventArgs e)
        {
            if (textBox15.Text == "")
            {
                textBox15.Text = "Айді замовлення";
                textBox15.ForeColor = Color.Gray;
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
        private void button3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM clients WHERE client_ID = @id", db.getConnection());
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
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO clients (client_ID, name, surname, client_adress, email, contact_information, order_ID) VALUES (@client_ID, @name, @surname, @address, @email, @contact, @orderId)", db.getConnection());
                insertCommand.Parameters.Add("@client_ID", MySqlDbType.VarChar).Value = textBox11.Text;
                insertCommand.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox8.Text;
                insertCommand.Parameters.Add("@surname", MySqlDbType.VarChar).Value = textBox7.Text;
                insertCommand.Parameters.Add("@address", MySqlDbType.VarChar).Value = textBox9.Text;
                insertCommand.Parameters.Add("@email", MySqlDbType.VarChar).Value = textBox10.Text;
                insertCommand.Parameters.Add("@contact", MySqlDbType.VarChar).Value = textBox16.Text;
                insertCommand.Parameters.Add("@orderId", MySqlDbType.Int32).Value = Convert.ToInt32(textBox15.Text);

                insertCommand.ExecuteNonQuery();

                MessageBox.Show("Запис успішно додано.");
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректні значення.");
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                case 0: // client_ID
                    columnName = "client_ID";
                    break;
                case 1: // name
                    columnName = "name";
                    break;
                case 2: // surname
                    columnName = "surname";
                    break;
                case 3: // client_adress
                    columnName = "client_adress";
                    break;
                case 4: // email
                    columnName = "email";
                    break;
                case 5: // contact_information
                    columnName = "contact_information";
                    break;
                case 6: // order_ID
                    columnName = "order_ID";
                    break;
                default:
                    MessageBox.Show("Будь ласка, виберіть критерій пошуку.");
                    return;
            }

            try
            {
                db.openConnection();

                // Формуємо SQL-запит для пошуку
                string query = $"SELECT * FROM clients WHERE {columnName} = @searchValue";
                MySqlCommand command = new MySqlCommand(query, db.getConnection());

                // Перевірка типу даних для order_ID
                if (columnName == "order_ID")
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
    }
    }
    

