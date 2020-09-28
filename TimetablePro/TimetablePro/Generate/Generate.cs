﻿using System;
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

        //static string Group_id = "Y3.S2.IT.04"; // seven days insertions timetable groupId
        //static string finalString = "%" + Group_id + "%";   // FORMATTED for SQL SELECT - seven days insertions timetable groupId

        public Generate()
        {
            InitializeComponent();
            

            displayFullTimetable();
        }

        private void DisplayData() // sessions table display 
        {

            //Console.WriteLine("the ID to Refer is " + Group_id);
            //Console.WriteLine("comboBox Text " + comboBoxID.Text);

            string finalString = "";
            if (comboBoxID.Text != "")
            {
                finalString = "%" + comboBoxID.Text + "%";
            }
            

            string query = "Select * from sessions_test_2222 s where s.session_data Like @id ";
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
            sqlcomm.Parameters.AddWithValue("@id", finalString);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void UpdateAllParallelSessions()
        {

            string finalString = "";
            if (comboBoxID.Text != "")
            {
                finalString = "%" + comboBoxID.Text + "%";
            }


            string query = "Select * from sessions_test_2222 s where s.session_data Like @id ";
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
            sqlcomm.Parameters.AddWithValue("@id", finalString);
            sqlcon.Open();
            SqlDataReader reader = sqlcomm.ExecuteReader(); //DataReader to update parallel all sessions

            if (reader.HasRows)
            {
                //variables to save previous session data
                int prevId = 1;
                int prevOrderNo = 0;
                string prevSData = "";

                while (reader.Read())
                {
                    if (reader.GetInt32(2) == prevOrderNo)//if parallel
                    {
                        //Console.WriteLine(reader.GetInt32(0) + " <<  Now in update Parallel All "+prevId + " "+ prevSData + "   " + reader.GetString(1) + " \n");
                        UpdateParallelAll(reader.GetInt32(0), prevId, prevSData, reader.GetString(1));
                    }
                    else
                    {
                        prevId = reader.GetInt32(0);
                        prevOrderNo = reader.GetInt32(2);
                        prevSData = reader.GetString(1);
                    }
                }
            }
            else
            {
                reader.Close();
            }
            sqlcon.Close();

        }


        int remainingTimeMonday = 5;
        int remainingTimeTuesday = 5;
        int remainingTimeWednesday = 10;
        int remainingTimeThursday = 5;
        int remainingTimeFriday = 9;


        private void InsertDatatoSevenDays()
        {

            string finalString = "";
            if (comboBoxID.Text != "")
            {
                finalString = "%" + comboBoxID.Text + "%";
            }


            SqlConnection sqlcon2 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            string query2 = "Select * from sessions_test_2222 s where s.session_data Like @id ";
            sqlcon2.Open();
            SqlCommand sqlcomm2 = new SqlCommand(query2, sqlcon2);
            sqlcomm2.Parameters.AddWithValue("@id", finalString);
            SqlDataReader reader2 = sqlcomm2.ExecuteReader(); //DataReader for inserting to monday,tuesday tables


            int MondayStartTime = 830;
            int TuesdayStartTime = 830;
            int WednesdayStartTime = 830;
            int ThursdayStartTime = 830;
            int FridayStartTime = 830;


            if (reader2.HasRows)
            {
                //variables to save previous session data
                int prevId = 1;
                int prevOrderNo=0;
                string prevSData ="";
            
                while (reader2.Read())
                {
                    //Console.WriteLine("{0}\n{1}\n{2}\n{3}\n", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3));
                    if ((remainingTimeMonday >= reader2.GetInt32(3)))
                    {  //Monday fill
                       
                        int s_id = reader2.GetInt32(0);
                        string s_data = reader2.GetString(1);
                        int s_order = reader2.GetInt32(2);
                        int s_duration = reader2.GetInt32(3);
                        
                        prevId = reader2.GetInt32(0);
                        prevOrderNo = reader2.GetInt32(2);
                        prevSData = reader2.GetString(1);

                        GroupTimetableUpdateCells gtuc = new GroupTimetableUpdateCells();
                        
                        int count = 1;
                        while (count <= s_duration)//add the same session twice to get better view
                        {
                            MondayStartTime += 100;//increment duration of time slot
                            gtuc.InsertCellsMonday(s_data, MondayStartTime);
                            count += 1;
                        }
                        remainingTimeMonday -= s_duration; //decrement remaining time available to add new sessions
                        
                       
                    }
                    
                    else if (remainingTimeTuesday >= reader2.GetInt32(3))
                    {//Tuesday fill

                        int s_id = reader2.GetInt32(0);
                        string s_data = reader2.GetString(1);
                        int s_order = reader2.GetInt32(2);
                        int s_duration = reader2.GetInt32(3);

                        prevId = reader2.GetInt32(0);
                        prevOrderNo = reader2.GetInt32(2);
                        prevSData = reader2.GetString(1);
                        
                        GroupTimetableUpdateCells gtuc = new GroupTimetableUpdateCells();

                        int count = 1;
                        while (count <= s_duration)//add the same session twice to get better view
                        {
                            TuesdayStartTime += 100;//increment duration of time slot
                            gtuc.InsertCellsTuesday(s_data, TuesdayStartTime);
                            count += 1;
                        }
                        remainingTimeTuesday -= s_duration; //decrement remaining time available to add new sessions
                    }

                    else if (remainingTimeWednesday >= reader2.GetInt32(3))
                    {//wednesday fill

                        int s_id = reader2.GetInt32(0);
                        string s_data = reader2.GetString(1);
                        int s_order = reader2.GetInt32(2);
                        int s_duration = reader2.GetInt32(3);

                        prevId = reader2.GetInt32(0);
                        prevOrderNo = reader2.GetInt32(2);
                        prevSData = reader2.GetString(1);

                        GroupTimetableUpdateCells gtuc = new GroupTimetableUpdateCells();

                        int count = 1;
                        while (count <= s_duration)//add the same session twice to get better view
                        {
                            WednesdayStartTime += 100;//increment duration of time slot
                            gtuc.InsertCellsWednesday(s_data, WednesdayStartTime);
                            count += 1;
                        }
                        remainingTimeWednesday -= s_duration; //decrement remaining time available to add new sessions
                    }

                    else if (remainingTimeThursday >= reader2.GetInt32(3))
                    {//Thursday fill

                        int s_id = reader2.GetInt32(0);
                        string s_data = reader2.GetString(1);
                        int s_order = reader2.GetInt32(2);
                        int s_duration = reader2.GetInt32(3);

                        prevId = reader2.GetInt32(0);
                        prevOrderNo = reader2.GetInt32(2);
                        prevSData = reader2.GetString(1);

                        GroupTimetableUpdateCells gtuc = new GroupTimetableUpdateCells();

                        int count = 1;
                        while (count <= s_duration)//add the same session twice to get better view
                        {
                            ThursdayStartTime += 100;//increment duration of time slot
                            gtuc.InsertCellsThursday(s_data, ThursdayStartTime);
                            count += 1;
                        }
                        remainingTimeThursday -= s_duration; //decrement remaining time available to add new sessions
                    }
                    else if (remainingTimeFriday >= reader2.GetInt32(3))
                    {//Friday fill

                        int s_id = reader2.GetInt32(0);
                        string s_data = reader2.GetString(1);
                        int s_order = reader2.GetInt32(2);
                        int s_duration = reader2.GetInt32(3);

                        prevId = reader2.GetInt32(0);
                        prevOrderNo = reader2.GetInt32(2);
                        prevSData = reader2.GetString(1);

                        GroupTimetableUpdateCells gtuc = new GroupTimetableUpdateCells();

                        int count = 1;
                        while (count <= s_duration)//add the same session twice to get better view
                        {
                            FridayStartTime += 100;//increment duration of time slot
                            gtuc.InsertCellsFriday(s_data, FridayStartTime);
                            count += 1;
                        }
                        remainingTimeFriday -= s_duration; //decrement remaining time available to add new sessions
                    }
                    else
                    {
                        
                        MessageBox.Show("Can't map following Data all Days full : \n" +
                           "Remaining TimeMonday :" + remainingTimeMonday + "hours\n" +
                           "Remaining TimeTuesday :" + remainingTimeTuesday + "hours\n" +
                           "Remaining TimeWednesday :" + remainingTimeWednesday + "hours\n" +
                           "Remaining TimeThursday :" + remainingTimeThursday + "hours\n" +
                           "Remaining TimeFriday :" + remainingTimeFriday + "hours\n" +
                            "" + reader2.GetInt32(0) + " " + reader2.GetString(1) + " " + reader2.GetInt32(2) + " " + reader2.GetInt32(3) + " \n");

                        break;
                    }
                }
            }
            else
            {
                reader2.Close();
            }
            sqlcon2.Close();

           
            displayFullTimetable();

           remainingTimeMonday = 5; //reset time durations
           remainingTimeTuesday = 5; //reset time durations
           remainingTimeWednesday = 10;
           remainingTimeThursday = 5;
           remainingTimeFriday = 9;
        }

        public void displayFullTimetable()
        {
            string query = "Select Time,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday from Group_Timetable";
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            sda.Fill(dt);
            dataGridView7.DataSource = dt;
            sqlcon.Close();
        }

        

        //---------UpdateParallelAll---in sessions_test_2222 table-----

        public void UpdateParallelAll(int c_record_id,int s_id, string s_data_prev, string s_data_now)
        {
            //Console.WriteLine(s_id.ToString() + "  Now in update Parallel All " + s_data_prev + "   " + s_data_now + " \n");
            using (SqlConnection con2 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con2.Open();
                string updateParallelAll = "UPDATE sessions_test_2222 SET session_data=@newData WHERE record_id = @record_id;" +
                    "DELETE From sessions_test_2222 where record_id = @current_record_id;";
                using (SqlCommand sqlcomm1 = new SqlCommand(updateParallelAll, con2))
                {
                    string newSessionData = "Parallel sessions available \n"+s_data_prev + "\n" + s_data_now;

                    sqlcomm1.Parameters.AddWithValue("@newData", newSessionData);
                    sqlcomm1.Parameters.AddWithValue("@record_id", s_id);
                    sqlcomm1.Parameters.AddWithValue("@current_record_id", c_record_id);
                    
                    sqlcomm1.ExecuteNonQuery();
                }
            }
        }


        







        //Side Menu buttons -ToDo: add to new class


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

        private void btn_UpdateParallelAll_Click(object sender, EventArgs e)
        {
            UpdateAllParallelSessions();
            DisplayData();
        }

        private void btn_InsertToSevenDays_Click(object sender, EventArgs e)
        {
            InsertDatatoSevenDays();
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    string finalString = "";
        //    if (comboBoxID.Text != "")
        //    {
        //        finalString = "%" + comboBoxID.Text + "%";
        //    }
            
        //    SevenDaysTablesClear sdtc = new SevenDaysTablesClear();
        //    sdtc.clearAllDays(finalString);
            
        //    displayFullTimetable();
            
        //}

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string finalString = "";
            if (comboBoxID.Text != "")
            {
                finalString = "%" + comboBoxID.Text + "%";
            }
            
            SessionsDataReset sdr = new SessionsDataReset();
            sdr.reverseAllParallelSessions(finalString);
            DisplayData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GroupTimetableUpdateCells gtuc = new GroupTimetableUpdateCells();
            gtuc.ResetALLCellsToNull();

            displayFullTimetable();
        }
    }
}
