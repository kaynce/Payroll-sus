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
    public partial class Dashboard3 : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);
       
        private string employeesToday;
        //private string totalEmployees;

        private string db_admin_Position;

        private string addYear;

        private int employeeLogout = 0; //For employee today

        public Dashboard3(string db_admin_Position)
        {
            InitializeComponent();
            //totalemployeesTimer.Start();      
            AllMethod();
            this.db_admin_Position = db_admin_Position;

            /* Single start
            if (employeeTodayTimer.Enabled)
            {
                employeeTodayTimer.Stop();
                employeeTodayTimer.Start();
            }
            */
            employeeTodayTimer.Enabled = true;

        }

        private void AllMethod()
        {
            TotalEmployees();
            Percentage();
            EmployeesToday();
            RetrieveData();
            TotalPositions();
            TotalDepartment();
            TotalUsersAcc();
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 170;
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
                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Employee_Attendance", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                //dataGridView1.ForeColor = Color.White;

                dataGridView1.EnableHeadersVisualStyles = false;

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 6;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                dataGridView1.Columns[0].HeaderText = "Date"; dataGridView1.Columns[0].DataPropertyName = "Date";

                dataGridView1.Columns[1].HeaderText = "Employee ID"; dataGridView1.Columns[1].DataPropertyName = "Employee_ID";

                dataGridView1.Columns[2].HeaderText = "Surname"; dataGridView1.Columns[2].DataPropertyName = "Surname";

                dataGridView1.Columns[3].HeaderText = "Firstname"; dataGridView1.Columns[3].DataPropertyName = "Firstname";

                dataGridView1.Columns[4].HeaderText = "Time In"; dataGridView1.Columns[4].DataPropertyName = "Time_In";

                dataGridView1.Columns[5].HeaderText = "Time Out"; dataGridView1.Columns[5].DataPropertyName = "Time_Out";

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

        public void AddYear()
        {
             try
            {
            int ID;

            con.Close();

            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

            SqlCommand cmd = new SqlCommand("SELECT MAX([Years]) from P_Employee_Hours_Trend_Report_Years ", con);
            SqlDataReader dta = cmd.ExecuteReader();

            try
            {
                if (dta.Read())
                {
                    ID = int.Parse(dta[0].ToString()) + 1;
                    addYear = ID.ToString("0000");
                }
            }
            catch
            {
                addYear = ("2020");
            }
            }
             catch { }
            /*
            //Database Code Empty
            if(Convert.IsDBNull(dta))
            {
                empidTxt.Text = ("000001");
            }
            */
        }
 
        private void Percentage()
        {
           try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }

                int total = 0;
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM P_Employee_Attendance ", con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                //employeeTodayLb.Text = count.ToString();
                 employeesToday = count.ToString();

                con.Close();

                int rowIndex = 0;

                foreach (DataGridViewRow row in dataGridView1.Rows) //To know the row index
                {
                    if (row.Cells[4].Value.ToString() != string.Empty && row.Cells[5].Value.ToString() != string.Empty)
                    {
                        rowIndex++;
                      //  MessageBox.Show(count.ToString());
                        employeeLogout = rowIndex;
                        //total = count - rowIndex;
                    }
                }


                //MessageBox.Show(total.ToString() + "L");

                float empToday = float.Parse(employeeTodayLb.Text);
                float totalEmp = float.Parse(totalEmployeesLb.Text.Trim());

                float percent = (empToday / totalEmp) * 100;

                if (totalEmployeesLb.Text.Equals("0") && employeeTodayLb.Text.Equals("0"))
                {
                    percentLb.Text = "0.00%";
                }
                else
                {
                    percentLb.Text = String.Format("{0:0.00}", Double.Parse(percent.ToString())) + "%";
                    //NaN =  Not a Number. showing only Slang/Internet. 
                    //MessageBox.Show(percent.ToString());
                }
                rowIndex = 0;
            }
            catch { }
         }

        private void EmployeesToday()
        {
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM P_Employee_Attendance ", con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                int total = count - employeeLogout;

                if (!total.ToString().Contains("-"))
                {
                    employeeTodayLb.Text = total.ToString();
                    //employeesToday = count.ToString();
                }
                else
                {
                    employeeTodayLb.Text = "0";
                }
                con.Close();
            }
            catch {}
        }

        private void TotalEmployees()
        {
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM P_Employee_List ", con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                totalEmployeesLb.Text = count.ToString();
                //totalEmployees = count.ToString();

                con.Close();
            }
            catch { }
        }

        private void TotalPositions()
        {
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM P_Positions ", con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                totalPositionsLb.Text = count.ToString();

                con.Close();
            }
            catch { }
        }

        private void TotalDepartment()
        {
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM P_Department ", con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                totalDepartmentLb.Text = count.ToString();

                con.Close();
            }
            catch { }
        }

        private void TotalUsersAcc()
        {
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM P_Admin_Users ", con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                totalUsersAccLb.Text = count.ToString();

                con.Close();
            }
            catch { }
        }

        private void employeeTodayTimer_Tick(object sender, EventArgs e)
        {
                AllMethod();
                //employeeTodayTimer.Enabled = false;
        }

        private void Dashboard3_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.employeeTodayTimer = new System.Windows.Forms.Timer(this.components);
            //employeeTodayTimer.Enabled = false;
           
        }
    }
}

