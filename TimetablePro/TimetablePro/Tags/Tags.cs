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
    public partial class TagsManagement : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public TagsManagement()
        {
            InitializeComponent();
            DisplayData();

        }

        private void DisplayData()
        {
            
            string query = "Select id,code,tag_name,description from tags";
            
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

            DataTable dt = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridView1.DataSource = dt;
            sqlcon.Close();

        }

        private void TagsManagement_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTagName.Text != "" && txtDescription.Text != "")
            {

                string query = "Insert into tags values(@code,@name,@desc)";

                sqlcon.Open();

                SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

                String s = txtTagName.Text.ToUpper();
                String code = s[0]+""+s[1]+""+s[2];

                sqlcomm.Parameters.AddWithValue("@code", code);
                sqlcomm.Parameters.AddWithValue("@name", txtTagName.Text);
                sqlcomm.Parameters.AddWithValue("@desc", txtDescription.Text);

                sqlcomm.ExecuteNonQuery();

                MessageBox.Show("Record is sucessfully saved");

                DisplayData();

                sqlcon.Close();

            }
            else {

                MessageBox.Show("Please Fill Blanks!");

            }
            
        }

        int indexRow;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];

           // MessageBox.Show("indexRow" + indexRow);

            txtCode.Text = row.Cells[1].Value.ToString();
            txtTagName.Text = row.Cells[2].Value.ToString();
            txtDescription.Text = row.Cells[3].Value.ToString();

            btnCancel.Visible = true;
            btnNew.Visible = true;
            btnUpdate.Visible = true;
            btnSave.Visible = false;
            btnDelete.Text = "Delete Selected Row";
            btnDelete.Visible = true;
            labelCode.Visible = true;
            txtCode.Visible = true;
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {

            string selected_id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();

            string delete_row = "delete from tags where id='" + selected_id + "'";

            sqlcon.Open();
            SqlCommand sqlcomm = new SqlCommand(delete_row, sqlcon);
            int count = sqlcomm.ExecuteNonQuery();

            if (count > 0)
            {
                MessageBox.Show("Data Deleted Sucessfully");
            }
            else
            {
                MessageBox.Show("Failed to Delete!!!");
            }
            sqlcon.Close();
            clearAll();
            DisplayData();

        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtTagName.Text != "" && txtDescription.Text != "")
            {

                string selected_id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();

                String s = txtTagName.Text.ToUpper();
                String code = s[0] + "" + s[1] + "" + s[2];
                txtCode.Text = code;

                SqlCommand cmd = new SqlCommand("Update tags set code=@code, tag_name=@tag_name, description=@desc WHERE id= @id", sqlcon);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@code", txtCode.Text);
                cmd.Parameters.AddWithValue("@tag_name", txtTagName.Text);
                cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
                cmd.Parameters.AddWithValue("@id", selected_id);

                sqlcon.Open();
                cmd.ExecuteNonQuery();
                DisplayData();
                sqlcon.Close();

                MessageBox.Show("Data Updated Sucessfully!");
            
             } else {

                MessageBox.Show("Please Fill Blanks!");

            }

}


private void btnCancel_Click(object sender, EventArgs e)
        {
           
                clearAll();

                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnCancel.Visible = false;
                btnNew.Visible = false;
        
        }

        private void clearAll()
        {

            txtCode.Text = "";
            txtTagName.Text = "";
            txtDescription.Text = "";

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

            clearAll();

            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnCancel.Visible = false;
            btnNew.Visible = false;
            labelCode.Visible = false;
            txtCode.Visible = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearAll();

            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnCancel.Visible = false;
            btnNew.Visible = false;
            labelCode.Visible = false;
            txtCode.Visible = false;
        }

        private void btnOpt2_Click(object sender, EventArgs e)
        {
            studentGroupsManagement studentGroupsManagement = new studentGroupsManagement();

            this.Hide();
            studentGroupsManagement.Show();
        }

        private void btnOpt1_Click(object sender, EventArgs e)
        {

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

        private void btnOpt7_Click(object sender, EventArgs e)
        {
            SessionsManagement sessionsManagement = new SessionsManagement();

            this.Hide();
            sessionsManagement.Show();
        }
    }
}
