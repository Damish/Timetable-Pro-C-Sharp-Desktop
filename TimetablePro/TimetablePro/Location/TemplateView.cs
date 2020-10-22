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
    public partial class TemplateView : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=tcp:timetableserverkisal.database.windows.net,1433;Initial Catalog=timetabledbkisal;Persist Security Info=False;User ID=demo;Password=myAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        //change class name
        public TemplateView()
        {
            InitializeComponent();
           

        }

        private void btnOpt11_Click(object sender, EventArgs e)
        {
            CommonView commonView = new CommonView();

            this.Hide();
            commonView.Show();
        }

        //post your methods here





    }
}
