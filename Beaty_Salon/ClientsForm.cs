using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Beaty_Salon
{
    public partial class ClientsForm : Form
    {
        private readonly MySqlConnection connection = new MySqlConnection("user = root; datasource=localhost;port=3306;password=root;database=beaty_salon;");
        int pageNumber = 0;
        int countRecords=0;
        int allrecords;
        int records=0;
        MySqlDataAdapter adapter;
        DataSet ds;
        MySqlCommand cmd;
        public ClientsForm()
        {
            InitializeComponent();
            comboBox2.KeyPress += (sender, e) => e.Handled = true;
            comboBox3.KeyPress += (sender, e) => e.Handled = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            using (connection)
            {
                adapter = new MySqlDataAdapter("SELECT ID,LastName,FirstName,Patronymic,Birthday,RegistrationDate,Email,Phone,(select Name from gender where client.GenderCode=gender.Code)'Пол',PhotoPath FROM client order by ID", connection);
                ds = new DataSet();
                adapter.Fill(ds,"client");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Фамилия";
                dataGridView1.Columns[2].HeaderText = "Имя";
                dataGridView1.Columns[3].HeaderText = "Отчество";
                dataGridView1.Columns[4].HeaderText = "День рождения";
                dataGridView1.Columns[5].HeaderText = "Дата регистрации";
                dataGridView1.Columns[6].HeaderText = "Email";
                dataGridView1.Columns[7].HeaderText = "Телефон";
                dataGridView1.Columns[8].HeaderText = "Пол";
                dataGridView1.Columns[9].HeaderText = "Картинка";
            }
            allrecords = ds.Tables["client"].Rows.Count;
            comboBox1.Text = "10";
            countRecords = 0;
            pageNumber = 0;
            using (connection)
            {
                GetSql();
            }
            records = ds.Tables["client"].Rows.Count;
            GetCountRows(records,allrecords);
        }

        private void GetCountRows(int records,int allrecords)
        {
            label3.Text = records+ "/"+allrecords;
        }

        public void GetSql()
        {
            if (comboBox1.Text=="Все")
            {
                adapter = new MySqlDataAdapter("SELECT ID,LastName,FirstName,Patronymic,Birthday,RegistrationDate,Email,Phone,(select Name from gender where client.GenderCode=gender.Code)'Пол',PhotoPath FROM client order by ID", connection);
                connection.Open();
                cmd = new MySqlCommand("SELECT ID,LastName,FirstName,Patronymic,Birthday,RegistrationDate,Email,Phone,(select Name from gender where client.GenderCode=gender.Code)'Пол',PhotoPath FROM client order by ID", connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                allrecords = dt.Rows.Count;
                connection.Close();
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                ds = new DataSet();
                if (ds.Tables.Contains("client"))
                {
                    ds.Tables["client"].Rows.Clear();
                }
                adapter.Fill(ds, "client");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Фамилия";
                dataGridView1.Columns[2].HeaderText = "Имя";
                dataGridView1.Columns[3].HeaderText = "Отчество";
                dataGridView1.Columns[4].HeaderText = "День рождения";
                dataGridView1.Columns[5].HeaderText = "Дата регистрации";
                dataGridView1.Columns[6].HeaderText = "Email";
                dataGridView1.Columns[7].HeaderText = "Телефон";
                dataGridView1.Columns[8].HeaderText = "Пол";
                dataGridView1.Columns[9].HeaderText = "Картинка";
            }
            else
            {
                if (textBox1.Text==null)
                {
                    adapter = new MySqlDataAdapter("SELECT ID,LastName,FirstName,Patronymic,Birthday,RegistrationDate,Email,Phone,(select Name from gender where client.GenderCode=gender.Code)'Пол',PhotoPath FROM client where (select Name from gender where client.GenderCode=gender.Code)='" + comboBox3.Text + "' order by ID Limit " + countRecords + "," + comboBox1.Text + "", connection);
                    cmd = new MySqlCommand("SELECT ID,LastName,FirstName,Patronymic,Birthday,RegistrationDate,Email,Phone,(select Name from gender where client.GenderCode=gender.Code)'Пол',PhotoPath FROM client where (select Name from gender where client.GenderCode=gender.Code)='" + comboBox3.Text + "' order by ID", connection);
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    ds = new DataSet();
                    if (ds.Tables.Contains("client"))
                    {
                        ds.Tables["client"].Rows.Clear();
                    }
                    adapter.Fill(ds, "client");
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "Фамилия";
                    dataGridView1.Columns[2].HeaderText = "Имя";
                    dataGridView1.Columns[3].HeaderText = "Отчество";
                    dataGridView1.Columns[4].HeaderText = "День рождения";
                    dataGridView1.Columns[5].HeaderText = "Дата регистрации";
                    dataGridView1.Columns[5].HeaderText = "Email";
                    dataGridView1.Columns[7].HeaderText = "Телефон";
                    dataGridView1.Columns[8].HeaderText = "Пол";
                    dataGridView1.Columns[9].HeaderText = "Картинка";
                }
                else
                {
                    adapter = new MySqlDataAdapter("SELECT ID,LastName,FirstName,Patronymic,Email,Phone,(select Name from gender where client.GenderCode=gender.Code)'Пол' FROM client where (select Name from gender where client.GenderCode=gender.Code)='" + comboBox3.Text + "' and CONCAT_WS('',LastName,FirstName,Patronymic,Phone,Email) LIKE '%" + textBox1.Text + "%' order by ID Limit " + countRecords + "," + comboBox1.Text + "", connection);
                    cmd = new MySqlCommand("SELECT ID,LastName,FirstName,Patronymic,Email,Phone,(select Name from gender where client.GenderCode=gender.Code)'Пол' FROM client where (select Name from gender where client.GenderCode=gender.Code)='" + comboBox3.Text + "' and CONCAT_WS('',LastName,FirstName,Patronymic,Phone,Email) LIKE '%" + textBox1.Text + "%' order by ID", connection);
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    ds = new DataSet();
                    if (ds.Tables.Contains("client"))
                    {
                        ds.Tables["client"].Rows.Clear();
                    }
                    adapter.Fill(ds, "client");
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "Фамилия";
                    dataGridView1.Columns[2].HeaderText = "Имя";
                    dataGridView1.Columns[3].HeaderText = "Отчество";
                    dataGridView1.Columns[4].HeaderText = "Email";
                    dataGridView1.Columns[5].HeaderText = "Телефон";
                    dataGridView1.Columns[6].HeaderText = "Пол";
                }
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                allrecords = dt.Rows.Count;
                connection.Close();
            }
        }

        private void RefreshDataSet()
        {
            GetSql();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((ds.Tables["client"].Rows.Count < Int32.Parse(comboBox1.Text)) || (comboBox1.Text=="Все")) return;
            pageNumber++;
            using (connection)
            {
                countRecords += Int32.Parse(comboBox1.Text);
                GetSql();
            }
            records = records + ds.Tables["client"].Rows.Count;
            GetCountRows(records, allrecords);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((pageNumber == 0) || (comboBox1.Text=="Все")) return;
            else
            {
                records = records - ds.Tables["client"].Rows.Count;
                GetCountRows(records, allrecords);
            }
            pageNumber--;
            using (connection)
            {
                countRecords -= Int32.Parse(comboBox1.Text);
                GetSql();
            }
            if (pageNumber == 0)
            {
                records = Int32.Parse(comboBox1.Text);
                GetCountRows(records, allrecords);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            countRecords = 0;
            pageNumber = 0;
            using (connection)
            {
                GetSql();
            }
            if (comboBox1.Text == "Все")
            {
                button1.Hide();
                button2.Hide();
                GetCountRows(records= allrecords, allrecords);
            }
            else
            {
                button1.Show();
                button2.Show();
                records = Int32.Parse(comboBox1.Text);
                if (records>allrecords)
                {
                    GetCountRows(allrecords, allrecords);
                }
                else
                {
                    GetCountRows(records, allrecords);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (connection)
            {
                GetSql();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormatClientForm formatClientForm = new FormatClientForm();
            formatClientForm.Show();
            formatClientForm.button1.Click += new EventHandler(formatClientForm.insertButton_Click);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Value != System.DBNull.Value)
            {
                FormatClientForm formatClientForm = new FormatClientForm();
                formatClientForm.Show();
                formatClientForm.button1.Click += new EventHandler(formatClientForm.updateButton_Click);
                cmd = new MySqlCommand("SELECT ID, LastName, FirstName, Patronymic, Birthday, RegistrationDate, Email, Phone, (select Name from gender where client.GenderCode = gender.Code)'Пол',PhotoPath FROM client where ID="+dataGridView1.CurrentRow.Cells[0].Value,connection);
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        formatClientForm.keyid = (int)reader.GetValue(0);
                        formatClientForm.textBox1.Text = reader.GetValue(1).ToString();
                        formatClientForm.textBox2.Text = reader.GetValue(2).ToString();
                        formatClientForm.textBox3.Text = reader.GetValue(3).ToString();
                        formatClientForm.dateTimePicker1.Value = (DateTime)reader.GetValue(4);
                        formatClientForm.textBox4.Text = reader.GetValue(6).ToString(); ;
                        formatClientForm.textBox5.Text = reader.GetValue(7).ToString(); ;
                        formatClientForm.comboBox1.Text = reader.GetValue(8).ToString();
                        formatClientForm.pictureBox1.Image = Image.FromFile("C:\\" + reader.GetValue(9).ToString().Trim());
                        formatClientForm.RefreshTagsdataGridView();
                    }
                }
                connection.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.Value!=System.DBNull.Value)
                {
                    DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Потвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        connection.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM client where ID=" + dataGridView1.CurrentRow.Cells[0].Value, connection);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        RefreshDataSet();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                adapter = new MySqlDataAdapter("SELECT ID,LastName,FirstName,Patronymic,Birthday,RegistrationDate,Email,Phone,(select Name from gender where client.GenderCode=gender.Code)'Пол',PhotoPath FROM client order by LastName", connection);
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                adapter = new MySqlDataAdapter("SELECT LastName,StartTime'Дата посещения' from client,clientservice where clientservice.CLIENTID=client.ID GROUP BY LastName order by StartTime", connection);
            }
            else
            {
                adapter = new MySqlDataAdapter("SELECT LastName,Count(*) as 'Количество посещений' from client,clientservice where clientservice.CLIENTID=client.ID GROUP BY LastName order by 'Количество посещений' desc", connection);
            }
            ds = new DataSet();
            adapter.Fill(ds,"client");
            dataGridView1.DataSource = ds.Tables[0];
            allrecords=ds.Tables[0].Rows.Count;
            GetCountRows(allrecords, allrecords);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Value != System.DBNull.Value)
            {
                ViewsForm viewsForm = new ViewsForm();
                viewsForm.keyid = (int)dataGridView1.CurrentRow.Cells[0].Value;
                viewsForm.Show();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            countRecords = 0;
            pageNumber = 0;
            using (connection)
            {
                GetSql();
                records = ds.Tables["client"].Rows.Count;
                GetCountRows(records, allrecords);
            }
        }
    }
}