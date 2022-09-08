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
    public partial class Admin_Update : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private string db_admin_ID;
        private string db_pass;

        public Admin_Update(string db_admin_ID)
        {
            InitializeComponent();
            this.db_admin_ID = db_admin_ID;
            retrieveData();
            userTxt.Select();

        }

        private void clearTxtBoxes() 
        {
            userTxt.Clear();
            fnameTxt.Clear();
            surnameTxt.Clear();
            cb_QuestionTxt.SelectedIndex = -1;
            answerTxt.Clear();
            currentPassTxt.Clear();
            newPassTxt.Clear();
            reTypePassTxt.Clear();
        }
        
        private void retrieveData()
        {
            try
            {
                //Retrieve Question in P_Questions_Password
                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlCommand cmd = new SqlCommand("SELECT Questions FROM P_Questions_Password", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cb_QuestionTxt.Items.Add(dr.GetValue(0).ToString());
                }

                con.Close();

                //Retrieve User Data in P_Admin_Users
                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlCommand cmd2 = new SqlCommand("SELECT [Surname], [Firstname], [Username], [Password], [Question], [Answer] FROM P_Admin_Users Where Admin_ID = @ID", con);

                cmd2.Parameters.AddWithValue("@ID", db_admin_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    surnameTxt.Text = dr2.GetValue(0).ToString();
                    fnameTxt.Text = dr2.GetValue(1).ToString();
                    userTxt.Text = dr2.GetValue(2).ToString();
                    db_pass = dr2.GetValue(3).ToString();
                    questionEditTxt.Text = dr2.GetValue(4).ToString();
                    answerTxt.Text = dr2.GetValue(5).ToString();
                    //currentPassTxt.Text = dta.GetValue(3).ToString();
                }
            }
            catch { }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void current_showhideBtn_Click(object sender, EventArgs e)
        {
            if (currentPassTxt.UseSystemPasswordChar.Equals(true))
            {
                current_showhideBtn.Text = "Hide";
                currentPassTxt.UseSystemPasswordChar = false;
            }
            else
            {
                current_showhideBtn.Text = "Show";
                currentPassTxt.UseSystemPasswordChar = true;
            }
        }

        private void new_showhideBtn_Click(object sender, EventArgs e)
        {
            if (newPassTxt.UseSystemPasswordChar.Equals(true))
            {
                new_showhideBtn.Text = "Hide";
                newPassTxt.UseSystemPasswordChar = false;
            }
            else
            {
                new_showhideBtn.Text = "Show";
                newPassTxt.UseSystemPasswordChar = true;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string user = userTxt.Text.Trim();
                string fname = fnameTxt.Text.Trim();
                string surname = surnameTxt.Text.Trim();
                string question1 = cb_QuestionTxt.Text.Trim();
                string question2 = questionEditTxt.Text.Trim();
                string question;
                string answer = answerTxt.Text.Trim();
                string currentPass = currentPassTxt.Text.Trim();
                string newPass = newPassTxt.Text.Trim();
                string reTypePass = reTypePassTxt.Text.Trim();

                if (cb_QuestionTxt.Visible.Equals(true))
                {
                    question = question1;
                }
                else
                {
                    question = question2;
                }

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(surname) ||
                    string.IsNullOrEmpty(answer) || string.IsNullOrEmpty(currentPass) || string.IsNullOrEmpty(newPass) ||
                    string.IsNullOrEmpty(reTypePass))
                {
                    MessageBox.Show("Fill out the empty field/s.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (questionEditTxt.Visible.Equals(true) && string.IsNullOrEmpty(question1))
                {
                    cb_QuestionTxt.Visible = true;
                    cb_QuestionTxt.BringToFront();
                    cb_QuestionTxt.Select();
                    MessageBox.Show("Choose question here.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cb_QuestionTxt.Visible.Equals(true) && string.IsNullOrEmpty(question1))
                {
                    if (cb_QuestionTxt.Visible.Equals(true))
                    {
                        cb_QuestionTxt.BringToFront();
                        cb_QuestionTxt.Select();
                        MessageBox.Show("Choose question here.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (!currentPass.Equals(db_pass))
                {
                    MessageBox.Show("Enter your current passwword.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    currentPassTxt.Clear();
                    currentPassTxt.Select();
                }
                else if (!currentPass.Equals(newPass))
                {
                    DialogResult confirm = MessageBox.Show("Do you want to update your account?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm.Equals(DialogResult.Yes))
                    {
                        con.Close();

                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd = new SqlCommand("UPDATE P_Users_ID SET Username = '" + user +
                            "' WHERE Admin_ID = " + db_admin_ID, con);
                        cmd.ExecuteNonQuery();

                        SqlCommand cmd2 = new SqlCommand("UPDATE P_Admin_Users SET Surname = '" + surname +
                                                        "', Firstname ='" + fname + "', Username ='" + user +
                                                        "', Password ='" + newPass + "', Question ='" + question +
                                                        "', Answer ='" + answer + "' WHERE Admin_ID = " + db_admin_ID, con);

                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Account successfully updated.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        this.Dispose();

                    }
                }
                else if (currentPass.Equals(newPass))
                {
                    MessageBox.Show("New password cannot be the same as current password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    newPassTxt.Clear();
                    reTypePassTxt.Clear();
                    newPassTxt.Select();
                }
                else
                {
                    MessageBox.Show("Field out the empty field/s.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void re_showhideBtn_Click(object sender, EventArgs e)
        {
            if (reTypePassTxt.UseSystemPasswordChar.Equals(true))
            {
                re_showhideBtn.Text = "Hide";
                reTypePassTxt.UseSystemPasswordChar = false;
            }
            else
            {
                re_showhideBtn.Text = "Show";
                reTypePassTxt.UseSystemPasswordChar = true;
            }
        }

        private void newPassTxt_TextChanged(object sender, EventArgs e)
        {
            string currentPass = currentPassTxt.Text.Trim();
            string newPass = newPassTxt.Text.Trim();
            string reTpyePass = reTypePassTxt.Text.Trim();

            if (newPass.Equals(reTpyePass) && !string.IsNullOrEmpty(reTpyePass)) { passMatchLb.Text = "Password Match."; }

            if (!newPass.Equals(reTpyePass)) { passMatchLb.Text = ""; }

            if (newPass.Equals(currentPass) && !string.IsNullOrEmpty(newPass)) { samePassLb.Text = "New password cannot be the same as current password."; }

            if (!newPass.Equals(currentPass)) { samePassLb.Text = ""; }
        }

        private void reTypePassTxt_TextChanged(object sender, EventArgs e)
        {
            string currentPass = currentPassTxt.Text.Trim();
            string newPass = newPassTxt.Text.Trim();
            string reTpyePass = reTypePassTxt.Text.Trim();

            if (newPass.Equals(reTpyePass) && !string.IsNullOrEmpty(reTpyePass)) { passMatchLb.Text = "Password Match."; }

            if (!newPass.Equals(reTpyePass)) { passMatchLb.Text = ""; }

        }

        private void userTxt_TextChanged(object sender, EventArgs e)
        {
            //The username is not available
        }

        private void answerTxtBtn_Click(object sender, EventArgs e)
        {
            if (answerTxt.UseSystemPasswordChar.Equals(true))
            {
                answerTxtBtn.Text = "Hide";
                answerTxt.UseSystemPasswordChar = false;
            }
            else
            {
                answerTxtBtn.Text = "Show";
                answerTxt.UseSystemPasswordChar = true;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            clearTxtBoxes();
        }

        private void editQuestionBtn_Click(object sender, EventArgs e)
        {
            if (questionEditTxt.Visible.Equals(true))
            {
                questionEditTxt.Visible = false;
                cb_QuestionTxt.BringToFront();
                cb_QuestionTxt.Visible = true;
            }
            else
            {
                cb_QuestionTxt.SelectedIndex = -1;
                cb_QuestionTxt.Visible = false;
                questionEditTxt.BringToFront();
                questionEditTxt.Visible = true;
            }
            
        }
    }
}
