using ConsoleApp1.Models;
using ConsoleApp1.Services;
using MySql.Data.MySqlClient;
using System.Reflection;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=stepik;Uid=root;Pwd=260808AnAnAn;";

        // Использование блока using для автоматического закрытия соединения
        using (var connection = new MySqlConnection(connectionString))
        {
            // Открытие соединения
            connection.Open();
            Console.WriteLine("Подключение открыто");

            // Вывод информации о подключении
            PrintProperties(connection);
        }
        Console.WriteLine("Подключение закрыто");
    }

    // Метод для вывода всех свойств объекта
    private static void PrintProperties(object obj)
    {
        // Получение типа объекта
        Type type = obj.GetType();
        // Получение всех свойств объекта
        PropertyInfo[] properties = type.GetProperties();

        // Перебор всех свойств
        foreach (var property in properties)
        {
            // Получение значения свойства
            object value = property.GetValue(obj, null);
            // Вывод имени и значения свойства
            Console.WriteLine($"{property.Name}: {value}");

        }
    }

    public static void RegisterUser()
    {
        Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.Write("Произошла ошибка, произведен выход на главную страницу\n");
            return;
        }

        string[] parts = input.Split(new[] { ' ' }, 2);
        if (parts.Length < 2)
        {
            Console.Write("Произошла ошибка, произведен выход на главную страницу\n");
            return;
        }

        string firstName = parts[0];
        string lastName = parts[1];

        // Создание объекта User и заполнение обязательного поля FullName
        User user = new User();
        user.FullName = $"{firstName} {lastName}";

        bool result = UsersService.Add(user);

        if (result)
        {
            Console.Write($"Пользователь '{firstName} {lastName}' успешно добавлен.\n");
        }
        else
        {
            Console.Write("Произошла ошибка, произведен выход на главную страницу\n");
        }
    }
}