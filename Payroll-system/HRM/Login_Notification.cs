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
    public partial class Login_Notification : Form
    {
        public Login_Notification(string message)
        {
            InitializeComponent();

        }

        public enum AlertType
        {
            success, info, warning, error
        }

        private void Login_Notification_Load(object sender, EventArgs e)
        {
            this.Top = 20;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 20;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            closeTimer.Start();
        }

        private int interval = 0;
        private void animateTimer_Tick(object sender, EventArgs e)
        {
            if (this.Top < 60)
            {
                this.Top += interval;
                interval += 2;
            }
            else
            {
                animateTimer.Stop();
            }
        }

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.05;
            }
            else
            {
                this.Dispose();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int interval = 0;
            textBox1.Text = Int32.Parse(textBox1.Text) + interval.ToString();
            interval++;
        }
    }
}
