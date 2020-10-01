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

        //static string Group_id = "Y3.S2.IT.04"; // seven days insertions timetable groupId
        //static string finalString = "%" + Group_id + "%";   // FORMATTED for SQL SELECT - seven days insertions timetable groupId

        public Generate()
        {
            InitializeComponent();
            FillComboGroup();

            displayFullTimetable();
        }

        private void DisplayData() // sessions table display bottom left corner table 
        {

            //Console.WriteLine("the ID to Refer is " + Group_id);
            //Console.WriteLine("comboBox Text " + comboBoxID.Text);

            string finalString = "";
            if (comboBoxID.Text != "")
            {
                finalString = "%" + comboBoxID.Text + "%";
            }
            

            string query = "Select * from sessions s where s.s_group_id Like @s_group_id ";
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
            sqlcomm.Parameters.AddWithValue("@s_group_id", finalString);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void onClick_UpdateAllParallel_Btn()
        {

            string finalString = "";
            if (comboBoxID.Text != "")
            {
                finalString = "%" + comboBoxID.Text + "%";
            }


            string query = "Select * from sessions s where s.s_group_id Like @id ";
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
            sqlcomm.Parameters.AddWithValue("@id", finalString);
            sqlcon.Open();
            SqlDataReader reader = sqlcomm.ExecuteReader(); //DataReader to update parallel all sessions


            //List<int> list = (from IDataRecord r in reader
            //                  select (int)r["sort_order"]
            //        ).ToList();

            //foreach (int s in list)
            //{
            //    Console.WriteLine("Data: " + s);
            //}


            if (reader.HasRows)
            {

                //ParallelMethods pm = new ParallelMethods();
                //Boolean isParallel = pm.checkParallelSessions(reader2.GetInt32(0));

                //ConsecutiveMethods cm = new ConsecutiveMethods();
                //Boolean isConsecutive = cm.checkConsecutiveSessions(reader2.GetInt32(0));
                ////&& reader2.GetInt32(18) == -1

                while (reader.Read())
                {
                    //Console.WriteLine("Is parallel: " + reader.GetString(15) + " IsConsecutive : " + reader.GetString(10));

                    string isParallel = reader.GetString(15);
                    string isConsecutive = reader.GetString(10);
                    int parallel_with = reader.GetInt32(16);
                    int consecutive = reader.GetInt32(17);

                    if ((isParallel.Equals("false")) && (!(parallel_with.Equals(-1))))
                    {
                        Console.WriteLine("Record ID: " + reader.GetInt32(0) + " Parallel With: " + parallel_with);

                        ParallelMethods pm = new ParallelMethods();
                        //Console.WriteLine("Find Session data : "+pm.getSessionData(106));
                        pm.NewUpdateParallelAll(reader.GetInt32(0), parallel_with, pm.getSessionData(parallel_with),reader.GetString(1));

                    }


                }

                //if (reader2.GetString(16) .Equals( false))//select sessions without already set parallel sessions
                //{
                //    if (reader2.GetString(10).Equals(false))// select sessions without already set consecutive sessions
                //    {

                //    }
                //}//x == false end



                ////variables to save previous session data
                //int prevId = 1;
                //int prevOrderNo = 0;
                //string prevSData = "";

                //while (reader.Read())
                //{

                //    if (reader.GetInt32(2) == prevOrderNo)//if parallel
                //    {
                //        Console.WriteLine(reader.GetInt32(0) + " <<  Now in update Parallel All " + prevId + " " + prevSData + "   " + reader.GetString(1) + " \n");

                //        UpdateParallelAll(reader.GetInt32(0), prevId, prevSData, reader.GetString(1));

                //    }
                //    else
                //    {
                //        prevId = reader.GetInt32(0);
                //        prevOrderNo = reader.GetInt32(2);
                //        prevSData = reader.GetString(1);
                //    }
                //    //Console.WriteLine("Current order" + reader.GetInt32(2) + "Prev order" + prevOrderNo);
                //}


            }
            else
            {
                reader.Close();
            }
            sqlcon.Close();

        }


        int remainingTimeMonday = 10;
        int remainingTimeTuesday = 10;
        int remainingTimeWednesday = 10;
        int remainingTimeThursday = 10;
        int remainingTimeFriday = 10;


        private void InsertDatatoSevenDays()
        {

            string finalString = "";
            if (comboBoxID.Text != "")
            {
                finalString = "%" + comboBoxID.Text + "%";
            }

            
            SqlConnection sqlcon2 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            string query2 = "Select * from sessions s where s.s_group_id Like @id ";
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
                int prevSDuration = 0;


                while (reader2.Read())
                {
                    ////hahahahahahahahaha
                    //if (reader2.GetString(15).Equals("false")) //isParallel {
                    //{
                    //    if (reader2.GetString(10).Equals("false"))//isConsecutive
                    //    {
                    //        //Add session to table
                    //        gtuc.InsertCellsTuesday(s_data, TuesdayStartTime, "Tuesday", start, end, s_id);
                    //    }
                    //    else
                    //    {
                    //        //Find consecutive session and put
                    //        ParallelMethods pm = new ParallelMethods();
                    //        string s_data_consecutive = pm.getSessionData(reader2.GetInt32(17));
                    //        gtuc.InsertCellsTuesday(s_data_consecutive, TuesdayStartTime, "Tuesday", start, end, s_id);
                    //    }
                    //}
                    ////hahahahahahahahaha


                    //Console.WriteLine("{0}\n{1}\n{2}\n{3}\n", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3));
                    if ((remainingTimeMonday >= reader2.GetInt32(3)))    // 5 >= 2
                    {  //Monday fill
                       
                        int s_id = reader2.GetInt32(0);
                        string s_data = reader2.GetString(1);
                        int s_order = reader2.GetInt32(2);
                        int s_duration = reader2.GetInt32(3);
                        
                        prevId = reader2.GetInt32(0);
                        prevOrderNo = reader2.GetInt32(2);
                        prevSData = reader2.GetString(1);
                        prevSDuration = reader2.GetInt32(3);

                        GroupTimetableUpdateCells gtuc = new GroupTimetableUpdateCells();
                        
                        int count = 1;
                        int start = 0;
                        int end = 0;
                        while (count <= s_duration)//add the same session twice to get better view
                        {
                            
                         start = MondayStartTime;
                         end = start + 100;

                            //Console.WriteLine("Session data: " + reader2.GetString(1));
                            //Console.WriteLine("Monday Start time: " + MondayStartTime + " start :" + start + " end: " + end + "\n");

                            MondayStartTime += 100;//increment duration of time slot

                            //hahahahahahahahaha
                            if (reader2.GetString(15).Equals("false")) //isParallel {
                            {
                                if (reader2.GetString(10).Equals("false"))//isConsecutive
                                {
                                    //Add session to table
                                    gtuc.InsertCellsMonday(s_data, MondayStartTime, "Monday", start, end, s_id);

                                }
                                else
                                {
                                    //Find consecutive session and put
                                    ParallelMethods pm = new ParallelMethods();
                                    string s_data_consecutive = pm.getSessionData(reader2.GetInt32(17));

                                    Console.WriteLine("s_data_consecutive to be added: " + s_data_consecutive);

                                    gtuc.InsertCellsMonday(s_data, MondayStartTime, "Monday", start, end, s_id);
                                    gtuc.InsertCellsMonday(s_data_consecutive, MondayStartTime+=(count*100), "Monday", start, end, s_id);
                                }
                            }
                            //hahahahahahahahaha

                            //gtuc.InsertCellsMonday(s_data, MondayStartTime,"Monday", start, end, s_id);
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
                        int start = 0;
                        int end = 0;
                        while (count <= s_duration)//add the same session twice to get better view
                        {
                            start = TuesdayStartTime ;
                            end = start + 100;
                            TuesdayStartTime += 100;//increment duration of time slot

                            //Console.WriteLine("Tuesday Start time: " + TuesdayStartTime + " start :" + start + " end: " + end);


                            //hahahahahahahahaha
                            if (reader2.GetString(15).Equals("false")) //isParallel {
                            {
                                if (reader2.GetString(10).Equals("false"))//isConsecutive
                                {
                                    //Add session to table
                                    gtuc.InsertCellsTuesday(s_data, TuesdayStartTime, "Tuesday", start, end, s_id);
                                }
                                else
                                {
                                    //Find consecutive session and put
                                    ParallelMethods pm = new ParallelMethods();
                                    string s_data_consecutive = pm.getSessionData(reader2.GetInt32(17));
                                    gtuc.InsertCellsTuesday(s_data_consecutive, TuesdayStartTime, "Tuesday", start, end, s_id);
                                }
                            }
                            //hahahahahahahahaha



                            //gtuc.InsertCellsTuesday(s_data, TuesdayStartTime, "Tuesday", start, end, s_id);
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

            remainingTimeMonday = 10; //reset time durations
            remainingTimeTuesday = 10; //reset time durations
            remainingTimeWednesday = 10;
            remainingTimeThursday = 10;
            remainingTimeFriday = 10;
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
                string updateParallelAll = "UPDATE sessions SET session_data=@newData WHERE record_id = @record_id;" +
                    "DELETE From sessions where record_id = @current_record_id;";
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






        private void FillComboGroup()
        {

            string query = "select s_group_id from sessions group by s_group_id ;";

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBoxID.Items.Add(DR[0]);

            }
            sqlcon.Close();
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

        int clickCount=0;
        private void btn_UpdateParallelAll_Click(object sender, EventArgs e)
        {

           
            //clickCount += 1;
            //if (clickCount > 1)
            //{
            //    MessageBox.Show("You have already Set Parallel sessions");
            //}
            //else
            //{
                onClick_UpdateAllParallel_Btn();
                DisplayData();
            //}
        }

        private void btn_InsertToSevenDays_Click(object sender, EventArgs e)
        {
            InsertDatatoSevenDays();
        }

       

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

            ParallelMethods pm = new ParallelMethods();
            pm.reverseAllParallelSessions(finalString);
            DisplayData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GroupTimetableUpdateCells gtuc = new GroupTimetableUpdateCells();
            gtuc.ResetALLCellsToNull();

            displayFullTimetable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ParallelSessionsManagement psm = new ParallelSessionsManagement();
            this.Hide();
            psm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ConsecutiveSessionsManagement csm = new ConsecutiveSessionsManagement();
            this.Hide();
            csm.Show();
        }

        private void btnOpt7_Click(object sender, EventArgs e)
        {
            SessionsManagement sessionsManagement = new SessionsManagement();

            this.Hide();
            sessionsManagement.Show();
        }
    }
}
