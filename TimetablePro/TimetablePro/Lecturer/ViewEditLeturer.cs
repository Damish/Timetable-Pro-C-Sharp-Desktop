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
    public partial class ViewEditLecturer : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name

        public ViewEditLecturer()
        {
            InitializeComponent();
            DisplayData();

        }
        //copy methods
        private void btnOpt4_Click(object sender, EventArgs e)
        {
            this.Hide();
            //AddSubjectDetails Addsubj = new AddSubjectDetails();
            //Addsubj.ShowDialog();
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

        private void DisplayData()
        {

            string query = "Select Emp_ID,Lecturer_Name,Faculty_Name,Department_Name,Center_Name,Building_Name,LNo,Rank_No    from Lecturer";

            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

            DataTable dt = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridView1.DataSource = dt;
            sqlcon.Close();

        }

        int indexRow;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];

            txtName.Text = row.Cells[1].Value.ToString();
            txtEmpId.Text = row.Cells[0].Value.ToString();
            dropdFac.Text = row.Cells[2].Value.ToString();
            dropdDept.Text = row.Cells[3].Value.ToString();
            dropdCent.Text = row.Cells[4].Value.ToString();
            dropdBuild.Text = row.Cells[5].Value.ToString();
            dropdLevel.Text = row.Cells[6].Value.ToString();
            txtRank.Text = row.Cells[7].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string selected_id = dataGridView1.CurrentRow.Cells["Emp_ID"].Value.ToString();
            string delete_row = "delete from Lecturer where Emp_ID='" + selected_id + "'";

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

        private void clearData()
        {
            txtName.Text = "";
            dropdFac.Text = "";
            dropdDept.Text = "";
            dropdCent.Text = "";
            dropdBuild.Text = "";
            dropdLevel.Text = "";
            txtRank.Text = "";
            txtEmpId.Text = "";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string selected_id = dataGridView1.CurrentRow.Cells["Emp_ID"].Value.ToString();
            //Lecturer_Name,Faculty_Name,Department_Name,Center_Name,Building_Name,LNo,Rank_No 
            SqlCommand cmd = new SqlCommand("Update Lecturer set Lecturer_Name = @Lecturer_Name,Faculty_Name = @Faculty_Name, Department_Name = @Department_Name," +
                " Center_Name = @Center_Name, Building_Name = @Building_Name, LNo = @LNo, Rank_No = @Rank_No WHERE Emp_ID= @Emp_ID", sqlcon);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Lecturer_Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Faculty_Name", dropdFac.Text);
            cmd.Parameters.AddWithValue("@Department_Name", dropdDept.Text);
            cmd.Parameters.AddWithValue("@Center_Name", dropdCent.Text);
            cmd.Parameters.AddWithValue("@Building_Name", dropdBuild.Text);
            cmd.Parameters.AddWithValue("@LNo", dropdLevel.Text);
            cmd.Parameters.AddWithValue("@Rank_No", txtRank.Text);
            cmd.Parameters.AddWithValue("@Emp_ID", selected_id);

            sqlcon.Open();
            cmd.ExecuteNonQuery();
            //DisplayData();
            sqlcon.Close();

            MessageBox.Show("Data Updated Sucessfully!");
            DisplayData();
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
    }
}
