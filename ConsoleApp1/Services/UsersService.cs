using ConsoleApp1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public class UsersService
    {
        public static bool Add(User user)
        {
            try
            {
                string connectionString = Constant.ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "INSERT INTO users (FullName, Details, JoinDate, Avatar, IsActive) VALUES (@FullName, @Details, @JoinDate, @Avatar, @IsActive)";
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FullName", user.FullName);
                        command.Parameters.AddWithValue("@Details", user.Details);
                        command.Parameters.AddWithValue("@JoinDate", user.JoinDate);
                        command.Parameters.AddWithValue("@Avatar", user.Avatar);
                        command.Parameters.AddWithValue("@IsActive", user.IsActive);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
