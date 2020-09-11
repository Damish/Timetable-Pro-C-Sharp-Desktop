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
    public partial class UserControl2 : UserControl
    {

        SqlConnection conn = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public UserControl2()
        {
            InitializeComponent();

        
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void UserControl2_Load(object sender, EventArgs e)
        {
            
        }

        public void lordstudentChartData()
        {
            DataTable dt = new DataTable();
            conn.Open();
            
            SqlCommand cmd = new SqlCommand("select department,count(PersonID)as No_Of_st from stdemo where Faculty = '" +combogetf.Text+ "' group by department",conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();

            String[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++) {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            chartstudent.Series[0].Points.DataBindXY(x, y);
            chartstudent.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;



        }

        private void btnselectfaculty_Click(object sender, EventArgs e)
        {
            lordstudentChartData();

        }

        private void combogetf_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
