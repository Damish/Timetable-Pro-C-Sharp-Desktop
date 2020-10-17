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

    //change class name
    public partial class RoomAllocation : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name
        public RoomAllocation()
        {
            InitializeComponent();
            display_Locationdata();

            FillComboTag();
            FillComboTagRoom();


            //GroupRoom

            FillComboGroupId();
            FillComboGroupRoom();

            //RoomForLecturers

            FillComboLecturerName();
            FillComboRoomLecturer();

            //RoomForSubjects


            FillComboSubjects();
            FillComboSubjectTag();
            FillComboSubjectRoom();




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnallocroomGroups_Click(object sender, EventArgs e)
        {
            if ((comboBoxGroupId.Text != string.Empty) && (comboBoxGroupRoom.Text != string.Empty))
            {
                //check duplicate values 
                SqlDataAdapter da = new SqlDataAdapter("select GroupName,Room from RoomsForGroups where GroupName ='" + comboBoxGroupId.Text + "' and Room= '" + comboBoxGroupRoom.Text + "'", sqlcon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("The room is already allocated for the selected Group/Sub-Group!!!.Please select another One");
                }
                else
                {
                    sqlcon.Open();
                    SqlCommand cmd = sqlcon.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO RoomsForGroups(GroupName,Room) VALUES ('" + comboBoxGroupId.Text + "','" + comboBoxGroupRoom.Text + "' )";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated!");
                    sqlcon.Close();
                    EmptyGroupfields();


                }
            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
        }

        //Reset Group section function

        public void EmptyGroupfields()
        {
            comboBoxGroupId.SelectedIndex = -1;
            comboBoxGroupRoom.SelectedIndex = -1;


        }
    

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RoomAllocation_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            SqlCommand command = sqlcon.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select* from RoomsForTags ";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter(command);
            dta.Fill(dt);
            dataGridViewRoomAllo.DataSource = dt;


            sqlcon.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        //post your methods here

        //combotag method

        void FillComboTag()
        {

            string sql = "select*from tags";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboBoxTag.Items.Add(ds.Tables[0].Rows[i][2]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }


        //method for room 
        public void FillComboTagRoom()
        {
            string sql = "select*from Location";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboBoxRoomTag.Items.Add(ds.Tables[0].Rows[i][2]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }
        //button for assign rooms for tag
        private void btnRoomtag_Click(object sender, EventArgs e)
        {
            if ((comboBoxTag.Text != string.Empty) && (comboBoxRoomTag.Text != string.Empty))
            {
                //check duplicate values 
                SqlDataAdapter da = new SqlDataAdapter("select Tag,Room from RoomsForTags where Tag ='" + comboBoxTag.Text + "' and Room= '" + comboBoxRoomTag.Text + "'", sqlcon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("The room is already allocated for the selected Tag!!!.Please select another One");
                }
                else
                {
                    sqlcon.Open();
                    SqlCommand cmd = sqlcon.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO RoomsForTags(Tag,Room) VALUES ('" + comboBoxTag.Text + "','" + comboBoxRoomTag.Text + "' )";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated!");
                    sqlcon.Close();
                    EmptytagfieldsOfTagSec();


                }
            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
        }


        //reset function room allocation for tag function

        public void EmptytagfieldsOfTagSec()
        {
            comboBoxTag.SelectedIndex = -1;
            comboBoxRoomTag.SelectedIndex = -1;


        }



        //Room Allocation For Groups
        //combobox fill Rooms for groups


        public void FillComboGroupId() {

            string sql = "select*from student_groups";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboBoxGroupId.Items.Add(ds.Tables[0].Rows[i][1]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }



        }

        public void FillComboGroupRoom()
        {
            string sql = "select*from Location";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboBoxGroupRoom.Items.Add(ds.Tables[0].Rows[i][2]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }




        //Room Allocation for Lecturers



        //Combobox fill for Lecturercombo


        public void FillComboLecturerName()
        {

            string sql = "select*from Lecturer";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboBoxLecturer.Items.Add(ds.Tables[0].Rows[i][2]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }



        }

        //fill comboRoom for Lecturer
        public void FillComboRoomLecturer()

        {
            string sql = "select*from Location";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboBoxRoomsForlect.Items.Add(ds.Tables[0].Rows[i][2]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }



        //Allocate Rooms For lecturer button

        private void btnAllocRoomForlec_Click(object sender, EventArgs e)
        {
            if ((comboBoxLecturer.Text != string.Empty) && (comboBoxRoomsForlect.Text != string.Empty))
            {
                //check duplicate values 
                SqlDataAdapter da = new SqlDataAdapter("select LecturerName,Room from RoomsForLecturers where LecturerName ='" + comboBoxLecturer.Text + "' and Room= '" + comboBoxRoomsForlect.Text + "'", sqlcon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("The room is already allocated for the selected Lecturer!!!.Please select another One");
                }
                else
                {
                    sqlcon.Open();
                    SqlCommand cmd = sqlcon.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO RoomsForLecturers(LecturerName,Room) VALUES ('" + comboBoxLecturer.Text + "','" + comboBoxRoomsForlect.Text + "' )";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated!");
                    sqlcon.Close();
                    EmptyLecturerfields();


                }
            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }

        }


        //reset rooms allocation field of lecturer
        public void EmptyLecturerfields()
        {
            comboBoxLecturer.SelectedIndex = -1;
            comboBoxRoomsForlect.SelectedIndex = -1;


        }


        //Allocate Room  for Subjects



        //fill combobox of subject
        public void FillComboSubjects()
        {

            string sql = "select*from Subjects";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboBoxSubject.Items.Add(ds.Tables[0].Rows[i][3]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }



        }

        //fill subject tags
        public void FillComboSubjectTag()
        {

            string sql = "select*from tags";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboBoxSubjectTag.Items.Add(ds.Tables[0].Rows[i][2]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }

        //fill combo subject Room

        public void FillComboSubjectRoom()
        {
            string sql = "select*from Location";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboBoxSubRoom.Items.Add(ds.Tables[0].Rows[i][2]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }
        //btn for allocate rooms for subjcts

        private void btnAllocateRoomSubj_Click(object sender, EventArgs e)
        {
            if ((comboBoxSubject.Text != string.Empty) && (comboBoxSubjectTag.Text != string.Empty) && (comboBoxSubRoom.Text != string.Empty))
            {
                //check duplicate values 
                SqlDataAdapter da = new SqlDataAdapter("select Subject,Tag,Room from RoomsForSubjects where Subject ='" + comboBoxSubject.Text + "' and Tag = '" + comboBoxSubjectTag.Text + "' and Room= '" + comboBoxSubRoom.Text + "'", sqlcon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("The room is already allocated for the selected Subject!!!.Please select another One");
                }
                else
                {
                    sqlcon.Open();
                    SqlCommand cmd = sqlcon.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO RoomsForSubjects(Subject,Tag,Room) VALUES ('" + comboBoxSubject.Text + "','" + comboBoxSubjectTag.Text + "','" + comboBoxSubRoom.Text + "' )";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated!");
                    sqlcon.Close();
                    EmptySubjectfields();


                }
            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }

           


        }

        //reset fields on Subjects
        public void EmptySubjectfields()
        {
            comboBoxSubject.SelectedIndex = -1;
            comboBoxSubjectTag.SelectedIndex = -1;
            comboBoxSubRoom.SelectedIndex = -1;


        }

        private void button7_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            SqlCommand command = sqlcon.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select* from RoomsForGroups ";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter(command);
            dta.Fill(dt);
            dataGridViewRoomAllo.DataSource = dt;


            sqlcon.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            SqlCommand command = sqlcon.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select* from RoomsForLecturers ";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter(command);
            dta.Fill(dt);
            dataGridViewRoomAllo.DataSource = dt;


            sqlcon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            SqlCommand command = sqlcon.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select* from RoomsForSubjects ";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter(command);
            dta.Fill(dt);
            dataGridViewRoomAllo.DataSource = dt;


            sqlcon.Close();
        }

        public void display_Locationdata()
        {

            sqlcon.Open();
            SqlCommand command = sqlcon.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select* from Location ";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter(command);
            dta.Fill(dt);
            dataGridViewRoomAllo.DataSource = dt;


            sqlcon.Close();
        }

        private void btnRoomdetails_Click(object sender, EventArgs e)
        {
            display_Locationdata();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RoomForTime roomForTime = new RoomForTime();
            this.Hide();
            roomForTime.Show();
        }

        private void btnsessionAll_Click(object sender, EventArgs e)
        {
            RoomsForSession roomsForSession = new RoomsForSession();
            this.Hide();
            roomsForSession.Show();
        }

        private void btnOpt11_Click(object sender, EventArgs e)
        {
            CommonView commonView = new CommonView();

            this.Hide();
            commonView.Show();
        }

        private void btnOpt6_Click(object sender, EventArgs e)
        {
            Location1 locationForm = new Location1();
            this.Hide();
            locationForm.Show();
        }

        private void btnOpt7_Click(object sender, EventArgs e)
        {
            SessionsManagement sessionsManagement = new SessionsManagement();

            this.Hide();
            sessionsManagement.Show();
        }

        private void btnOpt2_Click(object sender, EventArgs e)
        {
            studentGroupsManagement studentGroupsManagement = new studentGroupsManagement();

            this.Hide();
            studentGroupsManagement.Show();
        }
    }
}
