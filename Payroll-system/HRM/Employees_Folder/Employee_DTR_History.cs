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
    public partial class Employee_DTR_History : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        public Employee_DTR_History()
        {
            InitializeComponent();
            RetrieveData();
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 105;
            dataGridView1.Columns[1].Width = 114;
            dataGridView1.Columns[2].Width = 110;
            dataGridView1.Columns[3].Width = 110;
            dataGridView1.Columns[4].Width = 110;
            dataGridView1.Columns[5].Width = 110;
            dataGridView1.Columns[6].Width = 85;
            dataGridView1.Columns[7].Width = 85;
            dataGridView1.Columns[8].Width = 85;
            dataGridView1.Columns[9].Width = 85;
        }

        private void RetrieveData()
        {
            try
            {
                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Worker_Details", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 10;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                dataGridView1.Columns[0].HeaderText = "Date"; dataGridView1.Columns[0].DataPropertyName = "Date";

                dataGridView1.Columns[1].HeaderText = "Employee ID"; dataGridView1.Columns[1].DataPropertyName = "Employee_ID";

                dataGridView1.Columns[2].HeaderText = "Surname"; dataGridView1.Columns[2].DataPropertyName = "Surname";

                dataGridView1.Columns[3].HeaderText = "Firstnme"; dataGridView1.Columns[3].DataPropertyName = "Firstname";

                dataGridView1.Columns[4].HeaderText = "Time In"; dataGridView1.Columns[4].DataPropertyName = "Time_In";

                dataGridView1.Columns[5].HeaderText = "Time Out"; dataGridView1.Columns[5].DataPropertyName = "Time_Out";

                dataGridView1.Columns[6].HeaderText = "Worked Hours"; dataGridView1.Columns[6].DataPropertyName = "Work_Hours";

                dataGridView1.Columns[7].HeaderText = "Over Time Hours"; dataGridView1.Columns[7].DataPropertyName = "Over_Time_Hours";

                dataGridView1.Columns[8].HeaderText = "Late/Hours"; dataGridView1.Columns[8].DataPropertyName = "Late_Hours";

                dataGridView1.Columns[9].HeaderText = "Late/Mins"; dataGridView1.Columns[9].DataPropertyName = "Late_Mins";

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

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fromDate.Text.Equals(toDate.Text))
                {
                    DataTable dt_All = new DataTable();
                    string sql_All = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to";
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

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void fromDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

    }
}
