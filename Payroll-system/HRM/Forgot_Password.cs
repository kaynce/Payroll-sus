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
    public partial class Forgot_Password : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);
        
        private string db_pass;
        private string db_answer;

        private int count = 1;

        public Forgot_Password()
        {
            InitializeComponent();
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

        private void EnableBtn()
        {
            userTxt.Enabled = false;
            okBtn.Enabled = false;

            questionLb.Enabled = true;
            questionTxt.Enabled = true;

            answerLb.Enabled = true;
            answerTxt.Enabled = true;

            okBtn2.Enabled = true;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string user = userTxt.Text.Trim();

                string sql = "SELECT * FROM P_Admin_Users WHERE Username = @user";

                SqlCommand cmd_Pos = new SqlCommand(sql, con);

                cmd_Pos.Parameters.AddWithValue("@user", user);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_Pos = cmd_Pos.ExecuteReader();

                if (string.IsNullOrEmpty(user))
                {
                    userTxt.Select();
                    MessageBox.Show("Enter your username here.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dr_Pos.HasRows)
                {
                    con.Close();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    string sql2 = "SELECT Password, Question, Answer FROM P_Admin_Users WHERE Username ='" + user + "'";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);

                    SqlDataReader dr;

                    dr = cmd2.ExecuteReader();

                    while (dr.Read())
                    {
                        db_pass = dr["Password"].ToString();
                        questionTxt.Text = dr["Question"].ToString();
                        db_answer = dr["Answer"].ToString();
                    }
                    this.db_answer = db_answer.ToLower();

                    con.Close();
                    panel2.Visible = false;
                    lineShape1.BorderColor = Color.Green;
                    EnableBtn();
                    answerTxt.Select();

                }
                else
                {
                    userTxt.Clear();
                    MessageBox.Show("Sorry we can't find your account.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch {}
        }

        private void okBtn2_Click(object sender, EventArgs e)
        {
            string answer = answerTxt.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(answer))
            {
                answerTxt.Select();
                MessageBox.Show("Enter your answer here.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (count == 3)
            {
                MessageBox.Show("Sorry we can't find your account.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 this.Dispose();
            }
            else if (answer.Equals(db_answer))
            {
                yourPassTxt.Text = db_pass;
                ChangeOpacity_Second();
            }
            else
            {
                count++;
                answerTxt.Clear();
                MessageBox.Show("Sorry, wrong answer!\nPlease try again.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitBtn2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
