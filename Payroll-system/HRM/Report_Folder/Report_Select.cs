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
    public partial class Report_Select : Form
    {
        public Report_Select()
        {
            InitializeComponent();
            Salary_Report salary = new Salary_Report();
            switchPanel(salary);
        }

        private void switchPanel(Form panel)
        {
            this.employee_SwitchingPanel.Controls.Clear();
            panel.TopLevel = false;
            this.employee_SwitchingPanel.Controls.Add(panel);
            panel.Show();
        }

        private void cashAdvanceBtn_Click(object sender, EventArgs e)
        {
            Salary_Report salary = new Salary_Report();
            switchPanel(salary);
        }

        private void emplistBtn_Click(object sender, EventArgs e)
        {
            Thirteen_Month_Report report = new Thirteen_Month_Report();
            switchPanel(report);
        }
    }
}
