using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Hotel_Management.All_User_Controls
{
    public partial class UC_AddRoom : UserControl
    {

        private OleDbConnection conn = new OleDbConnection();
        public UC_AddRoom()
        {
            InitializeComponent();
            conn.ConnectionString = @" Provider = Microsoft.ACE.OLEDB.12.0; Data Source = D:\Project\HotelDatabase.accdb; Persist Security Info = False;";
        }

        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
            try
            {
               
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = conn;
                    string query = "SELECT * FROM Rooms";
                    cmd.CommandText = query;

                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        DataGridView1.DataSource = dt;


                        DataGridView1.EditMode = DataGridViewEditMode.EditOnEnter; 


                    }
                }
            }


            catch (Exception ex)
            {

                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRoomNo.Text != "" && txtType.Text != "" && txtBed.Text != "" && txtPrice.Text != "")
                {
                    string roomno = txtRoomNo.Text;
                    string roomtype = txtType.Text;
                    string bed = txtBed.Text;
                    string price = txtPrice.Text;

                    using (OleDbCommand cmdCheck = new OleDbCommand())
                    {
                        conn.Open();
                        cmdCheck.Connection = conn;
                        string checkQuery = "SELECT COUNT(*) FROM Rooms WHERE RoomNo = ?";
                        cmdCheck.Parameters.AddWithValue("roomno", txtRoomNo.Text);
                        cmdCheck.CommandText = checkQuery;

                        int roomCount = Convert.ToInt32(cmdCheck.ExecuteScalar());
                        if (roomCount > 0)
                        {
                            MessageBox.Show("Room number already exists. Please choose a different room number.", "Duplicate Room Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; 
                        }

                    }

                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        
                        cmd.Connection = conn;

                        string query = "INSERT INTO Rooms (RoomNo,RoomType,Bed,Price) VALUES(roomno, roomtype, bed, price)";
                        cmd.Parameters.AddWithValue("roomno", txtRoomNo.Text);
                        cmd.Parameters.AddWithValue("roomtype", txtType.Text);
                        cmd.Parameters.AddWithValue("bed", txtBed.Text);
                        cmd.Parameters.AddWithValue("price", txtPrice.Text);
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("your room is added");



                    }
                    UC_AddRoom_Load(this, null);
                    clearAll();


                }
                else
                {
                    MessageBox.Show("Fill All Fields.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error Here", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    clearAll();
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRoomNo.Text != "")
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        string query = "DELETE FROM Rooms WHERE RoomNo = ?";
                        cmd.Parameters.AddWithValue("roomno", txtRoomNo.Text);
                        cmd.CommandText = query;
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Room deleted successfully.");
                            UC_AddRoom_Load(this, null);
                           
                        }
                        else
                        {
                            MessageBox.Show("Room not found with the provided Room Number delete!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter Room Number to delete.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    clearAll();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRoomNo.Text != "")
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        string query = "UPDATE Rooms SET RoomType=?, Bed=?, Price=? WHERE RoomNo=?";
                        cmd.Parameters.AddWithValue("roomtype", txtType.Text);
                        cmd.Parameters.AddWithValue("bed", txtBed.Text);
                        cmd.Parameters.AddWithValue("price", txtPrice.Text);
                        cmd.Parameters.AddWithValue("roomno", txtRoomNo.Text);
                        cmd.CommandText = query;
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Room updated successfully.");
                            UC_AddRoom_Load(this, null);
                           
                        }
                        else
                        {
                            MessageBox.Show("Room not found with the provided  Room Number to update!.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter Room Number to update & Fill All Other Fields.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    clearAll();
                }
            }
        }
        
        public void clearAll()
        {
            txtRoomNo.Clear();
            txtType.SelectedIndex = -1;
            txtBed.SelectedIndex = -1;
            txtPrice.Clear();

        }

    }
}
