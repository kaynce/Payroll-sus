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
    public partial class Set_Up_Logo : Form
    {
        private SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_String"].ConnectionString);

        private string logo_ID;
        private string file;
        private Boolean exist = true;

        private string db_business_ID;

        public Set_Up_Logo()
        {
            InitializeComponent();
            retrievelogoLink();
            getBusinessName();
        }

        private void getBusinessName()
        {
            //Retrieve Business Name
            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

            SqlCommand cmd3 = new SqlCommand("SELECT ID, Business_Name, Address, Contact_Number FROM P_Business_Name", con);
            SqlDataReader dr3 = cmd3.ExecuteReader();

            while (dr3.Read())
            {
                db_business_ID = dr3["ID"].ToString();
                businessNameTxt.Text = dr3["Business_Name"].ToString();
                addressTxt.Text = dr3["Address"].ToString();
                contactNumberTxt.Text = dr3["Contact_Number"].ToString();
            }
            con.Close();
        }

        public void getNum()
        {
            try
            {
                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                //"SELECT ID FROM Payroll ORDER BY ID"
                SqlCommand cmd = new SqlCommand("SELECT MAX([ID]) from P_Company_Logo ", con);
                SqlDataReader dta = cmd.ExecuteReader();

                if (dta.Read())
                {
                    logo_ID = dta[0].ToString();
                    //textBox1.Text = ID;
                }
                con.Close();
            }
            catch { }
        }

        private void retrievelogoLink() //Logo Link
        {
            try
            {
                getNum();

                SqlCommand cmd = new SqlCommand("SELECT Logo_Link FROM P_Company_Logo Where ID = @ID", con);

                cmd.Parameters.AddWithValue("@ID", int.Parse(logo_ID));

                if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                SqlDataReader dta = cmd.ExecuteReader();

                if (dta.Read())
                {
                    file = dta.GetValue(0).ToString();
                }

                if (System.IO.File.Exists(file))
                {
                    exist = true;
                    comlogoPic.Visible = true;
                    //file = openFileDialog1.FileName;
                    comlogoPic.Image = Image.FromFile(file);
                }
                con.Close();
            }
            catch
            {
                con.Close();
            }
        }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                file = openFileDialog1.FileName;

                if (System.IO.File.Exists(file))
                {
                    exist = false;
                    comlogoPic.Visible = true;
                    file = openFileDialog1.FileName;
                    comlogoPic.Image = Image.FromFile(file);
                }
            }
            catch
            {
                MessageBox.Show("This file is not supported.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Close();
                string name = businessNameTxt.Text.Trim();
                string address = addressTxt.Text.Trim();
                string contactNum = contactNumberTxt.Text.Trim();

                SqlDataReader reader = null;
                SqlCommand cmd_Select = new SqlCommand("SELECT * FROM P_Admin_Users", con);
                //DataTable tb = new DataTable();

                if (exist.Equals(false))
                {
                    if (string.IsNullOrEmpty(logo_ID))
                    {
                        DialogResult confirm = MessageBox.Show("Do you want to update?", "Set up", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirm.Equals(DialogResult.Yes))
                        {
                            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                            SqlCommand cmd = new SqlCommand("INSERT INTO P_Company_Logo([Logo_Link]) VALUES('" + file + "')", con);
                            cmd.ExecuteNonQuery();

                            //Save in P_Business_Name
                            SqlCommand cmd3 = new SqlCommand("INSERT INTO P_Business_Name([Business_Name], [Address]," +
                                "[Contact_Number] ) VALUES('" + name + "','" + address + "','" + contactNum + "')", con);

                            con.Close();

                            MessageBox.Show("Successfully updated.\nClick the logo once to see the update.", "Set up", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Dispose();
                        }
                    }
                    else
                    {
                        DialogResult confirm = MessageBox.Show("Do you want to update?", "Set up", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirm.Equals(DialogResult.Yes))
                        {
                            if (con.State.Equals(ConnectionState.Closed)) { con.Open(); }

                            SqlCommand cmd = new SqlCommand("UPDATE P_Company_Logo SET Logo_Link = '" + file + "' WHERE ID = " + logo_ID, con);
                            cmd.ExecuteNonQuery();

                            SqlCommand cmd_Business_Name = new SqlCommand("UPDATE P_Business_Name SET Business_Name = '" + name +
                                            "', Address = '" + address + "', Contact_Number = '" + contactNum +
                                            "' WHERE ID = " + db_business_ID, con);

                            cmd_Business_Name.ExecuteNonQuery();

                            con.Close();
                            MessageBox.Show("Logo successfully updated.\nClick the logo once to see the update.", "Set up", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Dispose();
                        }
                    }
                    DialogResult = DialogResult.None;
                }
                else
                {
                    MessageBox.Show("Click the 'Browse' button to upload logo.", "Set up", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();

                retrievelogoLink();
            }
            catch { }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
