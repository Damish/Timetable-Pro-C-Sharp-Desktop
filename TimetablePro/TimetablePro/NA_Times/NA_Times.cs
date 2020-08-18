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
    public partial class NA_Times : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public NA_Times()
        {
            InitializeComponent();
            DisplayData();

        }

        private void DisplayData()
        {
            
            string query = "Select * from NA_Times";
            
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

            DataTable dt = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridView1.DataSource = dt;
            sqlcon.Close();

        }
        private void DisplayData_selected_category()
        {

            string selected_category = dropCategory.Text;

            string query = "Select * from NA_Times where category = '" + selected_category + "'";


            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);

            DataTable dt = new DataTable();

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dt);

            dataGridView1.DataSource = dt;
            sqlcon.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            string query = "Insert into NA_Times values(@id,@day,@s_time,@e_time,@desc,@category)";

            sqlcon.Open();

            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
            
            sqlcomm.Parameters.AddWithValue("@id", txtNAid.Text);
            sqlcomm.Parameters.AddWithValue("@day", dropDay.Text);
            sqlcomm.Parameters.AddWithValue("@s_time", dateTimePicker1.Text);
            sqlcomm.Parameters.AddWithValue("@e_time", dateTimePicker2.Text);
            sqlcomm.Parameters.AddWithValue("@desc", txtDescription.Text);
            sqlcomm.Parameters.AddWithValue("@category", dropCategoryNew.Text);

            sqlcomm.ExecuteNonQuery();

            MessageBox.Show("Record is sucessfully saved");

            DisplayData();

            sqlcon.Close();

           


        }

        private void dropCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearAll();
            DisplayData_selected_category();
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            clearAll();
            DisplayData();

        }

        int indexRow;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];

            txtNAid.Text = row.Cells[1].Value.ToString();
            dropDay.Text = row.Cells[2].Value.ToString();
            dateTimePicker1.Text = row.Cells[3].Value.ToString();
            dateTimePicker2.Text = row.Cells[4].Value.ToString();
            txtDescription.Text = row.Cells[5].Value.ToString();
            dropCategoryNew.Text = row.Cells[6].Value.ToString();

            btnCancel.Visible = true;
            btnNew.Visible = true;
            btnUpdate.Visible = true;
            btnSave.Visible = false;
            btnDelete.Text = "Delete Selected Row";
            btnDelete.Visible = true;
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
           
                string selected_id = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();

                string delete_row = "delete from NA_Times where Id='" + selected_id + "'";

                sqlcon.Open();

                SqlCommand sqlcomm = new SqlCommand(delete_row, sqlcon);

                //sqlcomm.ExecuteNonQuery();

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
                DisplayData_selected_category();
           
        }

        private void clearAll() {

            txtNAid.Text = "";
            dropDay.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";
            txtDescription.Text = "";
            dropCategoryNew.Text = "";

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

            clearAll();

            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnCancel.Visible = false;
            btnNew.Visible = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearAll();

            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnCancel.Visible = false;
            btnNew.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            string selected_id = dataGridView1.CurrentRow.Cells["record_id"].Value.ToString();

            SqlCommand cmd = new SqlCommand("Update NA_Times set id=@id, day=@day, start_time=@s_time, end_time=@e_time, description=@desc, category=@category WHERE record_id= @RowID", sqlcon);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", txtNAid.Text);
            cmd.Parameters.AddWithValue("@day", dropDay.Text);
            cmd.Parameters.AddWithValue("@s_time", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@e_time", dateTimePicker2.Text);
            cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
            cmd.Parameters.AddWithValue("@category", dropCategoryNew.Text);
            cmd.Parameters.AddWithValue("@RowID", selected_id);

            sqlcon.Open();
            cmd.ExecuteNonQuery();
            DisplayData();
            sqlcon.Close();

            MessageBox.Show("Data Updated Sucessfully!");

        }

      

       
    }
}
