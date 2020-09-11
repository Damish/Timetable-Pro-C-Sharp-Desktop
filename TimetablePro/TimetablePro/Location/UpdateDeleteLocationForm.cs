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
    public partial class UpdateDeleteLocationForm  : Form
    {
        SqlConnection con = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name
        public UpdateDeleteLocationForm()
        {
            InitializeComponent();
           

        }

        private void radioBtnuplec_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBoxupdateRoom_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxupCap_TextChanged(object sender, EventArgs e)
        {

        }

        //post your methods here


        private void UpdateDeleteLocationForm_Load(object sender, EventArgs e)
        {
            display_Locationdata();
        }

        public void display_Locationdata()
        {

            con.Open();
            SqlCommand command = con.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select* from Location ";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter(command);
            dta.Fill(dt);
            dataGridViewLocation.DataSource = dt;


            con.Close();


        }

        private void dataGridViewLocation_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridViewLocation.Rows[selectedRow];
            label2.Text = row.Cells[0].Value.ToString();
            comboupdatebuilding1.Text = row.Cells[1].Value.ToString();
            textBoxupdateRoom.Text = row.Cells[2].Value.ToString();
            String type = row.Cells[3].Value.ToString();

            if (type == "LectureRoom")
            {
                radioBtnuplec.Checked = true;
            }
            else
            {
                radioBtnuplab.Checked = true;
            }
            textBoxupCap.Text = row.Cells[4].Value.ToString();
            textBoxspecupd.Text = row.Cells[5].Value.ToString();

        }

        private void btnremloc_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from Location where LID ='" + label2.Text + "'", con);




                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Remove successfully!!!");
                display_Locationdata();





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error message");

            }
        }

        private void btnUpdateloc_Click(object sender, EventArgs e)
        {
            try {

                con.Open();
                SqlCommand cmd = new SqlCommand("update Location set BuildingName=@BuildingName,Room=@Room,RoomType=@RoomType,RoomCapacity=@RoomCapacity,Specialty=@Specialty where LID='"+label2.Text +"'", con);
                cmd.Parameters.AddWithValue("@BuildingName", comboupdatebuilding1.Text);
                cmd.Parameters.AddWithValue("@Room",textBoxupdateRoom.Text);
                if (radioBtnuplec.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@RoomType", radioBtnuplec.Text);
                }
                else if (radioBtnuplab.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@RoomType", radioBtnuplab.Text);
                }
                else
                {
                    MessageBox.Show("enter the RoomType");
                }

                cmd.Parameters.AddWithValue("@RoomCapacity", textBoxupCap.Text);
                cmd.Parameters.AddWithValue("@specialty", textBoxspecupd.Text);

                cmd.ExecuteNonQuery();




                con.Close();
                MessageBox.Show("Updated successfully!!!");
                display_Locationdata();

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error message");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NA_Times nA_Times = new NA_Times();

            this.Hide();
            nA_Times.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            studentGroupsManagement studentGroupsManagement1 = new studentGroupsManagement();

            this.Hide();
            studentGroupsManagement1.Show();
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
        private void btnOpt5_Click(object sender, EventArgs e)
        {
            TagsManagement tagsManagement = new TagsManagement();

            this.Hide();
            tagsManagement.Show();
        }
        private void btnOpt6_Click(object sender, EventArgs e)
        {
            Location1 locationForm = new Location1();
            this.Hide();
            locationForm.Show();

        }
        private void btnOpt9_Click(object sender, EventArgs e)
        {
            StatisticsForm statisticsForm = new StatisticsForm();
            this.Hide();
            statisticsForm.Show();

        }

       
    }
}
