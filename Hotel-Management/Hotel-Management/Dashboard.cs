using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ToolTip = System.Windows.Forms.ToolTip;
using System.Data.OleDb;





namespace Hotel_Management
{
    
    
   
    public partial class Dashboard : Form
    {
        //Declare the OleDbConnection globally
        private OleDbConnection conn = new OleDbConnection();
        private ToolTip toolTip;
       
        public Dashboard()
        {
           
            InitializeComponent();
            //Declare the ConnectionString globally

            conn.ConnectionString = @" Provider = Microsoft.ACE.OLEDB.12.0; Data Source = D:\Project\HotelDatabase.accdb; Persist Security Info = False;";
            toolTip = new ToolTip();
           
        }
     

        private void btnLogOut_Click(object sender, EventArgs e)
        {
           
             
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            
        }




        private void btnHome_Click(object sender, EventArgs e)
        {
            
            uC_AddRoom1.Visible = false;
            uc_Registration1.Visible = false;
            uC_Customer_Details1.Visible = false;
            
            
          
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            uC_AddRoom1.Visible = true;
            uc_Registration1.Visible = false;
            uC_Customer_Details1.Visible = false;

            
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            uC_AddRoom1.Visible = false;
            uc_Registration1.Visible = true;
            uC_Customer_Details1.Visible = false;


        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            uC_AddRoom1.Visible = false;
            uc_Registration1.Visible = false;
            uC_Customer_Details1.Visible = true;

        }
    }
}
