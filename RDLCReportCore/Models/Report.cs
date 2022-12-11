using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RDLCReportCore.Models
{
    public class Report
    {
        string connectionString = "Data Source=localhost;Initial Catalog=CRUDOnly;Integrated Security=SSPI; User ID=madbadmin; password=@dmin123";

        public DataTable GetReportList()
        {
            var dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SPReportExpense", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            return dt;
        }
    }
}
