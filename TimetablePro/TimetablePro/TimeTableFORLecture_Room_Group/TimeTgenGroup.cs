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
    public partial class TimeTgenGroup : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserver2020.database.windows.net,1433;Initial Catalog=TimetableDB;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name
        public TimeTgenGroup()
        {
            InitializeComponent();
           

        }

        private void button3_Click(object sender, EventArgs e)
        {

            TimeTgenLect timeTgenLect = new TimeTgenLect();
            this.Hide();
            timeTgenLect.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeTgen timeTgen = new TimeTgen();
            this.Hide();
            timeTgen.Show();
        }

        //post your methods here







    }
}
