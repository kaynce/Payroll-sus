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
    public partial class Add_Employee : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter dta = new SqlDataAdapter();

        //For uploading file
        private string file;

        private string checkMark = "\u2714";

        public Add_Employee()
        {
            InitializeComponent();
            RetrieveData();
            dateHiredTxt.Text = DateTime.Now.ToString("MM/dd/yyyy");
            surnameTxt.Select();
        }

        public void maxNum()
        {
            /*
            //string year_Date = dateHiredTxt.Text.Substring(6);

            int ID = 0;

            con.Close();

            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

           
            SqlCommand cmd = new SqlCommand("SELECT MAX([Employee_ID]) from P_Employee_Data_History ", con);
      
            SqlDataReader dta = cmd.ExecuteReader();
            try
            {
                if (dta.Read())
                {
                    ID = int.Parse(dta[0].ToString()) + 1;
                    empidTxt.Text = ID.ToString("0000000");
                }
            }
            catch 
            {
                empidTxt.Text = ID.ToString("0000001");
            }

            employeeUserTxt.Text = empidTxt.Text;
            employeePassTxt.Text = empidTxt.Text;
            employeePassTxt.isPassword = true;
            con.Close();
            */
            try
            {
                SqlCommand cmd_Max = new SqlCommand("SELECT MAX([Employee_ID]) from P_Employee_Data_History ", con);

                string year_Date = dateHiredTxt.Text.Substring(6);

                int ID = 0;
                string value = string.Empty;

                con.Open();


                value = cmd_Max.ExecuteScalar().ToString();

                if (string.IsNullOrEmpty(value))
                {
                    value = year_Date + "E" + ID.ToString("0000000");
                    empidTxt.Text = value;
                }

                value = value.Substring(5);

                ID = Int32.Parse(value) + 1;
                value = year_Date + "E" + ID.ToString("D7");

                empidTxt.Text = value;


                employeeUserTxt.Text = empidTxt.Text;
                employeePassTxt.Text = empidTxt.Text;
                employeePassTxt.isPassword = true;
                con.Close();


                /*
                //Database Code Empty
                if(Convert.IsDBNull(dta))
                {
                    empidTxt.Text = ("000001");
                }
                */
            }
            catch { }
        }

        private void RetrieveData()
        {
            try
            {
                con.Close();

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                cb_positionTxt.Items.Clear();
                //Putting Data to Position ComboBox from SQL "Position"            
                string sql2 = "SELECT Position FROM P_Positions";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                DataTable dt2 = new DataTable();

                SqlDataAdapter dta2 = new SqlDataAdapter(cmd2);
                dta2.Fill(dt2);
                // cmd.ExecuteNonQuery();
                foreach (DataRow dr2 in dt2.Rows)
                {
                    cb_positionTxt.Items.Add(dr2["Position"].ToString());
                    // cb_basicRateTxt.Items.Add(dr["Salary_Rate"].ToString());
                }
                con.Close();

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                cb_scheduleTxt.Items.Clear();
                //Putting Data to Schedule ComboBox from SQL "P_Add_Employee_Schedule" 
                string sql3 = "SELECT Time_In, Time_Out FROM P_Add_Employee_Schedule";
                SqlCommand cmd3 = new SqlCommand(sql3, con);
                DataTable dt3 = new DataTable();

                SqlDataAdapter dta3 = new SqlDataAdapter(cmd3);
                dta3.Fill(dt3);
                // cmd.ExecuteNonQuery();
                foreach (DataRow dr3 in dt3.Rows)
                {
                    cb_scheduleTxt.Items.Add(dr3["Time_In"].ToString() + " - " + dr3["Time_Out"].ToString());
                }
                con.Close();

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                cb_departmentTxt.Items.Clear();
                //Get the data of Department" 
                string sql_Department = "SELECT Department_Name FROM P_Department";
                SqlCommand cmd_Department = new SqlCommand(sql_Department, con);
                DataTable dt_Department = new DataTable();

                SqlDataAdapter da_Department = new SqlDataAdapter(cmd_Department);
                da_Department.Fill(dt_Department);
                // cmd.ExecuteNonQuery();
                foreach (DataRow dr_Department in dt_Department.Rows)
                {
                    cb_departmentTxt.Items.Add(dr_Department["Department_Name"].ToString());
                }
                con.Close();
            }
            catch { }
        }

        private void cleartxtBoxes()
        {
            empidTxt.Text = "";
            surnameTxt.Text = "";
            fnameTxt.Text = "";
            addressTxt.Text = "";
            coninfoTxt.Text = "";
            cb_genderTxt.SelectedIndex = -1;
            cb_positionTxt.SelectedIndex = -1;
            cb_basicrateTxt.SelectedIndex = -1;
            cb_OverTimeRateTxt.SelectedIndex = -1; // New
            cb_scheduleTxt.SelectedIndex = -1;
            cb_statusTxt.SelectedIndex = -1;
            cb_departmentTxt.SelectedIndex = -1;
            sssTxt.Text = "";
            hdmfTxt.Text = "";
            philHealthTxt.Text = "";

            pictureBox1.Visible = false;

            //Employee account
            employeeUserTxt.Text = "";
            employeePassTxt.Text = "";
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
        
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string emp_ID = empidTxt.Text.Trim();
                string dateHired = dateHiredTxt.Text.Trim();
                string surname = surnameTxt.Text.Trim();
                string firstname = fnameTxt.Text.Trim();
                string address = addressTxt.Text.Trim();
                string contactInfo = coninfoTxt.Text.Trim();
                string gender = cb_genderTxt.Text.Trim();
                string dateOfBirth = dateTimePicker1.Text.Trim();
                string position = cb_positionTxt.Text.Trim();
                string ratePerHour = cb_basicrateTxt.Text.Trim();
                string dailyRate = dailyRateTxt.Text.Trim(); //New
                string salary = salaryTxt.Text.Trim(); //New
                string overTimeRate = cb_OverTimeRateTxt.Text.Trim();
                string schedule = cb_scheduleTxt.Text.Trim();
                string status = cb_statusTxt.Text.Trim();
                string department = cb_departmentTxt.Text.Trim();
                string sss = sssTxt.Text.Trim();
                string hdmf = hdmfTxt.Text.Trim();
                string philHealth = philHealthTxt.Text.Trim();
                string photoLink = file;

                //Employee account
                string employee_Username = employeeUserTxt.Text.Trim();
                string employee_Password = employeePassTxt.Text.Trim();



                //new 
                //string picture = openFileDialog1.FileName;

                con.Close();

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                if (string.IsNullOrEmpty(emp_ID) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(firstname) ||
                    string.IsNullOrEmpty(address) || string.IsNullOrEmpty(contactInfo) || string.IsNullOrEmpty(gender) ||
                    string.IsNullOrEmpty(position) || string.IsNullOrEmpty(ratePerHour) || string.IsNullOrEmpty(dailyRate) ||
                    string.IsNullOrEmpty(salary) || string.IsNullOrEmpty(overTimeRate) || string.IsNullOrEmpty(schedule) ||
                    string.IsNullOrEmpty(file) || string.IsNullOrEmpty(employee_Username) || string.IsNullOrEmpty(employee_Password) ||
                    string.IsNullOrEmpty(department))
                {
                    MessageBox.Show("Please fill out all the fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (string.IsNullOrEmpty(sss) || string.IsNullOrEmpty(hdmf) || string.IsNullOrEmpty(philHealth))
                {
                    DialogResult confirm = MessageBox.Show("Some government ID are missing.\nAre you sure you want to save?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        con.Close();

                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd_Trend = new SqlCommand("INSERT INTO P_Employee_Hours_Trend_Report([Employee_ID], [Surname], [Firstname]) VALUES('" +
                                                             emp_ID + "','" + surname + "','" + firstname + "' )", con);
                        cmd_Trend.ExecuteNonQuery();

                        SqlCommand cmd_List = new SqlCommand("INSERT INTO P_Employee_List([Employee_ID], [Date_Hired], [Surname], [Firstname], [Address]," +
                                                            " [Contact_Info], [Gender], [Date_of_Birth], [Position], [Department], [Rate_per_Hour], [Daily_Rate], [Salary]," +
                                                            "[Over_Time_Rate], [Schedule], [Status], [SSS], [HDMF], [Phil_Health], [Photo_Link], [Username], [Password]) VALUES('" + emp_ID + "','" +
                                                            dateHired + "','" + surname + "','" + firstname + "','" + address + "', '" + contactInfo + "', '" + gender +
                                                            "', '" + dateOfBirth + "', '" + position + "', '" + department + "', '" + ratePerHour + "', '" + dailyRate + "', '" + salary +
                                                            "', '" + overTimeRate + "', '" + schedule + "', '" + status + "', '" + sss + "', '" + hdmf + "', '" +
                                                            philHealth + "', '" + photoLink + "', '" + employee_Username + "', '" + employee_Password + "')", con);
                        cmd_List.ExecuteNonQuery();

                        SqlCommand cmd_History = new SqlCommand("INSERT INTO P_Employee_Data_History([Employee_ID], [Date_Hired], [Surname], [Firstname], [Address]," +
                                                            " [Contact_Info], [Gender], [Date_of_Birth], [Position], [Department], [Rate_per_Hour], [Daily_Rate], [Salary]," +
                                                            "[Over_Time_Rate], [Schedule], [Status], [SSS], [HDMF], [Phil_Health], [Photo_Link], [Username], [Password]) VALUES('" + emp_ID + "','" +
                                                            dateHired + "','" + surname + "','" + firstname + "','" + address + "', '" + contactInfo + "', '" + gender +
                                                            "', '" + dateOfBirth + "', '" + position + "', '" + department + "', '" + ratePerHour + "', '" + dailyRate + "', '" + salary +
                                                            "', '" + overTimeRate + "', '" + schedule + "', '" + status + "', '" + sss + "', '" + hdmf + "', '" +
                                                            philHealth + "', '" + photoLink + "', '" + employee_Username + "', '" + employee_Password + "')", con);
                        cmd_History.ExecuteNonQuery();

                        //Insert into P_Employee_Schedules
                        SqlCommand cmd_Schedule = new SqlCommand("INSERT INTO P_Employee_Schedules([Employee_ID], [Surname], [Firstname], [Schedule])" +
                                                                 "VALUES('" + emp_ID + "','" + surname + "','" + firstname + "','" + schedule + "')", con);
                        cmd_Schedule.ExecuteNonQuery();

                        cleartxtBoxes();

                        MessageBox.Show(surname + " " + firstname + " has been added" + checkMark + ".", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                    }

                }
                else if (!string.IsNullOrEmpty(emp_ID))
                {
                    DialogResult confirm = MessageBox.Show("Are you sure you want to save?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm.Equals(DialogResult.Yes))
                    {
                        con.Close();

                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd_Trend = new SqlCommand("INSERT INTO P_Employee_Hours_Trend_Report([Employee_ID], [Surname], [Firstname]) VALUES('" +
                                                             emp_ID + "','" + surname + "','" + firstname + "' )", con);
                        cmd_Trend.ExecuteNonQuery();

                        SqlCommand cmd_List = new SqlCommand("INSERT INTO P_Employee_List([Employee_ID], [Date_Hired], [Surname], [Firstname], [Address]," +
                                                            " [Contact_Info], [Gender], [Date_of_Birth], [Position], [Department], [Rate_per_Hour], [Daily_Rate], [Salary]," +
                                                            "[Over_Time_Rate], [Schedule], [Status], [SSS], [HDMF], [Phil_Health], [Photo_Link], [Username], [Password]) VALUES('" + emp_ID + "','" +
                                                            dateHired + "','" + surname + "','" + firstname + "','" + address + "', '" + contactInfo + "', '" + gender +
                                                            "', '" + dateOfBirth + "', '" + position + "', '" + department + "', '" + ratePerHour + "', '" + dailyRate + "', '" + salary +
                                                            "', '" + overTimeRate + "', '" + schedule + "', '" + status + "', '" + sss + "', '" + hdmf + "', '" +
                                                            philHealth + "', '" + photoLink + "', '" + employee_Username + "', '" + employee_Password + "')", con);
                        cmd_List.ExecuteNonQuery();

                        SqlCommand cmd_History = new SqlCommand("INSERT INTO P_Employee_Data_History([Employee_ID], [Date_Hired], [Surname], [Firstname], [Address]," +
                                                            " [Contact_Info], [Gender], [Date_of_Birth], [Position], [Department], [Rate_per_Hour], [Daily_Rate], [Salary]," +
                                                            "[Over_Time_Rate], [Schedule], [Status], [SSS], [HDMF], [Phil_Health], [Photo_Link], [Username], [Password]) VALUES('" + emp_ID + "','" +
                                                            dateHired + "','" + surname + "','" + firstname + "','" + address + "', '" + contactInfo + "', '" + gender +
                                                            "', '" + dateOfBirth + "', '" + position + "', '" + department + "', '" + ratePerHour + "', '" + dailyRate + "', '" + salary +
                                                            "', '" + overTimeRate + "', '" + schedule + "', '" + status + "', '" + sss + "', '" + hdmf + "', '" +
                                                            philHealth + "', '" + photoLink + "', '" + employee_Username + "', '" + employee_Password + "')", con);
                        cmd_History.ExecuteNonQuery();

                        //Insert into P_Employee_Schedules
                        SqlCommand cmd_Schedule = new SqlCommand("INSERT INTO P_Employee_Schedules([Employee_ID], [Surname], [Firstname], [Schedule])" +
                                                                 "VALUES('" + emp_ID + "','" + surname + "','" + firstname + "','" + schedule + "')", con);
                        cmd_Schedule.ExecuteNonQuery();

                        cleartxtBoxes();
                        MessageBox.Show(surname + " " + firstname + " has been added" + checkMark + ".", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Employee ID No." + emp_ID + " is already taken.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Put double apostrope.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cb_positionTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cb_basicrateTxt.Items.Clear();
                cb_OverTimeRateTxt.Items.Clear();

                string sql = "SELECT Rate_per_Hour, Over_Time_Rate FROM P_Positions WHERE Position ='" + cb_positionTxt.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader dta;

                con.Close();

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                dta = cmd.ExecuteReader();

                while (dta.Read())
                {
                    cb_basicrateTxt.Items.Add(dta["Rate_per_Hour"].ToString());
                    cb_OverTimeRateTxt.Items.Add(dta["Over_Time_Rate"].ToString());
                }
                dailyRateTxt.Text = "";
                salaryTxt.Text = "";
                con.Close();
            }
            catch { }
        }

        private void uploadPicBtn_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();

                if (System.IO.File.Exists(openFileDialog1.FileName))
                {
                    pictureBox1.Visible = true;
                    file = openFileDialog1.FileName;
                    pictureBox1.Image = Image.FromFile(file);
                }
            }
            catch 
            {
                MessageBox.Show("This file is not supported.");

            }   
        }

        private void generateTxt_Click(object sender, EventArgs e)
        {
         
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            cleartxtBoxes();
        }

        private void dateTimer_Tick(object sender, EventArgs e)
        {
            dateHiredTxt.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cb_basicrateTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cb_basicrateTxt.Text))
            {
                double perHour = Double.Parse(cb_basicrateTxt.Text);
                dailyRateTxt.Text = String.Format("{0:0.00}", perHour * 8);

                double dailyRate = Double.Parse(dailyRateTxt.Text);
                salaryTxt.Text = String.Format("{0:0.00}", dailyRate * 26);
            }
        }

        private void emplistBtn_Click(object sender, EventArgs e)
        {
            maxNum();
        }

        private void show_HideBtn_Click(object sender, EventArgs e)
        { 
            if (employeePassTxt.isPassword == false)
            {
                show_HideBtn.Text = "Hide";
                employeePassTxt.isPassword = true;
            }
            else
            {
                show_HideBtn.Text = "Show";
                employeePassTxt.isPassword = false;
            }

        }
    }
}
