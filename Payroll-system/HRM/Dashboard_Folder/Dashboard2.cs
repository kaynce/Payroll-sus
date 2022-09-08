using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//New Import
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Configuration;

namespace HRM
{
    public partial class Dashboard2 : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

         //[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        //For Panel Move: Move Form
        private int move;
        private int moveX;
        private int moveY;

        //Admin
        private string statusOn = "ON";
        private string statusOff = "OFF";
        private string db_admin_ID;
        private string db_admin_Position;
        private string usernameHome;

        private string db_admin_surname;
        private string db_admin_firstname;

        //Company Logo
        private string logo_ID;
        private string file;

        public Dashboard2(string db_admin_ID, string usernameHome, 
            string db_admin_Position, string db_admin_surname, string db_admin_firstname)
        {
            InitializeComponent();
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            //Switch Home
            panelNav1.Height = dashboardBtn.Height;
            panelNav1.Top = dashboardBtn.Top;
            panelNav1.Left = dashboardBtn.Left;
            dashboardBtn.BackColor = Color.FromArgb(46, 51, 73);

            Dashboard3 dashboard3 = new Dashboard3(db_admin_Position);
            switchPanel(dashboard3);

            //OpacityForm();
            ChangeOpacity_Second();

            dateLb.Text = DateTime.Now.ToString("MMMM dd yyyy");
            timeLb.Text = DateTime.Now.ToString("hh:mm:ss tt");

            this.usernameHome = usernameHome;
            this.db_admin_ID = db_admin_ID;
            this.db_admin_Position = db_admin_Position;
            this.db_admin_surname = db_admin_surname;
            this.db_admin_firstname = db_admin_firstname;

            userPositionLb.Text = db_admin_Position;
            retrieveData();

            retrievelogoLink(); //Company Logo

            //this.WindowState = FormWindowState.Maximized;


            if (!string.IsNullOrEmpty(usernameLb.Text))
            {
                onlineShape.Visible = true;
                onlineLb.Visible = true;
            }

            DisableBtn();

            CustomizeDesign(); //Hide the sub menu
        }

        private void CustomizeDesign()
        {
            employees_SubMenuPanel.Visible = false;
            leaveSubMenuPanel.Visible = false;
            loans_SubMenuPanel.Visible = false;
            report_SubMenuPanel.Visible = false;
            setting_SubMenuPanel.Visible = false;
        }

