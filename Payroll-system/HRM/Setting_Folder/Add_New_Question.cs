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
    public partial class Add_New_Question : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private string checkMark = "\u2714";
        //private string db_ID;

        private string db_ID_Question;

        int row;

        public Add_New_Question()
        {
            InitializeComponent();
            RetrieveDataQuestions();
        }

        private void autoSize()
        {
            //if (dataGridView1.RowCount.Equals(0))
            //{
                dataGridView1.Columns[0].Width = 935;
            //}
            //else
            //{
                //dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //}
          
        }

        private void RetrieveDataQuestions()
        {
            try
            {
                //Bind specific columns
                SqlCommand cmd = new SqlCommand("SELECT * FROM P_Questions_Password", con);
                DataTable dt = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter(cmd);

                dataGridView1.DataSource = null;
                dta.Fill(dt);

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ColumnCount = 1;

                //dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].DataPropertyName = "ID";

                dataGridView1.Columns[0].HeaderText = "Questions"; dataGridView1.Columns[0].DataPropertyName = "Questions";

                dataGridView1.DataSource = dt;

                //
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Header

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; //Content

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold); //Header Design

                dataGridView1.DefaultCellStyle.Font = new Font("Century Gothic", 10); //Content Design

                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

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

                autoSize();
            }
            catch { }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string question = add_QuestionTxt.Text.Trim();

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                if (!string.IsNullOrEmpty(question))
                {
                    SqlCommand cmd_List = new SqlCommand("INSERT INTO P_Questions_Password([Questions]) VALUES('" + question + "');", con);

                    cmd_List.ExecuteNonQuery();

                    MessageBox.Show("Successfully Added " + checkMark + ".", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RetrieveDataQuestions();
                    add_QuestionTxt.Clear();
                }
                else
                {
                    MessageBox.Show("Insert question here!.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    add_QuestionTxt.Select();
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Put double apostrophe!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string question = add_QuestionTxt.Text.Trim();

                if (!string.IsNullOrEmpty(db_ID_Question))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to update this question: '" + question + "'?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result.Equals(DialogResult.Yes))
                    {
                        if (con.State == ConnectionState.Closed) { con.Open(); }

                        SqlCommand cmd = new SqlCommand("UPDATE P_Questions_Password SET Questions = '" + question +
                             "' WHERE ID = " + db_ID_Question, con);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Successfully Updated.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        db_ID_Question = "";
                        con.Close();
                        add_QuestionTxt.Clear();
                        RetrieveDataQuestions();
                    }
                }
                else
                {
                    MessageBox.Show("Choose Data to Update.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch{}
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void add_QuestionTxt_TextChanged(object sender, EventArgs e)
        {
            string question = add_QuestionTxt.Text.Trim();

            if (string.IsNullOrEmpty(question))
            {
                db_ID_Question = "";
                saveBtn.Enabled = true;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            add_QuestionTxt.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string question = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                if (!string.IsNullOrEmpty(question))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this question:  " + question + "'?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result.Equals(DialogResult.Yes))
                    {
                        if (con.State == ConnectionState.Closed) { con.Open(); }

                        SqlCommand cmd = con.CreateCommand();

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "DELETE FROM P_Questions_Password WHERE Questions = '" + question + "'";

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Successfully Deleted.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        db_ID_Question = "";
                        con.Close();
                        add_QuestionTxt.Clear();
                        RetrieveDataQuestions();
                    }
                }
                else
                {
                    MessageBox.Show("Choose Data to Delete.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
