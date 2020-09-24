using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TimetablePro
{
    class SessionsDataReset
    {

        //--------Methods for clear all days--------

        public void reverseAllParallelSessions(string s_group_id)
        {
            string final_group_id = "%" + s_group_id + "%";

            using (SqlConnection con1 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con1.Open();
                string cleardata = "" +
                    "DELETE FROM sessions_test_2222;" +
                    "INSERT INTO sessions_test_2222 SELECT session_data, sort_order, duration FROM sessions_test;";
                using (SqlCommand sqlcomm1 = new SqlCommand(cleardata, con1))
                {
                    sqlcomm1.Parameters.AddWithValue("@id", final_group_id);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }
        

    }
}
