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
    public partial class Employees_Choose : Form
    {
        public Employees_Choose()
        {
            InitializeComponent();
            Employee_Attendance attend = new Employee_Attendance();
            switchPanel(attend);
        }

        private void switchPanel(Form panel)
        {
            this.employee_SwitchingPanel.Controls.Clear();
            panel.TopLevel = false;
            this.employee_SwitchingPanel.Controls.Add(panel);
            panel.Show();
        }

        private void attendanceBtn_Click_1(object sender, EventArgs e)
        {
            Employee_Attendance attend = new Employee_Attendance();
            switchPanel(attend);
        }

        private void emplistBtn_Click_1(object sender, EventArgs e)
        {
            Employee_List emp = new Employee_List();
            switchPanel(emp);
        }

        private void schedulesBtn_Click(object sender, EventArgs e)
        {
            Add_Sched sched = new Add_Sched();
            switchPanel(sched);
        }

        private void desigBtn_Click(object sender, EventArgs e)
        {
            Add_Designation desig = new Add_Designation();
            switchPanel(desig);
        }

        private void employeeSchedule_Click(object sender, EventArgs e)
        {
            Employee_Schedule schedule = new Employee_Schedule();
            switchPanel(schedule);
        }

        private void dtrHistoryBtn_Click_1(object sender, EventArgs e)
        {
            Employee_DTR_History history = new Employee_DTR_History();
            switchPanel(history);
        }

        private void archivedBtn_Click_1(object sender, EventArgs e)
        {
            Employee_Archived archived = new Employee_Archived();
            switchPanel(archived);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void attendanceReportBtn_Click(object sender, EventArgs e)
        {
            Employee_Attendance_Report attendance = new Employee_Attendance_Report();
            switchPanel(attendance);
        }
    }
}
