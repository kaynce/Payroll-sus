<<<<<<< HEAD:Payroll-system/HRM/Login.cs
﻿using System;
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
    public partial class Login : Form
    {
        //Needed Reference .. System.Configuration

        // SqlConnection con = new SqlConnection(Properties.Settings.Default.);
        //static string conString = @"Data Source=DESKTOP-I9R457E;Initial Catalog=Payroll;Integrated Security=True";
        //private SqlConnection con = new SqlConnection(conString);

        // <add name="db" connectionString="Data Source=SERVER\SQL2017/SQL;AttachDbFilename=|DataDirectory|\DEMO;Integrated Security=True"
        //     providerName="System.Data.SqlClient"/>

        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);
        
        private SqlCommand cmd = new SqlCommand();

        private int clickpassLogo = 0;
        private string logo_ID;
        private string file;
        private static string id_search = "";
        private int count = 1;
        private Boolean match = false;

        //For Login
        private string username;
        private string password;

        //Move Form
        private int move;
        private int moveX;
        private int moveY;

        //admin
        private string db_admin_ID;
        private string db_admin_Surname;
        private string db_admin_Firstname;
        private string db_admin_Position;
        private string statusOn = "ON";
        //string statusOff = "OFF";
       //private string usernameHome;


        public Login()
        {
            InitializeComponent();
            //con.ConnectionString = @"Data Source=DESKTOP-I9R457E;Initial Catalog=Payroll;Integrated Security=True";
            retrievelogoLink();
            loginBtn.Enabled = false;
            showhideBtn.Enabled = false;
            settingBtn.Visible = false;

            //OpacityForm();
            //Employee_LogIn emp = new Employee_LogIn();
            //emp.Show();   

            ChangeOpacity_Second();

        }

        private void ChangeOpacity_Second()
        {
            this.Opacity = .1;
            // timer1.Interval = new TimeSpan(0, 0,);
            opacityTimer.Tick += ChangeOpacity;
            opacityTimer.Start();
        }

        void ChangeOpacity(object sender, EventArgs e)
        {
            this.Opacity += .07;
            if (this.Opacity == 1)
            {
                opacityTimer.Stop();
            }
        }

        /*
        private void OpacityForm() //Not Working
        {
            for (double opacity = 0.0; opacity <= 10.0; opacity += 0.1)
            {
                DateTime start = DateTime.Now;
                this.Opacity = opacity;

                while (DateTime.Now.Subtract(start).TotalMilliseconds <= 40.0)
                {
                    //messagePanel.Visible = true;
                    //messagePanel.BringToFront();
                    Application.DoEvents();
                }
            }
        }
        */

        private void enabledFalse()
        {
            exitBtn.Enabled = false;
            userTxt.Enabled = false;
            passTxt.Enabled = false;
            loginBtn.Enabled = false;
            showhideBtn.Enabled = false;
            max_minBtn.Enabled = false;
            
        }

        //Logo Link
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
                }

                con.Close();
            }
            catch { }
        }

        //Logo Link
        private void retrievelogoLink()
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
                    //pictureBox1.Visible = true;
                    //file = openFileDialog1.FileName;
                    comlogoPic.Image = Image.FromFile(file);
                }

                con.Close();
            }
            catch
            {
                con.Close();
            }
        }


        public string getID()
        {
            return id_search;
        }

        /*
        public void setID(string id_search)
        {
            this.id_search = id_search;
        }
        */
 
        private void clearTxtBoxes()
        {
            userTxt.Text = "";
            passTxt.Text = "";
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //this.Location = Screen.AllScreens[0].WorkingArea.Location;
            userTxt.Select();
            //label3.BackColor = System.Drawing.Color.Red;
            
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            //pictureBox3.Enabled = false;
            //System.Threading.Thread.Sleep(5000);
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseEnter_1(object sender, EventArgs e)
        {
          //button1.BackColor = System.Drawing.Color.SteelBlue;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void showhideBtn_Click_1(object sender, EventArgs e)
        {
            if (passTxt.UseSystemPasswordChar.Equals(true))
            {
                showhideBtn.Text = "Hide";
                passTxt.UseSystemPasswordChar = false;
            }
            else
            {
                showhideBtn.Text = "Show";
                passTxt.UseSystemPasswordChar = true;
            }
        }
 
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //lineShape2.Visible = true;           
                lineShape2.Y2 += 150;

                if (lineShape2.Y2 >= 500)
                {
                    lineShape2.Visible = false;
                    lineShape2.BringToFront();
                }

                panel3.Width += 4;

                SqlDataReader reader = null;
                SqlCommand cmd1 = new SqlCommand("SELECT * FROM P_Admin_Users", con);
                //DataTable tb = new DataTable();

                con.Close();

                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                    reader = cmd1.ExecuteReader();
                }

                try
                {

                    //Error in closing
                    while (reader.Read())
                    {
                        if (username.Equals(reader["Username"].ToString().Trim()) && password.Equals(reader["Password"].ToString().Trim()))
                        {
                            enabledFalse(); //Disable button/textbox

                            timer1.Start();
                            panel3.Visible = true;
                            if (panel3.Width >= panel2.Width)
                            {

                                db_admin_ID = reader["Admin_ID"].ToString().Trim();
                                db_admin_Surname = reader["Surname"].ToString().Trim();
                                db_admin_Firstname = reader["Firstname"].ToString().Trim();
                                db_admin_Position = reader["Position"].ToString().Trim();

                                //MessageBox.Show(db_admin_ID);

                                SqlCommand cmd2 = new SqlCommand("UPDATE P_Admin_Users SET Status = '" + statusOn + "' WHERE Admin_ID = " + db_admin_ID, con);
                                con.Close();
                                con.Open();
                                cmd2.ExecuteNonQuery();

                                timer1.Stop();
                                //match = true;

                                DialogResult confirm = MessageBox.Show("Welcome " + db_admin_Firstname + " " + db_admin_Surname + "!", "Greetings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (confirm.Equals(DialogResult.OK))
                                {
                                    clearTxtBoxes();

                                    /*
                                    Home home = new Home(db_admin_ID, username, db_admin_Position);
                                    this.Hide();
                                    //MessageBox.Show("Welcome " + db_admin_Firstname + " " + db_admin_Surname, "Greetings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    home.ShowDialog();
                                    this.Close(); 
                                    */
                                    string date = DateTime.Now.ToString("MM//dd/yyyy");
                                    SqlCommand cmd_Log_History = new SqlCommand("INSERT INTO P_Admin_Log_History([Admin_ID]," +
                                        "[Date], [Surname], [Firstname], [Time_In])" +
                                        "VALUES('" + db_admin_ID + "','" + date + "','" + db_admin_Surname +
                                        "','" + db_admin_Firstname + "','" + DateTime.Now.ToString("hh:mm tt") + "')", con);

                                    cmd_Log_History.ExecuteNonQuery();

                                    Dashboard2 dash = new Dashboard2(db_admin_ID, username, db_admin_Position, db_admin_Surname, db_admin_Firstname);
                                    this.Hide();

                                    dash.ShowDialog();
                                    this.Close();
                                }
                            }
                        }
                    }
                    con.Close();
                }
                catch { }
            }
            catch { }         
        }
      
        private void movePanel_MouseDown(object sender, MouseEventArgs e)
        {
            //Move Form
            move = 1;
            moveX = e.X;
            moveY = e.Y;
        }
    
        private void movePanel_MouseMove(object sender, MouseEventArgs e)
        {
            //Move Form
            if (move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - moveX, MousePosition.Y - moveY);
            }
        }
 
        private void movePanel_MouseUp(object sender, MouseEventArgs e)
        {
            //Move Form
            move = 0;
        }

        private void exitBtn_Click_1(object sender, EventArgs e)
        {
            //this.Dispose();
            Application.Exit();
          
        }

        private void userTxt_MouseClick(object sender, MouseEventArgs e)
        {
           // panelNav.Height = userTxt.Height;
            //panelNav.Top = userTxt.Top;
        }

        private void passTxt_MouseClick_1(object sender, MouseEventArgs e)
        {
            //panelNav.Height = passTxt.Height;
            //panelNav.Top = passTxt.Top;
        }
        void enabledBtn_Tick(object sender, EventArgs e)
        {
            /*
            if (!string.IsNullOrEmpty(userTxt.Text) && !string.IsNullOrEmpty(passTxt.Text))
            {
                loginBtn.Enabled = true;
            }
            else
            {
                loginBtn.Enabled = false;
            }
            
            if (!string.IsNullOrEmpty(passTxt.Text))
            {
                showhideBtn.Enabled = true;
            }
            else
            {
                showhideBtn.Enabled = false;
            }

            label3.Text = clickpassLogo.ToString();
            if (clickpassLogo > 6)
            {
                clickpassLogo = 0;
            }  
            */
        }

        private void settingBtn_Click(object sender, EventArgs e)
        {
            //settingBtn.Visible = passTxt.Text == "payroll_System" ? true: false;
            clearTxtBoxes();
            Admin_Setting_Account admin = new Admin_Setting_Account();
            admin.ShowDialog();
        }

        private void comlogoPic_Click(object sender, EventArgs e)
        {
   
          
        }

        private void passLogo_Click(object sender, EventArgs e)
        {
            clickpassLogo++;

            if (clickpassLogo >= 7)
            {
                clickpassLogo = 0;
                settingBtn.Visible = true;
            }
            else if (settingBtn.Visible == true)
            {
                settingBtn.Visible = false;
            }
            else
            {
                
            }
        }

        private void hasloginTimer_Tick(object sender, EventArgs e)
        {
           
        }

        private void refreshformBtn_Click(object sender, EventArgs e)
        {
            Login_Load(sender, e);
        }

        private void max_minBtn_Click(object sender, EventArgs e)
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

        private void createAccountLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearTxtBoxes();
            Add_New_User add = new Add_New_User(null, null, null, null, null, null);
            add.ShowDialog();
        }

        private void forgotPassLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearTxtBoxes();
            Forgot_Password forgot = new Forgot_Password();
            forgot.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
       
        }

        private void userTxt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(userTxt.Text) && !string.IsNullOrEmpty(passTxt.Text))
            {
                loginBtn.Enabled = true;
            }
            else
            {
                loginBtn.Enabled = false;
            }

            if (!string.IsNullOrEmpty(passTxt.Text))
            {
                showhideBtn.Enabled = true;
            }
            else
            {
                showhideBtn.Enabled = false;
            }
        }

        private void passTxt_TextChanged(object sender, EventArgs e)
        {
            settingBtn.Visible = passTxt.Text.Equals("the.four.horsemen") ? true : false;

            if (!string.IsNullOrEmpty(userTxt.Text) && !string.IsNullOrEmpty(passTxt.Text))
            {
                loginBtn.Enabled = true;
            }
            else
            {
                loginBtn.Enabled = false;
            }

            if (!string.IsNullOrEmpty(passTxt.Text))
            {
                showhideBtn.Enabled = true;
            }
            else
            {
                showhideBtn.Enabled = false;
            }

            //label3.Text = clickpassLogo.ToString();
            if (clickpassLogo > 6)
            {
                clickpassLogo = 0;
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                con.Open();                  
                SqlCommand cmd = new SqlCommand("SELECT * FROM AUTH WHERE Username = '" + userTxt.Text + "' and Password = '" + passTxt.Text + "'", con);
                cmd.Parameters.Add("Username", SqlDbType.VarChar).Value = userTxt.Text;       
                SqlDataReader reader1 = cmd.ExecuteReader();
                con.Close();              
                */
                username = userTxt.Text.Trim();
                password = passTxt.Text.Trim();

                /*
                SqlCommand cmd = new SqlCommand("select * from LoginCashier where Username = '" & usertxt.Text & "' and Password = '" & passtxt.Text & "'", con);
          
                SqlDataAdapter dta1 = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                dta1.Fill(table);
          
                If table1.Rows.Count <> 0 Then
                */

                SqlDataReader reader = null;
                SqlCommand cmd1 = new SqlCommand("SELECT * FROM P_Admin_Users", con);
                DataTable tb = new DataTable();

                con.Close();
                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                reader = cmd1.ExecuteReader();
                // reader = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                //id_search = "";

                if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter your username and password.", "Enter username and password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Please enter your username.", "Enter your username", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter your password.", "Enter your password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //else if (reader.Read())
                else if (reader.Read()) //(tb.Rows.Count.Equals(0)) 
                {
                    con.Close();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    reader = cmd1.ExecuteReader();

                    while (reader.Read())
                    {
                        if (username.Equals(reader["Username"].ToString().Trim()) && password.Equals(reader["Password"].ToString().Trim()))
                        {
                            //MessageBox.Show("yea");
                            lineShape2.Visible = true;
                            match = true;
                            timer1.Start();
                        }
                    }
                    if (count.Equals(3))
                    {
                        con.Close();
                        if (!match.Equals(true))
                        {
                            MessageBox.Show("Please try again later!", "Goodbye", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.Exit();
                        }
                    }
                    else if (match.Equals(false))
                    {
                        match = false;
                        MessageBox.Show("The username/password you've entered is incorrect.\nPlease try again.", "Incorrect username/password",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clearTxtBoxes();
                        count++;
                    }
                    con.Close();
                }
                else if (count.Equals(3))
                {
                    MessageBox.Show("Please try again later!", "Goodbye", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("The username/password you've entered is incorrect.\nPlease try again.", "Incorrect username/password",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clearTxtBoxes();
                    count++;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            /* Method 3 
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM AUTH WHERE Username = '" + userTxt.Text + "' and Password = '" + passTxt.Text + "'", con);
            cmd2.Parameters.Add("Username", SqlDbType.VarChar).Value = userTxt.Text;
            cmd2.Parameters.Add("Password", SqlDbType.VarChar).Value = passTxt.Text;

            SqlDataAdapter sda = new SqlDataAdapter(cmd2);
            DataTable tb1 = new DataTable();
            sda.Fill(tb1);

            //SqlCommand cmd3 = new SqlCommand("SELECT Password FROM AUTH", con);
           
           //SqlDataReader dr1 = new SqlDataReader() = cmd3.ExecuteReader();

            String username = userTxt.Text.Trim();
            String password = passTxt.Text.Trim();

            SqlCommand cmd = new SqlCommand("SELECT * FROM AUTH", con);

            SqlDataReader reader;
            con.Open();
            reader = cmd.ExecuteReader();

            Boolean match = true;
            while(match)
            //while (reader.Read())
            {
                if (count == 3)
                {
                    MessageBox.Show("Please try again later!", "Goodbye", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (userTxt.Text == "" && passTxt.Text == "")
                {
                    MessageBox.Show("Please enter your username and password.", "Enter username and password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (userTxt.Text == "")
                {
                    MessageBox.Show("Please enter your username.", "Enter your username", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (passTxt.Text == "")
                {
                    MessageBox.Show("Please enter your password.", "Enter your password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else if (username == reader["Username"].ToString().Trim() && password == reader["Password"].ToString().Trim())
                {
                    //id_search = reader["ID"].ToString().Trim();
                    clearTxtBoxes();
                    match = false;
                    new Home().Show();
                }
                else
                {
                    MessageBox.Show("The username/password you've entered is incorrect.\nPlease try again.", "Incorrect username/password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clearTxtBoxes();
                    userTxt.Focus();
                }
            }
            */


            /* Method 2
            SqlCommand cmd1 = new SqlCommand("select * from AUTH where Username = '" + userTxt.Text + "' and Password = '" + passTxt.Text + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM AUTH WHERE Username = '" + userTxt.Text + "' AND Password = '" + passTxt.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                // I have made a new page called home page. If the user is successfully authenticated then the form will be moved to the next form 
                this.Hide();
                new Home().Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
            */

            /* Method 1
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from AUTH";
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (userTxt.Text.Equals(dr["Username"].ToString()) && passTxt.Text.Equals(dr["Password"].ToString()))
                {
                    MessageBox.Show("Login Successfully", "Congrats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Username or password is wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            con.Close();
             */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dashboard2 dash = new Dashboard2(null, null, null, null, null);
            this.Hide();
            dash.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
    
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Main_Manual_Payroll payroll = new Main_Manual_Payroll();
            payroll.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Holiday holiday = new Holiday();
            holiday.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dashboard3 dash = new Dashboard3("");
            dash.Show();
        }
    }       
}
=======
﻿using System;
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
    public partial class Login : Form
    {
        //Needed Reference .. System.Configuration

        // SqlConnection con = new SqlConnection(Properties.Settings.Default.);
        //static string conString = @"Data Source=DESKTOP-I9R457E;Initial Catalog=Payroll;Integrated Security=True";
        //private SqlConnection con = new SqlConnection(conString);

        // <add name="db" connectionString="Data Source=SERVER\SQL2017/SQL;AttachDbFilename=|DataDirectory|\DEMO;Integrated Security=True"
        //     providerName="System.Data.SqlClient"/>

        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);
        
        private SqlCommand cmd = new SqlCommand();

        private int clickpassLogo = 0;
        private string logo_ID;
        private string file;
        private static string id_search = "";
        private int count = 1;
        private Boolean match = false;

        //For Login
        private string username;
        private string password;

        //Move Form
        private int move;
        private int moveX;
        private int moveY;

        //admin
        private string db_admin_ID;
        private string db_admin_Surname;
        private string db_admin_Firstname;
        private string db_admin_Position;
        private string statusOn = "ON";
        //string statusOff = "OFF";
       //private string usernameHome;


        public Login()
        {
            InitializeComponent();
            //con.ConnectionString = @"Data Source=DESKTOP-I9R457E;Initial Catalog=Payroll;Integrated Security=True";
            retrievelogoLink();
            loginBtn.Enabled = false;
            showhideBtn.Enabled = false;
            settingBtn.Visible = false;

            //OpacityForm();
            //Employee_LogIn emp = new Employee_LogIn();
            //emp.Show();   

            ChangeOpacity_Second();

        }

        private void ChangeOpacity_Second()
        {
            this.Opacity = .1;
            // timer1.Interval = new TimeSpan(0, 0,);
            opacityTimer.Tick += ChangeOpacity;
            opacityTimer.Start();
        }

        void ChangeOpacity(object sender, EventArgs e)
        {
            this.Opacity += .07;
            if (this.Opacity == 1)
            {
                opacityTimer.Stop();
            }
        }

        /*
        private void OpacityForm() //Not Working
        {
            for (double opacity = 0.0; opacity <= 10.0; opacity += 0.1)
            {
                DateTime start = DateTime.Now;
                this.Opacity = opacity;

                while (DateTime.Now.Subtract(start).TotalMilliseconds <= 40.0)
                {
                    //messagePanel.Visible = true;
                    //messagePanel.BringToFront();
                    Application.DoEvents();
                }
            }
        }
        */

        private void enabledFalse()
        {
            exitBtn.Enabled = false;
            userTxt.Enabled = false;
            passTxt.Enabled = false;
            loginBtn.Enabled = false;
            showhideBtn.Enabled = false;
            max_minBtn.Enabled = false;
            
        }

        //Logo Link
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
                }

                con.Close();
            }
            catch { }
        }

        //Logo Link
        private void retrievelogoLink()
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
                    //pictureBox1.Visible = true;
                    //file = openFileDialog1.FileName;
                    comlogoPic.Image = Image.FromFile(file);
                }

                con.Close();
            }
            catch
            {
                con.Close();
            }
        }


        public string getID()
        {
            return id_search;
        }

        /*
        public void setID(string id_search)
        {
            this.id_search = id_search;
        }
        */
 
        private void clearTxtBoxes()
        {
            userTxt.Text = "";
            passTxt.Text = "";
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //this.Location = Screen.AllScreens[0].WorkingArea.Location;
            userTxt.Select();
            //label3.BackColor = System.Drawing.Color.Red;
            
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            //pictureBox3.Enabled = false;
            //System.Threading.Thread.Sleep(5000);
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseEnter_1(object sender, EventArgs e)
        {
          //button1.BackColor = System.Drawing.Color.SteelBlue;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void showhideBtn_Click_1(object sender, EventArgs e)
        {
            if (passTxt.UseSystemPasswordChar.Equals(true))
            {
                showhideBtn.Text = "Hide";
                passTxt.UseSystemPasswordChar = false;
            }
            else
            {
                showhideBtn.Text = "Show";
                passTxt.UseSystemPasswordChar = true;
            }
        }
 
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //lineShape2.Visible = true;           
                lineShape2.Y2 += 150;

                if (lineShape2.Y2 >= 500)
                {
                    lineShape2.Visible = false;
                    lineShape2.BringToFront();
                }

                panel3.Width += 4;

                SqlDataReader reader = null;
                SqlCommand cmd1 = new SqlCommand("SELECT * FROM P_Admin_Users", con);
                //DataTable tb = new DataTable();

                con.Close();

                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                    reader = cmd1.ExecuteReader();
                }

                try
                {

                    //Error in closing
                    while (reader.Read())
                    {
                        if (username.Equals(reader["Username"].ToString().Trim()) && password.Equals(reader["Password"].ToString().Trim()))
                        {
                            enabledFalse(); //Disable button/textbox

                            timer1.Start();
                            panel3.Visible = true;
                            if (panel3.Width >= panel2.Width)
                            {

                                db_admin_ID = reader["Admin_ID"].ToString().Trim();
                                db_admin_Surname = reader["Surname"].ToString().Trim();
                                db_admin_Firstname = reader["Firstname"].ToString().Trim();
                                db_admin_Position = reader["Position"].ToString().Trim();

                                //MessageBox.Show(db_admin_ID);

                                SqlCommand cmd2 = new SqlCommand("UPDATE P_Admin_Users SET Status = '" + statusOn + "' WHERE Admin_ID = " + db_admin_ID, con);
                                con.Close();
                                con.Open();
                                cmd2.ExecuteNonQuery();

                                timer1.Stop();
                                //match = true;

                                DialogResult confirm = MessageBox.Show("Welcome " + db_admin_Firstname + " " + db_admin_Surname + "!", "Greetings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (confirm.Equals(DialogResult.OK))
                                {
                                    clearTxtBoxes();

                                    /*
                                    Home home = new Home(db_admin_ID, username, db_admin_Position);
                                    this.Hide();
                                    //MessageBox.Show("Welcome " + db_admin_Firstname + " " + db_admin_Surname, "Greetings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    home.ShowDialog();
                                    this.Close(); 
                                    */
                                    string date = DateTime.Now.ToString("MM//dd/yyyy");
                                    SqlCommand cmd_Log_History = new SqlCommand("INSERT INTO P_Admin_Log_History([Admin_ID]," +
                                        "[Date], [Surname], [Firstname], [Time_In])" +
                                        "VALUES('" + db_admin_ID + "','" + date + "','" + db_admin_Surname +
                                        "','" + db_admin_Firstname + "','" + DateTime.Now.ToString("hh:mm tt") + "')", con);

                                    cmd_Log_History.ExecuteNonQuery();

                                    Dashboard2 dash = new Dashboard2(db_admin_ID, username, db_admin_Position, db_admin_Surname, db_admin_Firstname);
                                    this.Hide();

                                    dash.ShowDialog();
                                    this.Close();
                                }
                            }
                        }
                    }
                    con.Close();
                }
                catch { }
            }
            catch { }         
        }
      
        private void movePanel_MouseDown(object sender, MouseEventArgs e)
        {
            //Move Form
            move = 1;
            moveX = e.X;
            moveY = e.Y;
        }
    
        private void movePanel_MouseMove(object sender, MouseEventArgs e)
        {
            //Move Form
            if (move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - moveX, MousePosition.Y - moveY);
            }
        }
 
        private void movePanel_MouseUp(object sender, MouseEventArgs e)
        {
            //Move Form
            move = 0;
        }

        private void exitBtn_Click_1(object sender, EventArgs e)
        {
            //this.Dispose();
            Application.Exit();
          
        }

        private void userTxt_MouseClick(object sender, MouseEventArgs e)
        {
           // panelNav.Height = userTxt.Height;
            //panelNav.Top = userTxt.Top;
        }

        private void passTxt_MouseClick_1(object sender, MouseEventArgs e)
        {
            //panelNav.Height = passTxt.Height;
            //panelNav.Top = passTxt.Top;
        }
        void enabledBtn_Tick(object sender, EventArgs e)
        {
            /*
            if (!string.IsNullOrEmpty(userTxt.Text) && !string.IsNullOrEmpty(passTxt.Text))
            {
                loginBtn.Enabled = true;
            }
            else
            {
                loginBtn.Enabled = false;
            }
            
            if (!string.IsNullOrEmpty(passTxt.Text))
            {
                showhideBtn.Enabled = true;
            }
            else
            {
                showhideBtn.Enabled = false;
            }

            label3.Text = clickpassLogo.ToString();
            if (clickpassLogo > 6)
            {
                clickpassLogo = 0;
            }  
            */
        }

        private void settingBtn_Click(object sender, EventArgs e)
        {
            //settingBtn.Visible = passTxt.Text == "payroll_System" ? true: false;
            clearTxtBoxes();
            Admin_Setting_Account admin = new Admin_Setting_Account();
            admin.ShowDialog();
        }

        private void comlogoPic_Click(object sender, EventArgs e)
        {
   
          
        }

        private void passLogo_Click(object sender, EventArgs e)
        {
            clickpassLogo++;

            if (clickpassLogo >= 7)
            {
                clickpassLogo = 0;
                settingBtn.Visible = true;
            }
            else if (settingBtn.Visible == true)
            {
                settingBtn.Visible = false;
            }
            else
            {
                
            }
        }

        private void hasloginTimer_Tick(object sender, EventArgs e)
        {
           
        }

        private void refreshformBtn_Click(object sender, EventArgs e)
        {
            Login_Load(sender, e);
        }

        private void max_minBtn_Click(object sender, EventArgs e)
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

        private void createAccountLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearTxtBoxes();
            Add_New_User add = new Add_New_User(null, null, null, null, null, null);
            add.ShowDialog();
        }

        private void forgotPassLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearTxtBoxes();
            Forgot_Password forgot = new Forgot_Password();
            forgot.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
       
        }

        private void userTxt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(userTxt.Text) && !string.IsNullOrEmpty(passTxt.Text))
            {
                loginBtn.Enabled = true;
            }
            else
            {
                loginBtn.Enabled = false;
            }

            if (!string.IsNullOrEmpty(passTxt.Text))
            {
                showhideBtn.Enabled = true;
            }
            else
            {
                showhideBtn.Enabled = false;
            }
        }

        private void passTxt_TextChanged(object sender, EventArgs e)
        {
            settingBtn.Visible = passTxt.Text.Equals("the.four.horsemen") ? true : false;

            if (!string.IsNullOrEmpty(userTxt.Text) && !string.IsNullOrEmpty(passTxt.Text))
            {
                loginBtn.Enabled = true;
            }
            else
            {
                loginBtn.Enabled = false;
            }

            if (!string.IsNullOrEmpty(passTxt.Text))
            {
                showhideBtn.Enabled = true;
            }
            else
            {
                showhideBtn.Enabled = false;
            }

            //label3.Text = clickpassLogo.ToString();
            if (clickpassLogo > 6)
            {
                clickpassLogo = 0;
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                con.Open();                  
                SqlCommand cmd = new SqlCommand("SELECT * FROM AUTH WHERE Username = '" + userTxt.Text + "' and Password = '" + passTxt.Text + "'", con);
                cmd.Parameters.Add("Username", SqlDbType.VarChar).Value = userTxt.Text;       
                SqlDataReader reader1 = cmd.ExecuteReader();
                con.Close();              
                */
                username = userTxt.Text.Trim();
                password = passTxt.Text.Trim();

                /*
                SqlCommand cmd = new SqlCommand("select * from LoginCashier where Username = '" & usertxt.Text & "' and Password = '" & passtxt.Text & "'", con);
          
                SqlDataAdapter dta1 = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                dta1.Fill(table);
          
                If table1.Rows.Count <> 0 Then
                */

                SqlDataReader reader = null;
                SqlCommand cmd1 = new SqlCommand("SELECT * FROM P_Admin_Users", con);
                DataTable tb = new DataTable();

                con.Close();
                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                reader = cmd1.ExecuteReader();
                // reader = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                //id_search = "";

                if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter your username and password.", "Enter username and password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Please enter your username.", "Enter your username", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter your password.", "Enter your password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //else if (reader.Read())
                else if (reader.Read()) //(tb.Rows.Count.Equals(0)) 
                {
                    con.Close();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    reader = cmd1.ExecuteReader();

                    while (reader.Read())
                    {
                        if (username.Equals(reader["Username"].ToString().Trim()) && password.Equals(reader["Password"].ToString().Trim()))
                        {
                            //MessageBox.Show("yea");
                            lineShape2.Visible = true;
                            match = true;
                            timer1.Start();
                        }
                    }
                    if (count.Equals(3))
                    {
                        con.Close();
                        if (!match.Equals(true))
                        {
                            MessageBox.Show("Please try again later!", "Goodbye", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.Exit();
                        }
                    }
                    else if (match.Equals(false))
                    {
                        match = false;
                        MessageBox.Show("The username/password you've entered is incorrect.\nPlease try again.", "Incorrect username/password",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clearTxtBoxes();
                        count++;
                    }
                    con.Close();
                }
                else if (count.Equals(3))
                {
                    MessageBox.Show("Please try again later!", "Goodbye", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("The username/password you've entered is incorrect.\nPlease try again.", "Incorrect username/password",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clearTxtBoxes();
                    count++;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            /* Method 3 
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM AUTH WHERE Username = '" + userTxt.Text + "' and Password = '" + passTxt.Text + "'", con);
            cmd2.Parameters.Add("Username", SqlDbType.VarChar).Value = userTxt.Text;
            cmd2.Parameters.Add("Password", SqlDbType.VarChar).Value = passTxt.Text;

            SqlDataAdapter sda = new SqlDataAdapter(cmd2);
            DataTable tb1 = new DataTable();
            sda.Fill(tb1);

            //SqlCommand cmd3 = new SqlCommand("SELECT Password FROM AUTH", con);
           
           //SqlDataReader dr1 = new SqlDataReader() = cmd3.ExecuteReader();

            String username = userTxt.Text.Trim();
            String password = passTxt.Text.Trim();

            SqlCommand cmd = new SqlCommand("SELECT * FROM AUTH", con);

            SqlDataReader reader;
            con.Open();
            reader = cmd.ExecuteReader();

            Boolean match = true;
            while(match)
            //while (reader.Read())
            {
                if (count == 3)
                {
                    MessageBox.Show("Please try again later!", "Goodbye", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (userTxt.Text == "" && passTxt.Text == "")
                {
                    MessageBox.Show("Please enter your username and password.", "Enter username and password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (userTxt.Text == "")
                {
                    MessageBox.Show("Please enter your username.", "Enter your username", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (passTxt.Text == "")
                {
                    MessageBox.Show("Please enter your password.", "Enter your password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else if (username == reader["Username"].ToString().Trim() && password == reader["Password"].ToString().Trim())
                {
                    //id_search = reader["ID"].ToString().Trim();
                    clearTxtBoxes();
                    match = false;
                    new Home().Show();
                }
                else
                {
                    MessageBox.Show("The username/password you've entered is incorrect.\nPlease try again.", "Incorrect username/password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clearTxtBoxes();
                    userTxt.Focus();
                }
            }
            */


            /* Method 2
            SqlCommand cmd1 = new SqlCommand("select * from AUTH where Username = '" + userTxt.Text + "' and Password = '" + passTxt.Text + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM AUTH WHERE Username = '" + userTxt.Text + "' AND Password = '" + passTxt.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                // I have made a new page called home page. If the user is successfully authenticated then the form will be moved to the next form 
                this.Hide();
                new Home().Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
            */

            /* Method 1
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from AUTH";
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (userTxt.Text.Equals(dr["Username"].ToString()) && passTxt.Text.Equals(dr["Password"].ToString()))
                {
                    MessageBox.Show("Login Successfully", "Congrats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Username or password is wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            con.Close();
             */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dashboard2 dash = new Dashboard2(null, null, null, null, null);
            this.Hide();
            dash.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
    
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Main_Manual_Payroll payroll = new Main_Manual_Payroll();
            payroll.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Holiday holiday = new Holiday();
            holiday.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dashboard3 dash = new Dashboard3("");
            dash.Show();
        }
    }       
}
>>>>>>> 0186f83cf639857a2731db2aee8139ed431c5b86:Payroll-system/Payroll-system/Login.cs
