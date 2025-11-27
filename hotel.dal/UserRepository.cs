using System;
using System.Data;
using System.Data.SqlClient;

namespace Hotel.DAL
{
    public class UserRepository
    {
        private string connString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=hotel;Integrated Security=True";

        public bool ValidateUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                // Simple check: Count rows where username & password match
                string sql = "SELECT COUNT(*) FROM tblUser WHERE username = @user AND password = @pass";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; // Returns True if user exists
                }
            }
        }
    }
}