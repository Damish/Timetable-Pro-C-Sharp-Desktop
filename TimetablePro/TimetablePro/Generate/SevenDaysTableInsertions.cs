using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TimetablePro
{
    class SevenDaysTableInsertions
    {


        //--------Methods for Monday--------


        public void InsertToMonday(int s_id, string s_data, int s_order, int s_duration)
        {

            //Console.WriteLine(s_id  + "  Now in InsertToMonday " + s_data + "   " + s_order + " "+s_duration +" \n");

            using (SqlConnection con1 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con1.Open();
                string insertoMonday = "Insert into monday_table values(@record_id,@s_data,@order,@duration)";
                using (SqlCommand sqlcomm1 = new SqlCommand(insertoMonday, con1))
                {
                    sqlcomm1.Parameters.AddWithValue("@record_id", s_id);
                    sqlcomm1.Parameters.AddWithValue("@s_data", s_data);
                    sqlcomm1.Parameters.AddWithValue("@order", s_order);
                    sqlcomm1.Parameters.AddWithValue("@duration", s_duration);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }



        //--------Methods for Tuesday--------


        public void InsertToTuesday(int s_id, string s_data, int s_order, int s_duration)
        {
            using (SqlConnection con1 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con1.Open();
                string insertoMonday = "Insert into tuesday_table values(@record_id,@s_data,@order,@duration)";
                using (SqlCommand sqlcomm1 = new SqlCommand(insertoMonday, con1))
                {
                    sqlcomm1.Parameters.AddWithValue("@record_id", s_id);
                    sqlcomm1.Parameters.AddWithValue("@s_data", s_data);
                    sqlcomm1.Parameters.AddWithValue("@order", s_order);
                    sqlcomm1.Parameters.AddWithValue("@duration", s_duration);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }

        //--------Methods for Wednesday--------

        public void InsertToWednesday(int s_id, string s_data, int s_order, int s_duration)
        {
            //Console.WriteLine(s_id  + "  Now in InsertToMonday " + s_data + "   " + s_order + " "+s_duration +" \n");

            using (SqlConnection con1 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con1.Open();
                string insertoMonday = "Insert into wednesday_table values(@record_id,@s_data,@order,@duration)";
                using (SqlCommand sqlcomm1 = new SqlCommand(insertoMonday, con1))
                {
                    sqlcomm1.Parameters.AddWithValue("@record_id", s_id);
                    sqlcomm1.Parameters.AddWithValue("@s_data", s_data);
                    sqlcomm1.Parameters.AddWithValue("@order", s_order);
                    sqlcomm1.Parameters.AddWithValue("@duration", s_duration);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }

        //--------Methods for Thursday--------

        public void InsertToThursday(int s_id, string s_data, int s_order, int s_duration)
        {
            //Console.WriteLine(s_id  + "  Now in InsertToMonday " + s_data + "   " + s_order + " "+s_duration +" \n");

            using (SqlConnection con1 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con1.Open();
                string insertoMonday = "Insert into thursday_table values(@record_id,@s_data,@order,@duration)";
                using (SqlCommand sqlcomm1 = new SqlCommand(insertoMonday, con1))
                {
                    sqlcomm1.Parameters.AddWithValue("@record_id", s_id);
                    sqlcomm1.Parameters.AddWithValue("@s_data", s_data);
                    sqlcomm1.Parameters.AddWithValue("@order", s_order);
                    sqlcomm1.Parameters.AddWithValue("@duration", s_duration);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }
    }
}
