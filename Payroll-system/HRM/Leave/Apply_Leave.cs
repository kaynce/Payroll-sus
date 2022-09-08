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
    public partial class Apply_Leave : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        string category_Leave;

        private List<string> data_Employee_ID = new List<string>();  //For Employee ID

        public Apply_Leave()
        {
            InitializeComponent();
            RetrieveData();

            fromDate.Text = fromDate.Text;

            if (Int32.Parse(no_VacationLeaveTxt.Text) == 0 && Int32.Parse(no_SickLeaveTxt.Text) == 0)
            {
                vacation_CheckBox.Checked = false;
                sickLeave_CheckBox.Checked = false;
                noPay_CheckBox.Checked = true;
            }
            //Clean();
        }

        private void Clean()
        {
            try
            {
                int x = 0;
                while (x < 3)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                    {

                        string emp_ID = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        string day_Delete = dataGridView1.Rows[i].Cells[7].Value.ToString();

                        DateTime from = DateTime.Now;
                        DateTime to = DateTime.Parse(day_Delete);

                        int totalDays = ((TimeSpan)(to - from)).Days;

                        //MessageBox.Show(totalDays.ToString());

                        if (totalDays.ToString().Contains("-"))
                        {
                            if (con.State == ConnectionState.Closed) { con.Open(); }

                            SqlCommand cmd = con.CreateCommand();

                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "DELETE FROM P_Leave_Details WHERE Employee_ID = '" + emp_ID + "'";

                            cmd.ExecuteNonQuery();

                            //MessageBox.Show("Successfully Deleted.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            con.Close();
                            //ClearTxtBoxes();
                            RetrieveData();
                        }
                    }
                    x++;
                }
            }
            catch { }
        }


        private void NotRegular()
        {
            try
            {
                int count = 0;
                //Count the total employee 
                if (con.State == ConnectionState.Closed) { con.Open(); }

                SqlCommand cmd_Select = new SqlCommand("SELECT COUNT(*) FROM P_Employee_List ", con);
                count = Convert.ToInt32(cmd_Select.ExecuteScalar());

                con.Close();

                //string[] data = new string[totalEmployee];

                string emp_ID = "";
                string status = "Regular";

                SqlDataReader reader = null;

                SqlCommand cmd_Check = new SqlCommand("SELECT  *  FROM P_Employee_List", con);

                con.Close();

                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }

                reader = cmd_Check.ExecuteReader();

                while (reader.Read())
                {
                    if (status.Trim() == (reader["Status"].ToString().Trim()))
                    {
                        //emp_ID = reader["Employee_ID"].ToString().Trim();
                        data_Employee_ID.Add(reader["Employee_ID"].ToString().Trim());
                    }
                }
                con.Close();
            }
            catch { }
        }

        private void UpdateEmployeeLeave()
        {
            try
            {
                con.Close();

                string emp_ID = cb_empidTxt.Text.Trim();

                if (category_Leave.Equals("Vacation Leave"))
                {
                    int deduc_Vacation_Leave = Int32.Parse(no_VacationLeaveTxt.Text.Trim()) - Int32.Parse(daysTxt.Text.Trim());

                    SqlCommand cmd_Vacation_Leave = con.CreateCommand();

                    cmd_Vacation_Leave.CommandType = CommandType.Text;

                    cmd_Vacation_Leave.CommandText = "UPDATE P_Leave_Manage SET Vacation_Leave = '" + deduc_Vacation_Leave +
                        "' WHERE Employee_ID = '" + emp_ID + "'";

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    cmd_Vacation_Leave.ExecuteNonQuery();

                    con.Close();
                }
                else if (category_Leave.Equals("Sick Leave"))
                {
                    int deduc_Sick_Leave = Int32.Parse(no_SickLeaveTxt.Text.Trim()) - Int32.Parse(daysTxt.Text.Trim());

                    SqlCommand cmd_Sick_Leave = con.CreateCommand();

                    cmd_Sick_Leave.CommandType = CommandType.Text;

                    cmd_Sick_Leave.CommandText = "UPDATE P_Leave_Manage SET Sick_Leave = '" + deduc_Sick_Leave +
                        "' WHERE Employee_ID = '" + emp_ID + "'";

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    cmd_Sick_Leave.ExecuteNonQuery();

                    con.Close();
                }
                else
                {
                    //None: No pay
                }

                con.Close();
            }
            catch { }
        }

        private void ClearTxtBoxes()
        {
            cb_empidTxt.SelectedIndex = -1;
            surnameTxt.Clear();
            firstnameTxt.Clear();
            no_VacationLeaveTxt.Text = "0";
            no_SickLeaveTxt.Text = "0";
            fromDate.Text = DateTime.Now.ToShortDateString();
            toDate.Text = DateTime.Now.ToShortDateString();
            daysTxt.Text = "0";
            positionTxt.Clear();
            departmentTxt.Clear();

            if (Int32.Parse(no_VacationLeaveTxt.Text) == 0 && Int32.Parse(no_SickLeaveTxt.Text) == 0)
            {
                vacation_CheckBox.Checked = false;
                sickLeave_CheckBox.Checked = false;
                noPay_CheckBox.Checked = true;
            }
        }

        private void CountTotalDaysLeave()
        {
            DateTime from = fromDate.Value.Date;
            DateTime to = toDate.Value.Date;

            int totalDays = ((TimeSpan)(to - from)).Days;

            daysTxt.Text = totalDays.ToString();
            /*
            DateTime from = Convert.ToDateTime(fromDate.Text.Trim());
            DateTime to = Convert.ToDateTime(toDate.Text.Trim());
            TimeSpan countDays = to.Subtract(from);
            int totalDays = Convert.ToInt32(countDays.ToString());
            if (Convert.ToInt32(countDays) >= 0)
            {
                daysTxt.Text = totalDays.ToString();
            }
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
        }

        private void RetrieveData()
        {
            try
            {
                con.Close();

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                NotRegular(); //Remove not regular Employee ID

                string sql = "SELECT Employee_ID FROM P_Employee_List";
                SqlCommand cmd_Fill = new SqlCommand(sql, con);
                DataTable dt_Fill = new DataTable();

                SqlDataAdapter da_Fill = new SqlDataAdapter(cmd_Fill);
                da_Fill.Fill(dt_Fill);

                // cmd.ExecuteNonQuery();
                cb_empidTxt.Items.Clear();

                // for (int x = 0; x < data_Employee_ID.Count(); x++)
                // {
                foreach (DataRow dr_Fill in dt_Fill.Rows)
                {
                    if (data_Employee_ID.Contains(dr_Fill["Employee_ID"].ToString()))
                    {
                        cb_empidTxt.Items.Add("       " + dr_Fill["Employee_ID"].ToString());
                    }
                }
                // }
                con.Close();

                //cb_empidTxt.Items.Remove(cb_empidTxt.Text);

                //Bind specific columns
                SqlCommand cmd_Details = new SqlCommand("SELECT * FROM P_Leave_Details", con);
                DataTable dt_Details = new DataTable();
                SqlDataAdapter dta_Details = new SqlDataAdapter(cmd_Details);

                dataGridView1.DataSource = null;
                dta_Details.Fill(dt_Details);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 9;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                dataGridView1.Columns[0].HeaderText = "Employee ID"; dataGridView1.Columns[0].DataPropertyName = "Employee_ID";

                dataGridView1.Columns[1].HeaderText = "Surname"; dataGridView1.Columns[1].DataPropertyName = "Surname";

                dataGridView1.Columns[2].HeaderText = "Firstname"; dataGridView1.Columns[2].DataPropertyName = "Firstname";

                dataGridView1.Columns[3].HeaderText = "Department"; dataGridView1.Columns[3].DataPropertyName = "Department";

                dataGridView1.Columns[4].HeaderText = "Position"; dataGridView1.Columns[4].DataPropertyName = "Position";

                dataGridView1.Columns[5].HeaderText = "Category"; dataGridView1.Columns[5].DataPropertyName = "Category";

                dataGridView1.Columns[6].HeaderText = "From"; dataGridView1.Columns[6].DataPropertyName = "From_Date";

                dataGridView1.Columns[7].HeaderText = "To"; dataGridView1.Columns[7].DataPropertyName = "To_Date";

                dataGridView1.Columns[8].HeaderText = "No. of Days"; dataGridView1.Columns[8].DataPropertyName = "Days";

                dataGridView1.DataSource = dt_Details;

                //
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                /*
                //Add first Update button 
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn);
                btn.HeaderText = "Action";
                btn.Name = "Delete";
                btn.Text = "Delete";
                btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btn.FlatStyle = FlatStyle.Flat;
                //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                btn.DefaultCellStyle.ForeColor = Color.Brown;
                //btn.CellTemplate.Style.BackColor = Color.Green;
                btn.UseColumnTextForButtonValue = true;
                btn.DisplayIndex = 0; //First line
                */
                autoSize();
            }
            catch { }
        }

        private void fromDate_ValueChanged(object sender, EventArgs e)
        {
            CountTotalDaysLeave();
            //daysTxt.Text = DateDiff(DateInterval.Day, datetimepicker1.Value.Date, datetimepicker2.Value)
        }

        private void toDate_ValueChanged(object sender, EventArgs e)
        {
            CountTotalDaysLeave();
        }

        private void cb_empidTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cb_empidTxt.Text.Trim()))
                {
                    con.Close();

                    saveBtn.Enabled = true;

                    string emp_ID = cb_empidTxt.Text.Trim();

                    //Get Employee Information
                    SqlCommand cmd2 = new SqlCommand("SELECT [Surname], [Firstname], [Department], [Position]" +
                        "FROM P_Employee_List Where Employee_ID = @ID", con);

                    cmd2.Parameters.AddWithValue("@ID", emp_ID);

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    SqlDataReader dta2 = cmd2.ExecuteReader();

                    while (dta2.Read())
                    {
                        surnameTxt.Text = dta2.GetValue(0).ToString();
                        firstnameTxt.Text = dta2.GetValue(1).ToString();
                        departmentTxt.Text = dta2.GetValue(2).ToString();
                        positionTxt.Text = dta2.GetValue(3).ToString();
                    }
                    con.Close();

                    //Get No. of V.L And S.L
                    SqlCommand cmd_Leave = new SqlCommand("SELECT [Vacation_Leave], [Sick_Leave]" +
                        "FROM P_Leave_Manage Where Employee_ID = @ID", con);

                    cmd_Leave.Parameters.AddWithValue("@ID", emp_ID);

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    SqlDataReader dr_Leave = cmd_Leave.ExecuteReader();

                    while (dr_Leave.Read())
                    {
                        no_VacationLeaveTxt.Text = dr_Leave.GetValue(0).ToString();
                        no_SickLeaveTxt.Text = dr_Leave.GetValue(1).ToString();
                    }
                    con.Close();

                    //Check if the no of leave is not 0
                    if (Int32.Parse(no_VacationLeaveTxt.Text) < 0 || Int32.Parse(no_SickLeaveTxt.Text) < 0)
                    {
                        noPay_CheckBox.Checked = true;
                    }
                }
                else
                {
                    ClearTxtBoxes();
                }
            }
            catch { }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            saveBtn.Enabled = true;

            ClearTxtBoxes();
        }

        private void vacation_CheckBox_OnChange(object sender, EventArgs e)
        {
            if (Int32.Parse(no_VacationLeaveTxt.Text) == 0 || Int32.Parse(no_SickLeaveTxt.Text) == 0)
            {
                vacation_CheckBox.Checked = false;
                sickLeave_CheckBox.Checked = false;
                noPay_CheckBox.Checked = true;
            }
            else
            {
                if (vacation_CheckBox.Checked)
                {
                    sickLeave_CheckBox.Checked = false;
                    noPay_CheckBox.Checked = false;
                }
                else
                {
                    vacation_CheckBox.Checked = true;
                }
            }

        }

        private void sickLeave_CheckBox_OnChange(object sender, EventArgs e)
        {
            if (Int32.Parse(no_VacationLeaveTxt.Text) == 0 || Int32.Parse(no_SickLeaveTxt.Text) == 0)
            {
                vacation_CheckBox.Checked = false;
                sickLeave_CheckBox.Checked = false;
                noPay_CheckBox.Checked = true;
            }
            else
            {           
                if (sickLeave_CheckBox.Checked)
                {
                    vacation_CheckBox.Checked = false;
                    noPay_CheckBox.Checked = false;
                }
                else
                {
                    sickLeave_CheckBox.Checked = true;
                }
            }
        }

        private void noPay_CheckBox_OnChange(object sender, EventArgs e)
        {
            if (Int32.Parse(no_VacationLeaveTxt.Text) == 0 && Int32.Parse(no_SickLeaveTxt.Text) == 0)
            {
                vacation_CheckBox.Checked = false;
                sickLeave_CheckBox.Checked = false;
                noPay_CheckBox.Checked = true;
            }
            else
            {
                if (noPay_CheckBox.Checked)
                {
                    vacation_CheckBox.Checked = false;
                    sickLeave_CheckBox.Checked = false;
                }
                else
                {
                    noPay_CheckBox.Checked = true;
                }
            }          
        }

        Boolean exist = false;

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string emp_ID = cb_empidTxt.Text.Trim();
                string surname = surnameTxt.Text.Trim();
                string firstname = firstnameTxt.Text.Trim();
                string vacationLeave = no_VacationLeaveTxt.Text.Trim();
                string sickLeave = no_SickLeaveTxt.Text.Trim();
                string position = positionTxt.Text.Trim();
                string depart = departmentTxt.Text.Trim();

                string days = daysTxt.Text.Trim();

                if (!days.ToString().Trim().Contains("-"))
                {
                    if (vacation_CheckBox.Checked) { category_Leave = "Vacation Leave"; }
                    else if (sickLeave_CheckBox.Checked) { category_Leave = "Sick Leave"; }
                    else { category_Leave = "No Pay"; }

                    string from = fromDate.Text.Trim();
                    string to = toDate.Text.Trim();
                   // string days = daysTxt.Text.Trim();

                    SqlCommand cmd_Check = new SqlCommand();
                    cmd_Check.CommandText = "SELECT * FROM P_Leave_Details";
                    cmd_Check.Connection = con;

                    if (con.State.Equals(ConnectionState.Closed))
                    {
                        con.Open();

                    }

                    SqlDataReader dr = cmd_Check.ExecuteReader();

                    if (!string.IsNullOrEmpty(emp_ID) && !days.Equals("0"))
                    {
                        if (category_Leave.Equals("Vacation Leave"))
                        {

                            while (dr.Read())
                            {
                                if (dr[1].ToString() == emp_ID)
                                {
                                    exist = true; //Data exist
                                    break;
                                }

                            }

                            if (exist == false) //Data not existing
                            {
                                if (Int32.Parse(vacationLeave) > 0 && (Int32.Parse(days) <= Int32.Parse(vacationLeave)))
                                {
                                    DialogResult confirm = MessageBox.Show("Are you sure you want to apply leave for " + surname + " " + firstname + "?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                    if (confirm.Equals(DialogResult.Yes))
                                    {
                                        con.Close();

                                        SqlCommand cmd_List = new SqlCommand("INSERT INTO P_Leave_Details([Employee_ID], [Surname], [Firstname], [Department], [Position]," +
                                                                                    " [Category], [From_Date], [To_Date], [Days]) VALUES('" + emp_ID + "','" +
                                                                                    surname + "','" + firstname + "','" + depart + "','" + position + "', '" +
                                                                                    category_Leave + "', '" + from + "', '" + to + "', '" + days + "')", con);

                                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                                        cmd_List.ExecuteNonQuery();

                                        con.Close();

                                        UpdateEmployeeLeave();

                                        ClearTxtBoxes();

                                        RetrieveData();

                                        MessageBox.Show("Leave application has been created successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    //Vacation Leave Zero
                                    MessageBox.Show(surname + " " + firstname + "\n" + "Vacation Leave: " + vacationLeave, "No. of Leave", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                //not found
                            }
                            else
                            {
                                MessageBox.Show("There is already existing leave!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else if (category_Leave.Equals("Sick Leave"))
                        {
                            while (dr.Read())
                            {
                                if (dr[1].ToString() == emp_ID)
                                {
                                    exist = true; //Data exist
                                    break;
                                }
                            }

                            if (exist == false) //Data not existing
                            {
                                if (Int32.Parse(sickLeave) > 0 && (Int32.Parse(days) <= Int32.Parse(sickLeave)))
                                {
                                    con.Close();

                                    DialogResult confirm = MessageBox.Show("Are you sure you want to apply leave for " + surname + " " + firstname + "?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                    if (confirm.Equals(DialogResult.Yes))
                                    {
                                        SqlCommand cmd_List = new SqlCommand("INSERT INTO P_Leave_Details([Employee_ID], [Surname], [Firstname], [Department], [Position]," +
                                                                                    " [Category], [From_Date], [To_Date], [Days]) VALUES('" + emp_ID + "','" +
                                                                                    surname + "','" + firstname + "','" + depart + "','" + position + "', '" +
                                                                                    category_Leave + "', '" + from + "', '" + to + "', '" + days + "')", con);

                                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                                        cmd_List.ExecuteNonQuery();

                                        con.Close();

                                        UpdateEmployeeLeave();

                                        ClearTxtBoxes();

                                        RetrieveData();

                                        MessageBox.Show("Leave application has been created successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    //Vacation Leave Zero
                                    MessageBox.Show(surname + " " + firstname + "\n" + "Sick Leave: " + sickLeave, "No. of Leave", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("There is already existing leave!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            while (dr.Read())
                            {
                                if (dr[1].ToString() == emp_ID)
                                {
                                    exist = true; //Data exist
                                    break;
                                }
                            }

                            if (exist == false) //Data not existing
                            {
                                DialogResult confirm = MessageBox.Show("Are you sure you want to apply leave for " + surname + " " + firstname + "?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                if (confirm.Equals(DialogResult.Yes))
                                {
                                    con.Close();

                                    SqlCommand cmd_List = new SqlCommand("INSERT INTO P_Leave_Details([Employee_ID], [Surname], [Firstname], [Department], [Position]," +
                                                                                " [Category], [From_Date], [To_Date], [Days]) VALUES('" + emp_ID + "','" +
                                                                                surname + "','" + firstname + "','" + depart + "','" + position + "', '" +
                                                                                category_Leave + "', '" + from + "', '" + to + "', '" + days + "')", con);

                                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                                    cmd_List.ExecuteNonQuery();

                                    con.Close();

                                    UpdateEmployeeLeave();

                                    ClearTxtBoxes();

                                    RetrieveData();

                                    MessageBox.Show("Leave application has been created successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("There is already existing leave!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        if (days.Equals("0"))
                        {
                            MessageBox.Show("Please connect 'From' - 'To'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Choose employee!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    con.Close();
                    exist = false;
                }
                else
                {
                    MessageBox.Show("The days is contains negative(-)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK , MessageBoxIcon.Information );
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
        
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 9)
                {
                    //try
                    //{
                    string emp_ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    string day_Delete = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();

                    if (!string.IsNullOrEmpty(emp_ID))
                    {
                        DateTime from = DateTime.Now;
                        DateTime to = DateTime.Parse(day_Delete);

                        int totalDays = ((TimeSpan)(to - from)).Days;

                        if (totalDays.ToString().Contains("-"))
                        {
                            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result.Equals(DialogResult.Yes))
                            {
                                if (con.State == ConnectionState.Closed) { con.Open(); }

                                SqlCommand cmd = con.CreateCommand();

                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = "DELETE FROM P_Leave_Details WHERE Employee_ID = '" + emp_ID + "'";

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Successfully Deleted.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                con.Close();
                                //ClearTxtBoxes();
                                RetrieveData();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unable to delete!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose Data to Delete.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch { }
        }

        private void cleanBtn_Click(object sender, EventArgs e)
        {
            Clean();
        }
    }
}
