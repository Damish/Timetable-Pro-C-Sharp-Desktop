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
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
          

        }
        SqlConnection conn = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            lordstudentChartData();
        }

        public void lordstudentChartData()
        {
            DataTable dt = new DataTable();
            conn.Open();

            SqlCommand cmd = new SqlCommand("select Center_Name ,count(Emp_ID)as No_OF_LEC from Lecturer  group by Center_Name", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();

            String[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            chart1lec.Series[0].Points.DataBindXY(x, y);
            chart1lec.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;



        }
    }
}
