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
    public partial class Department : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        //private string department_ID = ""; //Not needed
        private string db_Department_Code;

        private string update_DepartmentCode;

        public Department()
        {
            InitializeComponent();
            RetrieveData();
        }

        /*
        public void maxNum() //Not needed
        {
            con.Close();

            SqlCommand cmd_Max = new SqlCommand("SELECT MAX([Department_ID]) from P_Department ", con);

            int ID = 0;
            string value = string.Empty;

            con.Open();

            value = cmd_Max.ExecuteScalar().ToString();

            if (string.IsNullOrEmpty(value))
            {
                value = "D" + ID.ToString("0000000");
                department_ID = value;
            }

            value = value.Substring(1);

            ID = Int32.Parse(value) + 1;
            value = "D" + ID.ToString("D7");
            department_ID = value;

            con.Close();
        }
        */

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 432;
            dataGridView1.Columns[1].Width = 431;
        }

        private void RetrieveData()
        {
            try
            {
                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Department", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 2;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                dataGridView1.Columns[0].HeaderText = "Department Code"; dataGridView1.Columns[0].DataPropertyName = "Department_Code";

                dataGridView1.Columns[1].HeaderText = "Department Name"; dataGridView1.Columns[1].DataPropertyName = "Department_Name";

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

                //Add second Update button 
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

                //
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                autoSize();
            }
            catch { }
        }

        private void ClearTxtBoxes()
        {
            departmentCodeTxt.Clear();
            departmentTxt.Clear();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Close();

                string departCode = departmentCodeTxt.Text.Trim();
                string depart = departmentTxt.Text.Trim();

                //Check if the department is exist  
                string sql = "SELECT * FROM P_Department WHERE Department_Code = @Code";
                SqlCommand cmd_Check = new SqlCommand(sql, con);

                cmd_Check.Parameters.AddWithValue("@Code", departCode);

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dr_Check = cmd_Check.ExecuteReader();

                if (string.IsNullOrEmpty(departCode) || string.IsNullOrEmpty(depart))
                {
                    MessageBox.Show("Add department here.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    departmentTxt.Select();
                }
                else if (!dr_Check.HasRows)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to add?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result.Equals(DialogResult.Yes))
                    {
                        con.Close();

                        //maxNum();

                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        SqlCommand cmd_Department = new SqlCommand("INSERT INTO P_Department([Department_Code]," +
                            "[Department_Name]) VALUES('" + departCode + "','" + depart + "')", con);

                        cmd_Department.ExecuteNonQuery();
                        MessageBox.Show("New Department '" + depart + "' has been created successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearTxtBoxes();
                        RetrieveData();
                    }
                }
                else
                {
                    MessageBox.Show("The department is already exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    string departCode = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    string depart = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                    if (!string.IsNullOrEmpty(departCode) && !string.IsNullOrEmpty(depart))
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result.Equals(DialogResult.Yes))
                        {
                            con.Close();

                            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                            //SqlCommand cmd_Delete = new SqlCommand("DELETE FROM P_Department WHERE Department_Name = " + "THIS", con);

                            //cmd_Delete.ExecuteNonQuery();

                            SqlCommand cmd = con.CreateCommand();

                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "DELETE FROM P_Department WHERE Department_Code = '" + departCode + "'";

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Successfully Deleted.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //department_ID = "";

                            con.Close();
                            ClearTxtBoxes();
                            RetrieveData();

                            departmentCodeTxt.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose department to delete!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    con.Close();
                }

                if (e.ColumnIndex == 3)
                {
                    show_Update.Location = new Point(
                   this.ClientSize.Width / 2 - show_Update.Size.Width / 2,
                   this.ClientSize.Height / 2 - show_Update.Size.Height / 2);
                    show_Update.Anchor = AnchorStyles.None;
                    show_Update.BringToFront();
                    show_Update.Visible = true;

                    update_DepartmentCode = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    update_DepartmentCodeTxt.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    update_DepartmentTxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                }
            
            }
            catch { }                 
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
          

        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearTxtBoxes();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void show_CloseBtn_Click(object sender, EventArgs e)
        {
            show_Update.Visible = false;
        }

        private void show_UpdateBtn_Click(object sender, EventArgs e)
        {
            //try
           // {
                string departCode = update_DepartmentCodeTxt.Text.Trim();
                string depart = update_DepartmentTxt.Text.Trim();

                if (!string.IsNullOrEmpty(departCode) && !string.IsNullOrEmpty(depart))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to update?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result.Equals(DialogResult.Yes))
                    {


                        con.Close();

                        if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                        //SqlCommand cmd_Delete = new SqlCommand("DELETE FROM P_Department WHERE Department_Name = " + "THIS", con);

                        //cmd_Delete.ExecuteNonQuery();

                       /*
                        SqlCommand cmd = con.CreateCommand();

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE P_Department SET Department_Code = '" + departCode +
                                                   "', Department_Name = '" + depart + "' WHERE Department_Code = " + update_DepartmentCode + "";
                        */

                        SqlCommand cmd = con.CreateCommand();

                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "UPDATE P_Department SET Department_Code = '" + departCode +
                            "', Department_Name = '" + depart + "' WHERE Department_Code = '" + update_DepartmentCode + "'";

                        cmd.ExecuteNonQuery();


                        MessageBox.Show("Successfully Updated.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //department_ID = "";

                        con.Close();
                        ClearTxtBoxes();
                        RetrieveData();
                        show_Update.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Choose department to update!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            //}
            //catch { }
        }
    }
}
