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
    public partial class Manage_Leave : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        DataSet ds;
        //SqlDataAdapter read;


        public Manage_Leave()
        {
            InitializeComponent();
            Binding();
            RetrieveData();
            yearTxt.Text = DateTime.Now.Year.ToString();
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 175;
            dataGridView1.Columns[1].Width = 175;
            dataGridView1.Columns[2].Width = 175;
            dataGridView1.Columns[3].Width = 175;
            dataGridView1.Columns[4].Width = 175;
            dataGridView1.Columns[5].Width = 68;
        }

        private void RetrieveData()
        {
            try
            {
                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                //DataGridView
                SqlCommand cmd_Leave_Manage = new SqlCommand("SELECT * FROM P_Leave_Manage", con);
                DataTable dt_Leave_Manage = new DataTable();
                SqlDataAdapter da_Leave_Manage = new SqlDataAdapter(cmd_Leave_Manage);

                dataGridView1.DataSource = null;
                da_Leave_Manage.Fill(dt_Leave_Manage);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 6;

                dataGridView1.Columns[0].HeaderText = "Employee ID"; dataGridView1.Columns[0].DataPropertyName = "Employee_ID";

                dataGridView1.Columns[1].HeaderText = "Surname"; dataGridView1.Columns[1].DataPropertyName = "Surname";

                dataGridView1.Columns[2].HeaderText = "Firstname"; dataGridView1.Columns[2].DataPropertyName = "Firstname";

                dataGridView1.Columns[3].HeaderText = "Vacation Leave"; dataGridView1.Columns[3].DataPropertyName = "Vacation_Leave";

                dataGridView1.Columns[4].HeaderText = "Sick Leave"; dataGridView1.Columns[4].DataPropertyName = "Sick_Leave";

                dataGridView1.Columns[5].HeaderText = "Year"; dataGridView1.Columns[5].DataPropertyName = "Year";

                dataGridView1.DataSource = dt_Leave_Manage;

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                //Add button
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn);
                btn.HeaderText = "Action";
                btn.Name = "Update";
                btn.Text = "Update";
                btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btn.FlatStyle = FlatStyle.Flat;
                //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                btn.DefaultCellStyle.ForeColor = Color.ForestGreen;
                //btn.CellTemplate.Style.BackColor = Color.Green;
                btn.UseColumnTextForButtonValue = true;
                btn.DisplayIndex = 0; //First line

                autoSize();
            }
            catch { }
        }

        private void ClearTxtBoxes()
        {
            vacationLeaveTxt.Text = "0";
            sickLeaveTxt.Text = "0";
            //yearTxt.Clear();
        }

        private void deleteAllData()
        {
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }

                SqlCommand cmd = new SqlCommand("DELETE FROM P_Leave_Manage", con);

                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch { }
        }

        private void NotRegular()
        {
            try
            {
                int x = 0;
                int totalEmployee = 0;

                //Count the total employee 
                if (con.State == ConnectionState.Closed) { con.Open(); }

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM P_Employee_List ", con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                totalEmployee = count;
                con.Close();

                string[] data = new string[totalEmployee];

                string emp_ID = "";
                string status = "Contractual";

                SqlDataReader reader = null;

                SqlCommand cmd_Check = new SqlCommand("SELECT  *  FROM P_Employee_List", con);

                con.Close();

                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }

                reader = cmd_Check.ExecuteReader();

                while (reader.Read())
                {
                    if (status.Trim() == (reader["Status"].ToString().Trim()))
                    {
                        //emp_ID = reader["Employee_ID"].ToString().Trim();

                        //MessageBox.Show("HERE ", x.ToString());

                        data[x] = reader["Employee_ID"].ToString().Trim();
                    }
                    x++;
                }

                con.Close();

                for (x = 0; x < data.Length; x++)
                {
                    //MessageBox.Show(data[x]);

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    SqlCommand cmd_Delete = con.CreateCommand();

                    cmd_Delete.CommandType = CommandType.Text;

                    cmd_Delete.CommandText = "DELETE FROM P_Leave_Manage WHERE  Employee_ID  = '" + data[x] + "'";

                    cmd_Delete.ExecuteNonQuery();

                    con.Close();
                }
                con.Close();
            }
            catch { }
        }     

        private void Binding()
        {
            try
            {
                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                //To fil the data to dataset
                //SqlDataAdapter da_Leave_Details = new SqlDataAdapter("SELECT * FROM P_Leave_Manage", con);
                SqlDataAdapter da_Employee_list = new SqlDataAdapter("SELECT * FROM P_Employee_List", con);

                ds = new DataSet();

                //da_Leave_Details.Fill(ds, "P_Leave_Manage");
                da_Employee_list.Fill(ds, "P_Employee_List");
                con.Close();
            }
            catch { }
        }
        

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string vacation = vacationLeaveTxt.Value.ToString();
                string sicLeave = sickLeaveTxt.Value.ToString();
                string year = yearTxt.Text.Trim();

                if (Int32.Parse(vacation) != 0 || Int32.Parse(sicLeave) != 0)
                {
                    DialogResult confirm = MessageBox.Show("Are you sure you want to save?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (confirm.Equals(DialogResult.Yes))
                    {
                        deleteAllData(); //Delete all data in Leave_Manage

                        int x = 0;

                        for (x = 0; x <= ds.Tables["P_Employee_List"].Rows.Count - 1; x++)
                        {
                            con.Close();
                            SqlCommand cmd = new SqlCommand();

                            cmd.Connection = con;
                            cmd.CommandText = "INSERT INTO P_Leave_Manage(Employee_ID, Surname, Firstname," +
                                "Vacation_Leave, Sick_Leave, No_Pay, Year) VALUES(@Employee_ID, @Surname, @Firstname, @Vacation_Leave," +
                                "@Sick_Leave, @No_Pay, @Year)";

                            cmd.Parameters.AddWithValue("@Employee_ID", ds.Tables["P_Employee_List"].Rows[x].ItemArray[1].ToString());
                            cmd.Parameters.AddWithValue("@Surname", ds.Tables["P_Employee_List"].Rows[x].ItemArray[3].ToString());
                            cmd.Parameters.AddWithValue("@Firstname", ds.Tables["P_Employee_List"].Rows[x].ItemArray[4].ToString());
                            cmd.Parameters.AddWithValue("@Vacation_Leave", vacation);
                            cmd.Parameters.AddWithValue("@Sick_Leave", sicLeave);
                            cmd.Parameters.AddWithValue("@No_Pay", "0");
                            cmd.Parameters.AddWithValue("@Year", year);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            ClearTxtBoxes();
                        }
                        Binding();
                        NotRegular(); //Delete not regular employee
                        RetrieveData();
                        MessageBox.Show("Leave Registered Successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch { }
        }

        private void cb_empidTxt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            vacationLeaveTxt.Value.ToString(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            sickLeaveTxt.Value.ToString(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            */
            if (e.ColumnIndex == 6)
            {
                show_Update.Location = new Point(
                this.ClientSize.Width / 2 - show_Update.Size.Width / 2,
                this.ClientSize.Height / 2 - show_Update.Size.Height / 2);
                show_Update.Anchor = AnchorStyles.None;
                show_Update.BringToFront();
                show_Update.Visible = true;


                update_Employee_IDTxt.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                update_SurnameTxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                update_FirstnameTxt.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

                string vacation = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                int vacationLeave = Int32.Parse(vacation);
                update_VacationLeaveTxt.Value = vacationLeave;

                string sick = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                int sickLeave = Int32.Parse(sick);
                update_SickLeaveTxt.Value = sickLeave;
            }
        }

        private void update_CloseBtn_Click(object sender, EventArgs e)
        {
            update_VacationLeaveTxt.Value = 0;
            update_SickLeaveTxt.Value = 0;
            show_Update.Visible = false;
        }

        private void update_UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string emp_ID = update_Employee_IDTxt.Text.Trim();
                string vacationLeave = update_VacationLeaveTxt.Value.ToString();
                string sickLeave = update_SickLeaveTxt.Value.ToString();

                DialogResult = MessageBox.Show("Are you sure you want to update?", "Message",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (DialogResult.Equals(DialogResult.Yes))
                {
                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    SqlCommand cmd = new SqlCommand("UPDATE P_Leave_Manage SET Vacation_Leave = '" + vacationLeave +
                                                    "', Sick_Leave = '" + sickLeave + "' WHERE Employee_ID = " + emp_ID, con);

                    cmd.ExecuteNonQuery();

                    con.Close();

                    RetrieveData();

                    MessageBox.Show("Successfully Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    show_Update.Visible = false;
                }
            }
            catch { }
        }
    }
}
