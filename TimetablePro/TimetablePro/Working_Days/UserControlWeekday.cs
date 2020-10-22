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
    public partial class UserControlWeekday : UserControl
    {
        public UserControlWeekday()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection vid = new SqlConnection(@"Server=tcp:timetableserverkisal.database.windows.net,1433;Initial Catalog=timetabledbkisal;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                {
                    SqlCommand xp = new SqlCommand("Insert into weekdays(number_of_days, monday, tuesday, wednesday, Thursday, friday, start_time, end_time, time_slot)values(@number_of_days,@monday,@tuesday,@wednesday,@Thursday,@friday,@start_time,@end_time,@time_slot)", vid);

                    xp.Parameters.AddWithValue("@number_of_days", comboBox1.Text);
                   

                    if (checkBox1.Checked == true) {

                        xp.Parameters.AddWithValue("@monday", checkBox1.Text);

                    }
                    else
                    {
                        xp.Parameters.AddWithValue("@monday","");
                    }
                    if (checkBox2.Checked == true)
                    {

                        xp.Parameters.AddWithValue("@tuesday", checkBox2.Text);

                    }
                    else
                    {
                        xp.Parameters.AddWithValue("@tuesday", "");
                    }
                    if (checkBox4.Checked == true)
                    {

                        xp.Parameters.AddWithValue("@wednesday",checkBox4.Text);

                    }
                    else
                    {
                        xp.Parameters.AddWithValue("@wednesday", "");
                    }
                    if (checkBox3.Checked == true)
                    {

                        xp.Parameters.AddWithValue("@Thursday", checkBox3.Text);

                    }
                    else
                    {
                        xp.Parameters.AddWithValue("@Thursday", "");
                    }
                    if (checkBox5.Checked == true)
                    {

                        xp.Parameters.AddWithValue("@friday", checkBox5.Text);

                    }
                    else
                    {
                        xp.Parameters.AddWithValue("@friday", "");
                    }

                     
                    xp.Parameters.AddWithValue("@start_time", comboBox4.Text);
                    xp.Parameters.AddWithValue("@end_time", comboBox5.Text);
                    if (radiobtn1h.Checked == true)
                    {
                        xp.Parameters.AddWithValue("@time_slot", radiobtn1h.Text);
                    }
                    else if (radiobtn30.Checked == true)
                    {
                        xp.Parameters.AddWithValue("@time_slot", radiobtn30.Text);
                    }
                    else
                    {

                        MessageBox.Show("Select  Time Slot");
                    }


                    vid.Open();
                    xp.ExecuteNonQuery();
                    vid.Close();
                    MessageBox.Show("Inserted successfully!!!");

                }

            }
            catch (Exception ex) {


                MessageBox.Show(ex.Message, "Insertion error");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UserControlWeekday_Load(object sender, EventArgs e)
        {
            


       

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radiobtn1h_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
