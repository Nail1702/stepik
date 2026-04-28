using ConsoleApp1.Models;
using ConsoleApp1.Services;
using System;

class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine(@"
************************************************
* Добро пожаловать на онлайн платформу Stepik! *
************************************************

Выберите действие (введите число и нажмите Enter):

1. Зарегистрироваться
2. Войти
3. Закрыть приложение

************************************************
");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                RegisterUser();
            }
            else if (choice == "2")
            {
                LoginUser();
            }
            else if (choice == "3")
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

    public static void LoginUser()
    {
        Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Пользователь не найден, произведен выход на главную страницу\n");
            return;
        }

        User user = UsersService.Get(input);

        if (user != null)
        {
            Console.WriteLine($"Пользователь '{user.FullName}' успешно вошел\n");
        }
        else
        {
            Console.WriteLine("Пользователь не найден, произведен выход на главную страницу\n");
        }
    }
}