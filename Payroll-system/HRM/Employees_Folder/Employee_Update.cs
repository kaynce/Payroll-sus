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
    public partial class Employee_Update : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private string emp_ID = "";
        private string file;

        public Employee_Update(string emp_ID)
        {
            InitializeComponent();
            empidTxt.Text = emp_ID;
            emp_ID = empidTxt.Text;
            refreshData();
            surnameTxt.Select();
        }

        private void refreshData()
        {
            try
            {
                string gender = string.Empty;
                string dateTimePicker = string.Empty;
                string position = string.Empty;
                string basciRate = string.Empty;
                string dailyRate = string.Empty;
                string salary = string.Empty;
                string overTime = string.Empty;
                string schedule = string.Empty;
                string status = string.Empty;
                string department = string.Empty;

                string emp_ID = empidTxt.Text;
                SqlCommand cmd = new SqlCommand("SELECT [Employee_ID], [Date_Hired], [Surname], [Firstname], [Address], [Contact_Info]," +
                "[Gender], [Date_of_Birth], [Position], [Rate_per_Hour], [Daily_Rate], [Salary], [Over_Time_Rate], [Schedule], [Status], [Department], " +
                "[SSS], [HDMF], [Phil_Health], [Photo_Link] FROM P_Employee_List Where Employee_ID = @ID", con);

                cmd.Parameters.AddWithValue("@ID", emp_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dta = cmd.ExecuteReader();

                while (dta.Read())
                {
                    empidTxt.Text = dta.GetValue(0).ToString();
                    emp_ID = dta.GetValue(0).ToString();
                    dateHiredTxt.Text = dta.GetValue(1).ToString();
                    surnameTxt.Text = dta.GetValue(2).ToString();
                    fnameTxt.Text = dta.GetValue(3).ToString();
                    addressTxt.Text = dta.GetValue(4).ToString();
                    coninfoTxt.Text = dta.GetValue(5).ToString();

                    cb_genderTxt.Text = dta.GetValue(6).ToString();
                    gender = dta.GetValue(6).ToString();

                    dateTimePicker1.Text = dta.GetValue(7).ToString();
                    dateTimePicker = dta.GetValue(7).ToString();

                    cb_positionTxt.Text = dta.GetValue(8).ToString();
                    position = dta.GetValue(8).ToString();

                    cb_basicrateTxt.Text = dta.GetValue(9).ToString();
                    basciRate = dta.GetValue(9).ToString();

                    dailyRateTxt.Text = dta.GetValue(10).ToString();
                    dailyRate = dta.GetValue(10).ToString();

                    salaryTxt.Text = dta.GetValue(11).ToString();
                    salary = dta.GetValue(11).ToString();

                    cb_OverTimeRateTxt.Text = dta.GetValue(12).ToString();
                    overTime = dta.GetValue(12).ToString();

                    cb_scheduleTxt.Text = dta.GetValue(13).ToString();
                    schedule = dta.GetValue(13).ToString();

                    cb_statusTxt.Text = dta.GetValue(14).ToString();
                    status = dta.GetValue(14).ToString();

                    cb_departmentTxt.Text = dta.GetValue(15).ToString();
                    department = dta.GetValue(15).ToString();

                    sssTxt.Text = dta.GetValue(16).ToString();

                    hdmfTxt.Text = dta.GetValue(17).ToString();

                    philHealthTxt.Text = dta.GetValue(18).ToString();
                    //Retrieve Picture
                    file = dta.GetValue(19).ToString();
                    if (System.IO.File.Exists(file))
                    {
                        pictureBox1.Image = Image.FromFile(file);
                    }
                }

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

                cb_genderTxt.Text = gender;
                dateTimePicker1.Text = dateTimePicker;
                cb_positionTxt.Text = position;

                cb_basicrateTxt.Text = basciRate;

                dailyRateTxt.Text = dailyRate;
                salaryTxt.Text = salary;

                cb_OverTimeRateTxt.Text = overTime;

                cb_scheduleTxt.Text = schedule;
                cb_statusTxt.Text = status;

                cb_departmentTxt.Text = department;

                con.Close();
            }
            catch { }
        }

        private void cleartxtBoxes()
        {
            empidTxt.Text = ""; ;
            surnameTxt.Text = "";
            fnameTxt.Text = "";;
            addressTxt.Text = "";
            coninfoTxt.Text = "";
            cb_genderTxt.SelectedIndex = -1;
            dateTimePicker1.Text = "";
            cb_positionTxt.Text = "";
            cb_basicrateTxt.Text = "";
            cb_OverTimeRateTxt.Text = "";
            cb_scheduleTxt.Text = "";
            cb_departmentTxt.Text = "";
            cb_statusTxt.Text = "";

            sssTxt.Text = "";
            hdmfTxt.Text = "";
            philHealthTxt.Text = "";
            pictureBox1.Visible = false;	              
        }     

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Employee_Update_Load(object sender, EventArgs e)
        {

        }

        private void uploadPicBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (System.IO.File.Exists(openFileDialog1.FileName))
            {
                file = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(file);
            }
        }

        private void cb_positionTxt_SelectedIndexChanged_1(object sender, EventArgs e)
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

                dailyRateTxt.Text = string.Empty;
                salaryTxt.Text = string.Empty;
                con.Close();
            }
            catch { }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string empID = empidTxt.Text.Trim();
                string dateHired = dateHiredTxt.Text.Trim();
                string surname = surnameTxt.Text.Trim();
                string fname = fnameTxt.Text.Trim();
                string address = addressTxt.Text.Trim();
                string contact = coninfoTxt.Text.Trim();
                string gender = cb_genderTxt.Text.Trim();
                string dateofBirth = dateTimePicker1.Text.Trim();
                string position = cb_positionTxt.Text.Trim();
                string basicRate = cb_basicrateTxt.Text.Trim();
                string dailyRate = dailyRateTxt.Text.Trim();
                string salary = salaryTxt.Text.Trim();
                string overTimeRate = cb_OverTimeRateTxt.Text.Trim();
                string schedule = cb_scheduleTxt.Text.Trim();
                string department = cb_departmentTxt.Text.Trim();
                string status = cb_statusTxt.Text.Trim();
                string sss = sssTxt.Text.Trim();
                string hdmf = hdmfTxt.Text.Trim();
                string philHealth = philHealthTxt.Text.Trim();

                //Picture
                string picture = file;

                DialogResult = MessageBox.Show("Are you sure you want to update?", "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DialogResult.Equals(DialogResult.Yes))
                {
                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    SqlCommand cmd = new SqlCommand("UPDATE P_Employee_List SET Employee_ID = '" + empID +
                                                    "', Date_Hired = '" + dateHired + "', Surname = '" + surname +
                                                    "', Firstname = '" + fname + "', Address = '" + address +
                                                    "', Contact_Info = '" + contact + "', Gender = '" + gender +
                                                    "', Date_of_Birth = '" + dateofBirth + "', Position = '" + position +
                                                    "', Rate_per_Hour = '" + basicRate + "', Daily_Rate = '" + dailyRate +
                                                    "', Salary = '" + salary + "', Over_Time_Rate = '" + overTimeRate +
                                                    "', Schedule = '" + schedule + "', Status = '" + status +
                                                    "', Department = '" + department + "', SSS = '" + sss +
                                                    "', HDMF = '" + hdmf + "', Phil_Health = '" + philHealth +
                                                    "', Photo_Link = '" + picture + "' WHERE Employee_ID = " + empID, con);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("UPDATE P_Employee_Data_History SET Employee_ID = '" + empID +
                                                    "', Date_Hired = '" + dateHired + "', Surname = '" + surname +
                                                    "', Firstname = '" + fname + "', Address = '" + address +
                                                    "', Contact_Info = '" + contact + "', Gender = '" + gender +
                                                    "', Date_of_Birth = '" + dateofBirth + "', Position = '" + position +
                                                    "', Rate_per_Hour = '" + basicRate + "', Daily_Rate = '" + dailyRate +
                                                    "', Salary = '" + salary + "', Over_Time_Rate = '" + overTimeRate +
                                                    "', Schedule = '" + schedule + "', Status = '" + status +
                                                    "', Department = '" + department + "', SSS = '" + sss +
                                                    "', HDMF = '" + hdmf + "', Phil_Health = '" + philHealth +
                                                    "', Photo_Link = '" + picture + "' WHERE Employee_ID = " + empID, con);

                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd_Update_Sched = new SqlCommand("UPDATE P_Employee_Schedules SET Surname = '" + surname +
                                                   "', Firstname = '" + fname + "', Schedule = '" + schedule +
                                                   "' WHERE Employee_ID = " + empID, con);

                    cmd_Update_Sched.ExecuteNonQuery();

                    SqlCommand cmd_Update_Worker_Details_Employee = new SqlCommand("UPDATE P_Worker_Details_Employee SET Surname = '" + surname +
                                                 "', Firstname = '" + fname + "' WHERE Employee_ID = " + empID, con);

                    cmd_Update_Worker_Details_Employee.ExecuteNonQuery();

                    SqlCommand cmd_Update_Worker_Details = new SqlCommand("UPDATE P_Worker_Details SET Surname = '" + surname +
                                                   "', Firstname = '" + fname + "' WHERE Employee_ID = " + empID, con);

                    cmd_Update_Worker_Details.ExecuteNonQuery();

                    //Update Employee name
                    SqlCommand cmd_Update_Attendance_Name = new SqlCommand("UPDATE P_Employee_Attendance SET Surname = '" + surname +
                                                  "', Firstname = '" + fname + "' WHERE Employee_ID = " + empID, con);

                    cmd_Update_Attendance_Name.ExecuteNonQuery();




                    MessageBox.Show("Record Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    con.Close();
                    cleartxtBoxes();
                    this.Dispose();
                }
                DialogResult = DialogResult.None;
            }
            catch { }
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

        private void closeBtn_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
