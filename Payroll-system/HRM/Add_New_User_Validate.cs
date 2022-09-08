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
using System.Data.SqlClient;
using System.Configuration;

namespace HRM
{
    public partial class Add_New_User_Validate : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private string checkMark = "\u2714";
        private string db_ID;

        //User Info
        private string surname;
        private string fname;
        private string user;
        private string position;
        private string pass;
        private string confirmPass;

        public Add_New_User_Validate(string surname, string fname, string user, 
                                    string position, string pass, string confirmPass)
        {
            InitializeComponent();
            surnameTxt.Text = surname;
            fnameTxt.Text = fname;
            userTxt.Text = user;
            positionTxt.Text = position;
            passTxt.Text = pass;
            confirmpassTxt.Text = confirmPass;
            label4.Select();

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
                        db_ID = ID.ToString("0000000");
                    }
                }
                catch
                {
                    db_ID = ("0000001");
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

        private void cleartxtBoxes()
        {
            surnameTxt.Clear();
            fnameTxt.Clear();
            userTxt.Clear();
            passTxt.Clear();
            positionTxt.Clear();
            confirmpassTxt.Clear();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string surname = surnameTxt.Text.Trim();
                string firstname = fnameTxt.Text.Trim();
                string username = userTxt.Text.Trim();
                string position = positionTxt.Text.Trim();
                string password = passTxt.Text.Trim();
                string confirm_Pass = confirmpassTxt.Text.Trim();

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlCommand cmd1 = new SqlCommand("SELECT * FROM P_Users_ID where Username = '" + username + "'", con);

                SqlDataReader reader = cmd1.ExecuteReader();
                cmd1.Parameters.Add("@username", SqlDbType.VarChar).Value = userTxt.Text;

                if (string.IsNullOrEmpty(userTxt.Text) || string.IsNullOrEmpty(passTxt.Text) || string.IsNullOrEmpty(confirmpassTxt.Text))
                {
                    MessageBox.Show("Please fill up the empty field/s", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (reader.HasRows)
                {
                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    MessageBox.Show("The username '" + username + "' is already taken.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    userTxt.Clear();
                    con.Close();
                }
                else if (passTxt.TextLength < 5)
                {
                    MessageBox.Show("The password must atleast 5 characters.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!password.Equals(confirm_Pass))
                {
                    MessageBox.Show("Password does not match the confirm password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(username) &&
                    !string.IsNullOrEmpty(password) && password.Equals(confirm_Pass) && position.Equals(position))
                {
                    maxNum();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    DialogResult confirm = MessageBox.Show("Are you sure to submit your 'Account Information?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm.Equals(DialogResult.Yes))
                    {
                        cleartxtBoxes();

                        SqlCommand cmd = new SqlCommand("INSERT INTO P_Users_ID([Admin_ID], [Username]) VALUES('" +
                        db_ID + "','" + username + "')", con);
                        cmd.ExecuteNonQuery();

                        SqlCommand cmd2 = new SqlCommand("INSERT INTO P_Pending_Users([Admin_ID], [Surname], [Firstname], [Position], [Username], [Password]) VALUES('" +
                            db_ID + "','" + surname + "','" + firstname + "','" + position + "','" + username + "','" + password + "')", con);
                        cmd2.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("'Account Information' has been submitted." + checkMark + ".\nWait for admin approval.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Please fill up the empty field/s", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch {}
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            surname = surnameTxt.Text;
            fname = fnameTxt.Text;
            user = userTxt.Text;
            position = positionTxt.Text;
            pass = passTxt.Text;
            confirmPass = confirmpassTxt.Text;

            Add_New_User addNew = new Add_New_User(surname, fname, user, position, pass, confirmPass);
            addNew.ShowDialog();
            this.Dispose();
        }

        private void pass_showhideBtn_Click(object sender, EventArgs e)
        {
            if (passTxt.UseSystemPasswordChar == false)
            {
                pass_showhideBtn.Text = "Show";
                passTxt.UseSystemPasswordChar = true;
            }
            else
            {
                pass_showhideBtn.Text = "Hide";
                passTxt.UseSystemPasswordChar = false;
            }
        }

        private void confirm_showhideBtn_Click(object sender, EventArgs e)
        {
            if (confirmpassTxt.UseSystemPasswordChar == false)
            {
                confirm_showhideBtn.Text = "Show";
                confirmpassTxt.UseSystemPasswordChar = true;
            }
            else
            {
                confirm_showhideBtn.Text = "Hide";
                confirmpassTxt.UseSystemPasswordChar = false;
            }
        }
    }
}
