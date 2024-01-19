using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Hotel_Management.All_User_Controls
{
    public partial class Uc_Registration : UserControl
    {
        private OleDbConnection conn = new OleDbConnection();

        public Uc_Registration()
        {
            InitializeComponent();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Project\HotelDatabase.accdb;Persist Security Info=False;";
        }

        private void btnReserveRoom_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && txtPhone.Text != "" && txtNationality.Text != "" && txtGender.Text != ""
                   && txtDBirth.Text != "" && txtIdProof.Text != "" && txtAddress.Text != "" && txtCheckIn.Text != ""
                  && txtC_Bed.Text != "" && txtC_RoomType.Text != "" && txtC_Price.Text != "")
                {
                    using (OleDbCommand cmdCheck = new OleDbCommand())
                    {
                        conn.Open();
                        cmdCheck.Connection = conn;
                        string checkQuery = "SELECT COUNT(*) FROM customer WHERE idProof = @idProof";
                        cmdCheck.Parameters.AddWithValue("@idProof", txtIdProof.Text);
                        cmdCheck.CommandText = checkQuery;

                        int idProofCount = Convert.ToInt32(cmdCheck.ExecuteScalar());
                        if (idProofCount > 0)
                        {
                            MessageBox.Show("this  IdProof  number already exists. Please choose a different id number.", "Duplicate idProof Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; 
                        }

                    }

                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        
                        cmd.Connection = conn;

                        string query = "INSERT INTO customer (Name, Phone,Nationality, Gender, DBirth, idProof, Address, checkin, checkout, roomType, Bed, price)" +
                            " VALUES(@Name, @Phone, @Nationality, @Gender, @DBirth, @idProof, @Address, @checkin, @checkout, @roomType, @Bed, @price)";
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Phone", Convert.ToDecimal(txtPhone.Text));
                        cmd.Parameters.AddWithValue("@Nationality", txtNationality.Text);
                        cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                        cmd.Parameters.AddWithValue("@DBirth", DateTime.Parse(txtDBirth.Text));
                        cmd.Parameters.AddWithValue("@idProof", txtIdProof.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@checkin", DateTime.Parse(txtCheckIn.Text));
                        cmd.Parameters.AddWithValue("@checkout", DateTime.Parse(txtCheckIn.Text));
                        cmd.Parameters.AddWithValue("@roomType", txtC_RoomType.Text);
                        cmd.Parameters.AddWithValue("@Bed", txtC_Bed.Text);
                        cmd.Parameters.AddWithValue("@price", txtC_Price.Text);


                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("your reserve Room is added");
                    }
                }
                else
                {
                    MessageBox.Show("Please Fill All Fields.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                  
              

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }

            finally
            {
                if(conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    clearAll();

                }
            }

        }

        public void clearAll()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtNationality.Clear();
            txtGender.SelectedIndex = -1;
            txtDBirth.Value = DateTime.Now; 
            txtIdProof.Clear();
            txtAddress.Clear();
            txtCheckIn.Value = DateTime.Now; 
            txtC_Bed.SelectedIndex = -1;
            txtC_RoomType.SelectedIndex = -1;
            txtC_Price.Clear();
        }

    }
}
