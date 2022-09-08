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
    public partial class Employee_Loan_Choose : Form
    {
        public Employee_Loan_Choose()
        {
            InitializeComponent();
            Employee_Loan_Cash_Advance cash = new Employee_Loan_Cash_Advance();
            switchPanel(cash);
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
            Employee_Loan_Cash_Advance cash = new Employee_Loan_Cash_Advance();
            switchPanel(cash);
        }

        private void emplistBtn_Click(object sender, EventArgs e)
        {
            Employee_Loan_SSS sss = new Employee_Loan_SSS();
            switchPanel(sss);
        }

        private void schedulesBtn_Click(object sender, EventArgs e)
        {
            Employee_Loan_HDMF hdmf = new Employee_Loan_HDMF();
            switchPanel(hdmf);
        }

        private void cashAdvance_HistoryBtn_Click(object sender, EventArgs e)
        {
            Employee_Loan_Cash_Advance_History advance_History = new Employee_Loan_Cash_Advance_History();
            switchPanel(advance_History);
        }

        private void sss_HistoryBtn_Click(object sender, EventArgs e)
        {
            Employee_Loan_SSS_History sss_History = new Employee_Loan_SSS_History();
            switchPanel(sss_History);
        }

        private void hdmfHistoryBtn_Click(object sender, EventArgs e)
        {
            Employee_Loan_HDMF_History hdmf_History = new Employee_Loan_HDMF_History();
            switchPanel(hdmf_History);
        }
    }
}
