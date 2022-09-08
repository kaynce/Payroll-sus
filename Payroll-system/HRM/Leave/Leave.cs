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
    public partial class Leave : Form
    {
        public Leave()
        {
            InitializeComponent();

            Apply_Leave apply = new Apply_Leave();
            switchPanel(apply);
        }

        private void switchPanel(Form panel)
        {
            this.employee_SwitchingPanel.Controls.Clear();
            panel.TopLevel = false;
            this.employee_SwitchingPanel.Controls.Add(panel);
            panel.Show();
        }

        private void applyLeave_Click(object sender, EventArgs e)
        {
            Apply_Leave apply = new Apply_Leave();
            switchPanel(apply);
        }

        private void manageLeaveBtn_Click(object sender, EventArgs e)
        {
            Manage_Leave manage = new Manage_Leave();
            switchPanel(manage);
        }
    }
}
