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
    public partial class Location1 : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public Location1()
        {
            InitializeComponent();
           

        }
        private void btnAddlocation_Click(object sender, EventArgs e)
        {
            AddLocationForm ar = new AddLocationForm();
            this.Hide();
            ar.Show();
        }

        private void btnUpReRoom_Click(object sender, EventArgs e)
        {
            UpdateDeleteLocationForm urr = new UpdateDeleteLocationForm();
            this.Hide();
            urr.Show();
        }

      

        private void Location1_Load(object sender, EventArgs e)
        {

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
