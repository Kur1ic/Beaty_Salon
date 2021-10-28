using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Beaty_Salon
{
    public partial class AddTagForm : Form
    {

        private readonly MySqlConnection connection = new MySqlConnection("user=root;password=root;;datasource=localhost;database=beaty_salon;port=3306");

        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataSet ds;
        public int keyid;

        public AddTagForm()
        {
            InitializeComponent();
        }

        private void RefreshTagsGridView()
        {
            adapter = new MySqlDataAdapter("SELECT ID,Title from tag",connection);
            ds = new DataSet();
            adapter.Fill(ds,"tags");
            TagsGridView.DataSource = ds.Tables[0];
            cmd = new MySqlCommand("select color from tag",connection);
            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read() && (i<=TagsGridView.Rows.Count))
                {
                    TagsGridView.Rows[i].Cells[1].Style.BackColor = Color.FromName(reader.GetValue(0).ToString());
                    i++;
                }
            }
            connection.Close();
        }

        private void AddTagForm_Shown(object sender, EventArgs e)
        {
            RefreshTagsGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите добавить тег?","Потвердите действие",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result==DialogResult.Yes)
            {
                cmd = new MySqlCommand("Insert into `tagofclient`(ClientID,TagID) values('"+keyid+"','"+TagsGridView.CurrentRow.Cells[0].Value.ToString()+"')",connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close(); 
            }
            Form F = Application.OpenForms["FormatClientForm"];
            ((FormatClientForm)F).RefreshTagsdataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
