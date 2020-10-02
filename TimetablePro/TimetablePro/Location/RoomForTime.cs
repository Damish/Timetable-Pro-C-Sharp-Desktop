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
    public partial class RoomForTime : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name
        public RoomForTime()
        {
            InitializeComponent();
            FillComboRooms();

            //display

            Display_TimealloRoom();



        }

        //post your methods here
        public void FillComboRooms()
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

                    comboBoxRoomT.Items.Add(ds.Tables[0].Rows[i][2]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }

        private void buttonallocteTimeR_Click(object sender, EventArgs e)
        {
            if ((comboBoxRoomT.Text != string.Empty) && (comboBox2DAY.Text != string.Empty) && (comboBox3startTime.Text != string.Empty) && (comboBox4EndTime.Text != string.Empty))
            {
                if ((comboBox3startTime.Text != comboBox4EndTime.Text )) {

                    //check duplicate values 
                    SqlDataAdapter da = new SqlDataAdapter("select Room,Day,Start_time,End_time from TimeForRooms where Room ='" + comboBoxRoomT.Text + "' and Day = '" + comboBox2DAY.Text + "' and Start_time= '" + comboBox3startTime.Text + "'and End_time='"+ comboBox4EndTime.Text + "'", sqlcon);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count >= 1)
                    {
                        MessageBox.Show("The room is already Reserved for the selected Time!!!.Please select another One");
                    }
                    else
                    {
                        sqlcon.Open();
                        SqlCommand cmd = sqlcon.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO TimeForRooms(Room,Day,Start_time,End_time) VALUES ('" + comboBoxRoomT.Text + "','" + comboBox2DAY.Text + "','" + comboBox3startTime.Text + "','" + comboBox4EndTime.Text + "' )";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Room Reserved!");
                        sqlcon.Close();
                        EmptySubjectfields();
                        Display_TimealloRoom();




                    }
                }
                else {
                    MessageBox.Show("Do not add Same Tme slot for Start time and end Time!!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }



        }
        //empty fields
        public void EmptySubjectfields()
        {
            comboBoxRoomT.SelectedIndex = -1;
            comboBox2DAY.SelectedIndex = -1;
            comboBox3startTime.SelectedIndex = -1;
            comboBox4EndTime.SelectedIndex = -1;


        }

        //display data

        public void Display_TimealloRoom() {
            sqlcon.Open();
            SqlCommand command = sqlcon.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select* from TimeForRooms ";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter(command);
            dta.Fill(dt);
            dataGridViewRCR.DataSource = dt;


            sqlcon.Close();




        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand("delete from TimeForRooms where Rec_id ='" + label5.Text + "'", sqlcon);




                cmd.ExecuteNonQuery();
                sqlcon.Close();
                MessageBox.Show("Remove successfully!!!");
                Display_TimealloRoom();





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error message");

            }
            sqlcon.Close();
        }

        private void dataGridViewRCR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridViewRCR.Rows[selectedRow];
            label5.Text = row.Cells[0].Value.ToString();
        }

        private void btnOpt6_Click(object sender, EventArgs e)
        {
            Location1 location1 = new Location1();

            this.Hide();
            location1.Show();
        }

        private void btnOpt2_Click(object sender, EventArgs e)
        {
            studentGroupsManagement studentGroupsManagement1 = new studentGroupsManagement();

            this.Hide();
            studentGroupsManagement1.Show();
        }

        private void btnOpt11_Click(object sender, EventArgs e)
        {
            CommonView commonView = new CommonView();

            this.Hide();
            commonView.Show();
        }

        private void btnOpt7_Click(object sender, EventArgs e)
        {
            SessionsManagement sessionsManagement = new SessionsManagement();

            this.Hide();
            sessionsManagement.Show();
        }
    }
}
