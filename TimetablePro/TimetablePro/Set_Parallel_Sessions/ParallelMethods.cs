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

        public Boolean checkParallelSessions(int id)
        {
            using (SqlConnection con1 = new SqlConnection(@"Server=tcp:timetableserverkisal.database.windows.net,1433;Initial Catalog=timetabledbkisal;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
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

        public string getSessionData(int record_id) {

            using (SqlConnection con2 = new SqlConnection(@"Server=tcp:timetableserverkisal.database.windows.net,1433;Initial Catalog=timetabledbkisal;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con2.Open();
                string getData = "select session_data from sessions where record_id = @id";

                using (SqlCommand sqlcomm1 = new SqlCommand(getData, con2))
                {
                    sqlcomm1.Parameters.AddWithValue("@id", record_id);

                    SqlDataReader reader = sqlcomm1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }
            return "";
        }

        
        public void NewUpdateParallelAll(int c_record_id, int p_record_id, string p_session_data,string s_session_data)
        {
            //Console.WriteLine(s_id.ToString() + "  Now in update Parallel All " + s_data_prev + "   " + s_data_now + " \n");
            using (SqlConnection con2 = new SqlConnection(@"Server=tcp:timetableserverkisal.database.windows.net,1433;Initial Catalog=timetabledbkisal;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con2.Open();
                string updateParallelAll = "UPDATE sessions SET session_data=@newData WHERE record_id = @record_id;";

                using (SqlCommand sqlcomm1 = new SqlCommand(updateParallelAll, con2))
                {
                    string newSessionData = "Parallel sessions available \n" + p_session_data + "\n" + s_session_data;

                    sqlcomm1.Parameters.AddWithValue("@newData", newSessionData);
                    sqlcomm1.Parameters.AddWithValue("@record_id", c_record_id);
                    sqlcomm1.Parameters.AddWithValue("@current_record_id", c_record_id);

                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }

        public void reverseAllParallelSessions(string s_group_id)
        {
            string final_group_id = "%" + s_group_id + "%";

            using (SqlConnection con1 = new SqlConnection(@"Server=tcp:timetableserverkisal.database.windows.net,1433;Initial Catalog=timetabledbkisal;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con1.Open();
                string cleardata = 
                    "DELETE FROM sessions where s_group_id LIKE @id;" +
                    "INSERT INTO sessions " +
                    "SELECT session_data,sort_order,duration,s_subject_code,s_subject_name,s_lecturer_name,s_group_id,s_student_count,s_tag,isConsecutive,day,start_time,end_time,location,isParallel,parallel_With,consecutive " +
                    "FROM sessions_original where s_group_id LIKE @id;";
                using (SqlCommand sqlcomm1 = new SqlCommand(cleardata, con1))
                {
                    sqlcomm1.Parameters.AddWithValue("@id", final_group_id);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }


    }
}
