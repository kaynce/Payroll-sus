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
    public partial class Employee_Attendance_Report : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private Bitmap bmp;

        public Employee_Attendance_Report()
        {
            InitializeComponent();
            RetrieveData();
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

        private void RetrieveData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Employee_Attendance_Report", con);
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

                //Disable highlighting
                dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
                dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;

                autoSize();
            }
            catch { }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void paySlipBtn_Click(object sender, EventArgs e)
        {
            if (!dataGridView1.RowCount.Equals(0))
            {          
                //Print all
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                dataGridView1.Height = height;
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
                //printPreviewDialog1.Document = printDocument1;
                //printPreviewDialog1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Empty data.", "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (fromDate.Text != toDate.Text)
                {
                    con.Close();
                    con.Open();

                    /*
                    SqlCommand cmd = new SqlCommand("SELECT * FROM P_Worker_Details WHERE Date BETWEEN '" +
                    fromDate.Value.ToString() + "' AND '" + toDate.Value.ToString() + "'", con);
               
                    SqlDataAdapter dta = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dta.Fill(ds, "P_Worker_Details");
                    employeeDetailsView.DataSource = ds.Tables["P_Worker_Details"];
                    */

                    //These lines will filter date  of Casual Pay
                    DataTable dt = new DataTable();
                    string sql = "SELECT * FROM P_Employee_Attendance_Report WHERE Date Between @from and @to";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);

                    da.SelectCommand.Parameters.AddWithValue("@from", fromDate.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@to", toDate.Text.Trim());
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Please connect the date 'From' - 'To'", "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
