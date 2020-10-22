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
using System.Windows.Forms.DataVisualization;

namespace TimetablePro
{

    //change class name
    public partial class StatisticsForm: Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserverkisal.database.windows.net,1433;Initial Catalog=timetabledbkisal;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name
        public StatisticsForm()
        {
            InitializeComponent();
           

        }

        //post your methods here

        private void Dashboard_Load(object sender, EventArgs e)
        {
            userControl11.Show();
            userControl21.Hide();
            userControl31.Hide();

            userControl11.BringToFront();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            userControl11.Show();
            userControl21.Hide();
            userControl31.Hide();

            userControl11.BringToFront();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            userControl11.Hide();
            userControl21.Show();
            userControl31.Hide();

            userControl21.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            userControl11.Hide();
            userControl21.Hide();
            userControl31.Show();

            userControl31.BringToFront();
        }

        private void btnOpt1_Click(object sender, EventArgs e)
        {
            NA_Times nA_Times = new NA_Times();

            this.Hide();
            nA_Times.Show();
        }
        private void btnOpt2_Click(object sender, EventArgs e)
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

        private void btnOpt11_Click(object sender, EventArgs e)
        {
            CommonView commonView = new CommonView();

            this.Hide();
            commonView.Show();
        }
    }
}
