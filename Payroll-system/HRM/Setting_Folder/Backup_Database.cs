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
//using System.Speech.Systhesis;

namespace HRM
{
    public partial class Backup_Database : Form
    {
        //Needed references for progress bar
        //Microsoft.SqlServer.ConnectionInfo
        //Microsoft.SqlServer.ConnectionInfoExtended
        //Microsoft.SqlServer.Smo
        //Microsoft.SqlServer.SmoExtended
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);
        //static string conString = @"Data Source=DESKTOP-I9R457E;Initial Catalog=Payroll;Integrated Security=True";
        //private SqlConnection con = new SqlConnection(conString);
        
        public Backup_Database()
        {
            InitializeComponent();
            //progressBar1.Style = ProgressBarStyle.Marquee;

        }

        private void ClearTxtBoxes()
        {
            backupTxt.Clear();
            restoreTxt.Clear();
        }

        private void backup_BrowseLocationBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Browse location where do you wish to backup the database
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    backupTxt.Text = fbd.SelectedPath;
                    backup_TakeBtn.Enabled = true;
                }
            }
            catch { }
        }

        private void backup_TakeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to backup database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (confirm.Equals(DialogResult.Yes))
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }

                    string database = con.Database.ToString();

                    if (!string.IsNullOrEmpty(backupTxt.Text))
                    {
                        string sql_Backup = "BACKUP DATABASE [" + database + "] TO DISK='" + backupTxt.Text + "\\" + "Database" + "-" + DateTime.Now.ToString("MM-dd-yyyy") + ".back'";
                        SqlCommand cmd = new SqlCommand(sql_Backup, con);
                        cmd.ExecuteNonQuery();

                        ClearTxtBoxes();
                        MessageBox.Show("Database has been backup successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Please browse location!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    con.Close();
                }    
            }
            catch { }
        }

        private void restore_BrowseLocationBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //s.Speak("Please enter the valid restore file location");
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "SQL SERVER database backup files|*.back";
                ofd.Title = "Database Restore";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    restoreTxt.Text = ofd.FileName;
                    restore_DatabaseBtn.Enabled = true;
                }
            }
            catch { }
        }

        private void restore_DatabaseBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to restore database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (confirm.Equals(DialogResult.Yes))
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }

                    string database = con.Database.ToString();

                    string sql_1 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

                    SqlCommand cmd_1 = new SqlCommand(sql_1, con);
                    cmd_1.ExecuteNonQuery();

                    string sql_2 = string.Format("USE MASTER RESTORE DATABASE [" + database + "] FROM DISK = '" + restoreTxt.Text + "' WITH REPLACE;");

                    SqlCommand cmd_2 = new SqlCommand(sql_2, con);
                    cmd_2.ExecuteNonQuery();

                    string sql_3 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                    SqlCommand cmd_3 = new SqlCommand(sql_3, con);
                    cmd_3.ExecuteNonQuery();

                    con.Close();

                    ClearTxtBoxes();

                    //s.Speak("Database restored successfully")
                    MessageBox.Show("Database restored successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
