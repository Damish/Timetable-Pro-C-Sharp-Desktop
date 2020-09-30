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
    public partial class ViewEditSubjects : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name

        public ViewEditSubjects()
        {
            InitializeComponent();
            DisplayData();
            FillComboYear();
            FillComboSemester();

        }
        //copy methods
        private void btnOpt4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddSubjectDetails Addsubj = new AddSubjectDetails();
            Addsubj.ShowDialog();
        }

        private void ViewEditSubjects_Load(object sender, EventArgs e)
        {

        }

        private void lSem_Click(object sender, EventArgs e)
        {

        }

        private void lYr_Click(object sender, EventArgs e)
        {

        }

        private void lSubName_Click(object sender, EventArgs e)
        {

        }

        private void btnAddSub_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddSubjectDetails Addsubj = new AddSubjectDetails();
            Addsubj.ShowDialog();
        }

        private void FillComboYear()
        {

            string query = "Select * from groupID_option_year";

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBoxYr.Items.Add(DR[1]);

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
                dropdSem.Items.Add(DR[1]);

            }
            sqlcon.Close();
        }




        private void DisplayData()
        {

            string query = "Select Subject_ID,SubName,SubCode,OfferedYr,OfferedSem,LecHrs,TuteHrs,LabHrs,EvalHrs    from Subjects";

            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

            DataTable dt = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridView1.DataSource = dt;
            sqlcon.Close();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string selected_id = dataGridView1.CurrentRow.Cells["SubCode"].Value.ToString();
            string delete_row = "delete from Subjects where SubCode='" + selected_id + "'";

            sqlcon.Open();

            SqlCommand sqlcomm = new SqlCommand(delete_row, sqlcon);

            //sqlcomm.ExecuteNonQuery();

            int count = sqlcomm.ExecuteNonQuery();

            if (count > 0)
            {

                MessageBox.Show("Data Deleted Sucessfully");
                //    dataGridView1.Rows.Clear();

            }
            else
            {
                MessageBox.Show("Failed to Delete!!!");
            }
            sqlcon.Close();
            DisplayData();
        }
        int indexRow;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];
            //OfferedYr,OfferedSem,SubName,SubCode,LecHrs,TuteHrs,LabHrs,EvalHrs
            comboBoxYr.Text = row.Cells[3].Value.ToString();
            dropdSem.Text = row.Cells[4].Value.ToString();
            txtSubName.Text = row.Cells[1].Value.ToString();
            txtSubCode.Text = row.Cells[2].Value.ToString();
            numericUpDownLec.Text = row.Cells[5].Value.ToString();
            numericUpDownLab.Text = row.Cells[6].Value.ToString();
            numericUpDownTute.Text = row.Cells[7].Value.ToString();
            numericUpDownEval.Text = row.Cells[8].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtSubCode.Text.Length != 6 && txtSubCode.Text != "")
            {
                MessageBox.Show("Enter a valid subject code!");
            }
            else if (comboBoxYr.Text != "" && dropdSem.Text != "" && txtSubName.Text != "" && txtSubCode.Text != "" && numericUpDownLec.Text != "" && numericUpDownTute.Text != "" && numericUpDownLab.Text != "" && numericUpDownEval.Text != "")
            {
                string selected_id = dataGridView1.CurrentRow.Cells["Subject_ID"].Value.ToString();
            //OfferedYr,OfferedSem,SubName,SubCode,LecHrs,TuteHrs,LabHrs,EvalHrs
            SqlCommand cmd = new SqlCommand("Update Subjects set OfferedYr = @OfferedYr,OfferedSem = @OfferedSem, SubName = @SubName," +
                " SubCode = @SubCode, LecHrs = @LecHrs, TuteHrs = @TuteHrs, LabHrs = @LabHrs, EvalHrs = @EvalHrs WHERE Subject_ID = @Subject_ID ", sqlcon);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@OfferedYr", comboBoxYr.Text);
            cmd.Parameters.AddWithValue("@OfferedSem", dropdSem.Text);
            cmd.Parameters.AddWithValue("@SubName", txtSubName.Text);
            cmd.Parameters.AddWithValue("@SubCode", txtSubCode.Text);
            cmd.Parameters.AddWithValue("@LecHrs", numericUpDownLec.Text);
            cmd.Parameters.AddWithValue("@TuteHrs", numericUpDownTute.Text);
            cmd.Parameters.AddWithValue("@LabHrs", numericUpDownLab.Text);
            cmd.Parameters.AddWithValue("@EvalHrs", numericUpDownEval.Text);
            cmd.Parameters.AddWithValue("@Subject_ID", selected_id);

            sqlcon.Open();
            cmd.ExecuteNonQuery();
            //DisplayData();
            sqlcon.Close();

           // MessageBox.Show("Data Updated Sucessfully!");
            DisplayData();
                MessageBox.Show("Subject Data Updated sucessfully.");
            }
            else
            {

                MessageBox.Show("Fill all the blanks!");

            }
        }
        private void clearData()
        {
            comboBoxYr.Text = "";
            dropdSem.Text = "";
            txtSubName.Text = "";
            txtSubCode.Text = "";
            numericUpDownLec.Text = "";
            numericUpDownLab.Text = "";
            numericUpDownTute.Text = "";
            numericUpDownEval.Text = "";

        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void btnViewListSubj_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewEditSubjects viewsubj = new ViewEditSubjects();
            viewsubj.ShowDialog();
        }

        private void btnOpt3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddNewLecturer Addlec = new AddNewLecturer();
            Addlec.ShowDialog();
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
    }
}