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
    public partial class Employee_Attendance : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        public Employee_Attendance()
        {
            InitializeComponent();
            retrieveData();
            PresentToday();
        }

        private void PresentToday()
        {
            try
            {
                    con.Close();
                    con.Open();
                    string date = DateTime.Now.ToString("MM/dd/yyyy");
                    //These lines will filter date today
                    DataTable dt = new DataTable();
                    string sql = "SELECT * FROM P_Employee_Attendance WHERE Date Between @from and @to";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);

                    da.SelectCommand.Parameters.AddWithValue("@from", date);
                    da.SelectCommand.Parameters.AddWithValue("@to", date);
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    con.Close();
            }
            catch { }
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 174;
            dataGridView1.Columns[1].Width = 165;
            dataGridView1.Columns[2].Width = 165;
            dataGridView1.Columns[3].Width = 165;
            dataGridView1.Columns[4].Width = 165;
            dataGridView1.Columns[5].Width = 165;  
        }

        private void retrieveData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Employee_Attendance", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 6;

                dataGridView1.Columns[0].HeaderText = "Date"; dataGridView1.Columns[0].DataPropertyName = "Date";

                dataGridView1.Columns[1].HeaderText = "Employee ID"; dataGridView1.Columns[1].DataPropertyName = "Employee_ID";

                dataGridView1.Columns[2].HeaderText = "Surname"; dataGridView1.Columns[2].DataPropertyName = "Surname";

                dataGridView1.Columns[3].HeaderText = "Firstname"; dataGridView1.Columns[3].DataPropertyName = "Firstname";

                dataGridView1.Columns[4].HeaderText = "Time In"; dataGridView1.Columns[4].DataPropertyName = "Time_In";

                dataGridView1.Columns[5].HeaderText = "Time Out"; dataGridView1.Columns[5].DataPropertyName = "Time_Out";

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
            searchTxt._TextBox.Clear();
            retrieveData();
            PresentToday();
        }

        private void exitBtn_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            try
            {
                string search = searchTxt.text.Trim();

                SqlDataAdapter dta = new SqlDataAdapter("SELECT * FROM P_Employee_Attendance WHERE Surname LIKE '" + search + "%'", con);
                DataTable dt = new DataTable();
                dta.Fill(dt);
                dataGridView1.DataSource = dt;
                //dataGridView1.Columns.Remove("ID");  
            }
            catch { }
        }
    }
}
