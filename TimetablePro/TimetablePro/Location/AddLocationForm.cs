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
    public partial class AddLocationForm : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name
        public AddLocationForm()
        {
            InitializeComponent();
           

        }

        //post your methods here
        SqlConnection conn = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


        private void comboBoxBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddRoom_Load(object sender, EventArgs e)
        {

        }

        private void btnaddloc_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into Location(BuildingName,Room,RoomType,RoomCapacity,Specialty)values(@BuildingName,@Room,@RoomType,@RoomCapacity,@Specialty)", conn);
                cmd.Parameters.AddWithValue("@BuildingName", comboBoxBuilding.Text);
                cmd.Parameters.AddWithValue("@Room", textRoom.Text);
                if (radioBtnlec.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@RoomType", radioBtnlec.Text);
                }
                else if (RadioBtnLab.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@RoomType", RadioBtnLab.Text);
                }
                else
                {
                    MessageBox.Show("enter the RoomType");
                }

                cmd.Parameters.AddWithValue("@RoomCapacity", textcapacity.Text);
                cmd.Parameters.AddWithValue("@specialty", textspec.Text);

                cmd.ExecuteNonQuery();




                conn.Close();
                MessageBox.Show("Added successfully!!!");




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error message");

            }
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
            SessionsManagement sessionsManagement = new SessionsManagement();

            this.Hide();
            sessionsManagement.Show();

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
    }
}
