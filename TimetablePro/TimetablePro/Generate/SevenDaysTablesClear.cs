using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace TimetablePro
{
    class SevenDaysTablesClear
    {
        
        //--------Methods for clear all days--------
        
        public void clearAllDays(string s_group_id)
        { 
        string final_group_id = "%" + s_group_id + "%"; 

            using (SqlConnection con1 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con1.Open();
                string cleardata = "" +
                    "DELETE FROM monday_table  WHERE s_data Like @id ;" +
                    "DELETE FROM tuesday_table  WHERE s_data Like @id ;" +
                    "DELETE FROM wednesday_table  WHERE s_data Like @id ;"+
                    "DELETE FROM thursday_table  WHERE s_data Like @id ;"+
                    "DELETE FROM friday_table  WHERE s_data Like @id ;";
                using (SqlCommand sqlcomm1 = new SqlCommand(cleardata, con1))
                {
                    sqlcomm1.Parameters.AddWithValue("@id", final_group_id);
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }



    }
}
