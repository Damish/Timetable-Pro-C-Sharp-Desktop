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
    public partial class ViewSessionsManagement : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public ViewSessionsManagement()
        {
            InitializeComponent();
            DisplayData();
            DisplaySubjectData();
            DisplayLecData();
            DisplayGroupData();
            FilterbyLec();
            FilterbySubCode();
            FilterbyGroupID();
            //search(txtsrch.Text);
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

        

        private void btnOpt7_Click(object sender, EventArgs e)
        {
            SessionsManagement sessionsManagement = new SessionsManagement();

            this.Hide();
            sessionsManagement.Show();
        }
        

        
        private void DisplayData()
        {

            string query = "Select session_data,s_lecturer_name,s_subject_code,s_group_id from sessions";

            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

            DataTable dt = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridViewSessions.DataSource = dt;
            sqlcon.Close();

        }



        private void DisplayLecData()
        {

            string query = "Select Lecturer_Name  from lecturer";

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader DR2 = cmd.ExecuteReader();

            while (DR2.Read())
            {
                dropLecID.Items.Add(DR2[0]);

            }
            sqlcon.Close();


        }
        private void DisplaySubjectData()
        {

            string query = "Select SubCode  from subjects";

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader DR2 = cmd.ExecuteReader();

            while (DR2.Read())
            {
                dropSubject.Items.Add(DR2[0]);

            }
            sqlcon.Close();


        }

        private void DisplayGroupData()
        {

            string query = "Select group_id  from student_groups";

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader DR2 = cmd.ExecuteReader();

            while (DR2.Read())
            {
                dropGroupID.Items.Add(DR2[0]);

            }
            sqlcon.Close();


        }

        private void FilterbyLec()
        {
            string LecID = "";

            if (dropLecID.Text != "")
            {
                LecID = dropLecID.Text;
                LecID = "%" + LecID + "%";

                using (SqlConnection con4 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    con4.Open();
                 
                    string query2 = "Select session_data,s_lecturer_name,s_subject_code,s_group_id from  sessions s where s_lecturer_name LIKE @LecID";

                    SqlCommand sqlcomm2 = new SqlCommand(query2, sqlcon);
                    sqlcomm2.Parameters.AddWithValue("@LecID", LecID = dropLecID.Text);

                    sqlcon.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlcomm2);
                    sda.Fill(dt);
                    dataGridViewSessions.DataSource = dt;
                    sqlcon.Close();
                    con4.Close();
                    clearForm1();
                }
            }
        }

        private void FilterbySubCode()
        {
            string SubCode = "";

            if (dropSubject.Text != "")
            {
                SubCode = dropSubject.Text;
                SubCode = "%" + SubCode + "%";

                using (SqlConnection con4 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    con4.Open();

                    string query2 = "Select session_data,s_lecturer_name,s_subject_code,s_group_id from  sessions s where s_subject_code LIKE @SubCode";

                    SqlCommand sqlcomm2 = new SqlCommand(query2, sqlcon);
                    sqlcomm2.Parameters.AddWithValue("@SubCode", SubCode = dropSubject.Text);

                    sqlcon.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlcomm2);
                    sda.Fill(dt);
                    dataGridViewSessions.DataSource = dt;
                    sqlcon.Close();
                    con4.Close();
                    clearForm2();
                }
            }
        }

        private void FilterbyGroupID()
        {
            string GroupID = "";

            if (dropGroupID.Text != "")
            {
                GroupID = dropGroupID.Text;
                GroupID = "%" + GroupID + "%";

                using (SqlConnection con4 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    con4.Open();

                    string query2 = "Select session_data,s_lecturer_name,s_subject_code,s_group_id from  sessions s where s_group_id LIKE @GroupID";

                    SqlCommand sqlcomm2 = new SqlCommand(query2, sqlcon);
                    sqlcomm2.Parameters.AddWithValue("@GroupID", GroupID = dropGroupID.Text);

                    sqlcon.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlcomm2);
                    sda.Fill(dt);
                    dataGridViewSessions.DataSource = dt;
                    sqlcon.Close();
                    con4.Close();
                    clearForm3();
                }
            }
        }

        private void btnLecID_Click(object sender, EventArgs e)
        {
           FilterbyLec();
        }

    
        private void btnSessions_Click(object sender, EventArgs e)
        {
            DisplayData();
            clearForm1();
            clearForm2();
            clearForm3();
        }

        private void btnSubID_Click(object sender, EventArgs e)
        {
            FilterbySubCode();
        }

        private void btnGroupID_Click(object sender, EventArgs e)
        {
            FilterbyGroupID();
        }


        private void clearForm1()
        {
            dropSubject.Text = "";
            dropGroupID.Text = "";
        }
        private void clearForm2()
        {
            dropLecID.Text = "";
            dropGroupID.Text = "";
        }
        private void clearForm3()
        {
            dropLecID.Text = "";
            dropSubject.Text = "";
        }

        private void btnDelLec_Click(object sender, EventArgs e)
        {
            string LecID = "";
            string finalLecID = "";

            if (dropLecID.Text != "")
            {
                LecID = dropLecID.Text;
                finalLecID = "%" + LecID + "%";

                using (SqlConnection con4 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    con4.Open();

                    string query2 = "delete from  sessions  where s_lecturer_name LIKE @LecID;" +
                        "delete from  sessions_original where s_lecturer_name LIKE @LecID;";

                    SqlCommand sqlcomm2 = new SqlCommand(query2, con4);
                    sqlcomm2.Parameters.AddWithValue("@LecID", finalLecID);

                    sqlcomm2.ExecuteNonQuery();
                    DisplayData();
                    MessageBox.Show("Data Deleted Sucessfully");
                       
                    con4.Close();
                   
                }
            }
        }

        private void dataGridViewSessions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelSub_Click(object sender, EventArgs e)
        {
            string subID = "";
            string finalSubID = "";

            if (dropSubject.Text != "")
            {
                subID = dropSubject.Text;
                finalSubID = "%" + subID + "%";

                using (SqlConnection con4 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    con4.Open();

                    string query2 = "delete from  sessions  where s_subject_code LIKE @subID;" +
                        "delete from  sessions_original  where s_subject_code LIKE @subID;";

                    SqlCommand sqlcomm2 = new SqlCommand(query2, con4);
                    sqlcomm2.Parameters.AddWithValue("@subID", finalSubID);

                    sqlcomm2.ExecuteNonQuery();
                    DisplayData() ;
                    MessageBox.Show("Data Deleted Sucessfully");

                    con4.Close();

                }
            }
        }

        private void btnDelGroup_Click(object sender, EventArgs e)
        {
            string GroupID = "";
            string finalGroupID = "";

            if (dropGroupID.Text != "")
            {
                GroupID = dropGroupID.Text;
                finalGroupID = "%" + GroupID + "%";

                using (SqlConnection con4 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    con4.Open();

                    string query2 = "delete from  sessions  where s_group_id LIKE @GroupID;" +
                        "delete from  sessions_original  where s_group_id LIKE @GroupID;";

                    SqlCommand sqlcomm2 = new SqlCommand(query2, con4);
                    sqlcomm2.Parameters.AddWithValue("@GroupID", finalGroupID);

                    sqlcomm2.ExecuteNonQuery();
                    DisplayData();
                    MessageBox.Show("Data Deleted Sucessfully");

                    con4.Close();

                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
           
                using (SqlConnection con4 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    con4.Open();

                    string query2 = "delete from sessions;" +
                    "delete from sessions_original;";

                    SqlCommand sqlcomm2 = new SqlCommand(query2, con4);

                    sqlcomm2.ExecuteNonQuery();
                    DisplayData();
                    MessageBox.Show("Data Deleted Sucessfully");

                    con4.Close();

                }
        }

        private void btnOpt11_Click(object sender, EventArgs e)
        {
            Generate generate = new Generate();

            this.Hide();
            generate.Show();
        }

        private void saddbtn_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            SessionsManagement addsessions = new SessionsManagement();
            addsessions.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParallelSessionsManagement psm = new ParallelSessionsManagement();
            this.Hide();
            psm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConsecutiveSessionsManagement csm = new ConsecutiveSessionsManagement();
            this.Hide();
            csm.Show();
        }

        private void sviewbtn_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ViewSessionsManagement viewsessions = new ViewSessionsManagement();
            viewsessions.Show();
        }

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    string selected_id = dataGridView1.CurrentRow.Cells["SubCode"].Value.ToString();
        //    string delete_row = "delete from Subjects where SubCode='" + selected_id + "'";

        //    sqlcon.Open();

        //    SqlCommand sqlcomm = new SqlCommand(delete_row, sqlcon);

        //    //sqlcomm.ExecuteNonQuery();

        //    int count = sqlcomm.ExecuteNonQuery();

        //    if (count > 0)
        //    {

        //        MessageBox.Show("Data Deleted Sucessfully");
        //        //    dataGridView1.Rows.Clear();

        //    }
        //    else
        //    {
        //        MessageBox.Show("Failed to Delete!!!");
        //    }
        //    sqlcon.Close();
        //    DisplayData();
        //}

        //public void search(string search_data)
        //{
        //    sqlcon.Open();
        //    string query = "select session_data from sessions where session_data like '%'" + search_data + "'%'";
        //    SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    dataGridViewSessions.DataSource = dt;
        //    sqlcon.Close();
        //}


        //private void txtsrch_TextChanged(object sender, EventArgs e)
        //{
        //    search(txtsrch.Text);

        //}
    }
}
