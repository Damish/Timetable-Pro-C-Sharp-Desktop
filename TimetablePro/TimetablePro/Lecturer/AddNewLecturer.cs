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
    public partial class AddNewLecturer : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name

        public AddNewLecturer()
        {
            InitializeComponent();


        }
        //copy methods
        private void btnOpt4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddSubjectDetails Addsubj = new AddSubjectDetails();
            Addsubj.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtEmpId.Text.Length != 6 && txtEmpId.Text != "")
            {
                MessageBox.Show("Enter only 6 digits to the Employee ID!");
            }

            else if (txtEmpId.Text != "" && txtName.Text != "" && dropdFac.Text != "" && dropdDept.Text != "" && dropdCent.Text != ""
                && dropdBuild.Text != "" && dropdLevel.Text != "" )
                
            //&& txtRank.Text != ""
            {


                SqlCommand cmdSave = new SqlCommand("Insert into Lecturer(Emp_ID,Lecturer_Name,Faculty_Name,Department_Name,Center_Name,Building_Name,LNo,Rank_No)Values(@Emp_ID,@Lecturer_Name,@Faculty_Name,@Department_Name,@Center_Name,@Building_Name,@LNo,@Rank_No)", sqlcon);

                cmdSave.Parameters.AddWithValue("@Emp_ID", txtEmpId.Text);
                cmdSave.Parameters.AddWithValue("@Lecturer_Name", txtName.Text);
                cmdSave.Parameters.AddWithValue("@Faculty_Name", dropdFac.Text);
                cmdSave.Parameters.AddWithValue("@Department_Name", dropdDept.Text);
                cmdSave.Parameters.AddWithValue("@Center_Name", dropdCent.Text);
                cmdSave.Parameters.AddWithValue("@Building_Name", dropdBuild.Text);
                cmdSave.Parameters.AddWithValue("@LNo", dropdLevel.Text);
                cmdSave.Parameters.AddWithValue("@Rank_No", dropdLevel.Text+"."+txtEmpId.Text);
                //cmdSave.Parameters.AddWithValue("@Rank_No", txtRank.Text);


               // checklength();
                sqlcon.Open();
                cmdSave.ExecuteNonQuery();



                sqlcon.Close();

                clearForm();


                MessageBox.Show("Lecturer's Data saved sucessfully.");
            }
           
            else
            {

                    MessageBox.Show("Fill all the blanks!");
           
            }
    
        }
        private void clearForm()
        {

            txtEmpId.Text = "";
            txtName.Text = "";
            dropdBuild.Text = "";
            dropdCent.Text = "";
            dropdDept.Text = "";
            dropdFac.Text = "";
            dropdLevel.Text = "";
            //txtRank.Text = "";

        }
      

        private void btnOpt3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddNewLecturer Addlec = new AddNewLecturer();
            Addlec.ShowDialog();
        }

        private void btnAddLec_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddNewLecturer Addlec = new AddNewLecturer();
            Addlec.ShowDialog();
        }

        private void btnViewList_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewEditLecturer viewLec = new ViewEditLecturer();
            viewLec.ShowDialog();
        }

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

        private void btnOpt8_Click(object sender, EventArgs e)
        {
            WorkingDays workingDays = new WorkingDays();

            this.Hide();
            workingDays.Show();
        }

        private void txtEmpId_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void dropdLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

       
    }
}
