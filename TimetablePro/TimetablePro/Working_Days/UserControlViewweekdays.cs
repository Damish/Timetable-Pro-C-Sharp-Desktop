using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TimetablePro
{
    public partial class UserControlViewweekdays : UserControl
    {
        public UserControlViewweekdays()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
             
        }

        private void Weekdays_Click(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UserControlViewweekdays_Load(object sender, EventArgs e)
        {
            Display_dataWeekDay();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void labelwd_Click(object sender, EventArgs e)
        {

        }

        public void Display_dataWeekDay()
        {
            con.Open();
            SqlCommand command = con.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select* from weekdays ";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter(command);
            dta.Fill(dt);
            dataGridViewWD.DataSource = dt;

            con.Close();

          


        }

        private void dataGridViewWD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridViewWD.Rows[selectedRow];
            labelIdWD.Text = row.Cells[0].Value.ToString();
            comboBoxWDup.Text = row.Cells[1].Value.ToString();

            //for Monday
            String Monday = row.Cells[2].Value.ToString();
            if (Monday == "Monday") {

                checkmondayWDup.Checked = true;
            }
            else
            {
                checkmondayWDup.Checked = false;
         

            }
            //for Tuesday
            String Tuesday = row.Cells[3].Value.ToString();
            if (Tuesday == "Tuesday")
            {

                checkTuesdayWDup.Checked = true;
            }
            else
            {
                checkTuesdayWDup.Checked = false;


            }

            //for Wednesday
            String Wednesday = row.Cells[4].Value.ToString();
            if (Wednesday == "Wednesday")
            {

                checkwednWDup.Checked = true;
            }
            else
            {

                checkwednWDup.Checked = false;


            }


            //for Thursday
            String Thursday = row.Cells[5].Value.ToString();
            if (Thursday == "Thursday")
            {

                checkBoxThursWDup.Checked = true;
            }
            else
            {

                checkBoxThursWDup.Checked = false;


            }


            //for Friday

            String Friday = row.Cells[6].Value.ToString();
            if (Friday == "Friday")
            {

                checkFridayWDup.Checked = true;
            }
            else
            {

                checkFridayWDup.Checked = false;


            }


            comboBoxWstartTimeWDup.Text = row.Cells[7].Value.ToString();


            comboBoxWendTimeWDup.Text = row.Cells[8].Value.ToString();







            String radiotypeslot = row.Cells[9].Value.ToString();

            if (radiotypeslot == "one hour")
            {
               radioBtnoneHWDup.Checked = true;
            }
            else
            {
                radioBtnW30mWDup.Checked = true;
            }
            
        }

        private void DelbtnWD_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from weekdays where week_day_id ='" + labelIdWD.Text + "'", con);




                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Remove successfully!!!");
                Display_dataWeekDay();





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error message");

            }
        }

        private void UpdatebtnWD_Click(object sender, EventArgs e)
        {

            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("update weekdays set number_of_days=@number_of_days,monday=@monday,tuesday=@tuesday,wednesday=@wednesday,Thursday=@thurday,friday=@friday,start_time=@w_d_start_time,end_time=@w_d_end_time,time_slot=@time_slot where week_day_id='" + labelIdWD.Text + "'", con);
                cmd.Parameters.AddWithValue("@number_of_days", comboBoxWDup.Text);


                if (checkmondayWDup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@monday", checkmondayWDup.Text);

                }
                else {
                    cmd.Parameters.AddWithValue("@monday","");
                }

                if(checkTuesdayWDup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@tuesday", checkTuesdayWDup.Text);
                }
                else {
                    cmd.Parameters.AddWithValue("@tuesday","");
                }

                if (checkwednWDup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@wednesday", checkwednWDup.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@wednesday", "");
                }

                if (checkBoxThursWDup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@thurday", checkBoxThursWDup.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@thurday", "");
                }

                if (checkFridayWDup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@friday", checkFridayWDup.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@friday", "");
                }









                
                


                cmd.Parameters.AddWithValue("@w_d_start_time", comboBoxWstartTimeWDup.Text);
                cmd.Parameters.AddWithValue("@w_d_end_time", comboBoxWendTimeWDup.Text);
                 
                if (radioBtnoneHWDup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@time_slot", radioBtnoneHWDup.Text);
                }
                else if (radioBtnW30mWDup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@time_slot", radioBtnW30mWDup.Text);
                }
                else
                {
                    MessageBox.Show("enter the Time slot");
                }





                cmd.ExecuteNonQuery();




                con.Close();
                MessageBox.Show("Updated successfully!!!");
                Display_dataWeekDay();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error message");
            }
        }

        private void comboBoxWstartTimeWDup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewWD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
