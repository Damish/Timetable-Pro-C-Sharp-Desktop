using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimetablePro
{
    public partial class Generate : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public Generate()
        {
            InitializeComponent();
            DisplayData();

        }

        string Group_id = "Y3.S2.IT.04";


        private void DisplayData()
        {
            if (comboBoxID.Text != "")
            { 
                Group_id = comboBoxID.Text;
            }

            string finalString = "%" + Group_id + "%";
            string query = "Select * from sessions_test s where s.session_data Like @id ";
            
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
            sqlcomm.Parameters.AddWithValue("@id", finalString);

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridView1.DataSource = dt;
            

            sqlcon.Open();
            SqlDataReader reader = sqlcomm.ExecuteReader(); //DataReader
            
            int remainingTime = 9;

            if (reader.HasRows)
            {
                int prevId = 1;
                int prevOrderNo=1;
                string prevSData ="";
                while (reader.Read())
                {
                    // Console.WriteLine("{0}\n{1}\n{2}\n{3}\n", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3));
                    
                    if (remainingTime >= reader.GetInt32(3)) { 
                      
                        int s_id = reader.GetInt32(0);
                        string s_data = reader.GetString(1);
                        int s_order = reader.GetInt32(2);
                        int s_duration = reader.GetInt32(3);

                        if (s_order == prevOrderNo)//if parallel
                        {
                            UpdateParallelMonday(prevId, prevSData, s_data);
                        }
                        else {
                            prevId = reader.GetInt32(0);
                            prevOrderNo = reader.GetInt32(2);
                            prevSData = reader.GetString(1);


                            InsertToMonday(s_id, s_data, s_order, s_duration); //insert to table method (monday)
                            remainingTime -= s_duration;
                        }


                        

                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                reader.Close();
            }
            sqlcon.Close();

            displayMondayTable();

        }

        public void displayMondayTable() {

            string query = "Select * from monday_table";
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridView2.DataSource = dt;
            sqlcon.Close();
        }




        public void InsertToMonday(int s_id, string s_data, int s_order, int s_duration)
        {
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


        public void UpdateParallelMonday(int s_id, string s_data_prev, string s_data_now )
        {
            using (SqlConnection con1 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con1.Open();
                string updateParallelMonday = "UPDATE monday_table SET s_data=@newData WHERE record_id = @record_id; ";
                using (SqlCommand sqlcomm1 = new SqlCommand(updateParallelMonday, con1))
                {

                    string newSessionData = s_data_prev + "--parallel--" + s_data_now;

                    sqlcomm1.Parameters.AddWithValue("@newData", newSessionData);
                    sqlcomm1.Parameters.AddWithValue("@record_id", s_id);
                   
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }


        private void btnOpt2_Click(object sender, EventArgs e)
        {
            studentGroupsManagement studentGroupsManagement = new studentGroupsManagement();

            this.Hide();
            studentGroupsManagement.Show();
        }

        private void btnOpt1_Click(object sender, EventArgs e)
        {

        }
        private void btnOpt3_Click(object sender, EventArgs e)
        {
            AddNewLecturer addNewLecturer = new AddNewLecturer();

            this.Hide();
            addNewLecturer.Show();
        }

        private void btnOpt4_Click(object sender, EventArgs e)
        {
            AddSubjectDetails addSubjectDetails = new AddSubjectDetails();

            this.Hide();
            addSubjectDetails.Show();
        }


        private void btnOpt6_Click(object sender, EventArgs e)
        {
            Location1 location1 = new Location1();

            this.Hide();
            location1.Show();
        }

        private void btnOpt9_Click(object sender, EventArgs e)
        {
            StatisticsForm statisticsForm = new StatisticsForm();

            this.Hide();
            statisticsForm.Show();

        }

        private void btnOpt8_Click(object sender, EventArgs e)
        {
            WorkingDays workingDays = new WorkingDays();

            this.Hide();
            workingDays.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxID_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

       
    }
}
