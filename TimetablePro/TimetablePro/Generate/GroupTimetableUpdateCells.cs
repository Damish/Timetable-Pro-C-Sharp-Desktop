using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace TimetablePro
{
    class GroupTimetableUpdateCells
    {

        public void ResetALLCellsToNull()
        {
            using (SqlConnection con3 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con3.Open();
                for (int x = 830; x <= 2030; x += 100)
                {
                     string updateParallelAll = "UPDATE Group_Timetable SET Monday=null,Tuesday=null,Wednesday=null,THursday=null,Friday=null,Saturday=null,Sunday=null WHERE Time = @time;";
                     using (SqlCommand sqlcomm1 = new SqlCommand(updateParallelAll, con3))
                    {
                        
                        sqlcomm1.Parameters.AddWithValue("@time", x);
                        sqlcomm1.ExecuteNonQuery();
                    }
                }
            }
        }



        public void InsertCellsMonday(string s_data,int time)
        {
            using (SqlConnection con3 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con3.Open();
                string updateParallelAll = "UPDATE Group_Timetable SET Monday=@s_data WHERE Time = @time;";
                using (SqlCommand sqlcomm1 = new SqlCommand(updateParallelAll, con3))
                {
                    sqlcomm1.Parameters.AddWithValue("@s_data", s_data);
                    sqlcomm1.Parameters.AddWithValue("@time", time);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }

        public void InsertCellsTuesday(string s_data, int time)
        {
            using (SqlConnection con3 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con3.Open();
                string updateParallelAll = "UPDATE Group_Timetable SET Tuesday=@s_data WHERE Time = @time;";
                using (SqlCommand sqlcomm1 = new SqlCommand(updateParallelAll, con3))
                {
                    sqlcomm1.Parameters.AddWithValue("@s_data", s_data);
                    sqlcomm1.Parameters.AddWithValue("@time", time);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }

        public void InsertCellsWednesday(string s_data, int time)
        {
            using (SqlConnection con3 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con3.Open();
                string updateParallelAll = "UPDATE Group_Timetable SET Wednesday=@s_data WHERE Time = @time;";
                using (SqlCommand sqlcomm1 = new SqlCommand(updateParallelAll, con3))
                {
                    sqlcomm1.Parameters.AddWithValue("@s_data", s_data);
                    sqlcomm1.Parameters.AddWithValue("@time", time);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }

        public void InsertCellsThursday(string s_data, int time)
        {
            using (SqlConnection con3 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con3.Open();
                string updateParallelAll = "UPDATE Group_Timetable SET Thursday=@s_data WHERE Time = @time;";
                using (SqlCommand sqlcomm1 = new SqlCommand(updateParallelAll, con3))
                {
                    sqlcomm1.Parameters.AddWithValue("@s_data", s_data);
                    sqlcomm1.Parameters.AddWithValue("@time", time);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }

        public void InsertCellsFriday(string s_data, int time)
        {
            using (SqlConnection con3 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con3.Open();
                string updateParallelAll = "UPDATE Group_Timetable SET Friday=@s_data WHERE Time = @time;";
                using (SqlCommand sqlcomm1 = new SqlCommand(updateParallelAll, con3))
                {
                    sqlcomm1.Parameters.AddWithValue("@s_data", s_data);
                    sqlcomm1.Parameters.AddWithValue("@time", time);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }




    }
}
