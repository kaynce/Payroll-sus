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
    public partial class Employee_Archived : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private string emp_ID;
        /*
        private string dateHired;
        private string surname;
        private string firstname;
        private string address;
        private string contactInfo;
        private string gender;
        private string dateOfBirth;
        private string position;
        private string ratePerHour;
        private string dailyRate; //New
        private string salary; //New
        private string overTimeRate;
        private string schedule;
        private string status;
        private string sss;
        private string hdmf;
        private string philHealth;
        private string photoLink;

        //Employee account //New
        private string employee_Username;
        private string employee_Password;
        */

        public Employee_Archived()
        {
            InitializeComponent();
            RetrieveData();
        }

        private void CleartTxtBoxes()
        {
            emp_ID = null;
            /*
            dateHired = null;
            surname = null;
            firstname = null;
            address = null;
            contactInfo = null;
            gender = null;
            dateOfBirth = null;
            position = null;
            ratePerHour = null;
            dailyRate = null;
            salary = null;
            overTimeRate = null; 
            schedule = null;
            status = null;
            sss = null;
            hdmf = null;
            philHealth = null;
            photoLink = null;
            //Employee account //New
            employee_Username = null;
            employee_Password = null;
            */
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 150;
            dataGridView1.Columns[7].Width = 150;
            dataGridView1.Columns[8].Width = 150;
            dataGridView1.Columns[9].Width = 150;
            dataGridView1.Columns[10].Width = 150;
            dataGridView1.Columns[11].Width = 150;
            dataGridView1.Columns[12].Width = 150;
            dataGridView1.Columns[13].Width = 150;
            dataGridView1.Columns[14].Width = 150;
            dataGridView1.Columns[15].Width = 150;
            dataGridView1.Columns[16].Width = 150;
            dataGridView1.Columns[17].Width = 150;
            dataGridView1.Columns[18].Width = 150;
            dataGridView1.Columns[19].Width = 150;
            dataGridView1.Columns[20].Width = 150;
            dataGridView1.Columns[21].Width = 150;
            //dataGridView1.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridView1.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void RetrieveData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Employee_Archived", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;

                dataGridView1.ColumnCount = 22;

                dataGridView1.Columns[0].HeaderText = "Employee ID"; dataGridView1.Columns[0].DataPropertyName = "Employee_ID";

                dataGridView1.Columns[1].HeaderText = "Date Hired"; dataGridView1.Columns[1].DataPropertyName = "Date_Hired";

                dataGridView1.Columns[2].HeaderText = "Surname"; dataGridView1.Columns[2].DataPropertyName = "Surname";

                dataGridView1.Columns[3].HeaderText = "Firstname"; dataGridView1.Columns[3].DataPropertyName = "Firstname";

                dataGridView1.Columns[4].HeaderText = "Address"; dataGridView1.Columns[4].DataPropertyName = "Address";

                dataGridView1.Columns[5].HeaderText = "Contact Info"; dataGridView1.Columns[5].DataPropertyName = "Contact_Info";

                dataGridView1.Columns[6].HeaderText = "Gender"; dataGridView1.Columns[6].DataPropertyName = "Gender";

                dataGridView1.Columns[7].HeaderText = "Date of Birth"; dataGridView1.Columns[7].DataPropertyName = "Date_of_Birth";

                dataGridView1.Columns[8].HeaderText = "Position"; dataGridView1.Columns[8].DataPropertyName = "Position";

                dataGridView1.Columns[9].HeaderText = "Rate per Hour"; dataGridView1.Columns[9].DataPropertyName = "Rate_per_Hour";

                dataGridView1.Columns[10].HeaderText = "Daily Rate"; dataGridView1.Columns[10].DataPropertyName = "Daily_Rate";

                dataGridView1.Columns[11].HeaderText = "Salary"; dataGridView1.Columns[11].DataPropertyName = "Salary";

                dataGridView1.Columns[12].HeaderText = "Over Time Rate"; dataGridView1.Columns[12].DataPropertyName = "Over_Time_Rate";

                dataGridView1.Columns[13].HeaderText = "Schedule"; dataGridView1.Columns[13].DataPropertyName = "Schedule";

                dataGridView1.Columns[14].HeaderText = "Status"; dataGridView1.Columns[14].DataPropertyName = "Status";

                dataGridView1.Columns[15].HeaderText = "Department"; dataGridView1.Columns[15].DataPropertyName = "Department";

                dataGridView1.Columns[16].HeaderText = "SSS"; dataGridView1.Columns[16].DataPropertyName = "SSS";

                dataGridView1.Columns[17].HeaderText = "HDMF"; dataGridView1.Columns[17].DataPropertyName = "HDMF";

                dataGridView1.Columns[18].HeaderText = "Phil. Health"; dataGridView1.Columns[18].DataPropertyName = "Phil_Health";

                dataGridView1.Columns[19].HeaderText = "Photo Link"; dataGridView1.Columns[19].DataPropertyName = "Photo_Link";

                dataGridView1.Columns[20].HeaderText = "Username"; dataGridView1.Columns[20].DataPropertyName = "Username";

                dataGridView1.Columns[21].HeaderText = "Password"; dataGridView1.Columns[21].DataPropertyName = "Password";

                dataGridView1.DataSource = dt;

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                //Add second Archive button
                DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn2);
                btn2.HeaderText = "Action";
                btn2.Name = "Retrieve";
                btn2.Text = "Retrieve";
                btn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btn2.FlatStyle = FlatStyle.Flat;
                //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                btn2.DefaultCellStyle.ForeColor = Color.ForestGreen;
                //btn.CellTemplate.Style.BackColor = Color.Green;
                btn2.UseColumnTextForButtonValue = true;
                btn2.DisplayIndex = 0; //First line

                autoSize();
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void retrieveBtn_Click(object sender, EventArgs e)
        {
          
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 22)
                {
                    emp_ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    //string emp_ID;
                    string dateHired = string.Empty;
                    string surname = string.Empty;
                    string firstname = string.Empty;
                    string address = string.Empty;
                    string contactInfo = string.Empty;
                    string gender = string.Empty;
                    string dateOfBirth = string.Empty;
                    string position = string.Empty;
                    string ratePerHour = string.Empty;
                    string dailyRate = string.Empty;
                    string salary = string.Empty;
                    string overTimeRate = string.Empty;
                    string schedule = string.Empty;
                    string status = string.Empty;
                    string department = string.Empty;
                    string sss = string.Empty;
                    string hdmf = string.Empty;
                    string philHealth = string.Empty;
                    string photoLink = string.Empty;

                    string employee_Username = string.Empty;
                    string employee_Password = string.Empty;


                    if (!string.IsNullOrEmpty(emp_ID))
                    {
                        DialogResult confirm = MessageBox.Show("Are you sure you want to retrieve '" + emp_ID + "' data?", "Retrieve", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirm.Equals(DialogResult.Yes))
                        {
                            SqlCommand cmd = new SqlCommand("SELECT [Employee_ID], [Date_Hired], [Surname], [Firstname], [Address], [Contact_Info]," +
                            "[Gender], [Date_of_Birth], [Position], [Rate_per_Hour], [Daily_Rate], [Salary], [Over_Time_Rate], [Schedule], [Status]," +
                            "[Department], [SSS], [HDMF], [Phil_Health], [Photo_Link], [Username], [Password] FROM P_Employee_Archived Where Employee_ID = @ID", con);

                            cmd.Parameters.AddWithValue("@ID", emp_ID);

                            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                            SqlDataReader dta = cmd.ExecuteReader();

                            while (dta.Read())
                            {
                                emp_ID = dta.GetValue(0).ToString();
                                dateHired = dta.GetValue(1).ToString();
                                surname = dta.GetValue(2).ToString();
                                firstname = dta.GetValue(3).ToString();
                                address = dta.GetValue(4).ToString();
                                contactInfo = dta.GetValue(5).ToString();
                                gender = dta.GetValue(6).ToString();
                                dateOfBirth = dta.GetValue(7).ToString();
                                position = dta.GetValue(8).ToString();
                                ratePerHour = dta.GetValue(9).ToString();
                                dailyRate = dta.GetValue(10).ToString();
                                salary = dta.GetValue(11).ToString();
                                overTimeRate = dta.GetValue(12).ToString();
                                schedule = dta.GetValue(13).ToString();
                                status = dta.GetValue(14).ToString();
                                department = dta.GetValue(15).ToString();
                                sss = dta.GetValue(16).ToString();
                                hdmf = dta.GetValue(17).ToString();
                                philHealth = dta.GetValue(18).ToString();
                                //Retrieve Picture
                                photoLink = dta.GetValue(19).ToString();

                                employee_Username = dta.GetValue(20).ToString();
                                employee_Password = dta.GetValue(21).ToString();
                            }
                            con.Close();

                            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                            SqlCommand cmd_List = new SqlCommand("INSERT INTO P_Employee_List([Employee_ID], [Date_Hired], [Surname], [Firstname], [Address]," +
                                                                    " [Contact_Info], [Gender], [Date_of_Birth], [Position], [Department], [Rate_per_Hour], [Daily_Rate], [Salary]," +
                                                                    "[Over_Time_Rate], [Schedule], [Status], [SSS], [HDMF], [Phil_Health], [Photo_Link], [Username], [Password]) VALUES('" + emp_ID + "','" +
                                                                    dateHired + "','" + surname + "','" + firstname + "','" + address + "', '" + contactInfo + "', '" + gender +
                                                                    "', '" + dateOfBirth + "', '" + position + "', '" + department + "', '" + ratePerHour + "', '" + dailyRate + "', '" + salary +
                                                                    "', '" + overTimeRate + "', '" + schedule + "', '" + status + "', '" + sss + "', '" + hdmf + "', '" +
                                                                    philHealth + "', '" + photoLink + "', '" + employee_Username + "', '" + employee_Password + "')", con);

                            cmd_List.ExecuteNonQuery();

                            SqlCommand cmd_Delete = new SqlCommand("DELETE FROM P_Employee_Archived WHERE Employee_ID = " + emp_ID, con);

                            cmd_Delete.ExecuteNonQuery();


                            SqlCommand cmd_Sched = new SqlCommand("INSERT INTO P_Employee_Schedules([Employee_ID], [Surname], [Firstname], [Schedule])" +
                                                                   " VALUES('" + emp_ID + "','" + surname + "','" + firstname + "','" + schedule + "')", con);

                            cmd_Sched.ExecuteNonQuery();

                            con.Close();
                            CleartTxtBoxes();
                            RetrieveData();
                            MessageBox.Show("Successfully retrieved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose data!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
           }
           catch { }
        }

        private void searchTxt_OnTextChange(object sender, EventArgs e)
        {
            try
            {
                string search = searchTxt.text.Trim();

                SqlDataAdapter dta = new SqlDataAdapter("SELECT * FROM P_Employee_Archived WHERE Surname LIKE '" + search + "%'", con);
                DataTable dt = new DataTable();
                dta.Fill(dt);
                dataGridView1.DataSource = dt;
                //dataGridView1.Columns.Remove("ID"); 
            }
            catch { }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 21 && e.Value != null )
            {
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }
    }
}
