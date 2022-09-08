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
    public partial class Add_Designation : Form
    {
        //function fn = new function();

        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter dta = new SqlDataAdapter();

        private string checkMark = "\u2714";

        //private int count = 0;
        private string db_ID;
        private int row;

        private string update_Position;

        public Add_Designation()
        {
            InitializeComponent();
            RetrieveData();
            positionTxt.Select();
        }

        private void onlyNumbers(object sender, KeyPressEventArgs e) //It will only take numbers
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 299;
            dataGridView1.Columns[1].Width = 282;
            dataGridView1.Columns[2].Width = 282;

            //dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void RetrieveData()
        {
            /*
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //Retrieving data"Payroll_Position"
            string sql = "SELECT * FROM P_Positions";
            cmd = new SqlCommand(sql, con);
           
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            dta = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            
            dataGridView1.DataSource = dt;     
            //dataGridView1.Columns.Remove("ID");
            */
            try
            {
                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Positions", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 3;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                dataGridView1.Columns[0].HeaderText = "Position"; dataGridView1.Columns[0].DataPropertyName = "Position";

                dataGridView1.Columns[1].HeaderText = "Rate per Hour"; dataGridView1.Columns[1].DataPropertyName = "Rate_per_Hour";

                dataGridView1.Columns[2].HeaderText = "Over Time Rate"; dataGridView1.Columns[2].DataPropertyName = "Over_Time_Rate";

                dataGridView1.DataSource = dt;

                //
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                //Add first Update button 
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
                btn2.DisplayIndex = 1; //Second line

                autoSize();
            }
            catch { }
        }

        /*
        private void retrieveData()
        {
            try
            {
                SqlDataAdapter dta = new SqlDataAdapter("SELECT * FROM P_Positions", con);
                DataTable dt = new DataTable();
                dta.Fill(dt);
                dataGridView1.DataSource = dt;
  
                //dataGridView1.Columns.Remove("ID");
                autoSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
         */

        private void Add_Designation_Load(object sender, EventArgs e)
        {
        }

        private void ClearTxtBoxes()
        {
            positionTxt.Clear();
            ratePerHourTxt.Clear();
            overTimeRateTxt.Clear();
            db_ID = "";
        }
        private void clearBtn_Click_1(object sender, EventArgs e)
        {

            // OppositeEnable();

            ClearTxtBoxes();
        }

        private void exitBtn_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void refreshBtn_Click_1(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void ratehourTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyNumbers(sender, e);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string position = positionTxt.Text.Trim();
                string basicRate = ratePerHourTxt.Text.Trim();
                string overTimeRate = overTimeRateTxt.Text.Trim();

                //string date = dateLb.Text;   
                string sql = "SELECT * FROM P_Positions WHERE Position = @Pos";
                SqlCommand cmd_Pos = new SqlCommand(sql, con);

                cmd_Pos.Parameters.AddWithValue("@Pos", position);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_Pos = cmd_Pos.ExecuteReader();

                if (position.Contains("'"))
                {
                    MessageBox.Show("Apostrophe not supported", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (string.IsNullOrEmpty(position) && string.IsNullOrEmpty(basicRate) && string.IsNullOrEmpty(overTimeRate)
                    || string.IsNullOrEmpty(position) || string.IsNullOrEmpty(basicRate) || string.IsNullOrEmpty(overTimeRate))
                {
                    MessageBox.Show("Please put data in every text field.", "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!dr_Pos.HasRows)
                {
                    con.Close();

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    DialogResult confirm = MessageBox.Show("Are you sure you want to save?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        string rate = String.Format("{0:0.00}", Double.Parse(basicRate));
                        string overTime = String.Format("{0:0.00}", Double.Parse(overTimeRate));

                        ClearTxtBoxes();
                        cmd = new SqlCommand("INSERT INTO P_Positions([Position], [Rate_per_Hour], [Over_Time_Rate]) VALUES('" + position + "','" + rate + "','" + overTime + "')", con);
                        cmd.ExecuteNonQuery();
                        RetrieveData();
                        con.Close();
                        db_ID = "";
                        MessageBox.Show("Position added successfullly " + checkMark + ". ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(positionTxt.Text + " is already in the list.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    string position = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                    if (!string.IsNullOrEmpty(position))
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result.Equals(DialogResult.Yes))
                        {
                            update_RatePerHourTxt.Select();

                            if (con.State == ConnectionState.Closed) { con.Open(); }

                            /*
                            SqlCommand cmd = new SqlCommand("DELETE FROM P_Add_Employee_Schedule WHERE Shift_Name = " + shiftName, con);

                            cmd.ExecuteNonQuery();
                            */

                            SqlCommand cmd = con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "DELETE FROM P_Positions WHERE Position = '" + position + "'";

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

                    update_Position = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    update_PositionTxt.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    update_RatePerHourTxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    update_OverTimeRateTxt.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                }
            }
            catch { }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
         
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            show_Update.Visible = false;
        }

        private void update_UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string position = update_PositionTxt.Text.Trim();
                string ratePerHour = update_RatePerHourTxt.Text.Trim();
                string overTimeRate = update_OverTimeRateTxt.Text.Trim();

                if (!string.IsNullOrEmpty(position) && !string.IsNullOrEmpty(ratePerHour) && !string.IsNullOrEmpty(overTimeRate))
                {
                    DialogResult = MessageBox.Show("Are you sure you want to update?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DialogResult.Equals(DialogResult.Yes))
                    {
                        ratePerHour = String.Format("{0:0.00}", Double.Parse(ratePerHour));
                        overTimeRate = String.Format("{0:0.00}", Double.Parse(overTimeRate));

                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd = con.CreateCommand();

                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "UPDATE P_Positions SET Position = '" + position +
                             "', Rate_per_Hour = '" + ratePerHour + "', Over_Time_Rate = '" + 
                             overTimeRate + "' WHERE Position = '" + update_Position + "'";

                        cmd.ExecuteNonQuery();

                        RetrieveData();
                        MessageBox.Show("Successfully Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        ClearTxtBoxes();
                        show_Update.Visible = false;
                    }
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    MessageBox.Show("Choose data to update.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private void update_RatePerHourTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyNumbers(sender, e);
        }

        private void update_OverTimeRateTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyNumbers(sender, e);
        }
    }
}
