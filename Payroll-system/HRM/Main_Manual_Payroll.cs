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
using System.Linq;

namespace HRM
{
    public partial class Main_Manual_Payroll : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        int PW;
        Boolean hide;

        public Main_Manual_Payroll()
        {
            InitializeComponent();

            manualTimer.Enabled = true;

            RetrieveData(); //Retrieve data of Employee_List
            EmployeeDetailsRetrieveData(); //Employee Details

            //manualTimer.Start();
            

            ThirteenMonthPay();
            //this.StartPosition = FormStartPosition.CenterParent;

            PW = slidingPanel.Width;
            hide = false;

        }

        //Manual Page -----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------
        private string zero; //It will leave zero :zeroLeave() 
        private string blank;

        private string zeroTax; //It will leave zero in Tax:zeroLeaveTax() 
        private string blankTax;

        private double sss;
        private double hdmf;
        private double phil_Health;

        //Loans
        private string cash_Loan_Advance;
        private string cash_Loan_HDMF;
        private string cash_Loan_SSS;

        private List<string> data_Employee_ID_List = new List<string>();  //For Employee

        private List<string> holiday_Date = new List<string>(); //Get Holiday date
        //Sliding panel

        /*
        //Holiday variable
        private int reg_Holiday = 0;
        private int reg_Holiday_OT = 0;
        private int reg_Holiday_Hours_Late = 0;
        private int reg_Holiday_Mins_Late = 0;

        //Special Holiday
        private int spe_Holiday = 0;
        private int spe_Holiday_OT = 0;
        private int spe_Holiday_Hours_Late = 0;
        private int spe_Holiday_Mins_Late = 0;
        */

        //Employee Status 
        private string employeeStatus;

        private void cb_empidTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Close();