        private void HideSubMenu()
        {
            if (employees_SubMenuPanel.Visible == true) { employees_SubMenuPanel.Visible = false; }

            if (leaveSubMenuPanel.Visible == true) { leaveSubMenuPanel.Visible = false; }

            if (loans_SubMenuPanel.Visible == true) { loans_SubMenuPanel.Visible = false;}

            if (report_SubMenuPanel.Visible == true) { report_SubMenuPanel.Visible = false; }

            if (setting_SubMenuPanel.Visible == true) { setting_SubMenuPanel.Visible = false; }
        }

        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }


        private void switchPanel(Form panel)
        {
            switchingPanel.Controls.Clear();
            panel.TopLevel = false;
            switchingPanel.Controls.Add(panel);
            panel.Show();
        }

        private void DisableBtn()
        {
            try
            {
                if (db_admin_Position.Equals("Admin"))
                {
                    comlogoPic.Enabled = true;
                    settingBtn.Enabled = true;
                }

                if (db_admin_Position.Equals("Sub-Admin"))
                {
                    comlogoPic.Enabled = false;
                    settingBtn.Enabled = false;
                }
            }
            catch { }
        }

        private void ChangeOpacity_Second()
        {
            this.Opacity = .1;
            // timer1.Interval = new TimeSpan(0, 0,);
            timer1.Tick += ChangeOpacity;
            timer1.Start();
        }

       private void ChangeOpacity(object sender, EventArgs e)
        {
            this.Opacity += .08;
            if (this.Opacity == 1)
            {
                timer1.Stop();
            }
        }

        public void getNum()
        {
            try
            {
                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                //"SELECT ID FROM Payroll ORDER BY ID"
                SqlCommand cmd = new SqlCommand("SELECT MAX([ID]) from P_Company_Logo ", con);
                SqlDataReader dta = cmd.ExecuteReader();

                if (dta.Read())
                {
                    logo_ID = dta[0].ToString();
                    //textBox1.Text = ID;
                }
                con.Close();
            }
            catch { }
        }

        //Logo Link
        public void retrievelogoLink()
        {
            try
            {
                getNum();

                SqlCommand cmd = new SqlCommand("SELECT Logo_Link FROM P_Company_Logo Where ID = @ID", con);

                cmd.Parameters.AddWithValue("@ID", int.Parse(logo_ID));

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dta = cmd.ExecuteReader();

                if (dta.Read())
                {
                    file = dta.GetValue(0).ToString();
                }

                if (System.IO.File.Exists(file))
                {
                    //comlogoPic.Visible = true;
                    //file = openFileDialog1.FileName;
                    comlogoPic.Image = Image.FromFile(file);
                }
                con.Close();
            }
            catch { con.Close(); }
        }

        private void retrieveData()
        {
            try
            {
                string db_admin_Surname;
                string db_admin_Firstname;

                SqlCommand cmd = new SqlCommand("SELECT [Surname], [Firstname] FROM P_Admin_Users Where Admin_ID = @ID", con);

                cmd.Parameters.AddWithValue("@ID", Int32.Parse(db_admin_ID));

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dta = cmd.ExecuteReader();

                while (dta.Read())
                {
                    db_admin_Surname = dta.GetValue(1).ToString();
                    db_admin_Firstname = dta.GetValue(0).ToString();

                    usernameLb.Text = "Hi! " + db_admin_Surname + " " + db_admin_Firstname;

                    con.Close();
                }
            }
            catch { }
        }

        /*
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        */

        private void btnDashbord_MouseClick(object sender, MouseEventArgs e)
        {
            panelNav1.Height = dashboardBtn.Height;
            panelNav1.Top = dashboardBtn.Top;
            //panelNav.Left = btnDashbord.Left;
            dashboardBtn.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnAnalytics_MouseClick(object sender, MouseEventArgs e)
        {
            dashboardBtn.BackColor = Color.FromArgb(24, 30, 54);

            panelNav1.Height = employeesBtn.Height;
            panelNav1.Top = employeesBtn.Top;
            employeesBtn.BackColor = Color.FromArgb(46, 51, 73);  
        }

        private void payrollBtn_MouseClick(object sender, MouseEventArgs e)
        {
            dashboardBtn.BackColor = Color.FromArgb(24, 30, 54);

            panelNav1.Height = payrollBtn.Height;
            panelNav1.Top = payrollBtn.Top;
            payrollBtn.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnContactUs_MouseClick(object sender, MouseEventArgs e)
        {
            dashboardBtn.BackColor = Color.FromArgb(24, 30, 54);

            panelNav1.Height = reportBtn.Height;
            panelNav1.Top = reportBtn.Top;
            reportBtn.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void aboutBtn_MouseClick(object sender, MouseEventArgs e)
        {
            dashboardBtn.BackColor = Color.FromArgb(24, 30, 54);

            panelNav1.Height = aboutBtn.Height;
            panelNav1.Top = aboutBtn.Top;

            about_NavPanel.Height = aboutBtn.Height;
            about_NavPanel.Top = aboutBtn.Top;
            about_NavPanel.Visible = true;

            aboutBtn.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnsettings_MouseClick(object sender, MouseEventArgs e)
        {
            dashboardBtn.BackColor = Color.FromArgb(24, 30, 54);

            panelNav1.Height = settingBtn.Height;
            panelNav1.Top = settingBtn.Top;

            settingBtn.BackColor = Color.FromArgb(46, 51, 73);
        }   

        private void btnDashbord_Leave(object sender, EventArgs e) {dashboardBtn.BackColor = Color.FromArgb(24, 30, 54);}

        private void btnAnalytics_Leave(object sender, EventArgs e) {employeesBtn.BackColor = Color.FromArgb(24, 30, 54);}

        private void btnCalender_Leave(object sender, EventArgs e) {payrollBtn.BackColor = Color.FromArgb(24, 30, 54);}

        private void btnContactUs_Leave(object sender, EventArgs e) {reportBtn.BackColor = Color.FromArgb(24, 30, 54);}

        private void aboutBtn_Leave(object sender, EventArgs e) { about_NavPanel.Visible = false; aboutBtn.BackColor = Color.FromArgb(24, 30, 54); } 

        private void btnsettings_Leave(object sender, EventArgs e){settingBtn.BackColor = Color.FromArgb(24, 30, 54);}

        private void employeesBtn_Click(object sender, EventArgs e)
        {
            try { Application.OpenForms["Dashboard3"].Dispose(); }
            catch { }
            try { Application.OpenForms["Main_Manual_Payroll"].Dispose(); }
            catch { }

            Employee_Attendance attendance = new Employee_Attendance();
            switchPanel(attendance);
            ShowSubMenu(employees_SubMenuPanel);
        }

        private void dashboardBtn_Click(object sender, EventArgs e)
        {
            try { Application.OpenForms["Main_Manual_Payroll"].Dispose(); }
            catch { }
            Dashboard3 dashboard3 = new Dashboard3(db_admin_Position);
            switchPanel(dashboard3);
            HideSubMenu();
        }

        private void payrollBtn_Click(object sender, EventArgs e)
        {
            try { Application.OpenForms["Dashboard3"].Dispose(); }
            catch { }
            Main_Manual_Payroll main = new Main_Manual_Payroll();
            switchPanel(main);
            //main.ShowDialog();
            HideSubMenu();
        }

        private void btnContactUs_Click(object sender, EventArgs e)
        {

        }

        private void reportBtn_Click(object sender, EventArgs e)
        {
            try { Application.OpenForms["Dashboard3"].Dispose(); }
            catch { }
            try { Application.OpenForms["Main_Manual_Payroll"].Dispose(); }
            catch { }
            Salary_Report salary = new Salary_Report();
            switchPanel(salary);
            ShowSubMenu(report_SubMenuPanel);
        }

        private void settingBtn_Click(object sender, EventArgs e)
        {
            try { Application.OpenForms["Dashboard3"].Dispose(); }
            catch { }
            try { Application.OpenForms["Main_Manual_Payroll"].Dispose(); }
            catch { }
            Pending_User user = new Pending_User();
            switchPanel(user);
            ShowSubMenu(setting_SubMenuPanel);
            
        }

        private void dateTimer_Tick(object sender, EventArgs e)
        {
            dateLb.Text = DateTime.Now.ToString("MMMM dd yyyy");
            timeLb.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void comlogoPic_Click(object sender, EventArgs e)
        {
            retrievelogoLink();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void comlogoPic_DoubleClick(object sender, EventArgs e)
        {
            //Set_Up_Logo set = new Set_Up_Logo();
            //set.ShowDialog();
        }

        private void aboutBtn_Click(object sender, EventArgs e)
        {
            try { Application.OpenForms["Dashboard3"].Dispose(); }
            catch { }
            try { Application.OpenForms["Main_Manual_Payroll"].Dispose(); }
            catch { }
            About about = new About();
            switchPanel(about);
            HideSubMenu();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            if (update_signout_Panel.Visible == false)
            {
                update_signout_Panel.Visible = true;
                update_signout_Panel.BringToFront();
            }
            else
            {
                update_signout_Panel.SendToBack();
                update_signout_Panel.Visible = false;
            } 
        }

        private void signOutBtn_Click(object sender, EventArgs e)
        {          
            try
            {        
                DialogResult confirm = MessageBox.Show("Are you sure you want to log out?", "Log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm.Equals(DialogResult.Yes))
                {
                    con.Close();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    SqlDataReader reader = null;
                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM P_Admin_Users", con);
                    reader = cmd1.ExecuteReader();

                    //db_admin_ID = reader["Admin_ID"].ToString().Trim();
                    //MessageBox.Show(db_admin_ID);
                    SqlCommand cmd2 = new SqlCommand("UPDATE P_Admin_Users SET Status = '" + 
                        statusOff + "' WHERE Admin_ID = " + db_admin_ID, con);

                    con.Close();
                    con.Open();

                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd_Log_History = new SqlCommand("UPDATE P_Admin_Log_History SET Time_Out = '" +
                         DateTime.Now.ToString("hh:mm tt") + "' WHERE Admin_ID = " + db_admin_ID, con);

                    cmd_Log_History.ExecuteNonQuery();


                    //Get the Time in;
                    SqlCommand cmd_Db_Log_History = new SqlCommand("SELECT [Date], [Time_In] FROM P_Admin_Log_History Where Admin_ID = @ID", con);

                    cmd_Db_Log_History.Parameters.AddWithValue("@ID", db_admin_ID);

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    SqlDataReader dr_Db_Log_History = cmd_Db_Log_History.ExecuteReader();

                    string db_Date = "";
                    string db_Time_In = "";

                    while (dr_Db_Log_History.Read())
                    {
                        db_Date = dr_Db_Log_History.GetValue(0).ToString();
                        db_Time_In = dr_Db_Log_History.GetValue(1).ToString();
                    }

                    con.Close();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    SqlCommand cmd_Log_History2 = new SqlCommand("INSERT INTO P_Admin_Log_History2([Admin_ID]," +
                               "[Date], [Surname], [Firstname], [Time_In], [Time_Out])" +
                               "VALUES('" + db_admin_ID + "','" + db_Date + 
                               "','" + db_admin_surname + "','" + db_admin_firstname + "','" +
                               db_Time_In + "','" + DateTime.Now.ToString("hh:mm tt") + 
                               "')", con);

                    cmd_Log_History2.ExecuteNonQuery();

                    //Need to delete for unlimited login
                    SqlCommand cmd_History = new SqlCommand("DELETE FROM P_Admin_Log_History WHERE Admin_ID = " + db_admin_ID, con);

                    cmd_History.ExecuteNonQuery();

                    con.Close();

                    Login login = new Login();
                    this.Hide();
                    login.ShowDialog();
                    this.Close();
                }
            
            }
                
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                
                Login login = new Login();
                this.Hide();
                login.ShowDialog();
                this.Close();        
            }            
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(usernameHome))
            {
                Admin_Update admin = new Admin_Update(db_admin_ID);
                admin.ShowDialog();
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Close();

                SqlCommand cmd1 = new SqlCommand("DELETE FROM P_Add_Employee", con);

                SqlCommand cmd2 = new SqlCommand("DELETE FROM P_Employee_Attendance", con);
                SqlCommand cmd3 = new SqlCommand("DELETE FROM P_Employee_Attendance_Admin", con);

                SqlCommand cmd4 = new SqlCommand("DELETE FROM P_Employee_Attendance_Calculation", con);
                SqlCommand cmd5 = new SqlCommand("DELETE FROM P_Employee_Attendance_Calculation_Second", con);

                SqlCommand cmd6 = new SqlCommand("DELETE FROM P_Employee_Data_History", con);

                SqlCommand cmd7 = new SqlCommand("DELETE FROM P_Employee_List", con);

                SqlCommand cmd8 = new SqlCommand("DELETE FROM P_Employee_Total_Hours", con);

                SqlCommand cmd9 = new SqlCommand("DELETE FROM P_Main_Currency", con);
                SqlCommand cmd10 = new SqlCommand("DELETE FROM P_Main_Currency_Alternative", con);
                SqlCommand cmd11 = new SqlCommand("DELETE FROM P_Main_Currency_Alternative_Employee_Work_Days", con);
                SqlCommand cmd12 = new SqlCommand("DELETE FROM P_Main_Currency_Alternative_History", con);

                SqlCommand cmd13 = new SqlCommand("DELETE FROM P_Employee_Hours_Trend_Report", con);

                SqlCommand cmd14 = new SqlCommand("DELETE FROM P_Employee_Schedules", con);

                SqlCommand cmd15 = new SqlCommand("DELETE FROM P_Employee_Hours_Trend_Report", con);

                SqlCommand cmd16 = new SqlCommand("DELETE FROM P_Worker_Details", con);

                SqlCommand cmd17 = new SqlCommand("DELETE FROM P_Employee_Attendance_Report", con);

                con.Open();

                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();
                cmd5.ExecuteNonQuery();
                cmd6.ExecuteNonQuery();
                cmd7.ExecuteNonQuery();
                cmd8.ExecuteNonQuery();
                cmd9.ExecuteNonQuery();
                cmd10.ExecuteNonQuery();
                cmd11.ExecuteNonQuery();
                cmd12.ExecuteNonQuery();
                cmd13.ExecuteNonQuery();
                cmd14.ExecuteNonQuery();
                cmd15.ExecuteNonQuery();
                cmd16.ExecuteNonQuery();
                cmd17.ExecuteNonQuery();

                con.Close();
            }
            catch { }
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal))
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;

            }
        }

        private void loanBtn_Click(object sender, EventArgs e)
        {
            try { Application.OpenForms["Dashboard3"].Dispose(); }
            catch { }
            try { Application.OpenForms["Main_Manual_Payroll"].Dispose(); }
            catch { }
            Employee_Loan_Cash_Advance loan = new Employee_Loan_Cash_Advance();
            switchPanel(loan);
            ShowSubMenu(loans_SubMenuPanel);
        }

        private void loanBtn_MouseClick(object sender, MouseEventArgs e)
        {
            dashboardBtn.BackColor = Color.FromArgb(24, 30, 54);

            panelNav2.Height = loanBtn.Height;
            panelNav2.Top = loanBtn.Top;
            panelNav2.Visible = true;

            loanBtn.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void loanBtn_Leave(object sender, EventArgs e)
        {
            panelNav2.Visible = false; loanBtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void loanBtn_MouseClick_1(object sender, MouseEventArgs e)
        {
            dashboardBtn.BackColor = Color.FromArgb(24, 30, 54);

            //No erase the panel 1
            panelNav1.Height = loanBtn.Height;
            panelNav1.Top = loanBtn.Top;

            panelNav2.Height = loanBtn.Height;
            panelNav2.Top = loanBtn.Top;
            panelNav2.Visible = true;

            loanBtn.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            try { Application.OpenForms["Dashboard3"].Dispose(); }
            catch { }
            try { Application.OpenForms["Main_Manual_Payroll"].Dispose(); }
            catch { }
            Apply_Leave leave = new Apply_Leave();
            switchPanel(leave);
            ShowSubMenu(leaveSubMenuPanel);
        }

        private void leaveBtn_MouseClick(object sender, MouseEventArgs e)
        {
            dashboardBtn.BackColor = Color.FromArgb(24, 30, 54);

            panelNav1.Height = leaveBtn.Height;
            panelNav1.Top = leaveBtn.Top;

            leave_NavPanel.Height = leaveBtn.Height;
            leave_NavPanel.Top = leaveBtn.Top;
            leave_NavPanel.Visible = true;

            leaveBtn.BackColor = Color.FromArgb(46, 51, 73); 
        }

        private void leaveBtn_Leave(object sender, EventArgs e)
        {
            leave_NavPanel.Visible = false;
            leaveBtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        #region Employee SubMenu
        private void employees_AtttendanceBtn_Click(object sender, EventArgs e)
        {
            Employee_Attendance attendance = new Employee_Attendance();
            switchPanel(attendance);
            //HideSubMenu();
        }

        private void employees_Att_ReportBtn_Click(object sender, EventArgs e)
        {
            Employee_Attendance_Report report = new Employee_Attendance_Report();
            switchPanel(report);
            //HideSubMenu();
        }

        private void employees_ListBtn_Click(object sender, EventArgs e)
        {
            Employee_List list = new Employee_List();
            switchPanel(list);
           // HideSubMenu();
        }

        private void employee_ListSchedBtn_Click(object sender, EventArgs e)
        {
            Add_Sched sched = new Add_Sched();
            switchPanel(sched);
            //HideSubMenu();
        }

        private void employee_ListPosBtn_Click(object sender, EventArgs e)
        {
            Add_Designation designation = new Add_Designation();
            switchPanel(designation);
            //HideSubMenu();
        }

        private void employees_SchedBtn_Click(object sender, EventArgs e)
        {
            Employee_Schedule sched = new Employee_Schedule();
            switchPanel(sched);
            //HideSubMenu();
        }

        private void employees_DTR_HistoryBtn_Click(object sender, EventArgs e)
        {
            Employee_DTR_History history = new Employee_DTR_History();
            switchPanel(history);
           // HideSubMenu();
        }

        private void employees_ArchivedBtn_Click(object sender, EventArgs e)
        {
            Employee_Archived archived = new Employee_Archived();
            switchPanel(archived);
            //HideSubMenu();
        }
        #endregion

        #region Leave SubMenu
        private void leave_ApplyBtn_Click(object sender, EventArgs e)
        {
            Apply_Leave apply = new Apply_Leave();
            switchPanel(apply);
            //HideSubMenu();
        }

        private void leave_ManageBtn_Click(object sender, EventArgs e)
        {
            Manage_Leave manage = new Manage_Leave();
            switchPanel(manage);
            //HideSubMenu();
        }
        #endregion

        private void setting_ListUsersBtn_Click(object sender, EventArgs e)
        {
            Pending_User pending = new Pending_User();
            switchPanel(pending);
        }

        private void setting_AddQuestionBtn_Click(object sender, EventArgs e)
        {
            Add_New_Question question = new Add_New_Question();
            switchPanel(question);
        }

        private void setting_LogHistoryBtn_Click(object sender, EventArgs e)
        {
            Admin_Log_History history = new Admin_Log_History();
            switchPanel(history);
        }

        private void setting_DepartmentBtn_Click(object sender, EventArgs e)
        {
            Department department = new Department();
            switchPanel(department);
        }

        private void setting_BackupDBBtn_Click(object sender, EventArgs e)
        {
            Backup_Database db = new Backup_Database();
            switchPanel(db);
        }

        private void setting_Set_Up_LogoBtn_Click(object sender, EventArgs e)
        {
            Set_Up_Logo set = new Set_Up_Logo();
            switchPanel(set);
        }

        private void loans_CABtn_Click(object sender, EventArgs e)
        {
            Employee_Loan_Cash_Advance cash = new Employee_Loan_Cash_Advance();
            switchPanel(cash);
        }

        private void loans_sssBtn_Click(object sender, EventArgs e)
        {
            Employee_Loan_SSS sss = new Employee_Loan_SSS();
            switchPanel(sss);
        }

        private void loans_hdmfBtn_Click(object sender, EventArgs e)
        {
            Employee_Loan_HDMF hdmf = new Employee_Loan_HDMF();
            switchPanel(hdmf);
        }

        private void loans_CAHistoryBtn_Click(object sender, EventArgs e)
        {
            Employee_Loan_Cash_Advance_History caHistory = new Employee_Loan_Cash_Advance_History();
            switchPanel(caHistory);
        }

        private void loans_sssHistoryBtn_Click(object sender, EventArgs e)
        {
            Employee_Loan_SSS_History sssHistory = new Employee_Loan_SSS_History();
            switchPanel(sssHistory);
        }

        private void loans_hdmfHistoryBtn_Click(object sender, EventArgs e)
        {
            Employee_Loan_HDMF_History hdmfHistory = new Employee_Loan_HDMF_History();
            switchPanel(hdmfHistory);
        }

        private void report_SalaryBtn_Click(object sender, EventArgs e)
        {
            try { Application.OpenForms["Dashboard3"].Dispose(); }
            catch { }
            try { Application.OpenForms["Main_Manual_Payroll"].Dispose(); }
            catch { }
            Salary_Report report = new Salary_Report();
            switchPanel(report);
        }

        private void report_thirteenthBtn_Click(object sender, EventArgs e)
        {
            Thirteen_Month_Report thirteenthReport = new Thirteen_Month_Report();
            switchPanel(thirteenthReport);
        }

        private void setting_HolidayBtn_Click(object sender, EventArgs e)
        {
            Holiday holiday = new Holiday();
            switchPanel(holiday);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Dashboard3"].Dispose();
            
        }
    }
}
