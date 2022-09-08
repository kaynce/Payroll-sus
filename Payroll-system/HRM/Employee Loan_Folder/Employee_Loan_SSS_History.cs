using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//New Import
using System.Data.SqlClient;
using System.Configuration;

namespace HRM
{
    public partial class Employee_Loan_SSS_History : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        public Employee_Loan_SSS_History()
        {
            InitializeComponent();
            RetrieveData();
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 149;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 150;
        }

        private void RetrieveData()
        {
            try
            {
                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Employee_Loan_SSS_History", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                //dataGridView1.ForeColor = Color.White;

                dataGridView1.EnableHeadersVisualStyles = false;

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 7;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                dataGridView1.Columns[0].HeaderText = "Date"; dataGridView1.Columns[0].DataPropertyName = "Date";

                dataGridView1.Columns[1].HeaderText = "Employee ID"; dataGridView1.Columns[1].DataPropertyName = "Employee_ID";

                dataGridView1.Columns[2].HeaderText = "SSS No."; dataGridView1.Columns[2].DataPropertyName = "SSS_No";

                dataGridView1.Columns[3].HeaderText = "Surname"; dataGridView1.Columns[3].DataPropertyName = "Surname";

                dataGridView1.Columns[4].HeaderText = "Firstname"; dataGridView1.Columns[4].DataPropertyName = "Firstname";

                dataGridView1.Columns[5].HeaderText = "Loan Amount"; dataGridView1.Columns[5].DataPropertyName = "Loan_Amount";

                //dataGridView1.Columns[8].HeaderText = "Balance"; dataGridView1.Columns[8].DataPropertyName = "Balance";


                dataGridView1.DataSource = dt;

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                //dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold); //Header Design


                autoSize();
            }
            catch { }
        }

        private void searchTxt_OnTextChange(object sender, EventArgs e)
        {
            try
            {
                string search = searchTxt.text.Trim();

                SqlDataAdapter dta = new SqlDataAdapter("SELECT * FROM P_Employee_Loan_SSS_History WHERE Surname LIKE '" + search + "%'", con);
                DataTable dt = new DataTable();
                dta.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch { }
        }
    }
}
