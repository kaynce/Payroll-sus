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
    public partial class Add_New_User : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        //private string checkMark = "\u2714";
        private string db_ID;

        //User Info
        private string surname;
        private string fname;
        private string user;
        private string position;
        private string pass;
        private string confirmPass;

        public Add_New_User(string surname, string fname, string user,
                            string position, string pass, string confirmPass)
        {
            InitializeComponent();
            surnameTxt.Select();

            this.surname = surname;
            this.fname = fname;
            this.user = user;
            this.position = position;
            this.pass = pass;
            this.confirmPass = confirmPass;

            RetrieveDataFromValidate();        
        }

        private void RetrieveDataFromValidate() 
        {
            try
            {
                surnameTxt.Text = surname;
                fnameTxt.Text = fname;
                userTxt.Text = user;
                if (position.Equals("Admin"))
                {
                    cb_positionTxt.SelectedIndex = 0;
                }
                if (position.Equals("Sub-Admin"))
                {
                    cb_positionTxt.SelectedIndex = 1;
                }
                passTxt.Text = pass;
                confirmpassTxt.Text = confirmPass;
            }
            catch { }
        }

        private void cleartxtBoxes()
        {
            surnameTxt.Clear();
            fnameTxt.Clear();
            userTxt.Clear();
            passTxt.Clear();
            cb_positionTxt.SelectedIndex = -1;
            confirmpassTxt.Clear();
            passcheckLb.Text = "";
            passMatchLb.Text = "";
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                surname = surnameTxt.Text.Trim();
                fname = fnameTxt.Text.Trim();
                user = userTxt.Text.Trim();
                position = cb_positionTxt.Text.Trim();
                pass = passTxt.Text.Trim();
                confirmPass = confirmpassTxt.Text.Trim();

                string sql = "SELECT * FROM P_Users_ID WHERE Username = @user";

                SqlCommand cmd_Pos = new SqlCommand(sql, con);

                cmd_Pos.Parameters.AddWithValue("@user", user);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_Pos = cmd_Pos.ExecuteReader();

                if (surname.Contains("'") || fname.Contains("'") || user.Contains("'") ||
                    position.Contains("'") || pass.Contains("'") || confirmPass.Contains("'"))
                {
                    MessageBox.Show("Apostrophe not supported", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dr_Pos.HasRows)
                {
                    userTxt.Clear();
                    MessageBox.Show("The username is already taken!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(fname) && !string.IsNullOrEmpty(user) &&
                    !string.IsNullOrEmpty(position) && !string.IsNullOrEmpty(pass) && !string.IsNullOrEmpty(confirmPass))
                {
                    Add_New_User_Validate validate = new Add_New_User_Validate(surname, fname, user,
                                                                               position, pass, confirmPass);
                    validate.ShowDialog();
                    this.Dispose();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Please fill out all the fields.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch { }
        }

        private void current_showhideBtn_Click(object sender, EventArgs e)
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

        private void new_showhideBtn_Click(object sender, EventArgs e)
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

        private void clearBtn_Click(object sender, EventArgs e)
        {
            cleartxtBoxes();
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

        private void passTxt_TextChanged(object sender, EventArgs e)
        {
            string password = passTxt.Text.Trim();
            string confirmPass = confirmpassTxt.Text.Trim();

            if (passTxt.TextLength < 5 && !string.IsNullOrEmpty(password))
            {
                passcheckLb.Text = "The password must atleast 5 characters.";
            }
            else
            {
                passcheckLb.Text = "";
            }

            if (password.Equals(confirmPass) && !string.IsNullOrEmpty(password)) { passMatchLb.Text = "Password Match."; }

            if (!password.Equals(confirmPass)) { passMatchLb.Text = ""; }
        }

        private void passTxt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(passTxt.Text)) { passcheckLb.Text = ""; }
        }

        private void confirmpassTxt_TextChanged(object sender, EventArgs e)
        {
            string password = passTxt.Text.Trim();
            string confirmPass = confirmpassTxt.Text.Trim();

            if (password.Equals(confirmPass) && !string.IsNullOrEmpty(password)) { passMatchLb.Text = "Password Match."; }

            if (!password.Equals(confirmPass)) { passMatchLb.Text = ""; }
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

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void userTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string user = userTxt.Text.Trim();

                string sql = "SELECT * FROM P_Users_ID WHERE Username = @user";

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

