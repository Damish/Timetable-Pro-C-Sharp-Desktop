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
    public partial class AddSubjectDetails : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name

        public AddSubjectDetails()
        {
            InitializeComponent();
            

        }

        private void AddSubjectDetails_Load(object sender, EventArgs e)
        {

        }

        private void btnAddSub_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddSubjectDetails Addsubj = new AddSubjectDetails();
            Addsubj.ShowDialog();
        }

        private void btnViewListSubj_Click(object sender, EventArgs e)
        {

            this.Hide();
            ViewEditSubjects viewsubj = new ViewEditSubjects();
            viewsubj.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlCommand cmdSave = new SqlCommand("Insert into Subjects(OfferedYr,OfferedSem,SubName,SubCode,LecHrs,TuteHrs,LabHrs,EvalHrs)Values(@OfferedYr,@OfferedSem,@SubName,@SubCode,@LecHrs,@TuteHrs,@LabHrs,@EvalHrs)", sqlcon);


            cmdSave.Parameters.AddWithValue("@OfferedYr", comboBoxYr.Text);
            cmdSave.Parameters.AddWithValue("@OfferedSem", dropdSem.Text);
            cmdSave.Parameters.AddWithValue("@SubName", txtSubName.Text);
            cmdSave.Parameters.AddWithValue("@SubCode", txtSubCode.Text);
            cmdSave.Parameters.AddWithValue("@LecHrs", numericUpDownLec.Text);
            cmdSave.Parameters.AddWithValue("@TuteHrs", numericUpDownTute.Text);
            cmdSave.Parameters.AddWithValue("@LabHrs", numericUpDownLab.Text);
            cmdSave.Parameters.AddWithValue("@EvalHrs", numericUpDownEval.Text);



            sqlcon.Open();
            cmdSave.ExecuteNonQuery();
            MessageBox.Show("Data Saved Sucessfully");
            sqlcon.Close();

            clearForm();
        }
        private void clearForm()
        {

            comboBoxYr.Text = "";
            dropdSem.Text = "";
            txtSubName.Text = "";
            txtSubCode.Text = "";
            numericUpDownLec.Text = "";
            numericUpDownTute.Text = "";
            numericUpDownLab.Text = "";
            numericUpDownEval.Text = "";

        }

        private void btnOpt3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddNewLecturer Addlec = new AddNewLecturer();
            Addlec.ShowDialog();
        }




        //copy methods

        private void btnOpt2_Click(object sender, EventArgs e)
        {

            studentGroupsManagement studentGroupsManagement = new studentGroupsManagement();

            this.Hide();
            studentGroupsManagement.Show();


        }

        private void btnOpt5_Click(object sender, EventArgs e)
        {

            TagsManagement tagsManagement = new TagsManagement();

            this.Hide();
            tagsManagement.Show();
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
    }
}
