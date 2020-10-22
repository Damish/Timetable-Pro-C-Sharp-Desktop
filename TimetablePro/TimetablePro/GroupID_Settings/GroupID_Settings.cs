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
    public partial class GroupID_Settings : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserverkisal.database.windows.net,1433;Initial Catalog=timetabledbkisal;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


        public GroupID_Settings()
        {
           
                InitializeComponent();
                DisplayDataOnLoad();
          
        }

        private void DisplayDataOnLoad()
        {

            string query = "Select * from groupID_option_Programme";

            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
           
            DataTable dt = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridView1.DataSource = dt;
            sqlcon.Close();

        }

        public static string query;

        private void DisplayData_selected_category()
        {
           

            string selected_category = dropCategory.Text;
            if (selected_category == "Year") {
                 query = "Select * from groupID_option_year";
            }
            else if (selected_category == "Semester")
            {
                 query = "Select * from groupID_option_Semester";
            }
            else if (selected_category == "Programme")
            {
                 query = "Select * from groupID_option_Programme";
            }
            

            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

            DataTable dt = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridView1.DataSource = dt;
            sqlcon.Close();
        }

       private void btnSave_Click(object sender, EventArgs e)
        {

            if (dropCategory2.Text != "" &&  txtCode.Text != "" && txtName.Text != "")
            {

            string selected_category = dropCategory2.Text;
            if (selected_category == "Year")
            {
                query = "Insert into groupID_option_year values(@code,@name)";
            }
            else if (selected_category == "Semester")
            {
                query = "Insert into groupID_option_Semester values(@code,@name)";
            }
            else if (selected_category == "Programme")
            {
                query = "Insert into groupID_option_Programme values(@code,@name)";         
            }



            sqlcon.Open();

            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
            
           
            sqlcomm.Parameters.AddWithValue("@code", txtCode.Text);
            sqlcomm.Parameters.AddWithValue("@name", txtName.Text);

            sqlcomm.ExecuteNonQuery();

            MessageBox.Show("Record is sucessfully saved");

            DisplayData_selected_category();

            sqlcon.Close();

            }
            else{

                MessageBox.Show("Fill all the blanks!");

            }


        }

        private void dropCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            clearAll();
            DisplayData_selected_category();
        }

       

        int indexRow;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];

            txtCode.Text = row.Cells[1].Value.ToString();
            txtName.Text = row.Cells[2].Value.ToString();
            

            btnCancel.Visible = true;
            btnNew.Visible = true;
            //btnUpdate.Visible = true;
            btnSave.Visible = false;
            btnDelete.Text = "Delete Selected Row";
            btnDelete.Visible = true;
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {

            string selected_category = dropCategory.Text;

            string selected_id = dataGridView1.CurrentRow.Cells["record_id"].Value.ToString();
            
        
            if (selected_category == "Year")
            {
                query = "delete from groupID_option_year where record_id='" + selected_id + "'";
            }
            else if (selected_category == "Semester")
            {
                query = "delete from groupID_option_Semester where record_id='" + selected_id + "'";
            }
            else if (selected_category == "Programme")
            {
                query = "delete from groupID_option_Programme where record_id='" + selected_id + "'";
            }



                sqlcon.Open();

                SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

                //sqlcomm.ExecuteNonQuery();

                int count = sqlcomm.ExecuteNonQuery();

                if (count > 0)
                {

                clearAll();
                btnSave.Visible = true;
               // btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnCancel.Visible = false;
                btnNew.Visible = false;

                MessageBox.Show("Data Deleted Sucessfully");

                  
                }
                else
                {
                    MessageBox.Show("Failed to Delete!!!");
                }
                sqlcon.Close();
                
                DisplayData_selected_category();
           
        }

        private void clearAll() {

            txtCode.Text = "";          
            txtName.Text = "";

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

            clearAll();

            btnSave.Visible = true;
            //btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnCancel.Visible = false;
            btnNew.Visible = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearAll();

            //btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnCancel.Visible = false;
            btnNew.Visible = false;
            btnSave.Visible = true;

        }

        //private void btnUpdate_Click(object sender, EventArgs e)
        //{

        //    string selected_category = dropCategory.Text;

        //    string selected_id = dataGridView1.CurrentRow.Cells["record_id"].Value.ToString();


           


        //    if (selected_category == "Year")
        //    {
        //        query = "Update groupID_option_year set code=@code, name=@name WHERE record_id= @RowID";
        //    }
        //    else if (selected_category == "Semester")
        //    {
        //        query = "Update groupID_option_Semester set code=@code, name=@name WHERE record_id= @RowID";
        //    }
        //    else if (selected_category == "Programme")
        //    {
        //        query = "Update groupID_option_Programme set code=@code, name=@name WHERE record_id= @RowID";
        //    }

        //    SqlCommand cmd = new SqlCommand(query, sqlcon);


        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.AddWithValue("@id", txtCode.Text);
        //    cmd.Parameters.AddWithValue("@name", txtName.Text);
        //    cmd.Parameters.AddWithValue("@RowID", selected_id);

        //    sqlcon.Open();
            
        //    DisplayData_selected_category();

        //    sqlcon.Open();
        //    int count = cmd.ExecuteNonQuery();

        //    if (count > 0)
        //    {

        //        clearAll();
        //        btnSave.Visible = true;
        //        btnUpdate.Visible = false;
        //        btnDelete.Visible = false;
        //        btnCancel.Visible = false;
        //        btnNew.Visible = false;

        //        MessageBox.Show("Data Updated Sucessfully!");


        //    }
        //    else
        //    {
        //        MessageBox.Show("Failed to update!!!");
        //    }

        //    sqlcon.Close();

            

        //}

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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dropCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dropCategory.Text = dropCategory2.Text;
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

        private void btnOpt11_Click(object sender, EventArgs e)
        {
            CommonView commonView = new CommonView();

            this.Hide();
            commonView.Show();
        }
    }
}
