using ConsoleApp1.Models;
using MySql.Data.MySqlClient;
using System;

namespace ConsoleApp1.Services
{
    public class UsersService
    {
        public static bool Add(User user)
        {
            try
            {
                string connectionString = Constant.ConnectionString; 
                using var connection = new MySqlConnection(connectionString);
                connection.Open();

                // Вставляем в реальные колонки таблицы users
                string sql = @"INSERT INTO users (full_name, join_date, is_active) 
                               VALUES (@full_name, @join_date, @is_active)";
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@full_name", user.FullName);
                command.Parameters.AddWithValue("@join_date", DateTime.Now.Date); 
                command.Parameters.AddWithValue("@is_active", true);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Для отладки можно вывести ex.Message
                return false;
            }
        }

        public static User Get(string fullName)
        {
            string connectionString = Constant.ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // Используем реальные имена колонок: full_name, details, join_date, avatar, is_active
                string sqlQuery = "SELECT full_name, details, join_date, avatar, is_active FROM users WHERE full_name = @fullName AND is_active = 1";
                using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@fullName", fullName);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = new User();
                            user.FullName = reader.IsDBNull(reader.GetOrdinal("full_name")) ? null : reader.GetString(reader.GetOrdinal("full_name"));
                            user.Details = reader.IsDBNull(reader.GetOrdinal("details")) ? null : reader.GetString(reader.GetOrdinal("details"));
                            user.JoinDate = reader.IsDBNull(reader.GetOrdinal("join_date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("join_date"));
                            user.Avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString(reader.GetOrdinal("avatar"));
                            user.IsActive = reader.IsDBNull(reader.GetOrdinal("is_active")) ? false : reader.GetBoolean(reader.GetOrdinal("is_active"));
                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }
}