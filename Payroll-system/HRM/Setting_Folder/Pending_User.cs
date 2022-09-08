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
    public partial class Pending_User : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        SqlDataAdapter dta = new SqlDataAdapter();

        private string checkMark = "\u2714";

        private int row;

        private string db_user_ID;
        
        public Pending_User()
        {
            InitializeComponent();
            AllMethod();
        }

        private void AllMethod()
        {
            ListOfUsersRetrieveData();
            PendingUsersRetrieveData();
            BlockedUsersRetrieveData();
            ArchiveListRetrieveData();
        }

        private void ArchiveListRetrieveData()
        {
            try
            {
                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Archived_List", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                archiveDataGridView.DataSource = null;
                dta.Fill(dt);

                archiveDataGridView.AutoGenerateColumns = false;
                archiveDataGridView.ColumnCount = 3;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                archiveDataGridView.Columns[0].HeaderText = "Surname"; archiveDataGridView.Columns[0].DataPropertyName = "Surname";

                archiveDataGridView.Columns[1].HeaderText = "Firstname"; archiveDataGridView.Columns[1].DataPropertyName = "Firstname";

                archiveDataGridView.Columns[2].HeaderText = "Type"; archiveDataGridView.Columns[2].DataPropertyName = "Position";

                archiveDataGridView.DataSource = dt;

                //Add first Block button 
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                archiveDataGridView.Columns.Add(btn);
                btn.HeaderText = "Action";
                btn.Name = "Retrieve";
                btn.Text = "Retrieve";
                btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btn.FlatStyle = FlatStyle.Flat;
                //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                btn.DefaultCellStyle.ForeColor = Color.ForestGreen;
                //btn.CellTemplate.Style.BackColor = Color.Green;
                btn.UseColumnTextForButtonValue = true;
                btn.DisplayIndex = 0; //First line



                //
                archiveDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                archiveDataGridView.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                archiveDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                archiveDataGridView.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                archiveDataGridView.Columns[0].Width = 306;
                archiveDataGridView.Columns[1].Width = 306;
                archiveDataGridView.Columns[2].Width = 312;
            }
            catch { }
        }

        private void BlockedUsersRetrieveData()
        {
            try
            {
                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Blocked_List", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                blockListDataGridView.DataSource = null;
                dta.Fill(dt);

                blockListDataGridView.AutoGenerateColumns = false;
                blockListDataGridView.ColumnCount = 3;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                blockListDataGridView.Columns[0].HeaderText = "Surname"; blockListDataGridView.Columns[0].DataPropertyName = "Surname";

                blockListDataGridView.Columns[1].HeaderText = "Firstname"; blockListDataGridView.Columns[1].DataPropertyName = "Firstname";

                blockListDataGridView.Columns[2].HeaderText = "Type"; blockListDataGridView.Columns[2].DataPropertyName = "Position";

                blockListDataGridView.DataSource = dt;

                //Add first Block button 
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                blockListDataGridView.Columns.Add(btn);
                btn.HeaderText = "Action";
                btn.Name = "Retrieve";
                btn.Text = "Retrieve";
                btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btn.FlatStyle = FlatStyle.Flat;
                //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                btn.DefaultCellStyle.ForeColor = Color.ForestGreen;
                //btn.CellTemplate.Style.BackColor = Color.Green;
                btn.UseColumnTextForButtonValue = true;
                btn.DisplayIndex = 0; //First line


                //
                blockListDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                blockListDataGridView.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                blockListDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                blockListDataGridView.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                blockListDataGridView.Columns[0].Width = 306;
                blockListDataGridView.Columns[1].Width = 306;
                blockListDataGridView.Columns[2].Width = 307;
            }
            catch { }
        }

        private void PendingUsersRetrieveData()
        {
            try
            {
                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Pending_Users", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                pendingUsersDataGridView.DataSource = null;
                dta.Fill(dt);

                pendingUsersDataGridView.AutoGenerateColumns = false;
                pendingUsersDataGridView.ColumnCount = 3;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                pendingUsersDataGridView.Columns[0].HeaderText = "Surname"; pendingUsersDataGridView.Columns[0].DataPropertyName = "Surname";

                pendingUsersDataGridView.Columns[1].HeaderText = "Firstname"; pendingUsersDataGridView.Columns[1].DataPropertyName = "Firstname";

                pendingUsersDataGridView.Columns[2].HeaderText = "Type"; pendingUsersDataGridView.Columns[2].DataPropertyName = "Position";

                pendingUsersDataGridView.DataSource = dt;

                //Add first Block button 
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                pendingUsersDataGridView.Columns.Add(btn);
                btn.HeaderText = "Action";
                btn.Name = "Block";
                btn.Text = "Block";
                btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btn.FlatStyle = FlatStyle.Flat;
                //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                btn.DefaultCellStyle.ForeColor = Color.Brown;
                //btn.CellTemplate.Style.BackColor = Color.Green;
                btn.UseColumnTextForButtonValue = true;
                btn.DisplayIndex = 0; //First line

                DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
                pendingUsersDataGridView.Columns.Add(btn2);
                btn2.HeaderText = "";
                btn2.Name = "Approve";
                btn2.Text = "Approve";
                btn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btn2.FlatStyle = FlatStyle.Flat;
                //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                btn2.DefaultCellStyle.ForeColor = Color.ForestGreen;
                //btn2.CellTemplate.Style.BackColor = Color.Green;
                btn2.UseColumnTextForButtonValue = true;
                btn2.DisplayIndex = 1; //Second line



                //
                pendingUsersDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                pendingUsersDataGridView.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                pendingUsersDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                pendingUsersDataGridView.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                pendingUsersDataGridView.Columns[0].Width = 306;
                pendingUsersDataGridView.Columns[1].Width = 306;
                pendingUsersDataGridView.Columns[2].Width = 307;
            }
            catch { }
        }

        private void ListOfUsersRetrieveData()
        {
            try
            {
                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Admin_Users", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                listOfUserDataGridView.DataSource = null;
                dta.Fill(dt);

                listOfUserDataGridView.AutoGenerateColumns = false;
                listOfUserDataGridView.ColumnCount = 3;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                listOfUserDataGridView.Columns[0].HeaderText = "Surname"; listOfUserDataGridView.Columns[0].DataPropertyName = "Surname";

                listOfUserDataGridView.Columns[1].HeaderText = "Firstname"; listOfUserDataGridView.Columns[1].DataPropertyName = "Firstname";

                listOfUserDataGridView.Columns[2].HeaderText = "Type"; listOfUserDataGridView.Columns[2].DataPropertyName = "Position";


                listOfUserDataGridView.DataSource = dt;

                //Add first Delete button 
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                listOfUserDataGridView.Columns.Add(btn);
                btn.HeaderText = "Action";
                btn.Name = "Archive";
                btn.Text = "Archive";
                btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                btn.FlatStyle = FlatStyle.Flat;
                //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                btn.DefaultCellStyle.ForeColor = Color.ForestGreen;
                //btn.CellTemplate.Style.BackColor = Color.Green;
                btn.UseColumnTextForButtonValue = true;
                btn.DisplayIndex = 0; //First line

                //
                listOfUserDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                listOfUserDataGridView.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                listOfUserDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                listOfUserDataGridView.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                listOfUserDataGridView.Columns[0].Width = 306;
                listOfUserDataGridView.Columns[1].Width = 306;
                listOfUserDataGridView.Columns[2].Width = 307;
            }
            catch { }
        }

        private void approveBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void blockBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void archiveBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void refreshBtn1_Click(object sender, EventArgs e)
        {
            ListOfUsersRetrieveData();
        }

        private void refreshBtn2_Click(object sender, EventArgs e)
        {
            PendingUsersRetrieveData();
        }

        private void refreshBtn3_Click(object sender, EventArgs e)
        {
            BlockedUsersRetrieveData();
        }

        private void refreshBtn4_Click(object sender, EventArgs e)
        {
            ArchiveListRetrieveData();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void b_RetrieveBtn_Click(object sender, EventArgs e)
        {
      
        }

        private void archiveDataGridView_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
   
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void pendingUsersDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void blockListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void listOfUserDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {

                    row = listOfUserDataGridView.CurrentCell.RowIndex; //row

                    //row = dataGridView1.SelectedRows.Count;

                    //Get the row number
                    SqlCommand cmd = new SqlCommand("SELECT * FROM P_Admin_Users", con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter dta = new SqlDataAdapter(cmd);

                    listOfUserDataGridView.DataSource = null;
                    dta.Fill(dt);

                    listOfUserDataGridView.AutoGenerateColumns = false;
                    listOfUserDataGridView.ColumnCount = 1;

                    listOfUserDataGridView.Columns[0].HeaderText = "Admin ID"; listOfUserDataGridView.Columns[0].DataPropertyName = "Admin_ID";

                    //dataGridView1.Columns[0].HeaderText = "Position"; dataGridView1.Columns[1].DataPropertyName = "Position";
                    //dataGridView1.Columns[0].HeaderText = "Rate per Day"; dataGridView1.Columns[2].DataPropertyName = "Rate_per_Day";

                    listOfUserDataGridView.DataSource = dt;

                    //To know the row
                    db_user_ID = listOfUserDataGridView.Rows[row].Cells[0].Value.ToString();

                    string surname = string.Empty;
                    string firstname = string.Empty;
                    string position = string.Empty;
                    string username = string.Empty;
                    string password = string.Empty;
                    string status = string.Empty;
                    string question = string.Empty;
                    string answer = string.Empty;

                    SqlCommand cmd_Select = new SqlCommand("SELECT [Surname], [Firstname], [Position], [Username],  [Password], [Status]," +
                    "[Question], [Answer] FROM P_Admin_Users Where Admin_ID = @ID", con);

                    cmd_Select.Parameters.AddWithValue("@ID", db_user_ID);

                    if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                    SqlDataReader dr_Select = cmd_Select.ExecuteReader();

                    while (dr_Select.Read())
                    {
                        surname = dr_Select.GetValue(0).ToString();
                        firstname = dr_Select.GetValue(1).ToString();
                        position = dr_Select.GetValue(2).ToString();
                        username = dr_Select.GetValue(3).ToString();
                        password = dr_Select.GetValue(4).ToString();
                        status = dr_Select.GetValue(5).ToString();
                        question = dr_Select.GetValue(6).ToString();
                        answer = dr_Select.GetValue(7).ToString();
                    }
                    con.Close();
                    //------------------------------------------------------------------------------------------------------------------------------------------------------------                         

                    ListOfUsersRetrieveData();

                    con.Close();

                    if (!string.IsNullOrEmpty(db_user_ID))
                    {
                        con.Close();

                        DialogResult confirm = MessageBox.Show("Are you sure you want to archive '" + surname + " " + firstname + "'?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirm.Equals(DialogResult.Yes))
                        {
                            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                            SqlCommand cmd_Insert = new SqlCommand("INSERT INTO P_Archived_List([Admin_ID], [Surname], [Firstname], [Position]," +
                                                            "[Username], [Password], [Status], [Question], [Answer]) VALUES('" +
                                                            db_user_ID + "','" + surname + "','" + firstname + "','" + position + "','" +
                                                            username + "','" + password + "','" + status + "','" + question + "','" +
                                                            answer + "')", con);

                            cmd_Insert.ExecuteNonQuery();

                            SqlCommand cmd_Delete = new SqlCommand("DELETE FROM P_Admin_Users WHERE Admin_ID = " + db_user_ID, con);

                            cmd_Delete.ExecuteNonQuery();

                            con.Close();
                            ListOfUsersRetrieveData();
                            ArchiveListRetrieveData();

                            db_user_ID = ""; //To delete Admin_ID(value) in variable //db_user_ID .DefaultIfEmpty();

                            MessageBox.Show("User has been successfully archived.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose data in 'List of User(s)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("No data in pending user(s)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void archiveDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
            try
            {
                row = archiveDataGridView.CurrentCell.RowIndex; //row

                //row = dataGridView1.SelectedRows.Count;

                //Get the row number
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Archived_List", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                archiveDataGridView.DataSource = null;
                dta.Fill(dt);

                archiveDataGridView.AutoGenerateColumns = false;
                archiveDataGridView.ColumnCount = 1;

                archiveDataGridView.Columns[0].HeaderText = "Admin ID"; archiveDataGridView.Columns[0].DataPropertyName = "Admin_ID";

                //dataGridView1.Columns[0].HeaderText = "Position"; dataGridView1.Columns[1].DataPropertyName = "Position";
                //dataGridView1.Columns[0].HeaderText = "Rate per Day"; dataGridView1.Columns[2].DataPropertyName = "Rate_per_Day";

                archiveDataGridView.DataSource = dt;

                //To know the row
                db_user_ID = archiveDataGridView.Rows[row].Cells[0].Value.ToString();


                ArchiveListRetrieveData();


                //Get the data---------------------------------------------------------------------------------------------------------------------------------
                string surname = string.Empty;
                string firstname = string.Empty;
                string position = string.Empty;
                string username = string.Empty;
                string password = string.Empty;
                string status = string.Empty;
                string question = string.Empty;
                string answer = string.Empty;

                SqlCommand cmd_Select = new SqlCommand("SELECT [Surname], [Firstname], [Position], [Username], [Password], [Status]," +
                "[Question], [Answer] FROM P_Archived_List Where Admin_ID = @ID", con);

                cmd_Select.Parameters.AddWithValue("@ID", db_user_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_Select = cmd_Select.ExecuteReader();

                while (dr_Select.Read())
                {
                    surname = dr_Select.GetValue(0).ToString();
                    firstname = dr_Select.GetValue(1).ToString();
                    position = dr_Select.GetValue(2).ToString();
                    username = dr_Select.GetValue(3).ToString();
                    password = dr_Select.GetValue(4).ToString();
                    status = dr_Select.GetValue(5).ToString();
                    question = dr_Select.GetValue(6).ToString();
                    answer = dr_Select.GetValue(7).ToString();
                }
                con.Close();


                //Insert Data
                    if (!string.IsNullOrEmpty(db_user_ID))
                    {
                        con.Close();

                        DialogResult confirm = MessageBox.Show("Are you sure you want to retrieve '" + surname + " " + firstname + "' account?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirm.Equals(DialogResult.Yes))
                        {
                            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                            SqlCommand cmd_Insert = new SqlCommand("INSERT INTO P_Admin_Users([Admin_ID], [Surname], [Firstname], [Position]," +
                                                            "[Username], [Password], [Status], [Question], [Answer]) VALUES('" +
                                                            db_user_ID + "','" + surname + "','" + firstname + "','" + position + "','" +
                                                            username + "','" + password + "','" + status + "','" + question + "','" +
                                                            answer + "')", con);
                            cmd_Insert.ExecuteNonQuery();

                            SqlCommand cmd_Delete = new SqlCommand("DELETE FROM P_Archived_List WHERE Admin_ID = " + db_user_ID, con);

                            cmd_Delete.ExecuteNonQuery();

                            con.Close();
                            ListOfUsersRetrieveData();
                            ArchiveListRetrieveData();

                            MessageBox.Show("Account has been successfully retrieved." + checkMark + ".", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose data in 'Archived'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    con.Close();         
            }
            catch
            {
                MessageBox.Show("No data in Archived", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            }
        }

        private void pendingUsersDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3) //Block
            {

                row = pendingUsersDataGridView.CurrentCell.RowIndex; //row

                //row = dataGridView1.SelectedRows.Count;

                //Get the row number
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Pending_Users", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                pendingUsersDataGridView.DataSource = null;
                dta.Fill(dt);

                pendingUsersDataGridView.AutoGenerateColumns = false;
                pendingUsersDataGridView.ColumnCount = 1;

                pendingUsersDataGridView.Columns[0].HeaderText = "Admin ID"; pendingUsersDataGridView.Columns[0].DataPropertyName = "Admin_ID";

                //dataGridView1.Columns[0].HeaderText = "Position"; dataGridView1.Columns[1].DataPropertyName = "Position";
                //dataGridView1.Columns[0].HeaderText = "Rate per Day"; dataGridView1.Columns[2].DataPropertyName = "Rate_per_Day";

                pendingUsersDataGridView.DataSource = dt;

                //To know the row
                db_user_ID = pendingUsersDataGridView.Rows[row].Cells[0].Value.ToString();

                PendingUsersRetrieveData();

                //Get the data---------------------------------------------------------------------------------------------------------------------------------
                string surname = string.Empty;
                string firstname = string.Empty;
                string position = string.Empty;
                string username = string.Empty;
                string password = string.Empty;
                string status = string.Empty;
                string question = string.Empty;
                string answer = string.Empty;

                SqlCommand cmd_Select = new SqlCommand("SELECT [Surname], [Firstname], [Position], [Username], [Password], [Status]," +
                "[Question], [Answer] FROM P_Pending_Users Where Admin_ID = @ID", con);

                cmd_Select.Parameters.AddWithValue("@ID", db_user_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_Select = cmd_Select.ExecuteReader();

                while (dr_Select.Read())
                {
                    surname = dr_Select.GetValue(0).ToString();
                    firstname = dr_Select.GetValue(1).ToString();
                    position = dr_Select.GetValue(2).ToString();
                    username = dr_Select.GetValue(3).ToString();
                    password = dr_Select.GetValue(4).ToString();
                    status = dr_Select.GetValue(5).ToString();
                    question = dr_Select.GetValue(6).ToString();
                    answer = dr_Select.GetValue(7).ToString();
                }
                con.Close();

                 //Insert---------------------------------------------------------------------------------------------------------------------
                    con.Close();

                    if (!string.IsNullOrEmpty(db_user_ID))
                    {
                        con.Close();

                        DialogResult confirm = MessageBox.Show("Are you sure you want to block '" + surname + " " + firstname + "'?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirm.Equals(DialogResult.Yes))
                        {
                            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                            SqlCommand cmd_Insert = new SqlCommand("INSERT INTO P_Blocked_List([Admin_ID], [Surname], [Firstname], [Position]," +
                                                            "[Username], [Password], [Status], [Question], [Answer]) VALUES('" +
                                                            db_user_ID + "','" + surname + "','" + firstname + "','" + position + "','" +
                                                            username + "','" + password + "','" + status + "','" + question + "','" +
                                                            answer + "')", con);

                            cmd_Insert.ExecuteNonQuery();

                            SqlCommand cmd_Delete = new SqlCommand("DELETE FROM P_Pending_Users WHERE Admin_ID = " + db_user_ID, con);

                            cmd_Delete.ExecuteNonQuery();

                            con.Close();
                            PendingUsersRetrieveData();
                            BlockedUsersRetrieveData();

                            db_user_ID = ""; //To delete Admin_ID in variable

                            MessageBox.Show("User has been successfully blocked.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose data in 'Pending User(s)'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    con.Close();
                }

                 if (e.ColumnIndex == 4) // Approve
                 {
                      row = pendingUsersDataGridView.CurrentCell.RowIndex; //row

                //row = dataGridView1.SelectedRows.Count;

                //Get the row number
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Pending_Users", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                pendingUsersDataGridView.DataSource = null;
                dta.Fill(dt);

                pendingUsersDataGridView.AutoGenerateColumns = false;
                pendingUsersDataGridView.ColumnCount = 1;

                pendingUsersDataGridView.Columns[0].HeaderText = "Admin ID"; pendingUsersDataGridView.Columns[0].DataPropertyName = "Admin_ID";

                //dataGridView1.Columns[0].HeaderText = "Position"; dataGridView1.Columns[1].DataPropertyName = "Position";
                //dataGridView1.Columns[0].HeaderText = "Rate per Day"; dataGridView1.Columns[2].DataPropertyName = "Rate_per_Day";

                pendingUsersDataGridView.DataSource = dt;

                //To know the row
                db_user_ID = pendingUsersDataGridView.Rows[row].Cells[0].Value.ToString();

                PendingUsersRetrieveData();

                //Get the data---------------------------------------------------------------------------------------------------------------------------------
                string surname = string.Empty;
                string firstname = string.Empty;
                string position = string.Empty;
                string username = string.Empty;
                string password = string.Empty;
                string status = string.Empty;
                string question = string.Empty;
                string answer = string.Empty;

                SqlCommand cmd_Select = new SqlCommand("SELECT [Surname], [Firstname], [Position], [Username], [Password], [Status]," +
                "[Question], [Answer] FROM P_Pending_Users Where Admin_ID = @ID", con);

                cmd_Select.Parameters.AddWithValue("@ID", db_user_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_Select = cmd_Select.ExecuteReader();

                while (dr_Select.Read())
                {
                    surname = dr_Select.GetValue(0).ToString();
                    firstname = dr_Select.GetValue(1).ToString();
                    position = dr_Select.GetValue(2).ToString();
                    username = dr_Select.GetValue(3).ToString();
                    password = dr_Select.GetValue(4).ToString();
                    status = dr_Select.GetValue(5).ToString();
                    question = dr_Select.GetValue(6).ToString();
                    answer = dr_Select.GetValue(7).ToString();
                }
                con.Close();

                 //Insert--------------------------------------------------------------------------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(db_user_ID))
                {
                    con.Close();

                    DialogResult confirm = MessageBox.Show("Are you sure you want to approve '" + surname + " " + firstname + "' as new User?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm.Equals(DialogResult.Yes))
                    {
                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd_Insert = new SqlCommand("INSERT INTO P_Admin_Users([Admin_ID], [Surname], [Firstname], [Position]," +
                                                        "[Username], [Password], [Status], [Question], [Answer]) VALUES('" +
                                                        db_user_ID + "','" + surname + "','" + firstname + "','" + position + "','" +
                                                        username + "','" + password + "','" + status + "','" + question + "','" +
                                                        answer + "')", con);

                        cmd_Insert.ExecuteNonQuery();

                        SqlCommand cmd_Delete = new SqlCommand("DELETE FROM P_Pending_Users WHERE Admin_ID = " + db_user_ID, con);

                        cmd_Delete.ExecuteNonQuery();

                        con.Close();
                        PendingUsersRetrieveData();
                        ListOfUsersRetrieveData();


                        MessageBox.Show("New User has been approved." + checkMark + ".", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Choose data in 'Pending User(s)'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
        }

        private void blockListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row = blockListDataGridView.CurrentCell.RowIndex; //row

                //row = dataGridView1.SelectedRows.Count;

                //Get the row number
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Blocked_List", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                blockListDataGridView.DataSource = null;
                dta.Fill(dt);

                blockListDataGridView.AutoGenerateColumns = false;
                blockListDataGridView.ColumnCount = 1;

                blockListDataGridView.Columns[0].HeaderText = "Admin ID"; blockListDataGridView.Columns[0].DataPropertyName = "Admin_ID";

                blockListDataGridView.DataSource = dt;

                //To know the row
                db_user_ID = blockListDataGridView.Rows[row].Cells[0].Value.ToString();

                BlockedUsersRetrieveData();

                //Get the data---------------------------------------------------------------------------------------------------------------------------------
                string surname = string.Empty;
                string firstname = string.Empty;
                string position = string.Empty;
                string username = string.Empty;
                string password = string.Empty;
                string status = string.Empty;
                string question = string.Empty;
                string answer = string.Empty;

                SqlCommand cmd_Select = new SqlCommand("SELECT [Surname], [Firstname], [Position], [Username],  [Password],  [Status]," +
                "[Question], [Answer] FROM P_Blocked_List Where Admin_ID = @ID", con);

                cmd_Select.Parameters.AddWithValue("@ID", db_user_ID);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_Select = cmd_Select.ExecuteReader();

                while (dr_Select.Read())
                {
                    surname = dr_Select.GetValue(0).ToString();
                    firstname = dr_Select.GetValue(1).ToString();
                    position = dr_Select.GetValue(2).ToString();
                    username = dr_Select.GetValue(3).ToString();
                    password = dr_Select.GetValue(4).ToString();
                    status = dr_Select.GetValue(5).ToString();
                    question = dr_Select.GetValue(6).ToString();
                    answer = dr_Select.GetValue(7).ToString();
                }
                con.Close();

                //Insert--------------------------------------------------------------------------------------------------------------------------------------------
                if (!string.IsNullOrEmpty(db_user_ID))
                {
                    con.Close();

                    DialogResult confirm = MessageBox.Show("Are you sure you want to unblock '" + surname + " " + firstname + "' ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm.Equals(DialogResult.Yes))
                    {
                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd_Insert = new SqlCommand("INSERT INTO P_Pending_Users([Admin_ID], [Surname], [Firstname], [Position]," +
                                                        "[Username], [Password], [Status], [Question], [Answer]) VALUES('" +
                                                        db_user_ID + "','" + surname + "','" + firstname + "','" + position + "','" +
                                                        username + "','" + password + "','" + status + "','" + question + "','" +
                                                        answer + "')", con);

                        cmd_Insert.ExecuteNonQuery();

                        SqlCommand cmd_Delete = new SqlCommand("DELETE FROM P_Blocked_List WHERE Admin_ID = " + db_user_ID, con);

                        cmd_Delete.ExecuteNonQuery();

                        con.Close();
                        PendingUsersRetrieveData();
                        BlockedUsersRetrieveData();

                        MessageBox.Show("Account has been successfully unblock" + checkMark + ".", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Choose data in 'Pending User(s)'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch
            {
                MessageBox.Show("No data in Blocked List", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
