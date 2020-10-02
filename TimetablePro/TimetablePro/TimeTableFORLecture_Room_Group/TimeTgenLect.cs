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


using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TimetablePro
{

    //change class name
    public partial class TimeTgenLect : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        string[,] timetabledemo = new string[13, 8];
        //change class name
        public TimeTgenLect()
        {
            InitializeComponent();

            FilldataLect();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeTgen timeTgen = new TimeTgen();
            this.Hide();
            timeTgen.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TimeTgenGroup timeTgenGroup = new TimeTgenGroup();
            this.Hide();
            timeTgenGroup.Show();
        }

        //post your methods here


        public void FilldataLect() {

            string sql = "select s_lecturer_name from sessions_kisal group by s_lecturer_name";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    comboBoxlecturer.Items.Add(ds.Tables[0].Rows[i][0]);

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }




        }

        private void comboBoxlecturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBoxlecturer.Text;
            clear2DArray();
            createtabalTemplet();
            dayControler(selected);
            checkTabalIsEmpty();
            rmoveEmpty();
            bindDataToGrid();
        }

        private void dayControler(String selectedVlaue)
        {
            Monday(selectedVlaue);
            Tuesday(selectedVlaue);
            Wednesday(selectedVlaue);
            Thursday(selectedVlaue);
            Friday(selectedVlaue);
            Saturday(selectedVlaue);
            Sunday(selectedVlaue);
        }

        private void createtabalTemplet()
        {
            string[] day = { "Monday", "Tueday", "wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string[] time = { "830", "930", "1030", "1130", "1230", "1330", "1430", "1530", "1630", "1730", "1830", "1930" };
            timetabledemo[0, 0] = "time";
            for (int i = 1; i <= day.Length; i++)
            {
                timetabledemo[0, i] = day[i - 1];
            }

            for (int i = 1; i <= time.Length; i++)
            {
                timetabledemo[i, 0] = time[i - 1];
            }
        }

        private void clear2DArray()
        {
            Array.Clear(timetabledemo, 0, timetabledemo.Length);
        }

        private void Monday(String selectedVlaue)
        {
            string sql = "select start_time,end_time,session_data,duration from sessions_kisal where s_lecturer_name='" + selectedVlaue + "' and day = 'Monday';";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    for (int j = 1; j <= 12; j++)
                    {
                        string stime = ds.Tables[0].Rows[i][0].ToString();
                        Console.WriteLine(stime == timetabledemo[j, 0]);
                        if (stime == timetabledemo[j, 0])
                        {
                            timetabledemo[j, 1] = ds.Tables[0].Rows[i][2].ToString();
                            int duretion = Int32.Parse(ds.Tables[0].Rows[i][3].ToString());
                            if (duretion == 2)
                            {
                                timetabledemo[j + 1, 1] = ds.Tables[0].Rows[i][2].ToString();
                            }
                        }
                    }

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }

        private void Tuesday(String selectedVlaue)
        {

            string sql = "select start_time,end_time,session_data,duration from sessions_kisal where s_lecturer_name='" + selectedVlaue + "' and day = 'Tuesday'; ";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    for (int j = 1; j <= 12; j++)
                    {
                        string stime = ds.Tables[0].Rows[i][0].ToString();
                        Console.WriteLine(stime == timetabledemo[j, 0]);
                        if (stime == timetabledemo[j, 0])
                        {
                            timetabledemo[j, 2] = ds.Tables[0].Rows[i][2].ToString();

                            int duretion = Int32.Parse(ds.Tables[0].Rows[i][3].ToString());
                            if (duretion == 2)
                            {
                                timetabledemo[j + 1, 2] = ds.Tables[0].Rows[i][2].ToString();
                            }
                        }
                    }

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }


        }

        private void Wednesday(String selectedVlaue)
        {
            string sql = "select start_time,end_time,session_data,duration from sessions_kisal where s_lecturer_name='" + selectedVlaue + "' and day = 'Wednesday'; ";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    for (int j = 1; j <= 12; j++)
                    {
                        string stime = ds.Tables[0].Rows[i][0].ToString();
                        Console.WriteLine(stime == timetabledemo[j, 0]);
                        if (stime == timetabledemo[j, 0])
                        {
                            timetabledemo[j, 3] = ds.Tables[0].Rows[i][2].ToString();

                            int duretion = Int32.Parse(ds.Tables[0].Rows[i][3].ToString());
                            if (duretion == 2)
                            {
                                timetabledemo[j + 1, 3] = ds.Tables[0].Rows[i][2].ToString();
                            }
                        }
                    }

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }


        }

        private void Thursday(String selectedVlaue)
        {

            string sql = "select start_time,end_time,session_data,duration from sessions_kisal where s_lecturer_name='" + selectedVlaue + "' and day = 'Thursday'; ";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    for (int j = 1; j <= 12; j++)
                    {
                        string stime = ds.Tables[0].Rows[i][0].ToString();
                        Console.WriteLine(stime == timetabledemo[j, 0]);
                        if (stime == timetabledemo[j, 0])
                        {
                            timetabledemo[j, 4] = ds.Tables[0].Rows[i][2].ToString();


                            int duretion = Int32.Parse(ds.Tables[0].Rows[i][3].ToString());
                            if (duretion == 2)
                            {
                                timetabledemo[j + 1, 4] = ds.Tables[0].Rows[i][2].ToString();
                            }
                        }
                    }

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }


        }

        private void Friday(String selectedVlaue)
        {
            string sql = "select start_time,end_time,session_data,duration from sessions_kisal where s_lecturer_name='" + selectedVlaue + "' and day = 'Friday'; ";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    for (int j = 1; j <= 12; j++)
                    {
                        string stime = ds.Tables[0].Rows[i][0].ToString();
                        Console.WriteLine(stime == timetabledemo[j, 0]);
                        if (stime == timetabledemo[j, 0])
                        {
                            timetabledemo[j, 5] = ds.Tables[0].Rows[i][2].ToString();


                            int duretion = Int32.Parse(ds.Tables[0].Rows[i][3].ToString());
                            if (duretion == 2)
                            {
                                timetabledemo[j + 1, 5] = ds.Tables[0].Rows[i][2].ToString();
                            }
                        }
                    }

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }


        }

        private void Saturday(String selectedVlaue)
        {

            string sql = "select start_time,end_time,session_data,duration from sessions_kisal where s_lecturer_name='" + selectedVlaue + "' and day = 'Saturday'; ";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    for (int j = 1; j <= 12; j++)
                    {
                        string stime = ds.Tables[0].Rows[i][0].ToString();
                        Console.WriteLine(stime == timetabledemo[j, 0]);
                        if (stime == timetabledemo[j, 0])
                        {
                            timetabledemo[j, 6] = ds.Tables[0].Rows[i][2].ToString();

                            int duretion = Int32.Parse(ds.Tables[0].Rows[i][3].ToString());
                            if (duretion == 2)
                            {
                                timetabledemo[j + 1, 6] = ds.Tables[0].Rows[i][2].ToString();
                            }
                        }
                    }

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }


        }

        private void Sunday(String selectedVlaue)
        {
            string sql = "select start_time,end_time,session_data,duration from sessions_kisal where s_lecturer_name='" + selectedVlaue + "' and day = 'Sunday'; ";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);


            try
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    for (int j = 1; j <= 12; j++)
                    {
                        string stime = ds.Tables[0].Rows[i][0].ToString();
                        Console.WriteLine(stime == timetabledemo[j, 0]);
                        if (stime == timetabledemo[j, 0])
                        {
                            timetabledemo[j, 7] = ds.Tables[0].Rows[i][2].ToString();

                            int duretion = Int32.Parse(ds.Tables[0].Rows[i][3].ToString());
                            if (duretion == 2)
                            {
                                timetabledemo[j + 1, 7] = ds.Tables[0].Rows[i][2].ToString();
                            }
                        }
                    }

                }
                sqlcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }

        private void bindDataToGrid()
        {
            dataGridViewTable3.Rows.Clear();
            dataGridViewTable3.Refresh();

            int h = timetabledemo.GetLength(0);
            int w = timetabledemo.GetLength(1);

            this.dataGridViewTable3.ColumnCount = w;

            for (int r = 0; r < h; r++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dataGridViewTable3);

                for (int c = 0; c < w; c++)
                {
                    row.Cells[c].Value = timetabledemo[r, c];
                }

                this.dataGridViewTable3.Rows.Add(row);
            }
        }

        private void rmoveEmpty()
        {

            for (int i = 1; i <= 12; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    if (timetabledemo[i, j] == null)
                    {
                        timetabledemo[i, j] = "x";
                    }
                }
            }
        }

        private void checkTabalIsEmpty()
        {
            Boolean isEmpty = true;
            for (int i = 1; i <= 12; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    if(timetabledemo[i,j] != null)
                    {
                        isEmpty = false;
                    }
                }
            }

            if (isEmpty)
            {
                MessageBox.Show("No sessions availble for this lecturer!!!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("text.pdf", FileMode.Create));

            doc.Open();
            //iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance("I:\\csharpproject\\Timetable-Pro-C-Sharp-Desktop\\TimetablePro\\TimetablePro\\Resources\\121.png");
            //PNG.ScaleToFit(50f, 100f);
            //doc.Add(PNG);

            Paragraph para = new Paragraph("'" + comboBoxlecturer.Text + "'");
            doc.Add(para);



            PdfPTable table = new PdfPTable(dataGridViewTable3.Columns.Count);

            for (int j = 0; j < dataGridViewTable3.Columns.Count; j++)
            {
                table.AddCell(new Phrase(dataGridViewTable3.Columns[j].HeaderText));
            }
            table.HeaderRows = 1;

            for (int i = 0; i < dataGridViewTable3.Rows.Count; i++)
            {
                for (int k = 0; k < dataGridViewTable3.Columns.Count; k++)
                {

                    if (dataGridViewTable3[k, i].Value != null)
                    {
                        table.AddCell(new Phrase(dataGridViewTable3[k, i].Value.ToString()));
                    }
                }
            }
            doc.Add(table);
            MessageBox.Show("!!!TimeTable Printed !!!!");
            doc.Close();

        }

        private void btnOpt11_Click(object sender, EventArgs e)
        {
            CommonView commonView = new CommonView();
            this.Hide();
            commonView.Show();
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

        private void btnOpt7_Click(object sender, EventArgs e)
        {
            SessionsManagement sessionsManagement = new SessionsManagement();

            this.Hide();
            sessionsManagement.Show();
        }
    }
}
