using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetablePro
{
    class ParallelMethods
    {

        public Boolean returnSessionsWithoutParallelSessions(int id)
        {
            using (SqlConnection con1 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {

                string query = "Select isParallel from sessions s where s.record_id = @id ";
                SqlCommand sqlcomm = new SqlCommand(query, con1);
                sqlcomm.Parameters.AddWithValue("@id", id);
                con1.Open();
                SqlDataReader reader2 = sqlcomm.ExecuteReader();

                
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        if (reader2.GetString(0).Equals("true"))
                        {
                            return true;
                        }
                    }
                }

            }
            return false;

        }



    }
}
