using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using System.Data.SqlClient;
using System.Configuration;

namespace HRM
{
    public partial class Holiday : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        public Holiday()
        {
            InitializeComponent();
            RetrieveData();
            holidayNameTxt.Select();
        }

        private void ClearTxtBoxes()
        {
            holidayNameTxt.Clear();
            cb_HolidayType.SelectedIndex = -1;
            update_HolidayType.SelectedIndex = -1;
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 315;
            dataGridView1.Columns[1].Width = 315;
            dataGridView1.Columns[2].Width = 308;
        }

       private void RetrieveData()
       {
              try
              {
                  //Bind specific columns
                  SqlCommand cmd = new SqlCommand("SELECT * FROM P_Holiday", con);
                  DataTable dt = new DataTable();
                  SqlDataAdapter dta = new SqlDataAdapter(cmd);

                  dataGridView1.DataSource = null;
                  dta.Fill(dt);

                  dataGridView1.AutoGenerateColumns = false;
                  dataGridView1.ColumnCount = 3;

                  //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                  dataGridView1.Columns[0].HeaderText = "Holiday Name"; dataGridView1.Columns[0].DataPropertyName = "Holiday_Name";

                  dataGridView1.Columns[1].HeaderText = "Holiday Type"; dataGridView1.Columns[1].DataPropertyName = "Holiday_Type";

                  dataGridView1.Columns[2].HeaderText = "Date"; dataGridView1.Columns[2].DataPropertyName = "Holiday_Date";

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

          private void saveBtn_Click(object sender, EventArgs e)
          {
              try
              {
                  con.Close();

                  string holidayName = holidayNameTxt.Text.Trim();
                  string holidayType = cb_HolidayType.Text.Trim();
                  string date = holidayDate.Text.Trim();

                  //Check if the department is exist  
                  string sql = "SELECT * FROM P_Holiday WHERE Holiday_Name = @Holiday";
                  SqlCommand cmd_Check = new SqlCommand(sql, con);

                  cmd_Check.Parameters.AddWithValue("@Holiday", holidayName);

                  if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                  SqlDataReader dr_Check = cmd_Check.ExecuteReader();

                  if (string.IsNullOrEmpty(holidayName) || string.IsNullOrEmpty(holidayType) || string.IsNullOrEmpty(date))
                  {
                      MessageBox.Show("Enter holiday!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
                  else if (!dr_Check.HasRows)
                  {
                      DialogResult result = MessageBox.Show("Are you sure you want to add?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                      if (result.Equals(DialogResult.Yes))
                      {
                          con.Close();

                          //maxNum();

                          if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                          SqlCommand cmd_Department = new SqlCommand("INSERT INTO P_Holiday([Holiday_Name]," +
                              "[Holiday_Type], [Holiday_Date]) VALUES('" + holidayName + "','" + holidayType + "','" + date + "')", con);

                          cmd_Department.ExecuteNonQuery();
                          MessageBox.Show("New Holiday '" + holidayName + "' has been added successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          ClearTxtBoxes();
                          RetrieveData();
                      }
                  }
                  else
                  {
                      MessageBox.Show("The holiday is already exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
                  con.Close();
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Add another apostrophe to the holiday name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
          }

          private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
          {
             // try
              //{
                  if (e.ColumnIndex == 3)
                  {
                      DialogResult confirm = MessageBox.Show("Are you sure you want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                      if (confirm.Equals(DialogResult.Yes))
                      {
                          con.Close();

                          string date = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                          //string checkApostrosphe = holidayName;
                          //int count = 0;

                          /*
                          for (int i = 0; i < checkApostrosphe.Length; i++)
                          {
                              if (checkApostrosphe.ElementAt(i).ToString() == "'")
                             {
                                 //holidayName = checkApostrosphe.ElementAt(i).ToString() + "'";
                                 holidayName = holidayName + "'";
                                  count++;
                             }
                          }
                          MessageBox.Show(holidayName);
                           * */
                          /*
                          if (count % 2 == 0)
                          {

                              //Even
                          }
                          else
                          {
                              holidayName = holidayName + "'";
                              MessageBox.Show(holidayName);
                              //Odd
                          }
                          */
                          if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                          SqlCommand cmd = con.CreateCommand();

                          cmd.CommandType = CommandType.Text;
                          cmd.CommandText = "DELETE FROM P_Holiday WHERE Holiday_Date = '" + date + "'";

                          cmd.ExecuteNonQuery();

                          MessageBox.Show("Successfully Deleted.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                          con.Close();
                          ClearTxtBoxes();
                          RetrieveData();
                          con.Close();
                      }
                  }
              //}
              //catch { }

              try
              {
                  if (e.ColumnIndex == 4)
                  {
                      show_Update.Location = new Point(
                     this.ClientSize.Width / 2 - show_Update.Size.Width / 2,
                     this.ClientSize.Height / 2 - show_Update.Size.Height / 2);
                      show_Update.Anchor = AnchorStyles.None;
                      show_Update.BringToFront();
                      show_Update.Visible = true;

                      update_HolidayNameTxt.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                      update_HolidayType.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                      update_HolidayDate.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                  }
              }
              catch { }
          }

          private void update_UpdateBtn_Click(object sender, EventArgs e)
          {
              try
              {
                  string holidayName = update_HolidayNameTxt.Text.Trim();
                  string holidayType = update_HolidayType.Text.Trim();
                  string date = update_HolidayDate.Text.Trim();
                  string old_Date = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

                  if (!string.IsNullOrEmpty(holidayName) && !string.IsNullOrEmpty(date))
                  {
                      DialogResult result = MessageBox.Show("Are you sure you want to update?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                      if (result.Equals(DialogResult.Yes))
                      {
                          con.Close();

                          if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                          SqlCommand cmd = con.CreateCommand();

                          cmd.CommandType = CommandType.Text;

                          cmd.CommandText = "UPDATE P_Holiday SET Holiday_Date = '" + date +
                              "' WHERE Holiday_Date = '" + old_Date + "'";

                          cmd.ExecuteNonQuery();

                          cmd.CommandText = "UPDATE P_Holiday SET Holiday_Name = '" + holidayName +
                            "', Holiday_Type = '" + holidayType + "' WHERE Holiday_Date = '" + old_Date + "'";

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
              }
              catch (Exception ex)
              {
                  MessageBox.Show("Add another apostrophe to the holiday name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
              }
          }

          private void update_CloseBtn_Click(object sender, EventArgs e)
          {
              show_Update.Visible = false;
          }

          private void clearBtn_Click(object sender, EventArgs e)
          {
              holidayNameTxt.Clear();
              cb_HolidayType.SelectedIndex = -1;
          }
    }
}
