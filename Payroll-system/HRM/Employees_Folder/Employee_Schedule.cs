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
    public partial class Employee_Schedule : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private Bitmap bmp;

        public Employee_Schedule()
        {
            InitializeComponent();
            RetrieveData();
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 234;
            dataGridView1.Columns[1].Width = 255;
            dataGridView1.Columns[2].Width = 255;
            dataGridView1.Columns[3].Width = 255;
        }

        private void RetrieveData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Employee_Schedules", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 4;

                dataGridView1.Columns[0].HeaderText = "Employee ID"; dataGridView1.Columns[0].DataPropertyName = "Employee_ID";

                dataGridView1.Columns[1].HeaderText = "Surname"; dataGridView1.Columns[1].DataPropertyName = "Surname";

                dataGridView1.Columns[2].HeaderText = "Firstname"; dataGridView1.Columns[2].DataPropertyName = "Firstname";

                dataGridView1.Columns[3].HeaderText = "Schedule"; dataGridView1.Columns[3].DataPropertyName = "Schedule";

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

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            if (!dataGridView1.RowCount.Equals(0))
            {
              
                //Print all
               // int height = dataGridView1.Height;
                int width = dataGridView1.Width;

                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                //dataGridView1.Height = height;
                dataGridView1.Width = width;
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
              
            }
            else
            {
                MessageBox.Show("Empty data.", "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        StringFormat strFormat;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {    
            e.Graphics.DrawString("Employee Schedule", new Font("Verdana", 20, FontStyle.Bold), Brushes.Black, new Point(260, 10));

            e.Graphics.DrawImage(bmp, 0, 50);
        }
    }
}
