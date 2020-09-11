using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TimetablePro
{
    public partial class UserControlViewweekends : UserControl
    {
        public UserControlViewweekends()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserControlViewweekends_Load(object sender, EventArgs e)
        {

            Display_dataWeekEnds();

        }


        public void Display_dataWeekEnds()
        {
            con.Open();
            SqlCommand command = con.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select* from weekends ";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter(command);
            dta.Fill(dt);
            dataGridViewWE.DataSource = dt;

            con.Close();


        }

        private void dataGridViewWE_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridViewWE.Rows[selectedRow];
            labelIdWE.Text = row.Cells[0].Value.ToString();
            comboBoxWEup.Text = row.Cells[1].Value.ToString();

            //for Monday
            String Monday = row.Cells[2].Value.ToString();
            if (Monday == "Monday")
            {

                checkmondayWEup.Checked = true;
            }
            else
            {
                checkmondayWEup.Checked = false;


            }
            //for Tuesday
            String Tuesday = row.Cells[3].Value.ToString();
            if (Tuesday == "Tuesday")
            {

                checkBoxTuedayWEup.Checked = true;
            }
            else
            {
                checkBoxTuedayWEup.Checked = false;


            }

            //for Wednesday
            String Wednesday = row.Cells[4].Value.ToString();
            if (Wednesday == "Wednesday")
            {

                checkBoxWedWEup.Checked = true;
            }
            else
            {

                checkBoxWedWEup.Checked = false;


            }


            //for Thursday
            String Thursday = row.Cells[5].Value.ToString();
            if (Thursday == "Thursday")
            {

                checkBoxThursWEup.Checked = true;
            }
            else
            {

                checkBoxThursWEup.Checked = false;


            }


            //for Friday

            String Friday = row.Cells[6].Value.ToString();
            if (Friday == "Friday")
            {

                checkBoxfridayWEup.Checked = true;
            }
            else
            {

                checkBoxfridayWEup.Checked = false;


            }


            //for Saturday

            String Saturday = row.Cells[7].Value.ToString();
            if (Saturday == "Saturday")
            {

                checkBoxSatWEup.Checked = true;
            }
            else
            {

                checkBoxSatWEup.Checked = false;


            }

            //for Sunday

            String Sunday = row.Cells[8].Value.ToString();
            if (Sunday == "Sunday")
            {

                checkBoxSundayWEup.Checked = true;
            }
            else
            {

                checkBoxSundayWEup.Checked = false;


            }




            comboBoxWorkingHstartWEup.Text = row.Cells[9].Value.ToString();

            comboBoxWorkingHendWEup.Text = row.Cells[10].Value.ToString();



            comboBoxWorkingHstartWEup2.Text = row.Cells[11].Value.ToString();

            comboBoxWorkingHendWEup2.Text = row.Cells[12].Value.ToString();








            String radiotypeslotWE = row.Cells[13].Value.ToString();

            if (radiotypeslotWE == "one hour")
            {
                radioBtn1hWEup.Checked = true;
            }
            else
            {
                radioBtn30mWEup.Checked = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioBtn1hWEup_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void From_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnDelWE_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from weekends where week_end_id ='" + labelIdWE.Text + "'", con);




                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Remove successfully!!!");
                Display_dataWeekEnds();





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error message");

            }
        }



        //updateWE
        private void buttonupdateWE_Click(object sender, EventArgs e)
        {
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("update weekends set number_of_days=@number_of_days,monday=@monday,tuesday=@tuesday,wednesday=@wednesday,Thursday=@thurday,friday=@friday,saturday=@saturday,sunday=@sunday,w_d_start_time=@w_d_start_time,w_d_end_time=@w_d_end_time,w_e_start_time=@w_e_start_time,w_e_end_time=@w_e_end_time,time_slot=@time_slot where week_end_id='" + labelIdWE.Text + "'", con);
                cmd.Parameters.AddWithValue("@number_of_days", comboBoxWEup.Text);

                if (checkmondayWEup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@monday", checkmondayWEup.Text);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@monday", "");
                }

                if (checkBoxTuedayWEup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@tuesday", checkBoxTuedayWEup.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@tuesday", "");
                }

                if (checkBoxWedWEup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@wednesday", checkBoxWedWEup.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@wednesday", "");
                }

                if (checkBoxThursWEup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@thurday", checkBoxThursWEup.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@thurday", "");
                }

                if (checkBoxfridayWEup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@friday", checkBoxfridayWEup.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@friday", "");
                }

                if (checkBoxSatWEup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@saturday", checkBoxSatWEup.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@saturday", "");
                }

                if (checkBoxSundayWEup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@sunday", checkBoxSundayWEup.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sunday", "");
                }








               
            

                cmd.Parameters.AddWithValue("@w_d_start_time", comboBoxWorkingHstartWEup.Text);
                cmd.Parameters.AddWithValue("@w_d_end_time", comboBoxWorkingHendWEup.Text);
                cmd.Parameters.AddWithValue("@w_e_start_time", comboBoxWorkingHstartWEup2.Text);
                cmd.Parameters.AddWithValue("@w_e_end_time", comboBoxWorkingHendWEup2.Text);

                if (radioBtn1hWEup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@time_slot", radioBtn1hWEup.Text);
                }
                else if (radioBtn30mWEup.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@time_slot", radioBtn30mWEup.Text);
                }
                else
                {
                    MessageBox.Show("enter the Time slot");
                }





                cmd.ExecuteNonQuery();




                con.Close();
                MessageBox.Show("Updated successfully!!!");
                Display_dataWeekEnds();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error message");
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {

        }
    }
}
