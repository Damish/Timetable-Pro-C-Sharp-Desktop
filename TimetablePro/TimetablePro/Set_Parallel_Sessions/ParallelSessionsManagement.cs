﻿using System;
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
    public partial class ParallelSessionsManagement : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserverkisal.database.windows.net,1433;Initial Catalog=timetabledbkisal;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public ParallelSessionsManagement()
        {
            InitializeComponent();
            DisplayDataTable1();
            FillComboGroup();

        }



        private void DisplayDataTable1()
        {
            //get selected combobox1 group id
            string groupid = "";
            if (comboBoxID.Text != "")
            {
                groupid = "%" + comboBoxID.Text + "%";
            }
            string tag = "";
            if (comboBoxID.Text != "")
            {
                if (comboBoxTag.Text != "All")
                {
                    tag = "%" + comboBoxTag.Text + "%";
                }
                else
                {

                    tag = "%%";
                }
            }
            string query =
                "Select s.record_id as Id,s.session_data as Session, s.parallel_with, s.isParallel, s.isConsecutive, s.consecutive, s.sort_order as [Order]" +
                "from sessions s " +
                "where s.s_group_id Like @id and " +
                       "s.s_tag Like @tag "+
                       "AND s.isConsecutive LIKE '%false%'";
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
            sqlcomm.Parameters.AddWithValue("@id", groupid);
            sqlcomm.Parameters.AddWithValue("@tag", tag);
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            sda.Fill(dt1);
            dataGridView1.DataSource = dt1;

            DataTable dt2 = new DataTable();
            SqlDataAdapter sda2 = new SqlDataAdapter(sqlcomm);
            sda2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            sqlcon.Close();

        }


        private void FillComboGroup()
        {

            string query = "select s_group_id from sessions group by s_group_id ;";

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBoxID.Items.Add(DR[0]);

            }
            sqlcon.Close();
        }





        private void TagsManagement_Load(object sender, EventArgs e)
        {

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

        private void comboBoxID_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayDataTable1();
        }

        private void comboBoxSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayDataTable1();
        }

        private void comboBoxTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayDataTable1();
        }

        String tempOrder = "";
        int table1SelectedID  =0;
        int table2SelectedID = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tempOrder = dataGridView2.CurrentRow.Cells["Order"].Value.ToString();
            table1SelectedID = Int32.Parse(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());

            if (table1SelectedID == table2SelectedID)
            {
                MessageBox.Show("Same ID selected! Please select Different ID.Thanks!!!");
                lblSelected1.Text = "";
            }
            else
            {
            lblSelected1.Text = table1SelectedID.ToString();
            //Console.WriteLine("Selected order : "+dataGridView1.CurrentRow.Cells["Order"].Value.ToString());
            MessageBox.Show("Selected id table 1 : " + table1SelectedID);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            table2SelectedID = Int32.Parse(dataGridView2.CurrentRow.Cells["Id"].Value.ToString());

            if (table1SelectedID == table2SelectedID)
            {
                MessageBox.Show("Same ID selected! Please select Different ID.Thanks!!!");
                lblSelected2.Text ="";
            }
            else
            {
                lblSelected2.Text = table2SelectedID.ToString();
                MessageBox.Show("Selected id table 2 : " + table2SelectedID);
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    int count = 0;
        //    foreach (DataGridViewRow row in dataGridView2.SelectedRows)
        //    {
               
        //        tempid = row.Cells["id"].Value.ToString();
            

        //    using (SqlConnection con4 = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
        //    {
        //        con4.Open();
        //        string setParallelorder = "UPDATE sessions set sort_order = @order WHERE record_id = @id;";
        //        using (SqlCommand sqlcomm1 = new SqlCommand(setParallelorder, con4))
        //        {
        //            sqlcomm1.Parameters.AddWithValue("@order", tempOrder);
        //            sqlcomm1.Parameters.AddWithValue("@id", tempid);
        //            sqlcomm1.ExecuteNonQuery();
        //        }
        //    }
        //        count += 1;
        //    }
        //    MessageBox.Show(count +" Parallel sessions updated Sucessfully!");
        //    DisplayDataTable1();
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            //foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            //{

            //    table2SelectedID = Int32.Parse(row.Cells["Id"].Value.ToString());
            if (table1SelectedID != 0 && table2SelectedID != 0)
            {


                using (SqlConnection con4 = new SqlConnection(@"Server=tcp:timetableserverkisal.database.windows.net,1433;Initial Catalog=timetabledbkisal;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    con4.Open();
                    string setParallelorder = "UPDATE sessions set sort_order = @order WHERE record_id = @id;" +
                        "UPDATE sessions set parallel_with = @table2selectedid WHERE record_id = @id;"+
                        "UPDATE sessions set isParallel = 'true' WHERE record_id = @table2selectedid;";
                using (SqlCommand sqlcomm1 = new SqlCommand(setParallelorder, con4))
                    {
                        //sqlcomm1.Parameters.AddWithValue("@order", tempOrder);
                        sqlcomm1.Parameters.AddWithValue("@order", tempOrder);
                        sqlcomm1.Parameters.AddWithValue("@table2selectedid", table2SelectedID);
                        sqlcomm1.Parameters.AddWithValue("@id", table1SelectedID);
                        sqlcomm1.ExecuteNonQuery();
                    }
                }
                count += 1;
                //}
                lblSelected1.Text = "";
                lblSelected2.Text = "";

                MessageBox.Show(" Parallel sessions updated Sucessfully!");
                DisplayDataTable1();
            }
            else
            {
                MessageBox.Show(" Select group id and  two sessions to set Parallel!!!");
            }
        }

        private void onClick_UpdateAllParallel_Btn()
        {

            string finalString = "";
            if (comboBoxID.Text != "")
            {
                finalString = "%" + comboBoxID.Text + "%";
            }
            string query = "Select * from sessions s where s.s_group_id Like @id ";
            SqlCommand sqlcomm = new SqlCommand(query, sqlcon);
            sqlcomm.Parameters.AddWithValue("@id", finalString);
            sqlcon.Open();
            SqlDataReader reader = sqlcomm.ExecuteReader(); //DataReader to update parallel all sessions
            
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    //Console.WriteLine("Is parallel: " + reader.GetString(15) + " IsConsecutive : " + reader.GetString(10));
                    string isParallel = reader.GetString(15);
                    string isConsecutive = reader.GetString(10);
                    int parallel_with = reader.GetInt32(16);
                    int consecutive = reader.GetInt32(17);

                    if ((isParallel.Equals("false")) && (!(parallel_with.Equals(-1))))
                    {
                        Console.WriteLine("Record ID: " + reader.GetInt32(0) + " Parallel With: " + parallel_with);

                        ParallelMethods pm = new ParallelMethods();
                        //Console.WriteLine("Find Session data : "+pm.getSessionData(106));
                        pm.NewUpdateParallelAll(reader.GetInt32(0), parallel_with, pm.getSessionData(parallel_with), reader.GetString(1));

                    }
                }
                
            }
            else
            {
                reader.Close();
            }
            sqlcon.Close();
        }

        string prevText = "";

        private void button2_Click(object sender, EventArgs e)
        {
            string finalString = "";
            if (comboBoxID.Text != "")
            {
                finalString = "%" + comboBoxID.Text + "%";
                ParallelMethods pm = new ParallelMethods();
                pm.reverseAllParallelSessions(finalString);
                DisplayDataTable1();
                prevText = "";
                lblSelected1.Text = "";
                lblSelected2.Text = "";
                MessageBox.Show("All Parallel Sessions Removed Sucessfully!");
            }
            else
            {
                MessageBox.Show("Select group to reset data!!!");
            }

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

        private void button3_Click(object sender, EventArgs e)
        {
            ConsecutiveSessionsManagement csm = new ConsecutiveSessionsManagement();
            this.Hide();
            csm.Show();
        }

        private void lblSelected1_Click(object sender, EventArgs e)
        {

        }

        private void lblSelected2_Click(object sender, EventArgs e)
        {

        }

       

        int count = 0;
        
        private void button4_Click(object sender, EventArgs e)
        {
            
            count += 1;
            
            if (comboBoxID.Text != "" && !(prevText.Equals(comboBoxID.Text)))
            {
                prevText = comboBoxID.Text;
                onClick_UpdateAllParallel_Btn();
                DisplayDataTable1();
                lblSelected1.Text = "";
                lblSelected2.Text = "";
                MessageBox.Show("Changes Applied Sucessfully!");

            }
            else
            {
                MessageBox.Show("Select group to Apply Changes or Reset Data!!!");
            }




        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnOpt5_Click(object sender, EventArgs e)
        {
            TagsManagement tagsManagement = new TagsManagement();
            this.Hide();
            tagsManagement.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Generate generate = new Generate();

            this.Hide();
            generate.Show();
        }
    }
}
