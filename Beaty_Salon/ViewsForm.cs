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
    public partial class ViewsForm : Form
    {

        public int keyid;
        DataSet ds;
        MySqlDataAdapter adapter;

        private readonly MySqlConnection connection = new MySqlConnection("user=root;password=root;port=3306;datasource=localhost;database=beaty_salon");

        public ViewsForm()
        {
            InitializeComponent();
            dGVView.AutoResizeColumns();
            dGVViewFiles.AutoResizeColumns();
        }

        private void ViewsForm_Load(object sender, EventArgs e)
        {
            adapter = new MySqlDataAdapter("Select ID,(select LastName from client where clientservice.CLIENTID=client.ID),(select title from service where clientservice.serviceid=service.id),StartTime from clientservice where clientid=" + keyid, connection);
            ds = new DataSet();
            adapter.Fill(ds, "views");
            dGVView.DataSource = ds.Tables[0];
            dGVView.Columns[0].HeaderText = "ID";
            dGVView.Columns[1].HeaderText = "Клиент";
            dGVView.Columns[2].HeaderText = "Услуга";
            dGVView.Columns[3].HeaderText = "Дата посещений";
            if (ds.Tables["views"].Rows.Count>1)
            {
                adapter = new MySqlDataAdapter("SELECT ID,DOCUMENTPATH FROM documentbyservice where clientserviceid=" + dGVView.Rows[0].Cells[0].Value, connection);
                ds = new DataSet();
                adapter.Fill(ds, "service");
                dGVViewFiles.DataSource = ds.Tables[0];
                dGVViewFiles.Columns[0].HeaderText = "ID";
                dGVViewFiles.Columns[1].HeaderText = "Документ";
            }
        }

        private void dGVView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            adapter = new MySqlDataAdapter("SELECT ID,DOCUMENTPATH FROM documentbyservice where clientserviceid=" + dGVView.CurrentRow.Cells[0].Value, connection);
            ds = new DataSet();
            adapter.Fill(ds, "service");
            dGVViewFiles.DataSource = ds.Tables[0];
            dGVViewFiles.Columns[0].HeaderText = "ID";
            dGVViewFiles.Columns[1].HeaderText = "Документ";
        }
    }
}
