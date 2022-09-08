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
    public partial class Add_Sched : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        string checkMark = "\u2714";

        public Add_Sched()
        {
            InitializeComponent();
            RetrieveData();
            shiftNameTxt.Select();
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 299;
            dataGridView1.Columns[1].Width = 282;
            dataGridView1.Columns[2].Width = 282;
            //dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void RetrieveData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Add_Employee_Schedule", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 3;

                dataGridView1.Columns[0].HeaderText = "Shift Type"; dataGridView1.Columns[0].DataPropertyName = "Shift_Name";

                dataGridView1.Columns[1].HeaderText = "Start Time"; dataGridView1.Columns[1].DataPropertyName = "Time_In";

                dataGridView1.Columns[2].HeaderText = "End Time"; dataGridView1.Columns[2].DataPropertyName = "Time_Out";

                //
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                /*
                //This will add button in the last Column
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn);
                //btn.HeaderText = "Click Data";
                btn.Text = "Delete";
                //btn.Name = "btn";
                btn.UseColumnTextForButtonValue = true;
                */

                dataGridView1.DataSource = dt;

                //Add first Delete button 
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn);
                btn.HeaderText = "Action";
                btn.Name = "Delete";
                btn.Text = "Delete";
                btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btn.FlatStyle = FlatStyle.Flat;
                //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                btn.DefaultCellStyle.ForeColor = Color.Brown;
                //btn.CellTemplate.Style.BackColor = Color.Green;
                btn.UseColumnTextForButtonValue = true;
                btn.DisplayIndex = 0; //First line

                //Add second Archive button
                DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn2);
                btn2.HeaderText = "";
                btn2.Name = "Update";
                btn2.Text = "Update";
                btn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btn2.FlatStyle = FlatStyle.Flat;
                //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                btn2.DefaultCellStyle.ForeColor = Color.ForestGreen;
                //btn.CellTemplate.Style.BackColor = Color.Green;
                btn2.UseColumnTextForButtonValue = true;
                btn2.DisplayIndex = 1; //First line

                autoSize();
            }
            catch { }
        }

        private void ClearTxtBoxes()
        {
            shiftNameTxt.Clear();
            //startTimeDate.Clear();
            //endTimeDate.Clear();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearTxtBoxes();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    string shiftName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                    if (!string.IsNullOrEmpty(shiftName))
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result.Equals(DialogResult.Yes))
                        {
                            update_StartTimeDate.Select();

                            if (con.State == ConnectionState.Closed) { con.Open(); }

                            /*
                            SqlCommand cmd = new SqlCommand("DELETE FROM P_Add_Employee_Schedule WHERE Shift_Name = " + shiftName, con);

                            cmd.ExecuteNonQuery();
                            */

                            SqlCommand cmd = con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "DELETE FROM P_Add_Employee_Schedule WHERE Shift_Name = '" + shiftName + "'";

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Successfully Deleted.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            con.Close();
                            ClearTxtBoxes();
                            RetrieveData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose Data to Delete.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (e.ColumnIndex == 4)
                {
                    show_Update.Location = new Point(
                   this.ClientSize.Width / 2 - show_Update.Size.Width / 2,
                   this.ClientSize.Height / 2 - show_Update.Size.Height / 2);
                    show_Update.Anchor = AnchorStyles.None;
                    show_Update.BringToFront();
                    show_Update.Visible = true;

                    update_ShiftNameTxt.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    update_StartTimeDate.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    update_EndTimeDate.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                }
            }
            catch { }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string name = shiftNameTxt.Text.Trim();

                if (!string.IsNullOrEmpty(name))
                {
                    DialogResult confirm = MessageBox.Show("Are you sure you want to add?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm.Equals(DialogResult.Yes))
                    {
                        SqlDataReader reader = null;
                        SqlCommand cmd1 = new SqlCommand("SELECT * FROM P_Add_Employee_Schedule", con);
                        DataTable tb = new DataTable();

                        string shiftName = shiftNameTxt.Text.Trim();
                        string startTime = startTimeDate.Text.Trim();
                        string endTime = endTimeDate.Text.Trim();

                        if (con.State.Equals(ConnectionState.Closed))
                        {
                            con.Open();
                            reader = cmd1.ExecuteReader();
                        }

                        if (!string.IsNullOrEmpty(shiftName) && !string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                        {
                            con.Close();

                            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                            ClearTxtBoxes();
                            SqlCommand cmd = new SqlCommand("INSERT INTO P_Add_Employee_Schedule([Shift_Name], [Time_In], [Time_Out]) VALUES('" + shiftName + "','" + startTime + "','" + endTime + "')", con);
                            cmd.ExecuteNonQuery();
                            RetrieveData();
                            con.Close();
                            MessageBox.Show("New schedule has been added" + checkMark + ".", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Please put data in empty field(s).", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        con.Close();
                    }
                }
            }
            catch { }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void update_CloseBtn_Click(object sender, EventArgs e)
        {
            show_Update.Visible = false;
        }

        private void update_UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string shiftName = update_ShiftNameTxt.Text.Trim();
                string timeIn = update_StartTimeDate.Text.Trim();
                string timeOut = update_EndTimeDate.Text.Trim();

                if (string.IsNullOrEmpty(timeIn) && string.IsNullOrEmpty(timeOut)
                    || string.IsNullOrEmpty(timeIn) || string.IsNullOrEmpty(timeOut))
                {

                }

                if (!string.IsNullOrEmpty(timeIn) && !string.IsNullOrEmpty(timeOut))
                {
                    DialogResult = MessageBox.Show("Do you want to update this data?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DialogResult.Equals(DialogResult.Yes))
                    {
                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        /*
                        cmd = new SqlCommand("UPDATE P_Add_Employee_Schedule SET Shift_Name = '" + shiftName + "', Time_In = '" + timeIn + "', Time_Out = '" + timeOut +
                            "' WHERE ID = " + db_ID, con);
                        cmd.ExecuteNonQuery();
                        */

                        SqlCommand cmd = con.CreateCommand();

                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "UPDATE P_Add_Employee_Schedule SET Time_In = '" + timeIn + "', Time_Out = '" + timeOut + "' WHERE Shift_Name = '" + shiftName + "'";

                        cmd.ExecuteNonQuery();

                        RetrieveData();
                        MessageBox.Show("Successfully Updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        ClearTxtBoxes();
                        show_Update.Visible = false;
                    }
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    MessageBox.Show("Choose data to update.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }
    }
}
