using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//New Import
using System.Data.SqlClient;
using System.Windows.Forms; // to Message Box appeear

namespace HRM
{
    class Connection
    {
        static /*readonly*/ SqlConnection con;

        
        public static void Connection_DB()
        {
            try
            {
                string conString = @"Data Source=DESKTOP-I9R457E;Initial Catalog=Payroll;Integrated Security=True";
                con = new SqlConnection(conString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        

       }    
    }
}
