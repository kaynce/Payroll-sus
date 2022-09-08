using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//New import
using System.Data.SqlClient;
using System.Configuration;

namespace HRM
{
    public partial class Admin_Log_History : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        public Admin_Log_History()
        {
            InitializeComponent();
            RetrieveData();
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 189;
            dataGridView1.Columns[1].Width = 205;
            dataGridView1.Columns[2].Width = 205;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 200;
        }

        private void RetrieveData()
        {
            try
            {
                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Admin_Log_History2", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 5;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                dataGridView1.Columns[0].HeaderText = "Date"; dataGridView1.Columns[0].DataPropertyName = "Date";

                dataGridView1.Columns[1].HeaderText = "Surname"; dataGridView1.Columns[1].DataPropertyName = "Surname";

                dataGridView1.Columns[2].HeaderText = "Firstname"; dataGridView1.Columns[2].DataPropertyName = "Firstname";

                dataGridView1.Columns[3].HeaderText = "Time In"; dataGridView1.Columns[3].DataPropertyName = "Time_In";

                dataGridView1.Columns[4].HeaderText = "Time Out"; dataGridView1.Columns[4].DataPropertyName = "Time_Out";

                dataGridView1.DataSource = dt;

                //
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                autoSize();
            }
            catch { }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fromDate.Text.Equals(toDate.Text))
                {
                    DataTable dt_All = new DataTable();
                    string sql_All = "SELECT * FROM P_Admin_Log_History2 WHERE Date Between @from and @to";
                    SqlDataAdapter da_All = new SqlDataAdapter(sql_All, con);

                    da_All.SelectCommand.Parameters.AddWithValue("@from", fromDate.Text.Trim());
                    da_All.SelectCommand.Parameters.AddWithValue("@to", toDate.Text.Trim());

                    da_All.Fill(dt_All);

                    dataGridView1.DataSource = dt_All;
                }
                else
                {
                    MessageBox.Show("Please connect the date 'From' - 'To'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            RetrieveData();
        }
    }
}
