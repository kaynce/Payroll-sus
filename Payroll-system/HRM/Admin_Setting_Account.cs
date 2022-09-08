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
    public partial class Admin_Setting_Account : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private string db_business_ID;

        private string db_admin_ID;
        private string db_admin_User;
        private string db_admin_Pos = "Admin";
        //private string username;
        public Admin_Setting_Account()
        {
            InitializeComponent();
            businessNameTxt.Select();
            RetrieveData();
        }

        public void maxNum()
        {
            try
            {
                int ID;

                con.Close();

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlCommand cmd = new SqlCommand("SELECT MAX([Admin_ID]) FROM P_Users_ID", con);
                SqlDataReader dr = cmd.ExecuteReader();

                try
                {
                    if (dr.Read())
                    {
                        ID = int.Parse(dr[0].ToString()) + 1;
                        db_admin_ID = ID.ToString("0000000");
                    }
                }
                catch
                {
                    db_admin_ID = ("0000001");
                }

                /*
                //Database Code Empty
                if(Convert.IsDBNull(dta))
                {
                    empidTxt.Text = ("000001");
                }
                */
                con.Close();
            }
            catch { }
        }

        private void ChangeOpacity_Second()
        {
            this.Opacity = .1;
            // timer1.Interval = new TimeSpan(0, 0,);
            timer1.Tick += ChangeOpacity;
            timer1.Start();

            messagePanel.Location = new Point(
                this.ClientSize.Width / 2 - messagePanel.Size.Width / 2,
                this.ClientSize.Height / 2 - messagePanel.Size.Height / 2);
            messagePanel.Anchor = AnchorStyles.None;
            messagePanel.BringToFront();
            messagePanel.Visible = true;
        }

        void ChangeOpacity(object sender, EventArgs e)
        {
            this.Opacity += .35;
            if (this.Opacity == 1)
            {
                timer1.Stop();
            }
        }

        //Add Max Number
        private void RetrieveData()
        {
            try
            {
                //Retrieve Admin Data
                string position = "Admin";

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                string sql = "SELECT Admin_ID, Username, Password FROM P_Admin_Users WHERE Position ='" + position + "'";
                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader dta;

                dta = cmd.ExecuteReader();

                while (dta.Read())
                {
                    db_admin_ID = dta["Admin_ID"].ToString();
                    userTxt.Text = dta["Username"].ToString();
                    db_admin_User = dta["Username"].ToString();
                    passTxt.Text = dta["Password"].ToString();
                }
                con.Close();

                //Retrieve Business Name
                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlCommand cmd3 = new SqlCommand("SELECT ID, Business_Name, Address, Contact_Number FROM P_Business_Name", con);
                SqlDataReader dr3 = cmd3.ExecuteReader();

                while (dr3.Read())
                {
                    db_business_ID = dr3["ID"].ToString();
                    businessNameTxt.Text = dr3["Business_Name"].ToString();
                    addressTxt.Text = dr3["Address"].ToString();
                    contactNumberTxt.Text = dr3["Contact_Number"].ToString();

                    //db_business_Name = dr3["Business_Name"].ToString();
                }
                con.Close();
            }
            catch { }
        }

        private void OpacityForm()
        {
            for (double opacity = 0.0; opacity <= 1.0; opacity += 0.2)
            {
                DateTime start = DateTime.Now;
                this.Opacity = opacity;

                while (DateTime.Now.Subtract(start).TotalMilliseconds <= 40.0)
                {
                    messagePanel.Visible = true;
                    messagePanel.BringToFront();
                    Application.DoEvents();
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(db_admin_User))
                {
                    db_admin_User = "";
                }

                string name = businessNameTxt.Text.Trim();
                string address = addressTxt.Text.Trim();
                string contactNum = contactNumberTxt.Text.Trim();
                string user = userTxt.Text.Trim();
                string pass = passTxt.Text.Trim();

                if (name.Contains("'") || address.Contains("'") || contactNum.Contains("'") || user.Contains("'") || pass.Contains("'"))
                {
                    MessageBox.Show("Add another apostrophe!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!string.IsNullOrEmpty(user) || !string.IsNullOrEmpty(pass))
                {
                    string sql_Insert = "SELECT * FROM P_Users_ID WHERE Username = @user";

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }
                    SqlCommand cmd_Insert = new SqlCommand(sql_Insert, con);

                    cmd_Insert.Parameters.AddWithValue("@user", db_admin_User);

                    SqlDataReader dr_Insert = cmd_Insert.ExecuteReader();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    dr_Insert.Read();

                    if (!dr_Insert.HasRows)
                    {
                        maxNum();

                        con.Close(); if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }
                        //Save in P_Admin_Users
                        SqlCommand cmd = new SqlCommand("INSERT INTO P_Users_ID([Admin_ID], [Username]) VALUES('" +
                        db_admin_ID + "','" + user + "')", con);

                        cmd.ExecuteNonQuery();

                        SqlCommand cmd2 = new SqlCommand("INSERT INTO P_Admin_Users([Admin_ID], [Username], [Password], [Position]) VALUES('" +
                        db_admin_ID + "','" + user + "','" + pass + "','" + db_admin_Pos + "')", con);

                        cmd2.ExecuteNonQuery();
                        con.Close();


                        con.Close(); if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }
                        //Save in P_Business_Name
                        SqlCommand cmd3 = new SqlCommand("INSERT INTO P_Business_Name([Business_Name], [Address]," +
                            "[Contact_Number] ) VALUES('" + name + "','" + address + "','" + contactNum + "')", con);

                        cmd3.ExecuteNonQuery();
                        con.Close();

                        ChangeOpacity_Second();
                    }
                    else
                    {
                        con.Close();

                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        //Save in P_Admin_Users
                        con.Close(); if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd = new SqlCommand("UPDATE P_Users_ID SET Username = '" + user +
                            "' WHERE Admin_ID = " + db_admin_ID, con);

                        cmd.ExecuteNonQuery();

                        SqlCommand cmd2 = new SqlCommand("UPDATE P_Admin_Users SET Username = '" + user + "', Password ='" + pass +
                             "' WHERE Admin_ID = " + db_admin_ID, con);

                        cmd2.ExecuteNonQuery();
                        con.Close();

                        //Save in P_Admin_Users
                        con.Close(); if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd3 = new SqlCommand("UPDATE P_Business_Name SET Business_Name = '" + name +
                                                    "', Address = '" + address + "', Contact_Number = '" + contactNum +
                                                    "' WHERE ID = " + db_business_ID, con);

                        cmd3.ExecuteNonQuery();
                        con.Close();

                        ChangeOpacity_Second();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(user))
                    {
                        userTxt.Select();
                        MessageBox.Show("Input your username here.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        passTxt.Select();
                        MessageBox.Show("Input your password here.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                con.Close();
            }
            catch { }
            /*
            try
           {
                con.Close();

                if (string.IsNullOrEmpty(db_admin_User))
                {
                    db_admin_User = "";
                }

                string name = businessNameTxt.Text.Trim();
                string address = addressTxt.Text.Trim();
                string contactNum = contactNumberTxt.Text.Trim();
                string user = userTxt.Text.Trim();
                string pass = passTxt.Text.Trim();

                string sql = "SELECT * FROM P_Users_ID WHERE Username = @user";

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }
                SqlCommand cmd_Pos = new SqlCommand(sql, con);

                cmd_Pos.Parameters.AddWithValue("@user", db_admin_User);

                SqlDataReader dr_Pos = cmd_Pos.ExecuteReader();

                if (name.Contains("'") || address.Contains("'") || contactNum.Contains("'") || user.Contains("'") || pass.Contains("'"))
                {
                    MessageBox.Show("Apostrophe not supported", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dr_Pos.HasRows && user.Equals(db_admin_User))
                {
                    MessageBox.Show("The username is already taken!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!string.IsNullOrEmpty(user) || !string.IsNullOrEmpty(pass))
                {
                    con.Close();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    dr_Pos = cmd_Pos.ExecuteReader();

                    if (dr_Pos.Read() && dr_Pos.HasRows)
                    {
                        //Save in P_Admin_Users
                        con.Close(); if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd = new SqlCommand("UPDATE P_Users_ID SET Username = '" + user +
                            "' WHERE Admin_ID = " + db_admin_ID, con);

                        cmd.ExecuteNonQuery();

                        SqlCommand cmd2 = new SqlCommand("UPDATE P_Admin_Users SET Username = '" + user + "', Password ='" + pass +
                             "' WHERE Admin_ID = " + db_admin_ID, con);

                        cmd2.ExecuteNonQuery();
                        con.Close();

                        //Save in P_Admin_Users
                        con.Close(); if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd3 = new SqlCommand("UPDATE P_Business_Name SET Business_Name = '" + name +
                                                    "', Address = '" + address + "', Contact_Number = '" + contactNum +
                                                    "' WHERE ID = " + db_admin_ID, con);

                        cmd3.ExecuteNonQuery();
                        con.Close();

                        ChangeOpacity_Second();
                    }

                    con.Close();

                    string sql2 = "SELECT * FROM P_Users_ID WHERE Username = @user";

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }
                    SqlCommand cmd_Pos2 = new SqlCommand(sql2, con);

                    cmd_Pos2.Parameters.AddWithValue("@user", db_admin_User);

                    SqlDataReader dr_Pos2 = cmd_Pos2.ExecuteReader();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    if (!dr_Pos2.HasRows)
                        {
                            maxNum();

                            
                            con.Close(); if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }
                            //Save in P_Admin_Users
                            SqlCommand cmd = new SqlCommand("INSERT INTO P_Users_ID([Admin_ID], [Username]) VALUES('" +
                            db_admin_ID + "','" + user + "')", con);

                            cmd.ExecuteNonQuery();

                            SqlCommand cmd2 = new SqlCommand("INSERT INTO P_Admin_Users([Admin_ID], [Username], [Password], [Position]) VALUES('" +
                            db_admin_ID + "','" + user + "','" + pass + "','" + db_admin_Pos + "')", con);

                            cmd2.ExecuteNonQuery();
                            con.Close();

                           
                            con.Close(); if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }
                            //Save in P_Business_Name
                            SqlCommand cmd3 = new SqlCommand("INSERT INTO P_Business_Name([Business_Name], [Address]," +
                                "[Contact_Number] ) VALUES('" + name + "','" + address + "','" + contactNum + "')", con);

                            cmd3.ExecuteNonQuery();
                            con.Close();

                            ChangeOpacity_Second();
                        }               
                }
                else
                {
                    if (string.IsNullOrEmpty(user))
                    {
                        userTxt.Select();
                        MessageBox.Show("Input your username here.", "Username", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        passTxt.Select();
                        MessageBox.Show("Input your password here.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                con.Close();
            }
           catch
           {
             MessageBox.Show("Apostrophe not supported", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
           */

        }

        private void showhideBtn_Click(object sender, EventArgs e)
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

        private void okBtn_Click(object sender, EventArgs e)
        {
            //messagePanel.Visible = false;
            this.Dispose();
        }

        private void opacityTimer_Tick(object sender, EventArgs e)
        {

        }

        private void Admin_Setting_Account_Load(object sender, EventArgs e)
        {

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void userTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string user = userTxt.Text.Trim();

                string sql = "SELECT * FROM P_Admin_Users WHERE Username = @user";

                SqlCommand cmd_Pos = new SqlCommand(sql, con);

                cmd_Pos.Parameters.AddWithValue("@user", user);


                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_Pos = cmd_Pos.ExecuteReader();

                if (dr_Pos.HasRows)
                {
                    userCheckLb.Text = "The username is already taken.";
                }
                else
                {
                    userCheckLb.Text = "";
                }
                con.Close();
            }
            catch { }
        }
    }
}
