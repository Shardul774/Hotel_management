using System;
using System.Data;
using System.Data.SqlClient;

namespace Hotel.DAL
{
    public class GuestRepository
    {
        private string connString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=hotel;Integrated Security=True";
        public DataTable GetGuests()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                // Select all columns into a DataTable
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblGuest", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        public bool AddGuest(string fName, string mName, string lName, string address, string contact, string email, string gender)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string sql = @"INSERT INTO tblGuest 
                               (GuestFName, GuestMName, GuestLName, GuestAddress, GuestContactNumber, GuestGender, GuestEmail, Status, Remarks) 
                               VALUES 
                               (@fName, @mName, @lName, @address, @contact, @gender, @email, 'Active', 'Available')";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@fName", fName);
                    cmd.Parameters.AddWithValue("@mName", mName ?? ""); // Handle nulls
                    cmd.Parameters.AddWithValue("@lName", lName);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@contact", contact);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@email", email);

                    try
                    {
                        con.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Database Error: " + ex.Message);
                    }
                }
            }
        }
    }
}