                if (!string.IsNullOrEmpty(cb_empidTxt.Text.Trim()))
                {
                    manualTimer.Enabled = true;
                    ClearTxtBoxes();


                    string emp_ID = cb_empidTxt.Text.Trim();

                    SqlCommand cmd2 = new SqlCommand("SELECT [Employee_ID], [Date_Hired]," +
                        "[Surname], [Firstname], [Position], [Rate_per_Hour], [Over_Time_Rate]," +
                        "[Schedule], [Status] FROM P_Employee_List Where Employee_ID = @ID", con);

                    cmd2.Parameters.AddWithValue("@ID", emp_ID);

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    SqlDataReader dta2 = cmd2.ExecuteReader();

                    while (dta2.Read())
                    {
                        emp_IDTxt.Text = dta2.GetValue(0).ToString();
                        //Date
                        //fromDate.Text = dta2.GetValue(1).ToString();
                        surnameTxt.Text = dta2.GetValue(2).ToString();
                        fnameTxt.Text = dta2.GetValue(3).ToString();
                        designationTxt.Text = dta2.GetValue(4).ToString();
                        ratePerHourTxt.Text = dta2.GetValue(5).ToString();
                        overTimeRateTxt.Text = dta2.GetValue(6).ToString();
                        employee_ScheduleTxt.Text = dta2.GetValue(7).ToString();
                        //Employee status
                        employeeStatus = dta2.GetValue(8).ToString();
                    }
                    con.Close();

                    ManualRetrieveData(); //Manual Page

                    LoanRetrieveData();
                    EmployeeLeave(); //Retrieve Leave Days
                    //Employee Details Page

                    con.Close();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    string search = cb_empidTxt.Text.Trim();

                    SqlDataAdapter dta = new SqlDataAdapter("SELECT * FROM P_Worker_Details WHERE Employee_ID LIKE '" + search + "%'", con);
                    DataTable dt = new DataTable();
                    dta.Fill(dt);
                    employeeDetailsView.DataSource = dt;
                    //dataGridView1.Columns.Remove("ID");
                    EmployeeDetailsCalculate(); //Employee Details Pag: Not working

                    Tax();

                    ManualCalculate();

                }
                else
                {
                    ClearTxtBoxes();
                    manualTimer.Enabled = false;
                }
            }
            catch { }
        }

        private void show_HideBtn_Click(object sender, EventArgs e)
        {
            if (hide) show_HideBtn.Text = "HIDE";
            else show_HideBtn.Text = "SHOW";
            slidingTimer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hide)
            {
                slidingPanel.Width = slidingPanel.Width + 90;
                if (slidingPanel.Width >= PW)
                {
                    slidingTimer.Stop();
                    hide = false;
                   // this.Refresh();
                }
            }
            else
            {
                slidingPanel.Width = slidingPanel.Width - 90;
                if (slidingPanel.Width <= 0)
                {
                    slidingTimer.Stop();
                    hide = true;
                    //this.Refresh();
                }
            }
        }

        private void CheckHoliday()
        {
            //Check Holiday
            con.Close();

            string emp_ID = cb_empidTxt.Text.Trim();

            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

            string sql = "SELECT Holiday_Date FROM P_Holiday";
            SqlCommand cmd__Select = new SqlCommand(sql, con);
            DataTable dt__Select = new DataTable();

            SqlDataAdapter da_Select = new SqlDataAdapter(cmd__Select);
            da_Select.Fill(dt__Select);

            foreach (DataRow dr_Select in dt__Select.Rows)
            {
                holiday_Date.Add(dr_Select["Holiday_Date"].ToString());
            }

            con.Close();

            //Regular Holiday
            List<string> reg_Holiday = new List<string>();
            List<string> reg_Holiday_OT = new List<string>();

            //List<string> reg_Holiday_Hours_Late = new List<string>();
            //  List<string> reg_Holiday_Mins_Late = new List<string>();

            //Special Holiday
            List<string> spe_Holiday = new List<string>();
            List<string> spe_Holiday_OT = new List<string>();

            List<string> holiday_Hours_Late = new List<string>();
            List<string> holiday_Mins_Late = new List<string>();

            for (int i = 0; i < employeeDetailsView.RowCount; i++)
            {
                //MessageBox.Show(employeeDetailsView.Rows[i].Cells[1].Value.ToString());
                string date = employeeDetailsView.Rows[i].Cells[1].Value.ToString();

                //Holiday
                string holidayType = string.Empty;

                SqlCommand cmd_Select = new SqlCommand("SELECT [Holiday_Type] FROM P_Holiday Where Holiday_Date = @Date", con);
               

                SqlCommand cmd_Details_DTR = new SqlCommand("SELECT [Work_Hours], [Over_Time_Hours]," +
                    "[Late_Hours], [Late_Mins] FROM P_Worker_Details Where Date = @Date", con);
              
                if (holiday_Date.Contains(date))
                {
                    con.Close();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }
                    cmd_Select.Parameters.AddWithValue("@Date", date);
                    SqlDataReader dr_Select = cmd_Select.ExecuteReader();              

                    while (dr_Select.Read())
                    {
                        holidayType = dr_Select.GetValue(0).ToString();
                    }
                    con.Close();

                    if (holidayType.Equals("Regular Holiday"))
                    {
                        con.Close();
                        
                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }
                        cmd_Details_DTR.Parameters.AddWithValue("@Date", date);
                        SqlDataReader dr_Details_DTR = cmd_Details_DTR.ExecuteReader();

                        while (dr_Details_DTR.Read())
                        {
                            reg_Holiday.Add(dr_Details_DTR.GetValue(0).ToString());
                            reg_Holiday_OT.Add(dr_Details_DTR.GetValue(1).ToString());
                            holiday_Hours_Late.Add(dr_Details_DTR.GetValue(2).ToString());
                            holiday_Mins_Late.Add(dr_Details_DTR.GetValue(3).ToString());                     
                        }
                        con.Close();

                      //Add using List...Regular Holiday
                       int sum_Reg = reg_Holiday.ConvertAll(Convert.ToInt32).Sum();
                       reg_HolidayTxt.Text = sum_Reg.ToString();

                       int sum_Reg_OT = reg_Holiday_OT.ConvertAll(Convert.ToInt32).Sum();
                       reg_Holiday_OTTxt.Text = sum_Reg_OT.ToString();
                    }    

                    if (holidayType.Equals("Special Holiday"))
                    {
                        con.Close();

                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }
                        cmd_Details_DTR.Parameters.AddWithValue("@Date", date);
                        SqlDataReader dr_Details_DTR = cmd_Details_DTR.ExecuteReader();

                        while (dr_Details_DTR.Read())
                        {
                            spe_Holiday.Add(dr_Details_DTR.GetValue(0).ToString());
                            spe_Holiday_OT.Add(dr_Details_DTR.GetValue(1).ToString());
                            holiday_Hours_Late.Add(dr_Details_DTR.GetValue(2).ToString());
                            holiday_Mins_Late.Add(dr_Details_DTR.GetValue(3).ToString());      
                        }
                        con.Close();

                        //Add using List...Regular Holiday
                        int sum_Spe = spe_Holiday.ConvertAll(Convert.ToInt32).Sum();
                        spe_HolidayTxt.Text = sum_Spe.ToString();

                        int sum_Spe_OT = spe_Holiday_OT.ConvertAll(Convert.ToInt32).Sum();
                        spe_Holiday_OTTxt.Text = sum_Spe_OT.ToString();
    
                    }
                   // int sum_Spe_Hour_Late = holiday_Hours_Late.ConvertAll(Convert.ToInt32).Sum();
                   // latePerHoursTxt.Text = String.Format("{0:0}", sum_Spe_Hour_Late.ToString());

                   // int sum_Spe_Mins_Late = holiday_Mins_Late.ConvertAll(Convert.ToInt32).Sum();
                   // latePerMinsTxt.Text = String.Format("{0:0}", sum_Spe_Mins_Late.ToString());

                   // MessageBox.Show("Hour " + sum_Spe_Hour_Late.ToString());
                    //MessageBox.Show("Late " + sum_Spe_Mins_Late.ToString());

                    reg_Holiday.Clear();
                    reg_Holiday_OT.Clear();

                    //Special Holiday
                    spe_Holiday.Clear();
                    spe_Holiday_OT.Clear();

                    holiday_Hours_Late.Clear();
                    holiday_Mins_Late.Clear();
                }//End of If 
            }


  
            /*
            for (int i = 0; i < employeeDetailsView.RowCount; i++)
            {
                //MessageBox.Show(employeeDetailsView.Rows[i].Cells[1].Value.ToString());
                string date = employeeDetailsView.Rows[i].Cells[1].Value.ToString();

                //Holiday
                string holidayType = string.Empty;
                SqlCommand cmd_Select = new SqlCommand("SELECT [Holiday_Type] FROM P_Holiday Where Holiday_Date = @Date", con);


                SqlCommand cmd_Details_DTR = new SqlCommand("SELECT [Work_Hours], [Over_Time_Hours]," +
                    "[Late_Hours], [Late_Mins] FROM P_Worker_Details Where Date = @Date", con);    
            }
            */
           
        }

        private void CheckListData()
        {
            try
            {
                con.Close();

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                con.Close();

                //Bind specific columns
                //Check List for Manual Payroll----------------------------------------------------------------------------------------------------------------------------
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Main_Manual_Currency_History", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                casual_CheckListView.DataSource = null;
                dta.Fill(dt);

                casual_CheckListView.AutoGenerateColumns = false;
                casual_CheckListView.ColumnCount = 1;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                casual_CheckListView.Columns[0].HeaderText = "Employee ID"; casual_CheckListView.Columns[0].DataPropertyName = "Employee_ID";

                casual_CheckListView.DataSource = dt;

                //
                casual_CheckListView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                casual_CheckListView.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                casual_CheckListView.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                casual_CheckListView.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                casual_CheckListView.DefaultCellStyle.ForeColor = Color.Black; //Fore Color Black

                casual_CheckListView.Columns[0].Width = 294;
                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

                //Check List for 13th Month Pay-------------------------------------------------------------------------------------------------------------------------
                SqlCommand cmd_Thirteen = new SqlCommand("SELECT * FROM P_Main_Manual_Currency_Thirteenth_Month_Pay", con);
                DataTable dt_Thirteen = new DataTable();
                SqlDataAdapter da_Thirteen = new SqlDataAdapter(cmd_Thirteen);

                second_CheckListView.DataSource = null;
                da_Thirteen.Fill(dt_Thirteen);

                second_CheckListView.AutoGenerateColumns = false;
                second_CheckListView.ColumnCount = 1;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                second_CheckListView.Columns[0].HeaderText = "Employee ID"; second_CheckListView.Columns[0].DataPropertyName = "Employee_ID";

                second_CheckListView.DataSource = dt_Thirteen;

                //
                second_CheckListView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                second_CheckListView.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                second_CheckListView.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                second_CheckListView.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                second_CheckListView.DefaultCellStyle.ForeColor = Color.Black; //Fore Color Black

                second_CheckListView.Columns[0].Width = 294;
                //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            }
            catch { }
        }

        private void RetrieveData()
        {
            //EmployeeID_Fil(); // Fill Employee ID
            EmployeeID_Fil();
            CheckListData(); //Check List for already paid
        }

        private void EmployeeID_Fil() //Remove Contructual Employee ID
        {
            try
            {
                con.Close();

                cb_empidTxt.Items.Clear();
                thirteenth_Emp_IDTxt.Items.Clear();

                int count = 0;

                //Count the total employee--------------------------------------------------------------------------------------------------------
                if (con.State == ConnectionState.Closed) { con.Open(); }

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM P_Employee_List ", con);
                count = Convert.ToInt32(cmd.ExecuteScalar());

                con.Close();
                //----------------------------------------------------------------------------------------------------------------------------------------------

                //string[] data = new string[totalEmployee];

                //---------------------------------------------------------------------------------------------------------------------------------
                if (regularRadioBtn.Checked)
                {
                    //Remove Contractual----------------------------------------------------------------------------------------------------------------------------------------------

                    string status_Regular = "Regular";

                    SqlDataReader reader = null;

                    SqlCommand cmd_Check = new SqlCommand("SELECT  *  FROM P_Employee_List", con);

                    con.Close();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    reader = cmd_Check.ExecuteReader();

                    while (reader.Read())
                    {
                        if (status_Regular.Trim() == (reader["Status"].ToString().Trim()))
                        {
                            //emp_ID = reader["Employee_ID"].ToString().Trim();
                            data_Employee_ID_List.Add(reader["Employee_ID"].ToString().Trim());
                        }

                    }
                    con.Close();
                }
                //---------------------------------------------------------------------------------------------------------------------------------

                //---------------------------------------------------------------------------------------------------------------------------------
                if (contractualRadioBtn.Checked)
                {
                    //Remove Regular----------------------------------------------------------------------------------------------------------------------------------------------

                    string status_Contractual = "Contractual";

                    SqlDataReader reader = null;

                    SqlCommand cmd_Check = new SqlCommand("SELECT  *  FROM P_Employee_List", con);

                    con.Close();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    reader = cmd_Check.ExecuteReader();

                    while (reader.Read())
                    {
                        if (status_Contractual.Trim() == (reader["Status"].ToString().Trim()))
                        {
                            //emp_ID = reader["Employee_ID"].ToString().Trim();
                            data_Employee_ID_List.Add(reader["Employee_ID"].ToString().Trim());

                        }

                    }
                    con.Close();
                }
                //---------------------------------------------------------------------------------------------------------------------------------

                string sql = "SELECT Employee_ID FROM P_Employee_List";

                SqlCommand cmd_Select = new SqlCommand(sql, con);
                DataTable dt_Select = new DataTable();

                SqlDataAdapter dta_Select = new SqlDataAdapter(cmd_Select);
                dta_Select.Fill(dt_Select);

                cb_empidTxt.Items.Clear();
                foreach (DataRow dr_Select in dt_Select.Rows)
                {
                    //Put here
                    if (regularRadioBtn.Checked)
                    {
                        if (data_Employee_ID_List.Contains(dr_Select["Employee_ID"].ToString()))
                        {
                            cb_empidTxt.Items.Add("       " + dr_Select["Employee_ID"].ToString());
                            thirteenth_Emp_IDTxt.Items.Add("       " + dr_Select["Employee_ID"].ToString()); //Thirteenth Page
                        }
                    }
                    else /*if (contractualRadioBtn.Checked)*/
                    {
                        if (data_Employee_ID_List.Contains("       " + dr_Select["Employee_ID"].ToString()))
                        {
                            cb_empidTxt.Items.Add("       " + dr_Select["Employee_ID"].ToString());
                            thirteenth_Emp_IDTxt.Items.Add("       " + dr_Select["Employee_ID"].ToString()); //Thirteenth Page
                        }
                    }
                }
                //----------------------------------------------------------------------------------------------------------------------------------------------
            }
            catch { }
        }

        private void check_SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (check_FromDate.Text != check_ToDate.Text)
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
                    string sql = "SELECT * FROM P_Main_Manual_Currency_History WHERE Date Between @from and @to";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);

                    da.SelectCommand.Parameters.AddWithValue("@from", check_FromDate.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@to", check_ToDate.Text.Trim());
                    da.Fill(dt);

                    casual_CheckListView.DataSource = dt;

                    //These lines will filter date  of 13th Month Pay
                    DataTable dt_Thirteen = new DataTable();
                    string sql_Thirteen = "SELECT * FROM P_Main_Manual_Currency_Thirteenth_Month_Pay WHERE Date Between @from and @to";
                    SqlDataAdapter da_Thirteen = new SqlDataAdapter(sql_Thirteen, con);

                    da_Thirteen.SelectCommand.Parameters.AddWithValue("@from", check_FromDate.Text.Trim());
                    da_Thirteen.SelectCommand.Parameters.AddWithValue("@to", check_ToDate.Text.Trim());
                    da_Thirteen.Fill(dt_Thirteen);

                    second_CheckListView.DataSource = dt_Thirteen;

                    con.Close();
                }
                else
                {
                    MessageBox.Show("Please connect the date 'From' - 'To'", "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void regularRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (regularRadioBtn.Checked)
            {
                ClearTxtBoxes();
                ThirteenthClearTxtBoxes(); //Clear Text Boxes in Thirteenth Month Pay

                data_Employee_ID_List.Clear();

                cb_empidTxt.Items.Clear();
                thirteenth_Emp_IDTxt.Items.Clear(); //Clear items in Thirteenth Month Pay

                regularRadioBtn.Checked = true;
                contractualRadioBtn.Checked = false;

                //thirteenth_RegularRadioBtn.Checked = true;
               // thirteenth_ContractualRadioBtn.Checked = false;

                EmployeeID_Fil();
            }

            if (contractualRadioBtn.Checked)
            {
                ClearTxtBoxes();
                ThirteenthClearTxtBoxes(); //Clear Text Boxes in Thirteenth Month Pay

                data_Employee_ID_List.Clear();

                cb_empidTxt.Items.Clear();
                thirteenth_Emp_IDTxt.Items.Clear(); //Clear items in Thirteenth Month Pay

                regularRadioBtn.Checked = false;
                contractualRadioBtn.Checked = true;

              //  thirteenth_RegularRadioBtn.Checked = false;
               // thirteenth_ContractualRadioBtn.Checked = true;

                EmployeeID_Fil();
            }
        }

        private void contractualRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (regularRadioBtn.Checked)
            {
                ClearTxtBoxes();
                ThirteenthClearTxtBoxes(); //Clear Text Boxes in Thirteenth Month Pay

                data_Employee_ID_List.Clear();

                cb_empidTxt.Items.Clear();
                thirteenth_Emp_IDTxt.Items.Clear(); //Clear items in Thirteenth Month Pay

                regularRadioBtn.Checked = true;
                contractualRadioBtn.Checked = false;

                //thirteenth_RegularRadioBtn.Checked = true;
                // thirteenth_ContractualRadioBtn.Checked = false;

                EmployeeID_Fil();
            }

            if (contractualRadioBtn.Checked)
            {
                ClearTxtBoxes();
                ThirteenthClearTxtBoxes(); //Clear Text Boxes in Thirteenth Month Pay

                data_Employee_ID_List.Clear();

                cb_empidTxt.Items.Clear();
                thirteenth_Emp_IDTxt.Items.Clear(); //Clear items in Thirteenth Month Pay

                regularRadioBtn.Checked = false;
                contractualRadioBtn.Checked = true;

                //  thirteenth_RegularRadioBtn.Checked = false;
                // thirteenth_ContractualRadioBtn.Checked = true;

                EmployeeID_Fil();
            }
        }

        private void EmployeeLeave()
        {
            try
            {
                vacationLeaveTxt.Text = "0";
                sickLeaveTxt.Text = "0";
                string category = string.Empty;
                string daysLeave = string.Empty;

                string emp_ID = cb_empidTxt.Text.Trim();

                SqlCommand cmd_Leave = new SqlCommand("SELECT [Category], [Days] FROM P_Leave_Details Where Employee_ID = @ID", con);

                cmd_Leave.Parameters.AddWithValue("@ID", emp_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_Leave = cmd_Leave.ExecuteReader();

                while (dr_Leave.Read())
                {
                    category = dr_Leave.GetValue(0).ToString();
                    daysLeave = dr_Leave.GetValue(1).ToString();
                }

                if (category.Equals("Vacation Leave"))
                {
                    vacationLeaveTxt.Text = daysLeave;
                }

                if (category.Equals("Sick Leave"))
                {
                    sickLeaveTxt.Text = daysLeave;
                }
                con.Close();
            }
            catch { }
        }

        private void Holiday()
        {
            try
            {
                //Reg. Holiday:
                double ratePerHour = Double.Parse(ratePerHourTxt.Text);
                double reg = Double.Parse(reg_HolidayTxt.Text);
                double reg2 = Double.Parse(reg_HolidayTxt2.Text);
                //double reg3 = Double.Parse(reg_HolidayTxt3.Text);

                //double reg_Holiday = Double.Parse(ratePerHourTxt.Text) * 8;

                reg_HolidayTxt2.Text = String.Format("{0:0.00}", ratePerHour);

                reg_HolidayTxt3.Text = String.Format("{0:0.00}", reg * reg2);

                //Regular Holiday OT
                double overTimeRate = Double.Parse(overTimeRateTxt.Text);
                double percent = 0.30;

                reg_Holiday_OTTxt2.Text = String.Format("{0:0.00}", overTimeRate * percent);

                double regOT = Double.Parse(reg_Holiday_OTTxt.Text);
                double regOT2 = Double.Parse(reg_Holiday_OTTxt2.Text);

                double reg_HolidayOT = regOT * regOT2;

                reg_Holiday_OTTxt3.Text = String.Format("{0:0.00}", reg_HolidayOT);

                //Spe. non-working Holiday:
                double special = Double.Parse(spe_HolidayTxt.Text);
                double special2 = Double.Parse(spe_HolidayTxt2.Text);

                spe_HolidayTxt2.Text = String.Format("{0:0.00}", ratePerHour * percent);

                spe_HolidayTxt3.Text = String.Format("{0:0.00}", special * special2);

                //Special Holiday OT
                spe_Holiday_OTTxt2.Text = String.Format("{0:0.00}", overTimeRate * percent);

                double spec_OT = Double.Parse(spe_Holiday_OTTxt.Text);
                double spec_OT2 = Double.Parse(spe_Holiday_OTTxt2.Text);

                double spe_HolidayOT = spec_OT * spec_OT2;

                spe_Holiday_OTTxt3.Text = String.Format("{0:0.00}", spec_OT * spe_HolidayOT);
                //MessageBox.Show("Hello");
            }
            catch { }
        }

        private void ManualCalculate()
        {
            try
            {           
                double ratePerHour = Double.Parse(ratePerHourTxt.Text);
                double overTimeRate = Double.Parse(overTimeRateTxt.Text);

                double workingHours = Double.Parse(workingHoursTxt.Text);
                double overTimeHour = Double.Parse(overTimeHourTxt.Text);

                double latePerHour = Double.Parse(latePerHoursTxt.Text);
                double latePerMin = Double.Parse(latePerMinsTxt.Text);
                
                //Holiday
                double reg_Holiday = Double.Parse(reg_HolidayTxt3.Text);
                double reg_Holiday_OT = Double.Parse(reg_Holiday_OTTxt3.Text);

                double spe_Holiday = Double.Parse(spe_HolidayTxt3.Text);
                double spe_Holiday_OT = Double.Parse(spe_Holiday_OTTxt3.Text);

                //Employee leave
                double vacationLeave = Double.Parse(vacationLeaveTxt.Text);
                double sickLeave = Double.Parse(sickLeaveTxt.Text);

                double bir = Double.Parse(bir_TaxTxt.Text);
                //double sss = Double.Parse(sssTxt.Text);
                //double hdmf = Double.Parse(hdmfTxt.Text);
                //double phil_Health = Double.Parse(phil_HealthTxt.Text);
                double otherDeduc = Double.Parse(otherDeductionsTxt.Text);

                double total_Deduction = Double.Parse(total_DeductionTxt.Text);

                double grossPay = Double.Parse(grossPayTxt.Text);
                double netPay = Double.Parse(netPayTxt.Text);

                //Calculate
                double total_WorkHours = ratePerHour * workingHours; //Total sum of salary

                double total_OverTime = overTimeRate * overTimeHour; // Total sum of overtime

                double total_Late_Per_Hour = latePerHour * 60; // total sum of late per hour

                double total_Late_Per_Min = latePerMin * 1; // total sum of late per min

                double sum_Hours = total_WorkHours * total_OverTime;


                double holiday = reg_Holiday + reg_Holiday_OT + spe_Holiday + spe_Holiday_OT;

                //Employee leave
                double days_VacationLeave = vacationLeave * (ratePerHour * 8);
                double days_SickLeave = sickLeave * (ratePerHour * 8);

                double total_GrossPay = total_WorkHours + total_OverTime + holiday + days_VacationLeave + days_SickLeave;             

                //GrossPay
                grossPayTxt.Text = String.Format("{0:0.00}", Double.Parse(total_GrossPay.ToString()));

                Tax(); //Calculate tax

                double sss = 0.00;
                double phil_Health = 0.00;
                double hdmf = 0.00;

                hdmf = Double.Parse(hdmfTxt.Text);
                sss = Double.Parse(sssTxt.Text);
                phil_Health = Double.Parse(phil_HealthTxt.Text);

                if (contributionCheckBox.Checked)
                {             
                    if (hdmfCheckBox.Checked)
                    {                  
                        hdmf = this.hdmf;
                        hdmfTxt.Text = String.Format("{0:0.00}", hdmf);
                    }
                    else
                    {
                        hdmfTxt.Text = "0.00";
                        hdmf = 0.00;
                    }

                    if (sssCheckBox.Checked)
                    {                     
                        sss = this.sss;
                        sssTxt.Text = String.Format("{0:0.00}", sss);
                    }
                    else
                    {
                        sssTxt.Text = "0.00";
                        sss = 0.00;
                    }

                    if (phil_HealthCheckBox.Checked)
                    {
                        phil_Health = this.phil_Health;
                        phil_HealthTxt.Text = String.Format("{0:0.00}", phil_Health);
                    }
                    else
                    {
                        phil_HealthTxt.Text = "0.00";
                        phil_Health = 0.00;
                    }
                }
                else
                {
                    //bir_TaxTxt.Text = "0.00";
                    hdmfTxt.Text = "0.00";
                    sssTxt.Text = "0.00";
                    phil_HealthTxt.Text = "0.00";
                }

                //Loans
                double cashAdvanceLoan = Double.Parse(cb_cashAdvanceLoan.Text);
                double hdmfLoan = Double.Parse(cb_hdmfLoan.Text);
                double sssLoan = Double.Parse(cb_sssLoan.Text);

                double total_Deduc = (bir + sss + hdmf + phil_Health + otherDeduc) + (cashAdvanceLoan + hdmfLoan + sssLoan) +
                    total_Late_Per_Hour + total_Late_Per_Min;

                double total_NetPay = total_GrossPay -  total_Deduc;

                //Put Data into Text Fields           
                total_DeductionTxt.Text = String.Format("{0:0.00}", Double.Parse(total_Deduc.ToString()));
                netPayTxt.Text = String.Format("{0:0.00}", Double.Parse(total_NetPay.ToString()));

                Holiday(); // Get the holiday

            }
            catch { }
        }

        private void Tax()
        {
            /*
            SSS CONTRIBUTION TABLE
            SALARY RANGE	CONTRIBUTION AMOUNT
          1.  1000 – 1249.99	    36.30
          2. 1250 – 1749.99	        54.50
          3.  1750 – 2249.99	    72.70
          4.  2250 – 2749.99	    90.80
           5. 2750 – 3249.99	    109.00
          6.  3250 – 3749.99	    127.20
          7.  3750 – 4249.99	    145.30
          8.  4250 – 4749.99	    163.50
          9.  4750 – 5249.99	    181.70
          10.  5250 – 5749.99	    199.80
          11.  5750 – 6249.99	    218.00
          12.  6250 – 6749.99	    236.20
          13.  6750 – 7249.99	    254.30
          14.  7250 – 7749.99	    272.50
          15.  7750 – 8249.99	    290.70
          16.  8250 – 8749.99	    308.80
          17.  8750 – 9249.99	    327.00
          18.  9250 – 9749.99	    345.20
          19.  9750 – 10249.99	    363.30
          20.  10250 – 10749.99	    381.50
          21.  10750 – 11249.99	    399.70
          22.  11250 – 11749.99	    417.80
          23.  11750 – 12249.99	    436.00
          24.  12250 – 12749.99	    454.20
          25. 12750 – 13249.99	    472.30
          26.  13250 – 13749.99	    490.50
          27.  13750 – 14249.99	    508.70
          28.  14250 – 14749.99	    526.80
          29.  14750 – 15249.99	    545.00
          30  15250 – 15749.99	    563.20
          31.  15750 and above	    581.30
            */
            double grossPay = Double.Parse(grossPayTxt.Text);

            if (grossPay == 0)
            {
                sss = 0.00;
            }
            else if (grossPay < 1000)
            {
                sss = 26.20;
            }
            else if (grossPay >= 1000 && grossPay <= 1249.99) { sss = 36.30; } //1.

            else if (grossPay >= 1250 && grossPay <= 1749.99) { sss = 54.50; } //2.

            else if (grossPay >= 1750 && grossPay <= 2249.99) { sss = 72.70; } //3.

            else if (grossPay >= 2250 && grossPay <= 2749.99) { sss = 90.80; } //4.

            else if (grossPay >= 2750 && grossPay <= 3249.99) { sss = 109.00; } //5.

            else if (grossPay >= 3250 && grossPay <= 3749.99) { sss = 127.20; } //6.

            else if (grossPay >= 3750 && grossPay <= 4249.99) { sss = 145.30; } //7.

            else if (grossPay >= 4250 && grossPay <= 4749.99) { sss = 163.50; } //8.

            else if (grossPay >= 4750 && grossPay <= 5249.99) { sss = 181.70; } //9.

            else if (grossPay >= 5250 && grossPay <= 5749.99) { sss = 199.80; } //10.

            else if (grossPay >= 5750 && grossPay <= 6249.99) { sss = 218.00; } //11.

            else if (grossPay >= 6250 && grossPay <= 6749.99) { sss = 236.20; } //12.

            else if (grossPay >= 6750 && grossPay <= 7249.99) { sss = 254.30; } //13.

            else if (grossPay >= 7250 && grossPay <= 7749.99) { sss = 272.50; } //14.

            else if (grossPay >= 7750 && grossPay <= 8249.99) { sss = 290.70; } //15.

            else if (grossPay >= 8250 && grossPay <= 8749.99) { sss = 308.80; } //16.

            else if (grossPay >= 8750 && grossPay <= 9249.99) { sss = 327.00; } //17.

            else if (grossPay >= 9250 && grossPay <= 9749.99) { sss = 345.20; } //18.

            else if (grossPay >= 9750 && grossPay <= 10249.99) { sss = 363.30; } //19.

            else if (grossPay >= 10250 && grossPay <= 10749.99) { sss = 381.50; } //20.

            else if (grossPay >= 10750 && grossPay <= 11249.99) { sss = 399.70; } //21.

            else if (grossPay >= 11250 && grossPay <= 11749.99) { sss = 417.80; } //22.

            else if (grossPay >= 11750 && grossPay <= 12249.99) { sss = 436.00; } //23.

            else if (grossPay >= 12250 && grossPay <= 12749.99) { sss = 454.20; } //24.

            else if (grossPay >= 12750 && grossPay <= 13249.99) { sss = 472.30; } //25.

            else if (grossPay >= 13250 && grossPay <= 13749.99) { sss = 490.50; } //26.

            else if (grossPay >= 13750 && grossPay <= 14249.99) { sss = 508.70; } //27.

            else if (grossPay >= 14250 && grossPay <= 14749.99) { sss = 526.80; } //28.

            else if (grossPay >= 14750 && grossPay <= 15249.99) { sss = 545.00; } //29.

            else if (grossPay >= 15250 && grossPay <= 15749.99) { sss = 563.20; } //30.

            else { sss = 581.30; } // (grosspay >= 15750) //31.

            //sssTxt.Text = String.Format("{0:0.00}", sss);
            /*
            PHILHEALTH CONTRIBUTION TABLE

            SALARY RANGE (x 2.75%)	CONTRIBUTION AMOUNT
            10000 and below	        275.00
            10000.01 – 39999.99	    275.02 – 1099.99
            40000 and above	        1100
             * 
            Taxable Income = (23000) – (581.30 + ((23000 * 0.0275) / 2) + 100.00)
            = (23000) – (997.55)
            Taxable Income = 22002.45             
            */
            //double philTax = (581.30 + ((grossPay * 0.0275) / 2) + 100.00);

            if (grossPay == 0)
            {
                phil_Health = 0.00;
            }
            else if (grossPay <= 10000.00)
            {
                phil_Health = 275.00;
            }
            else if (grossPay >= 10000.01 && grossPay <= 20000.01)
            {        
                phil_Health = 550.00;
            } 
            else if (grossPay >= 20000.02 && grossPay <= 30000.01)
            {
                phil_Health = 875.00;
            }
            else if (grossPay >= 30000.02 && grossPay <= 39999.99)
            {
                phil_Health = 1099.99;
            }
            else
            {
                phil_Health = 1100.00;
            }

            //phil_HealthTxt.Text = String.Format("{0:0.00}", phil_Health);

            /*
            Pag-IBIG Contribution Table for 2020
            Monthly Income                      Contribution Rate
            At least Php 1,000 to Php 1,500      1%
            Over Php 1,500                       2%
            */


            if (grossPay == 0)
            {
                hdmf = 0;
            }
            else if (grossPay <= 1500)
            {
                hdmf = grossPay * 0.01;
            }
            else if (grossPay >= 5000)
            {
                hdmf = 100;
            }
            else
            {
                hdmf = grossPay * 0.02;
            }

            //hdmfTxt.Text = String.Format("{0:0.00}", hdmf);       
        }

        private void eraseZeroTax() // Erase zero in textbox
        {
            if (blankTax.Equals("0.00"))
            {
                blankTax = "";
            }
        }

        private void leaveZeroTax() //It will leave zero : zero
        {
            try
            {
                if (!string.IsNullOrEmpty(zeroTax) || Int32.Parse(zeroTax) > 0)
                {
                }
                else
                {
                    zeroTax = "0.00";
                }
            }
            catch (Exception)
            {
                zeroTax = "0.00";
            }
        }

        private void eraseZero() // Erase zero in textbox
        {
            if (blank.Equals("0"))
            {
                blank = "";
            }
        }

        private void leaveZero() //It will leave zero : zero
        {
            try
            {
                if (!string.IsNullOrEmpty(zero) || Int32.Parse(zero) > 0)
                {
                }
                else
                {
                    zero = "0";
                }
            }
            catch (Exception)
            {
                zero = "0";
            }
        }
        
        private void onlyNumbers(object sender, KeyPressEventArgs e) //It will only take numbers
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ClearTxtBoxes()
        {
            //cb_empidTxt.SelectedIndex = -1;
            emp_IDTxt.Clear();
            surnameTxt.Clear();
            surnameTxt.Clear();
            fnameTxt.Clear();
            designationTxt.Text = "0.00";
            ratePerHourTxt.Text = "0.00";
            overTimeRateTxt.Text = "0.00";

            presentDaysTxt.Text = "0";
            workingHoursTxt.Text = "0";
            overTimeHourTxt.Text = "0";

            reg_HolidayTxt.Text = "0.00";
            reg_Holiday_OTTxt.Text = "0.00";
            spe_HolidayTxt.Text = "0.00";
            spe_Holiday_OTTxt.Text = "0.00";

            vacationLeaveTxt.Text = "0";
            sickLeaveTxt.Text = "0";

            grossPayTxt.Text = "0.00";

            latePerHoursTxt.Text = "0";
            latePerMinsTxt.Text = "0";

            bir_TaxTxt.Text = "0.00";
            hdmfTxt.Text = "0.00";
            sssTxt.Text = "0.00";
            phil_HealthTxt.Text = "0.00";

            otherDeductionsTxt.Text = "0.00";

            //Loans
            cb_cashAdvanceLoan.Text = "0.00";
            cb_hdmfLoan.Text = "0.00";
            cb_sssLoan.Text = "0.00";

            cash_Loan_Advance = string.Empty;
            cash_Loan_HDMF = string.Empty;
            cash_Loan_SSS = string.Empty;

            cb_cashAdvanceLoan.Items.Clear();
            cb_cashAdvanceLoan.Items.Add("0.00");
            cb_cashAdvanceLoan.SelectedIndex = 0;

            cb_hdmfLoan.Items.Clear();
            cb_hdmfLoan.Items.Add("0.00");
            cb_hdmfLoan.SelectedIndex = 0;

            cb_sssLoan.Items.Clear();
            cb_sssLoan.Items.Add("0.00");
            cb_sssLoan.SelectedIndex = 0;

            cashAdvanceLoanLb.Visible = false;
            hdmfLoanLb.Visible = false;
            sssLoanLb.Visible = false;

            total_DeductionTxt.Text = "0.00";

            netPayTxt.Text = "0.00";

            //Employee Details
            employee_ScheduleTxt.Text = "";
            details_Present_DaysTxt.Text = "0";
            details_HoursTxt.Text = "0";
            detailsOverTimeTxt.Text = "0";
            detailsLateHoursTxt.Text = "0";
            detailsLateMinsTxt.Text = "0";
        }

        private void LoanRetrieveData() //Retrieve data like cash advance, hdmf loan, sss loan
        {
            con.Close();

            cb_cashAdvanceLoan.Items.Clear();
            cb_hdmfLoan.Items.Clear();
            cb_sssLoan.Items.Clear();

            string emp_ID = emp_IDTxt.Text;

            try
            {
                con.Close();

                SqlCommand cmd_Cash_Advance = new SqlCommand("SELECT [Monthly_Amort], [Semi_Monthly_Amort]" +
                    "FROM P_Employee_Loan_Cash_Advance Where Employee_ID = @ID", con);

                cmd_Cash_Advance.Parameters.AddWithValue("@ID", emp_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dta_Cash_Advance = cmd_Cash_Advance.ExecuteReader();

                while (dta_Cash_Advance.Read())
                {
                    cb_cashAdvanceLoan.Items.Add(dta_Cash_Advance["Monthly_Amort"].ToString());
                    cash_Loan_Advance = dta_Cash_Advance["Monthly_Amort"].ToString();
                    cb_cashAdvanceLoan.Items.Add(dta_Cash_Advance["Semi_Monthly_Amort"].ToString());
                }
                cb_cashAdvanceLoan.SelectedIndex = 0;
                con.Close();
            }
            catch { }

            con.Close();

            try
            {
                SqlCommand cmd_HDMF_Loan = new SqlCommand("SELECT [Monthly_Amort], [Semi_Monthly_Amort]" +
                  "FROM P_Employee_Loan_HDMF Where Employee_ID = @ID", con);

                cmd_HDMF_Loan.Parameters.AddWithValue("@ID", emp_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dta_HDMF_Loan = cmd_HDMF_Loan.ExecuteReader();

                while (dta_HDMF_Loan.Read())
                {
                    cb_hdmfLoan.Items.Add(dta_HDMF_Loan["Monthly_Amort"].ToString());
                    cash_Loan_HDMF = dta_HDMF_Loan["Monthly_Amort"].ToString();
                    cb_hdmfLoan.Items.Add(dta_HDMF_Loan["Semi_Monthly_Amort"].ToString());
                }
                cb_hdmfLoan.SelectedIndex = 0;
                con.Close();
            }
            catch { }

            con.Close();

            try
            {
                SqlCommand cmd_SSS_Loan = new SqlCommand("SELECT [Monthly_Amort], [Semi_Monthly_Amort]" +
                "FROM P_Employee_Loan_SSS Where Employee_ID = @ID", con);

                cmd_SSS_Loan.Parameters.AddWithValue("@ID", emp_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dta_SSS_Loan = cmd_SSS_Loan.ExecuteReader();

                while (dta_SSS_Loan.Read())
                {
                    cb_sssLoan.Items.Add(dta_SSS_Loan["Monthly_Amort"].ToString());
                    cash_Loan_SSS = dta_SSS_Loan["Monthly_Amort"].ToString();
                    cb_sssLoan.Items.Add(dta_SSS_Loan["Semi_Monthly_Amort"].ToString());
                }
                cb_sssLoan.SelectedIndex = 0;
                con.Close();
            }
            catch { }

            if (cb_cashAdvanceLoan.Text == string.Empty)
            {
                cb_cashAdvanceLoan.Items.Add("0.00");
                cb_cashAdvanceLoan.SelectedIndex = 0;
                //MessageBox.Show("ahaha");
            }

            if (cb_hdmfLoan.Text == string.Empty)
            {
                cb_hdmfLoan.Items.Add("0.00");
                cb_hdmfLoan.SelectedIndex = 0;
                //MessageBox.Show("ahaha");
            }

            if (cb_sssLoan.Text == string.Empty)
            {
                cb_sssLoan.Items.Add("0.00");
                cb_sssLoan.SelectedIndex = 0;
                //MessageBox.Show("ahaha");
            }


            con.Close();
        }

        private void ManualRetrieveData()
        {
            try
            {
                con.Close();

                string emp_ID = emp_IDTxt.Text;
                SqlCommand cmd = new SqlCommand("SELECT [Employee_ID], [Surname], [Firstname], [Position], [Rate_per_Hour], [Over_Time_Rate]," +
                "[Date_Hired] FROM P_Employee_List Where Employee_ID = @ID", con);

                cmd.Parameters.AddWithValue("@ID", emp_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dta = cmd.ExecuteReader();

                while (dta.Read())
                {
                    emp_IDTxt.Text = dta.GetValue(0).ToString();
                    surnameTxt.Text = dta.GetValue(1).ToString();
                    fnameTxt.Text = dta.GetValue(2).ToString();
                    designationTxt.Text = dta.GetValue(3).ToString();
                    ratePerHourTxt.Text = dta.GetValue(4).ToString();
                    overTimeRateTxt.Text = dta.GetValue(5).ToString();
                    //fromDate.Text = dta.GetValue(6).ToString(); Do I need this?
                }
                con.Close();
            }
            catch { }
        }

        private void viewBtn2_Click(object sender, EventArgs e)
        {
            Worker_Details worker = new Worker_Details();
            worker.ShowDialog();
        }

        private void id_clearBtn2_Click_1(object sender, EventArgs e)
        {
            cb_empidTxt.SelectedIndex = -1;
        }

        private void bir_TaxTxt2_KeyPress(object sender, KeyPressEventArgs e) {onlyNumbers(sender, e);}

        private void bir_TaxTxt2_Leave(object sender, EventArgs e)
        {
            zeroTax = bir_TaxTxt.Text;
            leaveZeroTax();
            bir_TaxTxt.Text = zeroTax;
        }

        private void bir_TaxTxt2_MouseClick(object sender, MouseEventArgs e)
        {
            blankTax = bir_TaxTxt.Text;
            eraseZeroTax();
            bir_TaxTxt.Text = blankTax;
        }

        private void hdmfTxt2_KeyPress(object sender, KeyPressEventArgs e) { onlyNumbers(sender, e); }

        private void hdmfTxt2_Leave(object sender, EventArgs e)
        {
            zeroTax = hdmfTxt.Text;
            leaveZeroTax();
            hdmfTxt.Text = zeroTax;
        }

        private void hdmfTxt2_MouseClick(object sender, MouseEventArgs e)
        {
            blankTax = hdmfTxt.Text;
            eraseZeroTax();
            hdmfTxt.Text = blankTax;
        }

        private void sssTxt2_KeyPress(object sender, KeyPressEventArgs e) { onlyNumbers(sender, e); }

        private void sssTxt2_Leave(object sender, EventArgs e)
        {
            zeroTax = sssTxt.Text;
            leaveZeroTax();
            sssTxt.Text = zeroTax;
        }

        private void sssTxt2_MouseClick(object sender, MouseEventArgs e)
        {
            blankTax = sssTxt.Text;
            eraseZeroTax();
            sssTxt.Text = blankTax;
        }

        private void phil_healthTxt2_KeyPress(object sender, KeyPressEventArgs e) { onlyNumbers(sender, e); }

        private void phil_healthTxt2_Leave(object sender, EventArgs e)
        {
            zeroTax = phil_HealthTxt.Text;
            leaveZeroTax();
            phil_HealthTxt.Text = zeroTax;
        }

        private void phil_healthTxt2_MouseClick(object sender, MouseEventArgs e)
        {
            blankTax = phil_HealthTxt.Text;
            eraseZeroTax();
            phil_HealthTxt.Text = blankTax;
        }

        private void otherDeductionsTxt2_KeyPress(object sender, KeyPressEventArgs e) { onlyNumbers(sender, e); }

        private void otherDeductionsTxt2_Leave(object sender, EventArgs e)
        {
            zeroTax = otherDeductionsTxt.Text;
            leaveZeroTax();
            otherDeductionsTxt.Text = zeroTax;
        } 

        private void otherDeductionsTxt2_MouseClick(object sender, MouseEventArgs e)
        {
            blankTax = otherDeductionsTxt.Text;
            eraseZeroTax();
            otherDeductionsTxt.Text = blankTax;
        }


        private void cb_cashAdvanceLoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Double.Parse(this.cash_Loan_Advance) == Double.Parse(cb_cashAdvanceLoan.Text))
                {
                    cashAdvanceLoanLb.Visible = true;
                    cashAdvanceLoanLb.Text = "Monthly Amort.";
                }
                else
                {
                    cashAdvanceLoanLb.Visible = true;
                    cashAdvanceLoanLb.Text = "Semi-Monthly Amort.";
                }
                //MessageBox.Show(this.cash_Loan.ToString());
            }
            catch
            {

            }

        }

        private void cb_hdmfLoan_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (Double.Parse(this.cash_Loan_HDMF) == Double.Parse(cb_hdmfLoan.Text))
                {
                    hdmfLoanLb.Visible = true;
                    hdmfLoanLb.Text = "Monthly Amort.";
                }
                else
                {
                    hdmfLoanLb.Visible = true;
                    hdmfLoanLb.Text = "Semi-Monthly Amort.";
                }
            }
            catch
            {

            }

        }

        private void cb_sssLoan_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (Double.Parse(this.cash_Loan_SSS) == Double.Parse(cb_sssLoan.Text))
                {
                    sssLoanLb.Visible = true;
                    sssLoanLb.Text = "Monthly Amort.";
                }
                else
                {
                    sssLoanLb.Visible = true;
                    sssLoanLb.Text = "Semi-Monthly Amort.";
                }
            }
            catch
            {

            }

        }





        //Employee Details ----------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------

        private void EmployeeDetailsCalculate()
        {
            //Present Days
            details_Present_DaysTxt.Text = employeeDetailsView.Rows.Count.ToString();

            presentDaysTxt.Text = employeeDetailsView.Rows.Count.ToString(); //Manual Page2

            //Total Work Hours
            details_HoursTxt.Text = (from DataGridViewRow row in employeeDetailsView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum().ToString();
            
            workingHoursTxt.Text = details_HoursTxt.Text; //Manual Page2

            //Total Over Time Hours
            detailsOverTimeTxt.Text = (from DataGridViewRow row in employeeDetailsView.Rows
                                       where row.Cells[2].FormattedValue.ToString() != string.Empty
                                       select Convert.ToInt32(row.Cells[5].FormattedValue)).Sum().ToString();

            overTimeHourTxt.Text = detailsOverTimeTxt.Text;//Manual Page2

            //Total Late per Hours
            detailsLateHoursTxt.Text = (from DataGridViewRow row in employeeDetailsView.Rows
                                        where row.Cells[2].FormattedValue.ToString() != string.Empty
                                        select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum().ToString();

            latePerHoursTxt.Text = detailsLateHoursTxt.Text;//Manual Page2

            //Total Late per Mins
            detailsLateMinsTxt.Text = (from DataGridViewRow row in employeeDetailsView.Rows
                                        where row.Cells[2].FormattedValue.ToString() != string.Empty
                                        select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum().ToString();

            latePerMinsTxt.Text = detailsLateMinsTxt.Text;//Manual Page2
        }

        private void autoSize3()
        {
            employeeDetailsView.Columns[0].Width = 144;
            employeeDetailsView.Columns[1].Width = 130;
            employeeDetailsView.Columns[2].Width = 120;
            employeeDetailsView.Columns[3].Width = 120;
            employeeDetailsView.Columns[4].Width = 120;
            employeeDetailsView.Columns[5].Width = 120;
            employeeDetailsView.Columns[6].Width = 120;
            employeeDetailsView.Columns[7].Width = 120;
        }

        private void EmployeeDetailsRetrieveData()
        {
            try
            {
                con.Close();

                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Worker_Details", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                employeeDetailsView.DataSource = null;
                dta.Fill(dt);

                employeeDetailsView.AutoGenerateColumns = false;
                employeeDetailsView.ColumnCount = 8;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                employeeDetailsView.Columns[0].HeaderText = "Employee ID"; employeeDetailsView.Columns[0].DataPropertyName = "Employee_ID";

                employeeDetailsView.Columns[1].HeaderText = "Date"; employeeDetailsView.Columns[1].DataPropertyName = "Date";

                employeeDetailsView.Columns[2].HeaderText = "Time In"; employeeDetailsView.Columns[2].DataPropertyName = "Time_In";

                employeeDetailsView.Columns[3].HeaderText = "Time Out"; employeeDetailsView.Columns[3].DataPropertyName = "Time_Out";

                employeeDetailsView.Columns[4].HeaderText = "Worked Hours"; employeeDetailsView.Columns[4].DataPropertyName = "Work_Hours";

                employeeDetailsView.Columns[5].HeaderText = "Over Time Hours"; employeeDetailsView.Columns[5].DataPropertyName = "Over_Time_Hours";

                employeeDetailsView.Columns[6].HeaderText = "Late/Hours"; employeeDetailsView.Columns[6].DataPropertyName = "Late_Hours";

                employeeDetailsView.Columns[7].HeaderText = "Late/Mins"; employeeDetailsView.Columns[7].DataPropertyName = "Late_Mins";

                employeeDetailsView.DataSource = dt;

                //
                employeeDetailsView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                employeeDetailsView.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                employeeDetailsView.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                employeeDetailsView.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                employeeDetailsView.DefaultCellStyle.ForeColor = Color.Black; //Fore Color Black

                autoSize3();
            }
            catch { }
        }

        private void manualTimer_Tick(object sender, EventArgs e)
        {
            dateLb.Text = DateTime.Now.ToString("MM/dd/yyyy");
            thirteenDateLb.Text = DateTime.Now.ToString("MM/dd/yyyy");

            if (!string.IsNullOrEmpty(emp_IDTxt.Text))
            {
                //manualTimer.Enabled = true;

                ManualRetrieveData(); //Manual Page
   
                Tax();
                ManualCalculate();
                CheckHoliday(); //Retrieve holiday
            }
            else
            {
                presentDaysTxt.Text = "0";
                workingHoursTxt.Text = "0";
                overTimeHourTxt.Text = "0";
                //manualTimer.Enabled = false;
            }

            Summary(); //Summary Compensation
            //manualTimer.Enabled = false;
        }

        private void calculateBtn_Click(object sender, EventArgs e)
        {
            EmployeeDetailsCalculate();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void id_clearBtn2_Click(object sender, EventArgs e)
        {
            cb_empidTxt.SelectedIndex = -1;
            EmployeeDetailsRetrieveData();

        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            netPayTxt.Select();
        }

        private void bir_TaxTxt_TextChanged(object sender, EventArgs e) {if (!bir_TaxTxt.Equals("0.00")) {ManualCalculate();}}

        private void hdmfTxt_TextChanged(object sender, EventArgs e) {if (!hdmfTxt.Equals("0.00")) {ManualCalculate();}}

        private void sssTxt_TextChanged(object sender, EventArgs e) {if (!hdmfTxt.Equals("0.00")) {ManualCalculate();}}

        private void phil_HealthTxt_TextChanged(object sender, EventArgs e) {if (!phil_HealthTxt.Equals("0.00")) {ManualCalculate();}}

        private void otherDeductionsTxt_TextChanged(object sender, EventArgs e) { if (!otherDeductionsTxt.Equals("0.00")) { ManualCalculate(); } }

        private void searchBtn_Click(object sender, EventArgs e)
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

                    //These lines will filter date
                    DataTable dt = new DataTable();
                    string sql = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);

                    da.SelectCommand.Parameters.AddWithValue("@from", fromDate.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@to", toDate.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@ID", emp_IDTxt.Text.Trim());
                    da.Fill(dt);

                    employeeDetailsView.DataSource = dt;

                    EmployeeDetailsCalculate();

                    con.Close();
                }
                else
                {
                    MessageBox.Show("Please connect the date 'From' - 'To'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void reg_HolidayTxt_KeyPress(object sender, KeyPressEventArgs e) {onlyNumbers(sender, e);}

        private void reg_HolidayTxt_Leave(object sender, EventArgs e)
        {
            zeroTax = reg_HolidayTxt.Text;
            leaveZeroTax();
            reg_HolidayTxt.Text = zeroTax;
        }

        private void reg_HolidayTxt_MouseClick(object sender, MouseEventArgs e)
        {
            blankTax = reg_HolidayTxt.Text;
            eraseZeroTax();
            reg_HolidayTxt.Text = blankTax;
        }

        private void reg_Holiday_OTTxt_KeyPress(object sender, KeyPressEventArgs e) {onlyNumbers(sender, e);}

        private void reg_Holiday_OTTxt_Leave(object sender, EventArgs e)
        {
            zeroTax = reg_Holiday_OTTxt.Text;
            leaveZeroTax();
            reg_Holiday_OTTxt.Text = zeroTax;
        }

        private void reg_Holiday_OTTxt_MouseClick(object sender, MouseEventArgs e)
        {
            blankTax = reg_Holiday_OTTxt.Text;
            eraseZeroTax();
            reg_Holiday_OTTxt.Text = blankTax;
        }  

        private void spe_HolidayTxt_KeyPress(object sender, KeyPressEventArgs e) {onlyNumbers(sender, e);}

        private void spe_HolidayTxt_MouseClick(object sender, MouseEventArgs e)
        {
            blankTax = spe_HolidayTxt.Text;
            eraseZeroTax();
            spe_HolidayTxt.Text = blankTax;
        }

        private void spe_HolidayTxt_Leave(object sender, EventArgs e)
        {
            zeroTax = spe_HolidayTxt.Text;
            leaveZeroTax();
            spe_HolidayTxt.Text = zeroTax;
        }

        private void spe_Holiday_OTTxt_KeyPress(object sender, KeyPressEventArgs e) {onlyNumbers(sender, e);}

        
        private void spe_Holiday_OTTxt_MouseClick(object sender, MouseEventArgs e)
        {
            blankTax = spe_Holiday_OTTxt.Text;
            eraseZeroTax();
            spe_Holiday_OTTxt.Text = blankTax;
        }

        private void spe_Holiday_OTTxt_Leave(object sender, EventArgs e)
        {
            zeroTax = spe_Holiday_OTTxt.Text;
            leaveZeroTax();
            spe_Holiday_OTTxt.Text = zeroTax;
        }

        private void Summary()
        {
            try
            {
                sum_IDTxt.Text = emp_IDTxt.Text;

                sum_SurnameTxt.Text = surnameTxt.Text;
                sum_firstnameTxt.Text = fnameTxt.Text;
                sum_DesignationTxt.Text = designationTxt.Text;

                sum_RatePerHourTxt.Text = ratePerHourTxt.Text;
                sum_OverTimeRateTxt.Text = overTimeRateTxt.Text;

                sum_PresentDaysTxt.Text = presentDaysTxt.Text;

                sum_WorkingHoursTxt.Text = String.Format("{0:0}", Double.Parse(workingHoursTxt.Text) * Double.Parse(sum_RatePerHourTxt.Text));
                sum_OverTimeHoursTxt.Text = String.Format("{0:0}", Double.Parse(overTimeHourTxt.Text) * Double.Parse(sum_OverTimeRateTxt.Text));

                sum_LatePerHourTxt.Text = String.Format("{0:0}", Double.Parse(latePerHoursTxt.Text) * 60);
                sum_LatePerMinTxt.Text = String.Format("{0:0}", Double.Parse(latePerMinsTxt.Text) * 1);

                sum_RegHolidayTxt.Text = reg_HolidayTxt3.Text;
                sum_RegHolidayOTTxt.Text = reg_Holiday_OTTxt3.Text;

                sum_Spe_HolidayTxt.Text = spe_HolidayTxt3.Text;
                sum_Spe_HolidayOTTxt.Text = spe_Holiday_OTTxt3.Text;

                sum_BIRTaxtTxt.Text = bir_TaxTxt.Text;
                sum_HDMFTxt.Text = hdmfTxt.Text;
                sum_SSSTxt.Text = sssTxt.Text;
                sum_Phil_HealthTxt.Text = phil_HealthTxt.Text;
                sum_OtherDeducTxt.Text = otherDeductionsTxt.Text;

                //Loans
                sum_Cash_Advance_LoanTxt.Text = cb_cashAdvanceLoan.Text;
                sum_HDMF_LoanTxt.Text = cb_hdmfLoan.Text;
                sum_SSS_LoanTxt.Text = cb_sssLoan.Text;

                sum_VacationTxt.Text = String.Format("{0:0.00}", Double.Parse(vacationLeaveTxt.Text) * (Double.Parse(sum_RatePerHourTxt.Text) * 8));
                sum_SickTxt.Text = String.Format("{0:0.00}", Double.Parse(sickLeaveTxt.Text) * (Double.Parse(sum_RatePerHourTxt.Text) * 8));

                sum_TotalDeducTxt.Text = total_DeductionTxt.Text;

                sum_GrossPayTxt.Text = grossPayTxt.Text;
                sum_NetPayTxt.Text = netPayTxt.Text;

            }
            catch { }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            EmployeeDetailsRetrieveData();
        }
        
        //Payment Salary Page------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------

        string cash_Advance_Loan_Balance = "0.00";
        string hdmf_Loan_Balance = "0.00";
        string sss_Loan_Balance = "0.00";

        private void PaymentSummaryRetrieveLoan()
        {
            try
            {
                string emp_ID = cb_empidTxt.Text.Trim();

                //Cash Advance
                SqlCommand cmd_Cash_Advance_Loan = new SqlCommand("SELECT [Balance] FROM P_Employee_Loan_Cash_Advance Where Employee_ID = @ID", con);

                cmd_Cash_Advance_Loan.Parameters.AddWithValue("@ID", emp_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_Cash_Advance_Loan = cmd_Cash_Advance_Loan.ExecuteReader();

                while (dr_Cash_Advance_Loan.Read())
                {
                    cash_Advance_Loan_Balance = dr_Cash_Advance_Loan.GetValue(0).ToString();
                }

                con.Close();

                //HDMF Loan
                SqlCommand cmd_HDMF_Loan = new SqlCommand("SELECT [Balance] FROM P_Employee_Loan_HDMF Where Employee_ID = @ID", con);

                cmd_HDMF_Loan.Parameters.AddWithValue("@ID", emp_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_HDMF_Loan = cmd_HDMF_Loan.ExecuteReader();

                while (dr_HDMF_Loan.Read())
                {
                    hdmf_Loan_Balance = dr_HDMF_Loan.GetValue(0).ToString();
                }

                con.Close();

                //SSS Loan
                SqlCommand cmd_SSS_Loan = new SqlCommand("SELECT [Balance] FROM P_Employee_Loan_SSS Where Employee_ID = @ID", con);

                cmd_SSS_Loan.Parameters.AddWithValue("@ID", emp_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_SSS_Loan = cmd_SSS_Loan.ExecuteReader();

                while (dr_SSS_Loan.Read())
                {
                    sss_Loan_Balance = dr_SSS_Loan.GetValue(0).ToString();
                }

                con.Close();
            }
            catch { }
        }

        private void DeleteLoans()
        {
            try
            {
                PaymentSummaryRetrieveLoan(); //Retrieve Balance

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                string emp_ID = cb_empidTxt.Text.Trim();

                //Delete Cash Advance Loan
                if (cash_Advance_Loan_Balance.Equals("0.00"))
                {
                    SqlCommand cmd_Delete_Cash_Advance = new SqlCommand("DELETE FROM P_Employee_Loan_Cash_Advance WHERE Employee_ID = " +
                        emp_ID, con);

                    cmd_Delete_Cash_Advance.ExecuteNonQuery();
                }

                //Delete HDMF Loan
                if (hdmf_Loan_Balance.Equals("0.00"))
                {
                    SqlCommand cmd_Delete_HDMF = new SqlCommand("DELETE FROM P_Employee_Loan_HDMF WHERE Employee_ID = " +
                        emp_ID, con);

                    cmd_Delete_HDMF.ExecuteNonQuery();
                }

                //Delete SSS Loan
                if (cash_Advance_Loan_Balance.Equals("0.00"))
                {
                    SqlCommand cmd_Delete_SSS = new SqlCommand("DELETE FROM P_Employee_Loan_SSS WHERE Employee_ID = " +
                        emp_ID, con);

                    cmd_Delete_SSS.ExecuteNonQuery();
                }
                con.Close();
            }
            catch { }
        }

        private void payment_Sum_saveBtn2_Click(object sender, EventArgs e)
        {
            try
            {
                string date = dateLb.Text.Trim();

                string emp_ID = emp_IDTxt.Text.Trim();
                string surname = surnameTxt.Text.Trim();
                string fname = fnameTxt.Text.Trim();
                string position = designationTxt.Text.Trim();
                string rate_Per_Hour = ratePerHourTxt.Text.Trim();
                string overTimeRate = overTimeRateTxt.Text.Trim();

                string presentDays = presentDaysTxt.Text.Trim();
                string workingHours = workingHoursTxt.Text.Trim();
                string overTimeHour = overTimeHourTxt.Text.Trim();

                //Holidays
                string reg_Holiday = reg_HolidayTxt.Text.Trim();
                string reg_Holiday_OT = reg_Holiday_OTTxt.Text.Trim();
                string spe_Holiday = spe_HolidayTxt.Text.Trim();
                string spe_Holiday_OT = spe_Holiday_OTTxt.Text.Trim();

                //Employee Leave
                string vacationLeave = vacationLeaveTxt.Text.Trim();
                string sickLeave = sickLeaveTxt.Text.Trim();

                string grossPay = grossPayTxt.Text.Trim();

                string latePerHour = latePerHoursTxt.Text.Trim();
                string latePerMin = latePerMinsTxt.Text.Trim();

                string bir_Tax = bir_TaxTxt.Text.Trim();
                string hdmf = hdmfTxt.Text.Trim();
                string sss = sssTxt.Text.Trim();
                string phil_Health = phil_HealthTxt.Text;

                //Loans
                string cash_Advance_Loan = cb_cashAdvanceLoan.Text;
                string hdmf_Loan = cb_hdmfLoan.Text;
                string sss_Loan = cb_sssLoan.Text;

                string otherDeductions = otherDeductionsTxt.Text.Trim();

                string total_Deduction = total_DeductionTxt.Text.Trim();

                string netPay = netPayTxt.Text.Trim();

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                string sql = "SELECT * FROM P_Main_Manual_Currency_History WHERE Employee_ID = @ID";
                SqlCommand cmd_ID = new SqlCommand(sql, con);
                cmd_ID.Parameters.AddWithValue("@ID", emp_ID);

                SqlDataReader dr_ID = cmd_ID.ExecuteReader();

                if (string.IsNullOrEmpty(surname) || Double.Parse(netPay) == 0)
                {
                    MessageBox.Show("No data to add.", "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Information
                        );
                }
                else if (dr_ID.HasRows || !dr_ID.HasRows)
                {
                    DialogResult comfirm = MessageBox.Show("Are you sure you want to save?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (comfirm.Equals(DialogResult.Yes))
                    {
                        con.Close();

                        PaymentSummaryRetrieveLoan(); //Retrieve Balance

                        string cash_Advance_Balance = "0.00";
                        string hdmf_Balance = "0.00";
                        string sss_Balance = "0.00";

                        con.Open();

                        //Cash advance loan deduction of balance
                        cash_Advance_Balance = String.Format("{0:0.00}",
                            Double.Parse(cash_Advance_Loan_Balance) - Double.Parse(cash_Advance_Loan));

                        //HDMF loan deduction of balance
                        hdmf_Balance = String.Format("{0:0.00}",
                            Double.Parse(hdmf_Loan_Balance) - Double.Parse(hdmf_Loan));

                        //SSS loan deduction of balance
                        sss_Balance = String.Format("{0:0.00}",
                            Double.Parse(sss_Loan_Balance) - Double.Parse(sss_Loan));


                        //Cash Advance Update
                        SqlCommand cmd_Cash_Advance_Loan_Update = new SqlCommand("UPDATE P_Employee_Loan_Cash_Advance SET Balance = '" +
                            cash_Advance_Balance + "' WHERE Employee_ID = " + emp_ID, con);

                        cmd_Cash_Advance_Loan_Update.ExecuteNonQuery();

                        //HDMF Loan Update
                        SqlCommand cmd_HDMF_Loan_Update = new SqlCommand("UPDATE P_Employee_Loan_HDMF SET Balance = '" +
                             hdmf_Balance + "' WHERE Employee_ID = " + emp_ID, con);

                        cmd_HDMF_Loan_Update.ExecuteNonQuery();

                        //SSS Loan Update
                        SqlCommand cmd_SSS_Loan_Update = new SqlCommand("UPDATE P_Employee_Loan_SSS SET Balance = '" +
                            sss_Balance + "' WHERE Employee_ID = " + emp_ID, con);

                        cmd_SSS_Loan_Update.ExecuteNonQuery();

                        con.Close();

                        //Delete Loans
                        DeleteLoans();

                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd_Main = new SqlCommand("INSERT INTO P_Main_Manual_Currency([Date], [Employee_ID], [Surname]," +
                                                         "[Firstname], [Position], [Rate_per_Hour], [Over_Time_Rate], [Present_Days], [Working_Hours]," +
                                                         "[Over_Time_Hours], [Reg_Holiday], [Reg_Holiday_OT], [spe_Holiday], [spe_Holiday_OT]," +
                                                         "[Vacation_Leave], [Sick_Leave], [Gross_Pay], [Late_per_Hour], [Late_per_Min], [BIR_Tax]," +
                                                         "[HDMF], [SSS], [Phil_Health], [Other_Deductions]," + "[Cash_Advance_Loan], [HDMF_Loan]," +
                                                         "[SSS_Loan], [Total_Deduction], [Net_Pay] ) VALUES('" + date + "','" + emp_ID + "','" +
                                                         surname + "','" + fname + "','" + position + "','" + rate_Per_Hour + "','" + overTimeRate +
                                                         "','" + presentDays + "','" + workingHours + "','" + overTimeHour + "','" + reg_Holiday +
                                                         "','" + reg_Holiday_OT + "','" + spe_Holiday + "','" + spe_Holiday_OT + "','" + vacationLeave +
                                                         "','" + sickLeave + "','" + grossPay + "','" + latePerHour + "','" + latePerMin + "','" +
                                                         bir_Tax + "','" + hdmf + "','" + sss + "','" + phil_Health + "','" + otherDeductions +
                                                         "','" + cash_Advance_Loan + "','" + hdmf_Loan + "','" + sss_Loan + "','" + total_Deduction +
                                                         "','" + netPay + "')", con);
                        cmd_Main.ExecuteNonQuery();

                        SqlCommand cmd_History = new SqlCommand("INSERT INTO P_Main_Manual_Currency_History([Date], [Employee_ID], [Surname]," +
                                                         "[Firstname], [Position], [Rate_per_Hour], [Over_Time_Rate], [Present_Days], [Working_Hours]," +
                                                         "[Over_Time_Hours], [Reg_Holiday], [Reg_Holiday_OT], [spe_Holiday], [spe_Holiday_OT]," +
                                                         "[Vacation_Leave], [Sick_Leave], [Gross_Pay], [Late_per_Hour], [Late_per_Min], [BIR_Tax]," +
                                                         "[HDMF], [SSS], [Phil_Health], [Other_Deductions]," + "[Cash_Advance_Loan], [HDMF_Loan]," +
                                                         "[SSS_Loan], [Total_Deduction], [Net_Pay] ) VALUES('" + date + "','" + emp_ID + "','" +
                                                         surname + "','" + fname + "','" + position + "','" + rate_Per_Hour + "','" + overTimeRate +
                                                         "','" + presentDays + "','" + workingHours + "','" + overTimeHour + "','" + reg_Holiday +
                                                         "','" + reg_Holiday_OT + "','" + spe_Holiday + "','" + spe_Holiday_OT + "','" + vacationLeave +
                                                         "','" + sickLeave + "','" + grossPay + "','" + latePerHour + "','" + latePerMin + "','" +
                                                         bir_Tax + "','" + hdmf + "','" + sss + "','" + phil_Health + "','" + otherDeductions +
                                                         "','" + cash_Advance_Loan + "','" + hdmf_Loan + "','" + sss_Loan + "','" + total_Deduction +
                                                         "','" + netPay + "')", con);
                        cmd_History.ExecuteNonQuery();

                        SqlCommand cmd_Delete =  new SqlCommand("DELETE FROM P_Leave_Details WHERE Employee_ID = '" + emp_ID + "'", con);

                        cmd_Delete.ExecuteNonQuery();
                        /*
                        SqlCommand update_cmd = new SqlCommand("UPDATE P_Main_Currency_Alternative SET Employee_ID = '" + emp_ID +
                                  "' WHERE Employee_ID = " + emp_ID, con);

                        update_cmd.ExecuteNonQuery();
                        */

                        MessageBox.Show("Securely Record.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearTxtBoxes();
                        RetrieveData();
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show(fname + ", " + surname + " is already in the list!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch { }
        }

        //13th Month Pay Page ------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------
        private void autoSize4()
        {
            thirteenth_MonthView.Columns[0].Width = 144;
            thirteenth_MonthView.Columns[1].Width = 130;
            thirteenth_MonthView.Columns[2].Width = 120;
            thirteenth_MonthView.Columns[3].Width = 120;
            thirteenth_MonthView.Columns[4].Width = 120;
            thirteenth_MonthView.Columns[5].Width = 120;
            thirteenth_MonthView.Columns[6].Width = 120;
            thirteenth_MonthView.Columns[7].Width = 120;
        }

        private void ThirteenMonthPay()
        {
            try
            {
                con.Close();

                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Worker_Details", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                thirteenth_MonthView.DataSource = null;
                dta.Fill(dt);

                thirteenth_MonthView.AutoGenerateColumns = false;
                thirteenth_MonthView.ColumnCount = 8;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                thirteenth_MonthView.Columns[0].HeaderText = "Employee ID"; thirteenth_MonthView.Columns[0].DataPropertyName = "Employee_ID";

                thirteenth_MonthView.Columns[1].HeaderText = "Date"; thirteenth_MonthView.Columns[1].DataPropertyName = "Date";

                thirteenth_MonthView.Columns[2].HeaderText = "Time In"; thirteenth_MonthView.Columns[2].DataPropertyName = "Time_In";

                thirteenth_MonthView.Columns[3].HeaderText = "Time Out"; thirteenth_MonthView.Columns[3].DataPropertyName = "Time_Out";

                thirteenth_MonthView.Columns[4].HeaderText = "Worked Hours"; thirteenth_MonthView.Columns[4].DataPropertyName = "Work_Hours";

                thirteenth_MonthView.Columns[5].HeaderText = "Over Time Hours"; thirteenth_MonthView.Columns[5].DataPropertyName = "Over_Time_Hours";

                thirteenth_MonthView.Columns[6].HeaderText = "Late/Hours"; thirteenth_MonthView.Columns[6].DataPropertyName = "Late_Hours";

                thirteenth_MonthView.Columns[7].HeaderText = "Late/Mins"; thirteenth_MonthView.Columns[7].DataPropertyName = "Late_Mins";

                thirteenth_MonthView.DataSource = dt;

                //
                thirteenth_MonthView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                thirteenth_MonthView.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                thirteenth_MonthView.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                thirteenth_MonthView.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                thirteenth_MonthView.DefaultCellStyle.ForeColor = Color.Black; //Fore Color Black

                autoSize4();
            }
            catch { }
        }

        private void ThirteenthClearTxtBoxes()
        {
            thirteenth_Emp_IDTxt.SelectedIndex = -1;
            thirteenth_JanTxt.Text = "0.00";
            thirteenth_FebTxt.Text = "0.00";
            thirteenth_MarchTxt.Text = "0.00";
            thirteenth_AprilTxt.Text = "0.00";
            thirteenth_MayTxt.Text = "0.00";
            thirteenth_JuneTxt.Text = "0.00";
            thirteenth_JulyTxt.Text = "0.00";
            thirteenth_AugTxt.Text = "0.00";
            thirteenth_SeptTxt.Text = "0.00";
            thirteenth_OctTxt.Text = "0.00";
            thirteenth_NovTxt.Text = "0.00";
            thirteenth_DecTxt.Text = "0.00";
            thirteenth_TotalTxt.Text = "0.00";

            thirteenth_SurnameTxt.Clear();
            thirteenth_FirstnameTxt.Clear();
            thirteenth_PositionTxt.Clear();
            thirteenth_RatePerHourTxt.Text = "0.00";
            thirteenth_DailyRateTxt.Text = "0.00";
            thirteenth_SalaryTxt.Text = "0.00";
            thirteenth_Month_PayTxt.Text = "0.00";
        }

        private void ThirteenthMonthPay()
        {
            try
            {
                con.Close();

                if (con.State.Equals(ConnectionState.Closed))  { con.Open();}

                string ID = thirteenth_Emp_IDTxt.Text.Trim();
                string date = thirteenDateLb.Text.Substring(5);

                double dailyRate = Double.Parse(thirteenth_DailyRateTxt.Text);
                double ratePerHour = Double.Parse(thirteenth_RatePerHourTxt.Text);

                //Total Work Hours
                double totalHours_Jan;
                double totalHours_Feb;
                double totalHours_Mar;
                double totalHours_April;
                double totalHours_May;
                double totalHours_June;
                double totalHours_July;
                double totalHours_Aug;
                double totalHours_Sept;
                double totalHours_Oct;
                double totalHours_Nov;
                double totalHours_Dec;

                double totalWorkHours;

                //Total Late Hours
                double totalLateHour_Jan;
                double totalLateHour_Feb;
                double totalLateHour_Mar;
                double totalLateHour_April;
                double totalLateHour_May;
                double totalLateHour_June;
                double totalLateHour_July;
                double totalLateHour_Aug;
                double totalLateHour_Sept;
                double totalLateHour_Oct;
                double totalLateHour_Nov;
                double totalLateHour_Dec;

                double totalLateHours;

                //Total Work Hours
                double totalLateMins_Jan;
                double totalLateMins_Feb;
                double totalLateMins_Mar;
                double totalLateMins_April;
                double totalLateMins_May;
                double totalLateMins_June;
                double totalLateMins_July;
                double totalLateMins_Aug;
                double totalLateMins_Sept;
                double totalLateMins_Oct;
                double totalLateMins_Nov;
                double totalLateMins_Dec;

                double totalLateMins;

                //These lines will filter date of Jan
                DataTable dt_Jan = new DataTable();
                string sql_Jan = "SELECT * FROM P_Worker_Details WHERE DATE Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_Jan = new SqlDataAdapter(sql_Jan, con);

                da_Jan.SelectCommand.Parameters.AddWithValue("@from", "01/01/" + date);
                da_Jan.SelectCommand.Parameters.AddWithValue("@to", "01/31/" + date);
                da_Jan.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_Jan.Fill(dt_Jan);

                thirteenth_MonthView.DataSource = dt_Jan;

                //thirteen_JanTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_Jan = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                  where row.Cells[2].FormattedValue.ToString() != string.Empty
                                  select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();


                //Total Late per Hours
                totalLateHour_Jan = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_Jan = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();


                con.Close();

                con.Open();

                //These lines will filter date of Feb
                DataTable dt2_Feb = new DataTable();
                string sql_Feb = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_Feb = new SqlDataAdapter(sql_Feb, con);

                da_Feb.SelectCommand.Parameters.AddWithValue("@from", "02/01/" + date);
                da_Feb.SelectCommand.Parameters.AddWithValue("@to", "02/31/" + date);
                da_Feb.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_Feb.Fill(dt2_Feb);

                thirteenth_MonthView.DataSource = dt2_Feb;

                //thirteen_FebTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_Feb = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                  where row.Cells[2].FormattedValue.ToString() != string.Empty
                                  select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();

                //Total Late per Hours
                totalLateHour_Feb = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_Feb = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();

                con.Close();

                con.Open();

                //These lines will filter date of March
                DataTable dt_Mar = new DataTable();
                string sql_Mar = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_Mar = new SqlDataAdapter(sql_Mar, con);

                da_Mar.SelectCommand.Parameters.AddWithValue("@from", "03/01/" + date);
                da_Mar.SelectCommand.Parameters.AddWithValue("@to", "03/31/" + date);
                da_Mar.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_Mar.Fill(dt_Mar);

                thirteenth_MonthView.DataSource = dt_Mar;

                //thirteen_MarchTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_Mar = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                  where row.Cells[2].FormattedValue.ToString() != string.Empty
                                  select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();

                //Total Late per Hours
                totalLateHour_Mar = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_Mar = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();

                con.Close();

                con.Open();

                //These lines will filter date of April
                DataTable dt_Apr = new DataTable();
                string sql_Apr = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_Apr = new SqlDataAdapter(sql_Apr, con);

                da_Apr.SelectCommand.Parameters.AddWithValue("@from", "04/01/" + date);
                da_Apr.SelectCommand.Parameters.AddWithValue("@to", "04/31/" + date);
                da_Apr.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_Apr.Fill(dt_Apr);

                thirteenth_MonthView.DataSource = dt_Apr;

                //thirteen_AprilTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_April = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                    where row.Cells[2].FormattedValue.ToString() != string.Empty
                                    select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();

                //Total Late per Hours
                totalLateHour_April = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                       where row.Cells[2].FormattedValue.ToString() != string.Empty
                                       select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_April = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                       where row.Cells[2].FormattedValue.ToString() != string.Empty
                                       select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();

                con.Close();

                con.Open();

                //These lines will filter date of May
                DataTable dt_May = new DataTable();
                string sql_May = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_May = new SqlDataAdapter(sql_May, con);

                da_May.SelectCommand.Parameters.AddWithValue("@from", "05/01/" + date);
                da_May.SelectCommand.Parameters.AddWithValue("@to", "05/31/" + date);
                da_May.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_May.Fill(dt_May);

                thirteenth_MonthView.DataSource = dt_May;

                //thirteen_MayTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_May = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                  where row.Cells[2].FormattedValue.ToString() != string.Empty
                                  select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();

                //Total Late per Hours
                totalLateHour_May = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_May = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();

                con.Close();
                con.Open();

                //These lines will filter date of June
                DataTable dt_June = new DataTable();
                string sql_June = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_June = new SqlDataAdapter(sql_June, con);

                da_June.SelectCommand.Parameters.AddWithValue("@from", "06/01/" + date);
                da_June.SelectCommand.Parameters.AddWithValue("@to", "06/31/" + date);
                da_June.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_June.Fill(dt_June);

                thirteenth_MonthView.DataSource = dt_June;

                //thirteen_JuneTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_June = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                   where row.Cells[2].FormattedValue.ToString() != string.Empty
                                   select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();

                //Total Late per Hours
                totalLateHour_June = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                      where row.Cells[2].FormattedValue.ToString() != string.Empty
                                      select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_June = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                      where row.Cells[2].FormattedValue.ToString() != string.Empty
                                      select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();

                con.Close();
                con.Open();

                //These lines will filter date of July
                DataTable dt_July = new DataTable();
                string sql_July = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_July = new SqlDataAdapter(sql_July, con);

                da_July.SelectCommand.Parameters.AddWithValue("@from", "07/01/" + date);
                da_July.SelectCommand.Parameters.AddWithValue("@to", "07/31/" + date);
                da_July.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_July.Fill(dt_July);

                thirteenth_MonthView.DataSource = dt_July;

                //thirteen_JulyTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_July = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                   where row.Cells[2].FormattedValue.ToString() != string.Empty
                                   select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();

                //Total Late per Hours
                totalLateHour_July = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                      where row.Cells[2].FormattedValue.ToString() != string.Empty
                                      select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_July = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                      where row.Cells[2].FormattedValue.ToString() != string.Empty
                                      select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();

                con.Close();

                con.Open();

                //These lines will filter date of August
                DataTable dt_Aug = new DataTable();
                string sql_Aug = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_Aug = new SqlDataAdapter(sql_Aug, con);

                da_Aug.SelectCommand.Parameters.AddWithValue("@from", "08/01/" + date);
                da_Aug.SelectCommand.Parameters.AddWithValue("@to", "08/31/" + date);
                da_Aug.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_Aug.Fill(dt_Aug);

                thirteenth_MonthView.DataSource = dt_Aug;

                //thirteen_AugTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_Aug = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                  where row.Cells[2].FormattedValue.ToString() != string.Empty
                                  select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();

                //Total Late per Hours
                totalLateHour_Aug = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_Aug = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();

                con.Close();

                con.Open();

                //These lines will filter date of Sept
                DataTable dt_Sept = new DataTable();
                string sql_Sept = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_Sept = new SqlDataAdapter(sql_Sept, con);

                da_Sept.SelectCommand.Parameters.AddWithValue("@from", "09/01/" + date);
                da_Sept.SelectCommand.Parameters.AddWithValue("@to", "09/31/" + date);
                da_Sept.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_Sept.Fill(dt_Sept);

                thirteenth_MonthView.DataSource = dt_Sept;

                //thirteen_SeptTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_Sept = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                   where row.Cells[2].FormattedValue.ToString() != string.Empty
                                   select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();

                //Total Late per Hours
                totalLateHour_Sept = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                      where row.Cells[2].FormattedValue.ToString() != string.Empty
                                      select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_Sept = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                      where row.Cells[2].FormattedValue.ToString() != string.Empty
                                      select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();

                con.Close();

                con.Open();

                //These lines will filter date of Oct
                DataTable dt_Oct = new DataTable();
                string sql_Oct = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_Oct = new SqlDataAdapter(sql_Oct, con);

                da_Oct.SelectCommand.Parameters.AddWithValue("@from", "10/01/" + date);
                da_Oct.SelectCommand.Parameters.AddWithValue("@to", "10/31/" + date);
                da_Oct.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_Oct.Fill(dt_Oct);

                thirteenth_MonthView.DataSource = dt_Oct;

                //thirteen_OctTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_Oct = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                  where row.Cells[2].FormattedValue.ToString() != string.Empty
                                  select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();

                //Total Late per Hours
                totalLateHour_Oct = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_Oct = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();

                con.Close();

                con.Open();

                //These lines will filter date of Nov
                DataTable dt_Nov = new DataTable();
                string sql_Nov = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_Nov = new SqlDataAdapter(sql_Nov, con);

                da_Nov.SelectCommand.Parameters.AddWithValue("@from", "11/01/" + date);
                da_Nov.SelectCommand.Parameters.AddWithValue("@to", "11/31/" + date);
                da_Nov.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_Nov.Fill(dt_Nov);

                thirteenth_MonthView.DataSource = dt_Nov;

                //thirteen_NovTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_Nov = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                  where row.Cells[2].FormattedValue.ToString() != string.Empty
                                  select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();

                //Total Late per Hours
                totalLateHour_Nov = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_Nov = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();

                con.Close();

                con.Open();

                //These lines will filter date of Dec
                DataTable dt_Dec = new DataTable();
                string sql_Dec = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_Dec = new SqlDataAdapter(sql_Dec, con);

                da_Dec.SelectCommand.Parameters.AddWithValue("@from", "12/01/" + date);
                da_Dec.SelectCommand.Parameters.AddWithValue("@to", "12/31/" + date);
                da_Dec.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_Dec.Fill(dt_Dec);

                thirteenth_MonthView.DataSource = dt_Dec;

                //thirteen_DecTxt.Text = String.Format("{0:0.00}", dailyRate * thirteen_MonthView.Rows.Count);

                //Total Work Hours
                totalHours_Dec = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                  where row.Cells[2].FormattedValue.ToString() != string.Empty
                                  select Convert.ToInt32(row.Cells[4].FormattedValue)).Sum();

                //Total Late per Hours
                totalLateHour_Dec = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[6].FormattedValue)).Sum();

                //Total Late per Mins
                totalLateMins_Dec = (from DataGridViewRow row in thirteenth_MonthView.Rows
                                     where row.Cells[2].FormattedValue.ToString() != string.Empty
                                     select Convert.ToInt32(row.Cells[7].FormattedValue)).Sum();

                con.Close();



                totalLateHours = totalLateHour_Jan + totalLateHour_Feb + totalLateHour_Mar + totalLateHour_April +
                    totalLateHour_May + totalLateHour_June + totalLateHour_July + totalLateHour_Aug +
                    totalLateHour_Sept + totalLateHour_Oct + totalLateHour_Nov + totalLateHour_Dec;

                //double deduc_TotalLateHours = totalLateHours * 1;

                totalLateMins = totalLateMins_Jan + totalLateMins_Feb + totalLateMins_Mar + totalLateMins_April +
                    totalLateMins_May + totalLateMins_June + totalLateMins_July + totalLateMins_Aug +
                    totalLateMins_Sept + totalLateMins_Oct + totalLateMins_Nov + totalLateMins_Dec;

                // double deduc_TotalLateMins = totalLateMins * 1;


                thirteenth_JanTxt.Text = String.Format("{0:0.00}", ((totalHours_Jan * ratePerHour) - (totalLateHour_Jan * 60)) - totalLateMins_Jan);

                thirteenth_FebTxt.Text = String.Format("{0:0.00}", ((totalHours_Feb * ratePerHour) - (totalLateHour_Feb * 60)) - totalLateMins_Feb);

                thirteenth_MarchTxt.Text = String.Format("{0:0.00}", ((totalHours_Mar * ratePerHour) - (totalLateHour_Mar * 60)) - totalLateMins_Mar);

                thirteenth_AprilTxt.Text = String.Format("{0:0.00}", ((totalHours_April * ratePerHour) - (totalLateHour_April * 60)) - totalLateMins_April);

                thirteenth_MayTxt.Text = String.Format("{0:0.00}", ((totalHours_May * ratePerHour) - (totalLateHour_May * 60)) - totalLateMins_May);

                thirteenth_JuneTxt.Text = String.Format("{0:0.00}", ((totalHours_June * ratePerHour) - (totalLateHour_May * 60)) - totalLateMins_June);

                thirteenth_JulyTxt.Text = String.Format("{0:0.00}", ((totalHours_July * ratePerHour) - (totalLateHour_July * 60)) - totalLateMins_July);

                thirteenth_AugTxt.Text = String.Format("{0:0.00}", ((totalHours_Aug * ratePerHour) - (totalLateHour_Aug * 60)) - totalLateMins_Aug);

                thirteenth_SeptTxt.Text = String.Format("{0:0.00}", ((totalHours_Sept * ratePerHour) - (totalLateHour_Sept * 60)) - totalLateMins_Sept);

                thirteenth_OctTxt.Text = String.Format("{0:0.00}", ((totalHours_Oct * ratePerHour) - (totalLateHour_Oct * 60)) - totalLateMins_Oct);

                thirteenth_NovTxt.Text = String.Format("{0:0.00}", ((totalHours_Nov * ratePerHour) - (totalLateHour_Nov * 60)) - totalLateMins_Nov);

                thirteenth_DecTxt.Text = String.Format("{0:0.00}", ((totalHours_Dec * ratePerHour) - (totalLateHour_Dec * 60)) - totalLateMins_Dec);

                double jan = Double.Parse(thirteenth_JanTxt.Text);
                double feb = Double.Parse(thirteenth_FebTxt.Text);
                double mar = Double.Parse(thirteenth_MarchTxt.Text);
                double apr = Double.Parse(thirteenth_AprilTxt.Text);
                double may = Double.Parse(thirteenth_MayTxt.Text);
                double june = Double.Parse(thirteenth_JuneTxt.Text);
                double july = Double.Parse(thirteenth_JulyTxt.Text);
                double aug = Double.Parse(thirteenth_AugTxt.Text);
                double sept = Double.Parse(thirteenth_SeptTxt.Text);
                double oct = Double.Parse(thirteenth_OctTxt.Text);
                double nov = Double.Parse(thirteenth_NovTxt.Text);
                double dec = Double.Parse(thirteenth_DecTxt.Text);

                double total = jan + feb + mar + apr + may + june + july + aug + sept + oct + nov + dec;

                double thirteen_Month_Pay = total / 12;

                thirteenth_TotalTxt.Text = String.Format("{0:0.00}", total);

                thirteenth_Month_PayTxt.Text = String.Format("{0:0.00}", thirteen_Month_Pay);

                //These lines will filter date of Jan to Dec
                DataTable dt_Jan_Dec = new DataTable();

                string sql_Jan_Dec = "SELECT * FROM P_Worker_Details WHERE Date Between @from and @to and Employee_ID = @ID";
                SqlDataAdapter da_Jan_Dec = new SqlDataAdapter(sql_Jan_Dec, con);

                da_Jan_Dec.SelectCommand.Parameters.AddWithValue("@from", "01/01/" + date);
                da_Jan_Dec.SelectCommand.Parameters.AddWithValue("@to", "12/01/" + date);
                da_Jan_Dec.SelectCommand.Parameters.AddWithValue("@ID", ID);
                da_Jan_Dec.Fill(dt_Jan_Dec);

                thirteenth_MonthView.DataSource = dt_Jan_Dec;
            }
            catch { }
        }

        private void thirteenth_Emp_IDTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Close();

                if (!string.IsNullOrEmpty(thirteenth_Emp_IDTxt.Text))
                {
                    string emp_ID = thirteenth_Emp_IDTxt.Text.Trim();


                    SqlCommand cmd_Info = new SqlCommand("SELECT [Surname], [Firstname], [Position], [Rate_per_Hour]," +
                        "[Daily_Rate], [Salary] FROM P_Employee_List Where Employee_ID = @ID", con);

                    cmd_Info.Parameters.AddWithValue("@ID", emp_ID);

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    SqlDataReader dr = cmd_Info.ExecuteReader();

                    while (dr.Read())
                    {
                        thirteenth_SurnameTxt.Text = dr.GetValue(0).ToString();
                        thirteenth_FirstnameTxt.Text = dr.GetValue(1).ToString();
                        thirteenth_PositionTxt.Text = dr.GetValue(2).ToString();
                        thirteenth_RatePerHourTxt.Text = dr.GetValue(3).ToString();
                        thirteenth_DailyRateTxt.Text = dr.GetValue(4).ToString();
                        thirteenth_SalaryTxt.Text = dr.GetValue(5).ToString();
                    }
                    con.Close();

                    ThirteenthMonthPay();
                }
                else
                {
                    ThirteenthClearTxtBoxes();
                }
            }
            catch { }
        }

        private void thirteenth_ClearTxt_Click(object sender, EventArgs e)
        {
            thirteenth_Emp_IDTxt.SelectedIndex = -1;
        }

        private void contributionCheckBox_OnChange(object sender, EventArgs e)
        {
            if (contributionCheckBox.Checked ==  true)
            {
                contributionCheckBox.Checked = true;
                hdmfCheckBox.Checked = true;
                sssCheckBox.Checked = true;
                phil_HealthCheckBox.Checked = true;
            }
            else
            {
                contributionCheckBox.Checked = false;
                hdmfCheckBox.Checked = false;
                sssCheckBox.Checked = false;
                phil_HealthCheckBox.Checked = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand("DELETE FROM P_Main_Manual_Currency", con);

                SqlCommand cmd2 = new SqlCommand("DELETE FROM P_Main_Manual_Currency_History", con);

                SqlCommand cmd3 = new SqlCommand("DELETE FROM P_Main_Manual_Currency_Thirteenth_Month_Pay", con);

                con.Open();

                cmd1.ExecuteNonQuery();

                cmd2.ExecuteNonQuery();

                cmd3.ExecuteNonQuery();

                con.Close();
            }
            catch { }
        }

        private void thirteenth_SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Close();

                string emp_ID = thirteenth_Emp_IDTxt.Text.Trim();
                string date = thirteenDateLb.Text.Trim();
                string surname = thirteenth_SurnameTxt.Text.Trim();
                string firstname = thirteenth_FirstnameTxt.Text.Trim();
                string position = thirteenth_PositionTxt.Text.Trim();
                string basicPay = thirteenth_SalaryTxt.Text.Trim();
                string thirteenth_Pay = thirteenth_Month_PayTxt.Text.Trim();

                if (!basicPay.Equals(string.Empty))
                {
                    DialogResult comfirm = MessageBox.Show("Are you sure you want to save?", "Payroll System", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (comfirm.Equals(DialogResult.Yes))
                    {
                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd_Thirteenth = new SqlCommand("INSERT INTO P_Main_Manual_Currency_Thirteenth_Month_Pay([Date], [Employee_ID], [Surname]," +
                                                                 "[Firstname], [Position], [Basic_Pay], [Salary]) VALUES('" + date + "','" +
                                                                 emp_ID + "','" + surname + "','" + firstname + "','" + position + "','" + basicPay +
                                                                 "','" + thirteenth_Pay + "')", con);
                        cmd_Thirteenth.ExecuteNonQuery();

                        MessageBox.Show("Securely Record.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ThirteenthClearTxtBoxes();

                        RetrieveData();
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No data to add!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch { }
        }

        private void thirteenth_RegularRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void thirteenth_ContractualRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }
    }
}
