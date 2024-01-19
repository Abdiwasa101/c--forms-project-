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
using System.Windows.Forms.DataVisualization.Charting;

namespace Hotel_Management.User_Controls
{
    public partial class UC_Customer_Details : UserControl
    {
        private OleDbConnection conn = new OleDbConnection();

        public UC_Customer_Details()
        {
            InitializeComponent();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Project\HotelDatabase.accdb;Persist Security Info=False;";
        }

        private void UC_Customer_Details_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = conn;
                    string query = "SELECT * FROM customer";
                    cmd.CommandText = query;

                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        DataGridView1.DataSource = dt;





                    }
                }
            }


            catch (Exception ex)
            {

                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();

                }
            }
        }

        private void btnShowCharts_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT Gender, COUNT(*) AS GenderCount FROM customer GROUP BY Gender", conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        chart1.Series["Gender"].Points.Clear();
                        while (reader.Read())

                        {
                            string gender = reader["Gender"].ToString();
                            int genderCount = Convert.ToInt32(reader["GenderCount"]);


                            double percentage = (genderCount / (double)TotalCustomerCount()) * 100;


                            chart1.Series["Gender"].Points.AddXY($"{gender} ({percentage:0.##}%)", genderCount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        private int TotalCustomerCount()
        {
            using (OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM customer", conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {


                conn.Open();

                if (DataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = DataGridView1.SelectedRows[0].Index;
                    string customerId = DataGridView1.Rows[selectedRowIndex].Cells["C_ID"].Value.ToString(); // Updated to use "C_ID"


                    using (OleDbCommand cmd = new OleDbCommand($"DELETE FROM customer WHERE C_ID = {customerId}", conn))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
                            // Refresh the DataGridView after deletion
                            UC_Customer_Details_Load(this, null);
                            conn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
    }
}   
