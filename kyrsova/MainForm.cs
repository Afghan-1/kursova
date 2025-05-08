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
    public partial class MainForm : Form
    {
        public MainForm()
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
            textBox6.ForeColor = Color.Gray;
            textBox11.ForeColor = Color.Gray;
            textBox7.ForeColor = Color.Gray;
            textBox8.ForeColor = Color.Gray;
            textBox9.ForeColor = Color.Gray;
            textBox10.ForeColor = Color.Gray;
            textBox12.ForeColor = Color.Gray;
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBox1.Items.AddRange(new string[] { "order_ID", "order_date", "amount", "client_ID", "worker_ID" });

        }




        private User currentUser;

        public MainForm(User user)
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
            comboBox1.Items.AddRange(new string[] { "order_ID", "order_date", "amount", "client_ID", "worker_ID" });

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


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM orders", db.getConnection());

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

                // Знаходимо рядок в DataGridView за order_ID
                int orderIdToFind = Convert.ToInt32(textBox1.Text);
                DataGridViewRow selectedRow = null;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow && Convert.ToInt32(row.Cells["order_ID"].Value) == orderIdToFind)
                    {
                        selectedRow = row;
                        break;
                    }
                }

                if (selectedRow != null)
                {
                    // Оновлюємо дані в базі даних
                    MySqlCommand command = new MySqlCommand("UPDATE orders SET order_date = @date, amount = @amount, client_ID = @client, worker_ID = @worker WHERE order_ID = @id", db.getConnection());
                    command.Parameters.Add("@date", MySqlDbType.Date).Value = Convert.ToDateTime(textBox2.Text);
                    command.Parameters.Add("@amount", MySqlDbType.Int32).Value = Convert.ToInt32(textBox3.Text);
                    command.Parameters.Add("@client", MySqlDbType.Int32).Value = Convert.ToInt32(textBox4.Text);
                    command.Parameters.Add("@worker", MySqlDbType.Int32).Value = Convert.ToInt32(textBox5.Text);
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = orderIdToFind;

                    command.ExecuteNonQuery();

                    MessageBox.Show("Запис успішно оновлено.");
                }
                else
                {
                    MessageBox.Show("Запис з order_ID " + orderIdToFind + " не знайдено.");
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
                MySqlCommand command = new MySqlCommand("DELETE FROM orders WHERE order_ID = @id", db.getConnection());
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
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO orders (order_ID, order_date, amount, client_ID, worker_ID) VALUES (@id, @date, @amount, @client, @worker)", db.getConnection());
                insertCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToInt32(textBox11.Text); // order_ID
                insertCommand.Parameters.Add("@date", MySqlDbType.Date).Value = Convert.ToDateTime(textBox7.Text);
                insertCommand.Parameters.Add("@amount", MySqlDbType.Int32).Value = Convert.ToInt32(textBox8.Text);
                insertCommand.Parameters.Add("@client", MySqlDbType.Int32).Value = Convert.ToInt32(textBox9.Text);
                insertCommand.Parameters.Add("@worker", MySqlDbType.Int32).Value = Convert.ToInt32(textBox10.Text);

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
            if (textBox2.Text == "Введіть дату")
                textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Введіть дату";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Введіть суму")
                textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Введіть суму";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Айді клієнта")
                textBox4.Text = "";
            textBox4.ForeColor = Color.Black;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Айді клієнта";
                textBox4.ForeColor = Color.Gray;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {

            if (textBox5.Text == "Айді працівника")
                textBox5.Text = "";
            textBox5.ForeColor = Color.Black;
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Айді працівника";
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
            if (textBox11.Text == "Айді замовлення")
                textBox11.Text = "";
            textBox11.ForeColor = Color.Black;
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            if (textBox11.Text == "")
            {
                textBox11.Text = "Айді замовлення";
                textBox11.ForeColor = Color.Gray;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Введіть дату")
                textBox7.Text = "";
            textBox7.ForeColor = Color.Black;
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "Введіть дату";
                textBox7.ForeColor = Color.Gray;
            }
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text == "Введіть суму")
                textBox8.Text = "";
            textBox8.ForeColor = Color.Black;
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
            {
                textBox8.Text = "Введіть суму";
                textBox8.ForeColor = Color.Gray;
            }
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            if (textBox9.Text == "Айді клієнта")
                textBox9.Text = "";
            textBox9.ForeColor = Color.Black;
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                textBox9.Text = "Айді клієнта";
                textBox9.ForeColor = Color.Gray;
            }
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            if (textBox10.Text == "Айді працівника")
                textBox10.Text = "";
            textBox10.ForeColor = Color.Black;
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if (textBox10.Text == "")
            {
                textBox10.Text = "Айді працівника";
                textBox10.ForeColor = Color.Gray;
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
                case 0: // order_ID
                    columnName = "order_ID";
                    break;
                case 1: // order_date
                    columnName = "order_date";
                    break;
                case 2: // amount
                    columnName = "amount";
                    break;
                case 3: // client_ID
                    columnName = "client_ID";
                    break;
                case 4: // worker_ID
                    columnName = "worker_ID";
                    break;
                default:
                    MessageBox.Show("Будь ласка, виберіть критерій пошуку.");
                    return;
            }

            try
            {
                db.openConnection();

                // Формуємо SQL-запит для пошуку
                string query = $"SELECT * FROM orders WHERE {columnName} = @searchValue";
                MySqlCommand command = new MySqlCommand(query, db.getConnection());
                command.Parameters.AddWithValue("@searchValue", searchValue);

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
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
            finally
            {
                db.closeConnection();
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
        private void button16_Click(object sender, EventArgs e)
        {
            this.Hide();
            tursForm hotelssForm = new tursForm(currentUser);
            hotelssForm.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("SELECT SUM(amount) FROM orders", db.getConnection());
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    MessageBox.Show("Загальна сума: " + result.ToString());
                }
                else
                {
                    MessageBox.Show("Немає даних для обчислення загальної суми.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка бази даних: " + ex.Message);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("SELECT AVG(amount) FROM orders", db.getConnection());
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    MessageBox.Show("Середнє значення: " + result.ToString());
                }
                else
                {
                    MessageBox.Show("Немає даних для обчислення середнього значення.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка бази даних: " + ex.Message);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("SELECT MAX(amount) FROM orders", db.getConnection());
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    MessageBox.Show("Максимальне значення: " + result.ToString());
                }
                else
                {
                    MessageBox.Show("Немає даних для знаходження максимального значення.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка бази даних: " + ex.Message);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("SELECT MIN(amount) FROM orders", db.getConnection());
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    MessageBox.Show("Мінімальне значення: " + result.ToString());
                }
                else
                {
                    MessageBox.Show("Немає даних для знаходження мінімального значення.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка бази даних: " + ex.Message);
            }
            finally
            {
                db.closeConnection();
            }
        }

        
    }
    }




    
    

