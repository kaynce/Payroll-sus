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
    public partial class Thirteen_Month_Report : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        //Business Name
        string db_business_ID;
        string db_Business_Name;
        string db_Address;
        string db_Contact_Number;

        public Thirteen_Month_Report()
        {
            InitializeComponent();
            RetrieveData();
        }

        private void RetrieveBusinessName()
        {
            try
            {
                //Retrieve Business Name
                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlCommand cmd = new SqlCommand("SELECT ID, Business_Name, Address, Contact_Number FROM P_Business_Name", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    db_business_ID = dr["ID"].ToString();
                    db_Business_Name = dr["Business_Name"].ToString();
                    db_Address = dr["Address"].ToString();
                    db_Contact_Number = dr["Contact_Number"].ToString();

                    //db_business_Name = dr3["Business_Name"].ToString();
                }
                con.Close();
            }
            catch { }
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 110;
            dataGridView1.Columns[1].Width = 140;
            dataGridView1.Columns[2].Width = 140;
            dataGridView1.Columns[3].Width = 140;
            dataGridView1.Columns[4].Width = 140;
            dataGridView1.Columns[5].Width = 140;
            dataGridView1.Columns[6].Width = 162;
        }

        private void RetrieveData()
        {
            try
            {
                RetrieveBusinessName();

                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Main_Manual_Currency_Thirteenth_Month_Pay", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 7;

                dataGridView1.Columns[0].HeaderText = "Date"; dataGridView1.Columns[0].DataPropertyName = "Date";
                dataGridView1.Columns[1].HeaderText = "Employee ID"; dataGridView1.Columns[1].DataPropertyName = "Employee_ID";
                dataGridView1.Columns[2].HeaderText = "Surname"; dataGridView1.Columns[2].DataPropertyName = "Surname";
                dataGridView1.Columns[3].HeaderText = "Firstname"; dataGridView1.Columns[3].DataPropertyName = "Firstname";
                dataGridView1.Columns[4].HeaderText = "Position"; dataGridView1.Columns[4].DataPropertyName = "Position";
                dataGridView1.Columns[5].HeaderText = "Basic Pay"; dataGridView1.Columns[5].DataPropertyName = "Basic_Pay";
                dataGridView1.Columns[6].HeaderText = "13th Month Pay"; dataGridView1.Columns[6].DataPropertyName = "Salary";
                
                dataGridView1.DataSource = dt;

                //
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                autoSize();

                //Add first Update button 
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn);
                btn.HeaderText = "Action";
                btn.Name = "Print";
                btn.Text = "Print";
                btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btn.FlatStyle = FlatStyle.Flat;
                //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                btn.DefaultCellStyle.ForeColor = Color.ForestGreen;
                //btn.CellTemplate.Style.BackColor = Color.Green;
                btn.UseColumnTextForButtonValue = true;
                btn.DisplayIndex = 0; //First line

                con.Close();
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
                    string sql_All = "SELECT * FROM P_Main_Manual_Currency_Thirteenth_Month_Pay WHERE Date Between @from and @to";
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string date = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            string surname = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            string fname = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

            string position = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            string basicPay = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

            string salary = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();

            string dash = "-------------------------------------------------------------------------------------------------------------------";

            e.Graphics.DrawString(db_Business_Name, new System.Drawing.Font("Segoe UI", 26, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 5));
            e.Graphics.DrawString(db_Address, new System.Drawing.Font("Segoe UI", 17, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 50));
            e.Graphics.DrawString(db_Contact_Number, new System.Drawing.Font("Segoe UI", 17, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 80));

            e.Graphics.DrawString("Date: " + date, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(650, 100));
            e.Graphics.DrawString(dash, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 140));

            e.Graphics.DrawString("Surname\t\t\t\t: " + surname, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 160));
            e.Graphics.DrawString("Firstname\t\t\t\t: " + fname, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 180));

            e.Graphics.DrawString("Position\t\t\t\t:" + position, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 200));

            e.Graphics.DrawString("Basic Pay\t\t\t\t: " + basicPay, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 220));
            e.Graphics.DrawString("13th Month Pay\t\t\t\t: " + salary, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 240));
        }

        private void paySlipBtn_Click(object sender, EventArgs e)
        {
          
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void searchTxt_OnTextChange(object sender, EventArgs e)
        {
            try
            {
                string search = searchTxt.text.Trim();

                SqlDataAdapter dta = new SqlDataAdapter("SELECT * FROM P_Main_Manual_Currency_Thirteenth_Month_Pay WHERE Surname LIKE '" + search + "%'", con);
                DataTable dt = new DataTable();
                dta.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            if (!dataGridView1.RowCount.Equals(0))
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Empty data.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
