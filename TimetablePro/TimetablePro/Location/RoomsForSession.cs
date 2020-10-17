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
    public partial class RoomsForSession : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name
        public RoomsForSession()
        {
            InitializeComponent();

            FillComboGroupId();

            FillComboSessionRoom();



        }

        //post your methods here


        public void FillComboGroupId()
        {

            string sql = "select*from student_groups";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboBoxgroupID.Items.Add(ds.Tables[0].Rows[i][1]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }



        }

        private void comboBoxgroupID_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlcon.Open();
            SqlCommand command = sqlcon.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select record_id,session_data,s_subject_name,s_group_id,day,start_time,end_time,s_lecturer_name from sessions_kisal where s_group_id='" + comboBoxgroupID.Text+"'  ";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter(command);
            dta.Fill(dt);
            dataGridViewsessions.DataSource = dt;


            sqlcon.Close();
        }


        //display session


        public void diplaysession() {

            sqlcon.Open();
            SqlCommand command = sqlcon.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select record_id,session_data,s_subject_name,s_group_id,day,start_time,end_time,s_lecturer_name from sessions_kisal where s_group_id='" + comboBoxgroupID.Text + "'  ";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter(command);
            dta.Fill(dt);
            dataGridViewsessions.DataSource = dt;


            sqlcon.Close();



        }

        private void dataGridViewsessions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //fill room combo


        public void FillComboSessionRoom()
        {
            string sql = "select*from Location";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboselectRoom.Items.Add(ds.Tables[0].Rows[i][2]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }

        private void dataGridViewsessions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridViewsessions.Rows[selectedRow];
            label1.Text = row.Cells[0].Value.ToString();
            seeiondatalbl.Text = row.Cells[1].Value.ToString();
            label5.Text = row.Cells[4].Value.ToString();
            labelstart.Text = row.Cells[5].Value.ToString();
            labelend.Text = row.Cells[6].Value.ToString();
            lablelecturer.Text = row.Cells[7].Value.ToString();


        }
        //allocate rooms for session btn
        private void btnallocte_Click(object sender, EventArgs e)
        {
            if ((comboselectRoom.Text != string.Empty) && (seeiondatalbl.Text != string.Empty) && (label5.Text != string.Empty) && (labelstart.Text != string.Empty) && (labelend.Text != string.Empty) && (lablelecturer.Text != string.Empty))
            {
                //check duplicate values 
                SqlDataAdapter da = new SqlDataAdapter("select record_id from sessions_kisal where day ='" + label5.Text + "' and start_time = '" + labelstart.Text + "' and end_time= '" + labelend.Text + "'and location= '" + comboselectRoom.Text + "' ", sqlcon);
                Console.WriteLine("select record_id from sessions_kisal where day ='" + label5.Text + "' and start_time = '" + labelstart.Text + "' and end_time= '" + labelend.Text +  "'and location= '" + comboselectRoom.Text + "' ");
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("The room is already allocated for a session in same day,time and lecturer!!!.Please select another One");
                }
                else
                {
                    sqlcon.Open();
                    string session_dataupt = seeiondatalbl.Text+ "\n" + comboselectRoom.Text;
                    
                    SqlCommand cmd = sqlcon.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Update sessions_kisal set session_data= '" + session_dataupt + "',location='" + comboselectRoom.Text + "' where record_id='" + label1.Text + "'";
                    //cmd.CommandText = "INSERT INTO RoomsForSubjects(Subject,Tag,Room) VALUES ('" + comboBoxSubject.Text + "','" + comboBoxSubjectTag.Text + "','" + comboBoxSubRoom.Text + "' )";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated!");
                    
                    sqlcon.Close();

                    diplaysession();



                }
            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }

        }

        private void btnOpt1_Click(object sender, EventArgs e)
        {
            studentGroupsManagement studentGroupsManagement = new studentGroupsManagement();
            this.Hide();
            studentGroupsManagement.Show();
        }

        private void btnOpt6_Click(object sender, EventArgs e)
        {
            Location1 location1 = new Location1();
            this.Hide();
            location1.Show();
        }

        private void btnOpt11_Click(object sender, EventArgs e)
        {
            CommonView commonView = new CommonView();

            this.Hide();
            commonView.Show();
        }

        private void btnOpt7_Click(object sender, EventArgs e)
        {
            SessionsManagement sessionsManagement = new SessionsManagement();

            this.Hide();
            sessionsManagement.Show();
        }

        private void btnOpt2_Click(object sender, EventArgs e)
        {
            studentGroupsManagement studentGroupsManagement = new studentGroupsManagement();

            this.Hide();
            studentGroupsManagement.Show();
        }
    }
}
