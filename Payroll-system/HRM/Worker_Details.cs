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
    public partial class Worker_Details : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        public Worker_Details()
        {
            InitializeComponent();
            RetrieveData();
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 150;
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Worker_Details", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 7;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                dataGridView1.Columns[0].HeaderText = "Employee ID"; dataGridView1.Columns[0].DataPropertyName = "Employee_ID";

                dataGridView1.Columns[1].HeaderText = "Date"; dataGridView1.Columns[1].DataPropertyName = "Date";

                dataGridView1.Columns[2].HeaderText = "Time In"; dataGridView1.Columns[2].DataPropertyName = "Time_In";

                dataGridView1.Columns[3].HeaderText = "Time Out"; dataGridView1.Columns[3].DataPropertyName = "Time_Out";

                dataGridView1.Columns[4].HeaderText = "Work Hours"; dataGridView1.Columns[4].DataPropertyName = "Work_Hours";

                dataGridView1.Columns[5].HeaderText = "Over Time Hours"; dataGridView1.Columns[5].DataPropertyName = "Over_Time_Hours";

                dataGridView1.Columns[6].HeaderText = "Late Hours"; dataGridView1.Columns[6].DataPropertyName = "Late_Hours";

                dataGridView1.DataSource = dt;

                //
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                autoSize();
            }
            catch { }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            RetrieveData();
        }


       
        private void calculateBtn_Click(object sender, EventArgs e)
        {
            //Total Work Hours
            int[] columnData = new int[dataGridView1.Rows.Count];
            columnData = (from DataGridViewRow row in dataGridView1.Rows
                          where row.Cells[2].FormattedValue.ToString() != string.Empty
                          select Convert.ToInt32(row.Cells[4].FormattedValue)).ToArray();

            details_HoursTxt.Text = columnData.Sum().ToString();

            //Total Over Time Hours
            int[] columnData2 = new int[dataGridView1.Rows.Count];
            columnData2 = (from DataGridViewRow row in dataGridView1.Rows
                          where row.Cells[2].FormattedValue.ToString() != string.Empty
                          select Convert.ToInt32(row.Cells[5].FormattedValue)).ToArray();

            detailsOverTimeTxt.Text = columnData2.Sum().ToString();

            //Total Late Hours
            int[] columnData3 = new int[dataGridView1.Rows.Count];
            columnData3 = (from DataGridViewRow row in dataGridView1.Rows
                          where row.Cells[2].FormattedValue.ToString() != string.Empty
                          select Convert.ToInt32(row.Cells[6].FormattedValue)).ToArray();

            detailsLateHoursTxt.Text = columnData3.Sum().ToString();


        }
    }
}
