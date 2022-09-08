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
    public partial class Salary_Report : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter dta = new SqlDataAdapter();

        private DataTable dt = new DataTable();

        //Business Name
        string db_business_ID;
        string db_Business_Name;
        string db_Address;
        string db_Contact_Number;

        public Salary_Report()
        {
            InitializeComponent();
            RetrieveData();
        }

        private void RetrieveBusinessName()
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

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 170;
            dataGridView1.Columns[6].Width = 150;
            dataGridView1.Columns[7].Width = 150;
            dataGridView1.Columns[8].Width = 150;
            dataGridView1.Columns[9].Width = 150;
            dataGridView1.Columns[10].Width = 150;
            dataGridView1.Columns[11].Width = 150;
            dataGridView1.Columns[12].Width = 150;
            dataGridView1.Columns[13].Width = 150;
            dataGridView1.Columns[13].Width = 150;
            dataGridView1.Columns[14].Width = 150;
            dataGridView1.Columns[15].Width = 150;
            dataGridView1.Columns[16].Width = 150;
            dataGridView1.Columns[17].Width = 150;
            dataGridView1.Columns[18].Width = 150;
            dataGridView1.Columns[19].Width = 150;
            dataGridView1.Columns[20].Width = 150;
            dataGridView1.Columns[21].Width = 150;
            dataGridView1.Columns[22].Width = 150;
            dataGridView1.Columns[23].Width = 150;
            dataGridView1.Columns[24].Width = 150;
            dataGridView1.Columns[25].Width = 150;
            dataGridView1.Columns[26].Width = 150;
            dataGridView1.Columns[27].Width = 150;
            dataGridView1.Columns[28].Width = 150;
        }

        private void RetrieveData()
        {
            try
            {
                RetrieveBusinessName(); //Retrieve Data of Business

                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Main_Manual_Currency_History", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 29;

                dataGridView1.Columns[0].HeaderText = "Date"; dataGridView1.Columns[0].DataPropertyName = "Date";
                dataGridView1.Columns[1].HeaderText = "Employee ID"; dataGridView1.Columns[1].DataPropertyName = "Employee_ID";
                dataGridView1.Columns[2].HeaderText = "Surname"; dataGridView1.Columns[2].DataPropertyName = "Surname";
                dataGridView1.Columns[3].HeaderText = "Firstname"; dataGridView1.Columns[3].DataPropertyName = "Firstname";
                dataGridView1.Columns[4].HeaderText = "Position"; dataGridView1.Columns[4].DataPropertyName = "Position";
                dataGridView1.Columns[5].HeaderText = "Rate per Hour"; dataGridView1.Columns[5].DataPropertyName = "Rate_per_Hour";
                dataGridView1.Columns[6].HeaderText = "Over Time Rate"; dataGridView1.Columns[6].DataPropertyName = "Over_Time_Rate";

                dataGridView1.Columns[7].HeaderText = "No. of Days Worked"; dataGridView1.Columns[7].DataPropertyName = "Present_Days";

                dataGridView1.Columns[8].HeaderText = "Worked Hours"; dataGridView1.Columns[8].DataPropertyName = "Working_Hours";
                dataGridView1.Columns[9].HeaderText = "Over Time Hours"; dataGridView1.Columns[9].DataPropertyName = "Over_Time_Hours";

                dataGridView1.Columns[10].HeaderText = "Reg. Holiday"; dataGridView1.Columns[10].DataPropertyName = "Reg_Holiday";
                dataGridView1.Columns[11].HeaderText = "Reg. Holiday OT"; dataGridView1.Columns[11].DataPropertyName = "Reg_Holiday_OT";
                dataGridView1.Columns[12].HeaderText = "Spec. Holiday"; dataGridView1.Columns[12].DataPropertyName = "spe_Holiday";
                dataGridView1.Columns[13].HeaderText = "Spec. Holiday OT"; dataGridView1.Columns[13].DataPropertyName = "spe_Holiday_OT";

                dataGridView1.Columns[14].HeaderText = "Vacation Leave"; dataGridView1.Columns[14].DataPropertyName = "Vacation_Leave";
                dataGridView1.Columns[15].HeaderText = "Sick Leave"; dataGridView1.Columns[15].DataPropertyName = "Sick_Leave";

                dataGridView1.Columns[16].HeaderText = "Gross Pay"; dataGridView1.Columns[16].DataPropertyName = "Gross_Pay";
                dataGridView1.Columns[17].HeaderText = "Late(Hour/s)"; dataGridView1.Columns[17].DataPropertyName = "Late_per_Hour";
                dataGridView1.Columns[18].HeaderText = "Late(Min/s)"; dataGridView1.Columns[18].DataPropertyName = "Late_per_Min";
                dataGridView1.Columns[19].HeaderText = "BIR"; dataGridView1.Columns[19].DataPropertyName = "BIR_Tax";
                dataGridView1.Columns[20].HeaderText = "HDMF"; dataGridView1.Columns[20].DataPropertyName = "HDMF";

                dataGridView1.Columns[21].HeaderText = "SSS"; dataGridView1.Columns[21].DataPropertyName = "SSS";
                dataGridView1.Columns[22].HeaderText = "Phil. Health"; dataGridView1.Columns[22].DataPropertyName = "Phil_Health";
                dataGridView1.Columns[23].HeaderText = "Other Deduction"; dataGridView1.Columns[23].DataPropertyName = "Other_Deductions";

                dataGridView1.Columns[24].HeaderText = "Cash Advance Loan"; dataGridView1.Columns[24].DataPropertyName = "Cash_Advance_Loan";
                dataGridView1.Columns[25].HeaderText = "HDMF Loan"; dataGridView1.Columns[25].DataPropertyName = "HDMF_Loan";
                dataGridView1.Columns[26].HeaderText = "SSS Loan"; dataGridView1.Columns[26].DataPropertyName = "SSS_Loan";

                dataGridView1.Columns[27].HeaderText = "Total Deduction"; dataGridView1.Columns[27].DataPropertyName = "Total_Deduction";
                dataGridView1.Columns[28].HeaderText = "Net Pay"; dataGridView1.Columns[28].DataPropertyName = "Net_Pay";

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

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Salary_Report_Load(object sender, EventArgs e)
        {

        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            searchTxt._TextBox.Clear();
            RetrieveData();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /*
            //Logo: Opacity blurd: 
            //size logo: 768 x 768
            Bitmap bmp = Properties.Resources.PayrollSystem_Logo;
            Image newImage = bmp;
            e.Graphics.DrawImage(newImage, 0, 0, newImage.Width, newImage.Height);

            */

            //Need to fix strucuture in pay slip
            string date = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            string surname = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            string fname = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

            string position = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            string rate_Per_Hour = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

            string over_Time_Rate = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            string presemtDays = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            string working_Hours = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            string over_Time_Hours = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();

            string reg_Holiday = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
            string reg_Holiday_OT = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();
            string spec_Holiday = dataGridView1.SelectedRows[0].Cells[12].Value.ToString();
            string spec_Holiday_OT = dataGridView1.SelectedRows[0].Cells[13].Value.ToString();

            //Employee Leave
            string vacationLeave = dataGridView1.SelectedRows[0].Cells[14].Value.ToString();
            string sickLeave = dataGridView1.SelectedRows[0].Cells[15].Value.ToString();

            string gross_Pay = dataGridView1.SelectedRows[0].Cells[16].Value.ToString();

            string late_per_Hour = dataGridView1.SelectedRows[0].Cells[17].Value.ToString();
            string late_per_Mins = dataGridView1.SelectedRows[0].Cells[18].Value.ToString();

            string BIR_Tax = dataGridView1.SelectedRows[0].Cells[19].Value.ToString();
            string HDMF = dataGridView1.SelectedRows[0].Cells[20].Value.ToString();
            string SSS = dataGridView1.SelectedRows[0].Cells[21].Value.ToString();
            string Phil_Health = dataGridView1.SelectedRows[0].Cells[22].Value.ToString();

            string Other_Deductions = dataGridView1.SelectedRows[0].Cells[23].Value.ToString();

            //Loans
            string cashAdvance_Loan = dataGridView1.SelectedRows[0].Cells[24].Value.ToString();
            string hdmf_Loan = dataGridView1.SelectedRows[0].Cells[25].Value.ToString();
            string sss_Loan = dataGridView1.SelectedRows[0].Cells[26].Value.ToString();

            string Total_Deduction = dataGridView1.SelectedRows[0].Cells[27].Value.ToString();
            string netPay = dataGridView1.SelectedRows[0].Cells[28].Value.ToString();

            string dash = "-------------------------------------------------------------------------------------------------------------------";

            e.Graphics.DrawString(db_Business_Name, new System.Drawing.Font("Segoe UI", 26, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 5));
            e.Graphics.DrawString(db_Address, new System.Drawing.Font("Segoe UI", 17, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 50));
            e.Graphics.DrawString(db_Contact_Number, new System.Drawing.Font("Segoe UI", 17, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 80));

            e.Graphics.DrawString("Date: " + date, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(650, 100));
            e.Graphics.DrawString(dash, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 140));

            e.Graphics.DrawString("Lastname\t\t\t\t: " + surname, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 160));
            e.Graphics.DrawString("Firstname\t\t\t\t: " + fname, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 180));

            e.Graphics.DrawString(dash, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 200));

            e.Graphics.DrawString("Position\t\t\t\t: " + position, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 220));
            e.Graphics.DrawString("Rate per Hour\t\t\t\t: " + rate_Per_Hour, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 240));
            e.Graphics.DrawString("Over Time Rate\t\t\t\t: " + over_Time_Rate, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 260));

            e.Graphics.DrawString("No. of Days Worked\t\t\t: " + presemtDays, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 280));
            e.Graphics.DrawString("Worked Hours\t\t\t\t: " + working_Hours, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 300));
            e.Graphics.DrawString("Over Time Hours\t\t\t: " + over_Time_Hours, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 320));

            e.Graphics.DrawString("Reg. Holiday\t\t\t\t: " + reg_Holiday, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 340));
            e.Graphics.DrawString("Reg. Holiday OT\t\t\t\t: " + reg_Holiday_OT, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 360));
            e.Graphics.DrawString("Spec. Holiday\t\t\t\t: " + spec_Holiday, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 380));
            e.Graphics.DrawString("Spec. Holiday OT\t\t\t: " + spec_Holiday_OT, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 400));

            //Employee Leave
            e.Graphics.DrawString("V.L\t\t\t\t\t: " + vacationLeave, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 420));
            e.Graphics.DrawString("S.L\t\t\t\t\t: " + sickLeave, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 440));

            e.Graphics.DrawString(dash, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 460));

            e.Graphics.DrawString("Gross Pay\t\t\t\t: " + gross_Pay, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 480));

            e.Graphics.DrawString(dash, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 500));

            e.Graphics.DrawString("Deductions", new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 520));

            e.Graphics.DrawString("Late(Hour/s)\t\t\t\t: " + late_per_Hour, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 540));
            e.Graphics.DrawString("Late(Min/s)\t\t\t\t: " + late_per_Hour, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 560));

            e.Graphics.DrawString("BIR Tax\t\t\t\t\t: " + BIR_Tax, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 580));
            e.Graphics.DrawString("HDMF\t\t\t\t\t: " + HDMF, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 600));
            e.Graphics.DrawString("SSS\t\t\t\t\t: " + SSS, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 620));
            e.Graphics.DrawString("Phil. Health\t\t\t\t: " + Phil_Health, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 640));
            e.Graphics.DrawString("Other Deductions\t\t\t: " + Other_Deductions, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 660));

            //Loans
            e.Graphics.DrawString("Cash Advance Loan\t\t\t: " + Other_Deductions, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 680));
            e.Graphics.DrawString("HDMF Loan\t\t\t\t: " + Other_Deductions, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 700));
            e.Graphics.DrawString("SSS Loan\t\t\t\t: " + Other_Deductions, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 720));

            e.Graphics.DrawString(dash, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 740));

            e.Graphics.DrawString("Total Deduction\t\t\t\t: " + Total_Deduction, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 760));

            e.Graphics.DrawString(dash, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 780));

            //e.Graphics.DrawString("BIR\t\t\t\t\t: " + bir, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 520));
            e.Graphics.DrawString("Net Pay\t\t\t\t\t: " + netPay, new System.Drawing.Font("Arial", 15, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(20, 800));  
        }

        private void paySlipBtn_Click(object sender, EventArgs e)
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

        private void searchTxt_OnTextChange(object sender, EventArgs e)
        {
            try
            {
                string search = searchTxt.text.Trim();

                SqlDataAdapter dta = new SqlDataAdapter("SELECT * FROM P_Main_Manual_Currency_History WHERE Surname LIKE '" + search + "%'", con);
                DataTable dt = new DataTable();
                dta.Fill(dt);
                dataGridView1.DataSource = dt;
                //dataGridView1.Columns.Remove("ID");  
            }
            catch { }
        }

        private void paySlipBtn_Click_1(object sender, EventArgs e)
        {
            if (!dataGridView1.RowCount.Equals(0))
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Empty data.", "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
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

        private void refreshBtn_Click_1(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 29)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
        }
    }
}
