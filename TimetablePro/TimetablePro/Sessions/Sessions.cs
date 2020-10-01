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
    public partial class SessionsManagement : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public SessionsManagement()
        {
            InitializeComponent();
            DisplayGroupID();
            AddToSessions();
        }



        private void DisplayData()
        {

            string query = "Select * from Sessions";

            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

            DataTable dt = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridView1.DataSource = dt;
            sqlcon.Close();

        }

        int orderNo = 0; //Manually Incrementing orderNo

        public void AddToSessions() {

            string GroupID ="";
            string substrYear = "";
            string substrSemester = "";
            if (comboBoxGroupID.Text != "") {

                GroupID = comboBoxGroupID.Text;
                substrYear = GroupID.Substring(0, 2);
                substrSemester = GroupID.Substring(2, 2);
                

            Console.WriteLine("Year: "+substrYear +" Semester: "+ substrSemester);

            substrYear = "%" + substrYear + "%";
            substrSemester = "%" + substrSemester + "%";


            using (SqlConnection con3 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con3.Open();

                string query = "Select SubCode,SubName,Lecturer_Name,LecHrs,TuteHrs,LabHrs" +
                " from subjects s, lecturer l " +
                "where s.SubCode = l.Subject " +
                "AND offeredYr LIKE @year " +
                "AND offeredSem LIKE @semester";

                SqlCommand sqlcomm = new SqlCommand(query, con3);
                sqlcomm.Parameters.AddWithValue("@year", substrYear);
                sqlcomm.Parameters.AddWithValue("@semester", substrSemester);


                SqlDataReader dataReader = sqlcomm.ExecuteReader();

                

                while (dataReader.Read())
                {

                    string subjectCode = dataReader.GetString(0);
                    string subname = dataReader.GetString(1);
                    string lecturerName = dataReader.GetString(2);
                    int lecHrs = dataReader.GetInt32(3);
                    int tuteHrs = dataReader.GetInt32(4);
                    int labHrs = dataReader.GetInt32(5);

                    //Console.WriteLine("subcode: " + subjectCode + " subname: " + subname+ " LecturerName: " + lecturerName + " LecHrs: " + lecHrs + " tuteHrs: " + tuteHrs + " LabHrs: " + labHrs + "\n");

                        string sessionString = GroupID+"\n"+lecturerName + "\n" + subname +"("+subjectCode+")"+"\n";
                        //Console.WriteLine(sessionString);

                        if (lecHrs >= 1)
                        {
                            //create lecture sessions
                            orderNo += 1; //Incrementing orderNo by 1
                            string sessionString1 = sessionString + "Lecture\n" + lecHrs+" hours";
                            Console.WriteLine(sessionString1);
                            InsertToSessionTable(sessionString1, orderNo, lecHrs, subjectCode, subname, lecturerName, GroupID,120, "Lecture");
                        }

                        if (labHrs >= 1)
                        {
                            //create lab sessions
                            orderNo += 1;//Incrementing orderNo by 1
                            string sessionString2 = sessionString + "Lab\n" + labHrs + " hours";
                            Console.WriteLine(sessionString2);
                            InsertToSessionTable(sessionString2, orderNo, labHrs, subjectCode, subname, lecturerName, GroupID, 120, "Lab");
                        }

                        if (tuteHrs >= 1)
                        {
                            //create lab sessions
                            orderNo += 1;//Incrementing orderNo by 1
                            string sessionString3 = sessionString + "Tute\n" + tuteHrs + " hours";
                            Console.WriteLine(sessionString3);
                            InsertToSessionTable(sessionString3, orderNo, tuteHrs, subjectCode, subname, lecturerName, GroupID, 120, "Tute");
                        }




                    }


            }

            using (SqlConnection con4 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con4.Open();
                string query2 = "Select record_id,session_data,sort_order" +
                " from Sessions ";

                SqlCommand sqlcomm2 = new SqlCommand(query2, sqlcon);
               
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm2);
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            }

        }


        public void InsertToSessionTable(string session_data1, int sort_order1, int duration1, string s_subject_code1, string s_subject_name1, string s_lecturer_name1, string s_group_id1, int s_student_count1, string s_tag1) {

            using (SqlConnection con3 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con3.Open();

                string query = "Insert into sessions(session_data,sort_order,duration,s_subject_code,s_subject_name,s_lecturer_name,s_group_id,s_student_count,s_tag)" +
                    "values(@session_data,@sort_order,@duration,@s_subject_code,@s_subject_name,@s_lecturer_name,@s_group_id,@s_student_count,@s_tag); "+
                    "Insert into sessions_original(session_data,sort_order,duration,s_subject_code,s_subject_name,s_lecturer_name,s_group_id,s_student_count,s_tag)" +
                    "values(@session_data,@sort_order,@duration,@s_subject_code,@s_subject_name,@s_lecturer_name,@s_group_id,@s_student_count,@s_tag);";

                SqlCommand sqlcomm = new SqlCommand(query, con3);
               
                sqlcomm.Parameters.AddWithValue("@session_data", session_data1);
                sqlcomm.Parameters.AddWithValue("@sort_order", sort_order1);
                sqlcomm.Parameters.AddWithValue("@duration", duration1);
                sqlcomm.Parameters.AddWithValue("@s_subject_code", s_subject_code1);
                sqlcomm.Parameters.AddWithValue("@s_subject_name", s_subject_name1);
                sqlcomm.Parameters.AddWithValue("@s_lecturer_name", s_lecturer_name1);
                sqlcomm.Parameters.AddWithValue("@s_group_id", s_group_id1);
                sqlcomm.Parameters.AddWithValue("@s_student_count", s_student_count1);
                sqlcomm.Parameters.AddWithValue("@s_tag", s_tag1);

                sqlcomm.ExecuteNonQuery();

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

        private void sviewbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewSessionsManagement viewsessions = new ViewSessionsManagement();
            viewsessions.Show();
        }

        private void saddbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            SessionsManagement addsessions = new SessionsManagement();
            addsessions.Show();
        }

        private void btnOpt7_Click(object sender, EventArgs e)
        {
            this.Hide();
            SessionsManagement addsessions = new SessionsManagement();
            addsessions.Show();
        }

       

        private void DisplayGroupID()
        {

            string query = "Select *  from student_groups";

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader DR3 = cmd.ExecuteReader();

            while (DR3.Read())
            {
               comboBoxGroupID.Items.Add(DR3[1]);

            }
            sqlcon.Close();

        }

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (comboBoxGroupID.Text != "" )
        //    {


        //        SqlCommand cmdSave = new SqlCommand("Insert into sessionsTb(lecturer_name,group_id,no_of_std,tag,duration,subj_name,subj_code)Values(@lecturer_name,@group_id,@no_of_std,@tag,@duration,@subj_name,@subj_code)", sqlcon);

        //        cmdSave.Parameters.AddWithValue("@group_id", comboBoxGroupID.Text);

        //        sqlcon.Open();
        //        cmdSave.ExecuteNonQuery();

        //        sqlcon.Close();

        //        clearForm();
        //        MessageBox.Show("Subject's Data saved sucessfully.");
        //    }
        //    else
        //    {

        //        MessageBox.Show("Fill all the blanks!");

        //    }
        //}
        //private void clearForm()
        //{
        //    comboBoxGroupID.Text = "";
        //}

        private void SessionsManagement_Load(object sender, EventArgs e)
        {

        }

        private void lLevel_Click(object sender, EventArgs e)
        {

        }

      
        private void btnGroupID_Click(object sender, EventArgs e)
        {
            AddToSessions();
        }

        private void comboBoxGroupID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
