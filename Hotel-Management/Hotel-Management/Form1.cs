using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ToolTip = System.Windows.Forms.ToolTip;

namespace Hotel_Management
{
    public partial class Form1 : Form
    {
        //Declare the OleDbConnection globally
        private OleDbConnection conn = new OleDbConnection();
        private ToolTip toolTip1;
        public Form1()
        {
            InitializeComponent();
            //Declare the ConnectionString globally

            conn.ConnectionString = @" Provider = Microsoft.ACE.OLEDB.12.0; Data Source = D:\Project\HotelDatabase.accdb; Persist Security Info = False;";

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = conn;
                   string query= "SELECT *FROM Login WHERE User=@User AND Password=@Password";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@User", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        int count = 0;
                      
                        while (reader.Read())
                        {
                            count++;
                          
                        }

                        if (count == 1)
                        {
                            labelError.Visible = false;
                            this.Hide();
                            Dashboard ds = new Dashboard();
                            ds.Show();
                        }
                        else
                        {
                            labelError.Visible = true;
                          
                            txtPassword.Clear();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occurred: " + ex.Message + "\n\nPlease resolve the error before attempting to start the program again.");
            }
            finally
            {
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
          
        }
        private void btnShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (btnShowPass.Checked )
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
