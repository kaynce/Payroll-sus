using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//New Import
using System.Data.SqlClient;
using System.Configuration;

namespace HRM
{
    public partial class Employee_Loan_HDMF : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private string zero; //It will leave zero :zeroLeave() 
        private string blank;

        private string zeroTax; //It will leave zero in Tax:zeroLeaveTax() 
        private string blankTax;

        private List<string> data_Employee_ID = new List<string>();  //For Employee ID

        public Employee_Loan_HDMF()
        {
            InitializeComponent();
            RetrieveData();
            dateTxt.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void NotRegular()
        {
            try
            {
                int count = 0;
                //Count the total employee 
                if (con.State == ConnectionState.Closed) { con.Open(); }

                SqlCommand cmd_Select = new SqlCommand("SELECT COUNT(*) FROM P_Employee_List ", con);
                count = Convert.ToInt32(cmd_Select.ExecuteScalar());

                con.Close();

                //string[] data = new string[totalEmployee];

                string emp_ID = "";
                string status = "Regular";

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
                        data_Employee_ID.Add(reader["Employee_ID"].ToString().Trim());
                    }
                }
                con.Close();
            }
            catch { }
        }

        private void eraseZeroTax() // Erase zero in textbox
        {
            if (blankTax.Equals("0.00"))
            {
                blankTax = "";
            }
        }

        private void leaveZeroTax() //It will leave zero : zero
        {
            try
            {
                if (!string.IsNullOrEmpty(zeroTax) || Int32.Parse(zeroTax) > 0)
                {
                }
                else
                {
                    zeroTax = "0.00";
                }
            }
            catch (Exception)
            {
                zeroTax = "0.00";
            }
        }

        private void eraseZero() // Erase zero in textbox
        {
            if (blank.Equals("0"))
            {
                blank = "";
            }
        }

        private void leaveZero() //It will leave zero : zero
        {
            try
            {
                if (!string.IsNullOrEmpty(zero) || Int32.Parse(zero) > 0)
                {
                }
                else
                {
                    zero = "0";
                }
            }
            catch (Exception)
            {
                zero = "0";
            }
        }

        private void onlyNumbers(object sender, KeyPressEventArgs e) //It will only take numbers
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void EnableBtn()
        {
            hdmfTxt.Enabled = true;
            surnameTxt.Enabled = true;
            fnameTxt.Enabled = true;
            loanAmountTxt.Enabled = true;
            monthlyAmortTxt.Enabled = true;
            semi_MonthlyAmortTxt.Enabled = true;
            balanceTxt.Enabled = true;
        }

        private void ClearTxtBoxes()
        {
            hdmfTxt.Text = "0";
            surnameTxt.Clear();
            fnameTxt.Clear();
            loanAmountTxt.Text = "0.00";
            monthlyAmortTxt.Text = "0.00";
            semi_MonthlyAmortTxt.Text = "0.00";
            balanceTxt.Text = "0.00";
        }

        private void autoSize()
        {
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 150;
            dataGridView1.Columns[7].Width = 150;
            dataGridView1.Columns[8].Width = 150;
        }

        private void RetrieveData()
          {
              try
              {
                  con.Close();

                  if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                  NotRegular(); //Remove not regular Employee ID

                  string sql = "SELECT Employee_ID FROM P_Employee_List";
                  SqlCommand cmd_Fill = new SqlCommand(sql, con);
                  DataTable dt_Fill = new DataTable();

                  SqlDataAdapter da_Fill = new SqlDataAdapter(cmd_Fill);
                  da_Fill.Fill(dt_Fill);

                  // cmd.ExecuteNonQuery();
                  cb_empidTxt.Items.Clear();

                  // for (int x = 0; x < data_Employee_ID.Count(); x++)
                  // {
                  foreach (DataRow dr_Fill in dt_Fill.Rows)
                  {
                      if (data_Employee_ID.Contains(dr_Fill["Employee_ID"].ToString()))
                      {
                          cb_empidTxt.Items.Add("              " + dr_Fill["Employee_ID"].ToString());
                      }
                  }
                  // }
                  con.Close();

                  //Bind specific columns
                  SqlCommand cmd = new SqlCommand("SELECT * FROM P_Employee_Loan_HDMF", con);
                  DataTable dt = new DataTable();
                  SqlDataAdapter dta = new SqlDataAdapter(cmd);

                  //dataGridView1.ForeColor = Color.White;

                  dataGridView1.EnableHeadersVisualStyles = false;

                  dataGridView1.DataSource = null;
                  dta.Fill(dt);

                  dataGridView1.AutoGenerateColumns = false;
                  dataGridView1.ColumnCount = 9;

                  //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                  dataGridView1.Columns[0].HeaderText = "Date"; dataGridView1.Columns[0].DataPropertyName = "Date";

                  dataGridView1.Columns[1].HeaderText = "Employee ID"; dataGridView1.Columns[1].DataPropertyName = "Employee_ID";

                  dataGridView1.Columns[2].HeaderText = "HDMF No."; dataGridView1.Columns[2].DataPropertyName = "HDMF_No";

                  dataGridView1.Columns[3].HeaderText = "Surname"; dataGridView1.Columns[3].DataPropertyName = "Surname";

                  dataGridView1.Columns[4].HeaderText = "Firstname"; dataGridView1.Columns[4].DataPropertyName = "Firstname";

                  dataGridView1.Columns[5].HeaderText = "Loan Amount"; dataGridView1.Columns[5].DataPropertyName = "Loan_Amount";

                  dataGridView1.Columns[6].HeaderText = "Monthly Amortization"; dataGridView1.Columns[6].DataPropertyName = "Monthly_Amort";

                  dataGridView1.Columns[7].HeaderText = "Semi-Monthly Amortization"; dataGridView1.Columns[7].DataPropertyName = "Semi_Monthly_Amort";

                  dataGridView1.Columns[8].HeaderText = "Balance"; dataGridView1.Columns[8].DataPropertyName = "Balance";

                  dataGridView1.DataSource = dt;

                  /*
                  //Add first Block button 
                  DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                  dataGridView1.Columns.Add(btn);
                  btn.HeaderText = "Action";
                  btn.Name = "Update";
                  btn.Text = "Update";
                  btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                  btn.FlatStyle = FlatStyle.Flat;
                  //btn.DefaultCellStyle.Font = new Font("SEGUI UI", 10);
                  btn.DefaultCellStyle.ForeColor = Color.Brown;
                  //btn.CellTemplate.Style.BackColor = Color.Green;
                  btn.UseColumnTextForButtonValue = true;
                  btn.DisplayIndex = 0; //First line
                  */

                  dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                  dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                  dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                  dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                  dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                  //dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold); //Header Design


                  autoSize();

                  con.Close();
              }
              catch { }
          }

          private void cb_empidTxt_SelectedIndexChanged(object sender, EventArgs e)
          {
              try
              {
                  if (!string.IsNullOrEmpty(cb_empidTxt.Text.Trim()))
                  {
                      string emp_ID = cb_empidTxt.Text.Trim();

                      SqlCommand cmd2 = new SqlCommand("SELECT [HDMF], [Surname], [Firstname] FROM P_Employee_List Where Employee_ID = @ID", con);

                      cmd2.Parameters.AddWithValue("@ID", emp_ID);

                      if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                      SqlDataReader dta2 = cmd2.ExecuteReader();

                      while (dta2.Read())
                      {
                          hdmfTxt.Text = dta2.GetValue(0).ToString();
                          surnameTxt.Text = dta2.GetValue(1).ToString();
                          fnameTxt.Text = dta2.GetValue(2).ToString();
                      }
                      con.Close();
                      EnableBtn(); //Enable button
                  }
                  else
                  {
                      hdmfTxt.Enabled = false;
                      surnameTxt.Enabled = false;
                      fnameTxt.Enabled = false;
                      loanAmountTxt.Enabled = false;
                      monthlyAmortTxt.Enabled = false;
                      semi_MonthlyAmortTxt.Enabled = false;
                      balanceTxt.Enabled = false;

                      ClearTxtBoxes();
                  }
              }
              catch { }
          }

          private void saveBtn_Click(object sender, EventArgs e)
          {
              try
              {
                  string emp_ID = cb_empidTxt.Text.Trim();
                  string hdmf = hdmfTxt.Text.Trim();
                  string surname = surnameTxt.Text.Trim();
                  string fname = fnameTxt.Text.Trim();
                  string loanAmount = String.Format("{0:0.00}", Double.Parse(loanAmountTxt.Text.Trim()));
                  string monthlyAmort = monthlyAmortTxt.Text.Trim();
                  string semi_Amort = semi_MonthlyAmortTxt.Text.Trim();
                  string balance = balanceTxt.Text.Trim();
                  string date = dateTxt.Text.Trim();

                  SqlDataReader reader = null;
                  SqlCommand cmd_Check = new SqlCommand("SELECT * FROM P_Employee_Loan_HDMF", con);

                  if (con.State.Equals(ConnectionState.Closed))
                  {
                      con.Open();
                      reader = cmd_Check.ExecuteReader();
                  }

                  if (!string.IsNullOrEmpty(emp_ID) && !loanAmount.Equals("0.00"))
                  {
                      if (!reader.HasRows)
                      {
                          DialogResult confirm = MessageBox.Show("Are you sure you want to add loan?", "Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                          if (confirm.Equals(DialogResult.Yes))
                          {
                              con.Close();

                              if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                              SqlCommand cmd = new SqlCommand("INSERT INTO P_Employee_Loan_HDMF([Date], [Employee_ID]," +
                                  "[HDMF_No], [Surname], [Firstname], [Loan_Amount], [Monthly_Amort], [Semi_Monthly_Amort], [Balance]) VALUES('" +
                                  date + "','" + emp_ID + "','" + hdmf + "','" + surname + "','" + fname + "','" + loanAmount +
                                  "','" + monthlyAmort + "','" + semi_Amort + "','" + balance + "')", con);

                              cmd.ExecuteNonQuery();

                              SqlCommand cmd_History = new SqlCommand("INSERT INTO P_Employee_Loan_HDMF_History([Date], [Employee_ID]," +
                                  "[HDMF_No], [Surname], [Firstname], [Loan_Amount], [Monthly_Amort], [Semi_Monthly_Amort], [Balance]) VALUES('" +
                                  date + "','" + emp_ID + "','" + hdmf + "','" + surname + "','" + fname + "','" + loanAmount +
                                  "','" + monthlyAmort + "','" + semi_Amort + "','" + balance + "')", con);

                              cmd_History.ExecuteNonQuery();

                              con.Close();
                              RetrieveData();
                              ClearTxtBoxes();
                              MessageBox.Show("Loan has been added.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          }

                          DialogResult = DialogResult.None;
                      }
                      else
                      {
                          MessageBox.Show(emp_ID + " has already 'Loan'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      }
                  }
                  else
                  {
                      cb_empidTxt.Select();
                      MessageBox.Show("Choose Employee ID", "Loan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
                  con.Close();
              }
              catch { }
          }

          private void timer1_Tick(object sender, EventArgs e)
          {
              try
              {
                  double loanAmount = Double.Parse(loanAmountTxt.Text);
                  balanceTxt.Text = String.Format("{0:0.00}", loanAmount);
                  double monthlyAmort = Double.Parse(monthlyAmortTxt.Text);
                  double semi_MonthlyAmort = Double.Parse(semi_MonthlyAmortTxt.Text);
                  double balance = Double.Parse(balanceTxt.Text);

                  monthlyAmort = loanAmount / 2;

                  monthlyAmortTxt.Text = String.Format("{0:0.00}", monthlyAmort);

                  semi_MonthlyAmort = monthlyAmort / 2;

                  semi_MonthlyAmortTxt.Text = String.Format("{0:0.00}", semi_MonthlyAmort);
              }
              catch { }  
          }

          private void loanAmountTxt_KeyPress(object sender, KeyPressEventArgs e)
          {
              onlyNumbers(sender, e);
          }

          private void loanAmountTxt_Leave(object sender, EventArgs e)
          {
              zeroTax = loanAmountTxt.Text;
              leaveZeroTax();
              loanAmountTxt.Text = zeroTax;
          }

          private void loanAmountTxt_MouseClick(object sender, MouseEventArgs e)
          {
              blankTax = loanAmountTxt.Text;
              eraseZeroTax();
              loanAmountTxt.Text = blankTax;
          }

          private void clearBtn_Click(object sender, EventArgs e)
          {
              cb_empidTxt.SelectedIndex = -1;
          }
    }
}
