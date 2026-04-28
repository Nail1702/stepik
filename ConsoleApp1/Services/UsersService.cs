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
    }
}