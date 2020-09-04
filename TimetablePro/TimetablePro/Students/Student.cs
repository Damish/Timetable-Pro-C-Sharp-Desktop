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
    public partial class studentGroupsManagement : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public studentGroupsManagement()
        {
            InitializeComponent();
            FillComboYear();
            FillComboSemester();
            FillComboProgramme();
            DisplayData();

        }

        private void FillComboYear() {

            string query = "Select * from groupID_option_year";

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBoxYear.Items.Add(DR[1]);

            }
            sqlcon.Close();
        }

        private void FillComboSemester()
        {

            string query = "Select * from groupID_option_Semester";

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBoxSemester.Items.Add(DR[1]);

            }
            sqlcon.Close();
        }

        private void FillComboProgramme()
        {

            string query = "Select * from groupID_option_Programme";

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBoxProgramme.Items.Add(DR[1]);

            }
            sqlcon.Close();
        }

     
        private void DisplayData()
        {
            
            string query = "Select * from student_groups";
            
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

            DataTable dt = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridView1.DataSource = dt;
            sqlcon.Close();

        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (comboBoxYear.Text != "" && comboBoxSemester.Text != "" && comboBoxProgramme.Text != "" && textBox1.Text != "" && txtStudentCount.Text != "" && txtSubGroupCount.Text != "")
            {

                string year = comboBoxYear.Text;
                string semester = comboBoxSemester.Text;
                string programme = comboBoxProgramme.Text;

                int noOfStudents = Int32.Parse(textBox1.Text);
                int groupStudentCount = Int32.Parse(txtStudentCount.Text);
                int noOfSubGroups = Int32.Parse(txtSubGroupCount.Text);

                int noOfGroups = (noOfStudents) / (groupStudentCount); //no of main groups 100/30 = 3 

                int remainder = noOfStudents % groupStudentCount;

                int extra = 0;

                if (remainder != 0)
                {

                    extra += 1;//adds new group for excess students
                }
                int sub_group_student_Count = groupStudentCount / noOfSubGroups; // 30 / 2 = 15

                for (int x = 1; x <= noOfGroups + extra; x++)
                {

                    if (x > noOfGroups) // if( 4 > 3)
                    {
                        noOfSubGroups = 1;
                    }
                    for (int y = 1; y <= noOfSubGroups; y++)
                    {

                        string Group_ID = year + "" + semester + "." + programme + "." + x + "." + y; // generate group ID format

                        if (x > noOfGroups) // if( 4 > 3)
                        {
                            sub_group_student_Count = noOfStudents;
                        }
                        else
                        {
                            noOfStudents -= sub_group_student_Count;
                        }

                        string query = "Insert into student_groups values(@group_id,@year,@semester,@faculty,@department,@main_group,@sub_group_no,@student_count)";

                        sqlcon.Open();

                        SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

                        sqlcomm.Parameters.AddWithValue("@group_id", Group_ID);
                        sqlcomm.Parameters.AddWithValue("@year", comboBoxYear.Text);
                        sqlcomm.Parameters.AddWithValue("@semester", comboBoxSemester.Text);
                        sqlcomm.Parameters.AddWithValue("@faculty", comboBoxProgramme.Text);
                        sqlcomm.Parameters.AddWithValue("@department", comboBoxProgramme.Text);
                        sqlcomm.Parameters.AddWithValue("@main_group", x);
                        sqlcomm.Parameters.AddWithValue("@sub_group_no", y);
                        sqlcomm.Parameters.AddWithValue("@student_count", sub_group_student_Count);


                        sqlcomm.ExecuteNonQuery();

                        DisplayData();

                        sqlcon.Close();

                    }
                }

                MessageBox.Show("Record is sucessfully saved");
            }
            else {

                MessageBox.Show("Fill all the blanks!");

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NA_Times nA_Times = new NA_Times();

            this.Hide();
            nA_Times.Show();
        }

        private void btnOpt5_Click(object sender, EventArgs e)
        {
            TagsManagement tagsManagement = new TagsManagement();

            this.Hide();
            tagsManagement.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            comboBoxYear.Text = "";
            comboBoxSemester.Text = "";
            comboBoxProgramme.Text = "";
            textBox1.Text = "";
            txtStudentCount.Text = "";
            txtSubGroupCount.Text = "";
        }


        int indexRow;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];

            textBox1.Text = row.Cells[6].Value.ToString();
            txtStudentCount.Text = row.Cells[7].Value.ToString();
            txtSubGroupCount.Text = row.Cells[8].Value.ToString();
            comboBoxYear.Text = row.Cells[2].Value.ToString();
            comboBoxSemester.Text = row.Cells[3].Value.ToString();
            comboBoxProgramme.Text = row.Cells[5].Value.ToString();

            label4.Text = "Main group";
            label5.Text = "Sub Group No";
            label6.Text = "Student Count";

            label4.ForeColor = Color.OrangeRed;
            label5.ForeColor = Color.OrangeRed;
            label6.ForeColor = Color.OrangeRed;

            btnCancel.Visible = true;
            //btnNew.Visible = true;
            btnUpdate.Visible = true;
            btnSave.Visible = false;
            btnDelete.Text = "Delete Selected Row";
            btnDelete.Visible = true;
            

        }

        private void clearAll()
        {

            textBox1.Text = "";
            txtStudentCount.Text = "";
            txtSubGroupCount.Text = "";
            comboBoxYear.Text = "";
            comboBoxSemester.Text = "";
            comboBoxProgramme.Text = "";

            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;

            label4.Text = "No of Students";
            label5.Text = "Group Student Count";
            label6.Text = "No of Sub Groups";

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

            clearAll();

            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnCancel.Visible = false;
           //btnNew.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string selected_id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();

            string delete_row = "delete from student_groups where id='" + selected_id + "'";

            sqlcon.Open();

            SqlCommand sqlcomm = new SqlCommand(delete_row, sqlcon);

            //sqlcomm.ExecuteNonQuery();

            int count = sqlcomm.ExecuteNonQuery();

            if (count > 0)
            {
                clearAll();
                MessageBox.Show("Data Deleted Sucessfully");

            }
            else
            {
                MessageBox.Show("Failed to Delete!!!");
            }
            sqlcon.Close();
            DisplayData();

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            GroupID_Settings groupID_Settings = new GroupID_Settings();

            this.Hide();
            groupID_Settings.Show();
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
    }
}
