using ConsoleApp1.Models;
using ConsoleApp1.Services;
using MySql.Data.MySqlClient;

class Program
{
    public static void Main()
    {
        string connectionString = "Server=localhost;Database=stepik;Uid=root;Pwd=260808AnAnAn;";

        // Показываем всех пользователей при старте
        ShowAllUsers(connectionString);

        // Главное меню
        while (true)
        {
            Console.WriteLine(@"
************************************************
* Добро пожаловать на онлайн платформу Stepik! *
************************************************

Выберите действие (введите число и нажмите Enter):

1. Зарегистрироваться
2. Закрыть приложение

************************************************
");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                RegisterUser();
                ShowAllUsers(connectionString); // показываем обновлённый список
            }
            else if (choice == "2")
            {
                Console.WriteLine("До свидания!");
                break;
            }
            else
            {
                Console.WriteLine("Неверный выбор. Попробуйте снова.");
            }
        }
    }

    // Вывод всех пользователей из таблицы users
    public static void ShowAllUsers(string connectionString)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        string sqlQuery = "SELECT id, full_name, join_date, is_active, solved_tasks FROM users;";
        using var command = new MySqlCommand(sqlQuery, connection);
        using var reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            Console.WriteLine($"{"id",-5} {"full_name",-30} {"join_date",-12} {"is_active",-10} {"solved_tasks",-15}");
            Console.WriteLine(new string('-', 75));

            while (reader.Read())
            {
                var id = reader["id"].ToString().PadRight(5);
                var fullName = reader["full_name"].ToString().PadRight(30);
                var joinDate = Convert.ToDateTime(reader["join_date"]).ToShortDateString().PadRight(12);
                var isActive = Convert.ToBoolean(reader["is_active"]) ? "Да" : "Нет";
                var solvedTasks = reader["solved_tasks"].ToString().PadRight(15);

                Console.WriteLine($"{id} {fullName} {joinDate} {isActive,-10} {solvedTasks}");
            }
        }
        else
        {
            Console.WriteLine("Пользователей нет.");
        }
        Console.WriteLine();
    }

    // Регистрация нового пользователя
    public static void RegisterUser()
    {
        Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Произошла ошибка, произведен выход на главную страницу\n");
            return;
        }

        string[] parts = input.Split(new[] { ' ' }, 2);
        if (parts.Length < 2)
        {
            Console.WriteLine("Произошла ошибка, произведен выход на главную страницу\n");
            return;
        }

        string firstName = parts[0];
        string lastName = parts[1];

        User user = new User();
        user.FullName = $"{firstName} {lastName}";

        bool result = UsersService.Add(user);

        if (result)
        {
            Console.WriteLine($"Пользователь '{firstName} {lastName}' успешно добавлен.\n");
        }
        else
        {
            Console.WriteLine("Произошла ошибка, произведен выход на главную страницу\n");
        }
    }
}