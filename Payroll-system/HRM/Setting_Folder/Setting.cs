using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRM
{
    public partial class Setting : Form
    {
        private string db_admin_Position;
        //Form blurForm;

        public Setting(string db_admin_Position)
        {
            InitializeComponent();
            this.db_admin_Position = db_admin_Position;
            //DisableBtn();
            Pending_User pending = new Pending_User();
            switchPanel(pending);
        }

        private void switchPanel(Form panel)
        {
            switchingPanel.Controls.Clear();
            panel.TopLevel = false;
            switchingPanel.Controls.Add(panel);
            panel.Show();
        }

        /*
        private void blurBackground()
        {
            // Need to Top Most = true the Set_Up_Logo Form to work
            //All forms need to Top Most = true;
            Form formBackground = new Form();

            try
            {
                using (blurForm)
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                    formBackground.FormBorderStyle = FormBorderStyle.None;
                    formBackground.Opacity = .80d;
                    formBackground.BackColor = Color.Black;
                    formBackground.WindowState = FormWindowState.Maximized;
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    formBackground.ShowInTaskbar = false;
                    formBackground.Show();

                    blurForm.Owner = formBackground;
                    blurForm.ShowDialog();

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }
        */

        private void button3_Click(object sender, EventArgs e)
        {
    
        }

        private void contributionBtn_Click(object sender, EventArgs e)
        {
  

        }

        private void wagesSettingBtn_Click(object sender, EventArgs e)
        {

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void contributionBtn2_Click(object sender, EventArgs e)
        {
      
        }

        private void pendingUserBtn_Click(object sender, EventArgs e)
        {
            Pending_User pending = new Pending_User();
            switchPanel(pending);
        }

        private void addQuestionBtn_Click(object sender, EventArgs e)
        {
            Add_New_Question question = new Add_New_Question();
            switchPanel(question);
        }

        private void logHistoryBtn_Click(object sender, EventArgs e)
        {
            Admin_Log_History admin = new Admin_Log_History();
            switchPanel(admin);
        }

        private void departmentBtn_Click(object sender, EventArgs e)
        {
            Department department = new Department();
            switchPanel(department);
        }

        private void backupDBtn_Click(object sender, EventArgs e)
        {
            Backup_Database backup = new Backup_Database();
            switchPanel(backup);
        }

        private void setUpLogoBtn_Click(object sender, EventArgs e)
        {
            Set_Up_Logo setUp = new Set_Up_Logo();
            switchPanel(setUp);
        }
    }
}
