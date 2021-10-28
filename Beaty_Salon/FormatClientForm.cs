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
using MySql.Data.MySqlClient;

namespace Beaty_Salon
{
    public partial class FormatClientForm : Form
    {

        private readonly MySqlConnection connection = new MySqlConnection("user = root; datasource=localhost;port=3306;password=root;database=beaty_salon;");
        private FileInfo file;
        private int changeImage = 0;
        public int keyid;
        MySqlCommand cmd;
        DataSet ds;
        MySqlDataAdapter adapter;
        public FormatClientForm()
        {
            InitializeComponent();
        }

        public void RefreshTagsdataGridView()
        {
            adapter = new MySqlDataAdapter("select (select ID from tag where tagofclient.TagID=tag.ID),(select Title from tag where tagofclient.TagID=tag.ID) from tagofclient where ClientID=" + keyid,connection);
            ds = new DataSet();
            if (ds.Tables.Contains("tags"))
            {
                ds.Tables["tags"].Rows.Clear();
            }
            adapter.Fill(ds, "tags");
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Тег";
            cmd = new MySqlCommand("Select (select color from tag where tagofclient.TagID=tag.ID) from tagofclient where ClientID="+keyid, connection);
            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                int i=0;
                while (reader.Read() && (i <= dataGridView1.RowCount))
                {
                    dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.FromName(reader.GetValue(0).ToString());
                    i++;
                }
            }
            connection.Close();
            dataGridView1.Columns[0].Width = 30;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image (*.jpg; *.png)|*.jpg; *.png";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
                file = new FileInfo(opf.FileName);
                changeImage = 1;
            }
    }

        public void insertButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Добавить запись?", "Потвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (textBox1.TextLength <= 50 && textBox2.TextLength <= 50 && textBox3.TextLength <= 50 && IsValidEmail(textBox4.Text))
                    {
                        connection.Open();
                        cmd = new MySqlCommand(String.Format("insert into `client`(`LastName`, `FirstName`, `Patronymic`,`Birthday`,`RegistrationDate`,`Email`,`Phone`,`GenderCode`,`PhotoPath`) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "',@birthday,@register,'" + textBox4.Text + "','" + textBox5.Text + "',(select Code from gender where Name='" + comboBox1.Text + "'),'" + (string)file.FullName + "')"), connection);
                        cmd.Parameters.Add("@birthday", MySqlDbType.DateTime);
                        cmd.Parameters["@birthday"].Value = dateTimePicker1.Value;
                        cmd.Parameters.Add("@register", MySqlDbType.Date);
                        cmd.Parameters["@register"].Value = DateTime.Now;
                        cmd.ExecuteNonQuery();
                        MemoryStream ms = new MemoryStream();
                        pictureBox1.Image.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
                        byte[] b = ms.ToArray();
                        MemoryStream ms1 = new MemoryStream();
                        foreach (byte b1 in b)
                        {
                            ms1.WriteByte(b1);
                        }
                        Image image = Image.FromStream(ms1);
                        image.Save(@"C:\Клиенты\" + (string)file.Name, System.Drawing.Imaging.ImageFormat.Png);
                        connection.Close();
                    }
                    
                    else
                    {
                        MessageBox.Show("Отформатируйте поля");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Хотите изменить запись?", "Потвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (textBox1.TextLength <= 50 && textBox2.TextLength <= 50 && textBox3.TextLength <= 50 && IsValidEmail(textBox4.Text))
                    {
                        connection.Open();
                        if (changeImage==1)
                        {
                            cmd = new MySqlCommand(String.Format("update client set LastName='" + textBox1.Text + "', FirstName='" + textBox2.Text + "', Patronymic='" + textBox3.Text + "', Birthday=@birthday, RegistrationDate=@register, Email='" + textBox4.Text + "', Phone='" + textBox5.Text + "', GenderCode=(select Code from gender where Name='" + comboBox1.Text + "'),PhotoPath='" + @"Клиенты/" + (string)file.Name + "' Where ID=" + keyid + ""), connection);
                        }
                        else
                        {
                            cmd = new MySqlCommand(String.Format("update client set LastName='" + textBox1.Text + "', FirstName='" + textBox2.Text + "', Patronymic='" + textBox3.Text + "', Birthday=@birthday, RegistrationDate=@register, Email='" + textBox4.Text + "', Phone='" + textBox5.Text + "', GenderCode=(select Code from gender where Name='" + comboBox1.Text + "') Where ID=" + keyid + ""), connection);
                        }
                        cmd.Parameters.Add("@birthday", MySqlDbType.DateTime);
                        cmd.Parameters["@birthday"].Value = dateTimePicker1.Value;
                        cmd.Parameters.Add("@register", MySqlDbType.Date);
                        cmd.Parameters["@register"].Value = DateTime.Now;
                        cmd.ExecuteNonQuery();
                        if (changeImage==1)
                        {
                            MemoryStream ms = new MemoryStream();
                            pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] b = ms.ToArray();
                            MemoryStream ms1 = new MemoryStream();
                            foreach (byte b1 in b)
                            {
                                ms1.WriteByte(b1);
                            }
                            Image image = Image.FromStream(ms1);
                            image.Save(@"C:\Клиенты\" + (string)file.Name, System.Drawing.Imaging.ImageFormat.Png);
                        }
                        connection.Close();
                        Form F = Application.OpenForms["ClientsForm"];
                        ((ClientsForm)F).GetSql();
                    }
                    else
                    {
                        MessageBox.Show("Отформатируйте поля");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 'А' || e.KeyChar > 'я') && e.KeyChar != '\b' && e.KeyChar != '.')
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 'А' || e.KeyChar > 'я') && e.KeyChar != '\b' && e.KeyChar != '.')
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 'А' || e.KeyChar > 'я') && e.KeyChar != '\b' && e.KeyChar != '.')
                e.Handled = true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(textBox4.Text);
                if (addr.Address == email)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 59) && e.KeyChar != '\b' && e.KeyChar != '.')
                e.Handled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить тег?", "Потвердите действие", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result==DialogResult.Yes)
            {
                connection.Open();
                cmd = new MySqlCommand("DELETE from tagofclient where ClientID="+keyid+" and (select Title from tag where tag.ID=tagofclient.TagID)='"+dataGridView1.CurrentRow.Cells[1].Value+"'",connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                RefreshTagsdataGridView();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddTagForm addTagForm = new AddTagForm();
            addTagForm.keyid = keyid;
            addTagForm.Show();
        }
    }
}
