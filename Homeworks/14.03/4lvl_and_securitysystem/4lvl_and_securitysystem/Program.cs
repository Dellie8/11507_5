using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace Levels;

class Program
{
    static void Main()
    {
        // LEVEL 1

        Console.WriteLine("LEVEL 1\n");

        OrderProcessor processor = new OrderProcessor();

        processor.Logger += WriteRed;
        processor.Logger += WriteNormal;

        processor.Process();

        static void WriteRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void WriteNormal(string message)
        {
            Console.WriteLine(message);
        }
        
        // LEVEL 2

        List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "Брайн", Salary = 60000, Experience = 3 },
            new Employee { Name = "Макс", Salary = 45000, Experience = 7 },
            new Employee { Name = "Тоха", Salary = 80000, Experience = 10 }
        };

        EmployeeManager manager = new EmployeeManager();
            
        manager.OnEmployeeFound += ShowEmployee;

        Console.WriteLine("Зарплата больше 50000:");
        manager.FilterEmployees(
            employees,
            e => e.Salary > 50000);

        Console.WriteLine("\nСтаж больше 5 лет:");
        manager.FilterEmployees(
            employees,
            e => e.Experience > 5);


        static void ShowEmployee(Employee employee)
        {
            Console.WriteLine(
                $"{employee.Name} | {employee.Salary} | {employee.Experience}");
        }
        
        // LEVEL 3
        
        User user = new User
        {
            Name = "Gleb228"
        };

        UserNotifier notifier = new UserNotifier();
            
        notifier.OnNotify += SendEmail;
        notifier.OnNotify += SaveToDatabase;
        notifier.OnNotify += UpdateStatistics;

        notifier.SafeNotify(user);

        static void SendEmail(User user)
        {
            Console.WriteLine(
                $"Email отправлен пользователю {user.Name}");
        }

        static void SaveToDatabase(User user)
        {
            throw new Exception("Ошибка записи в базе данных");
        }

        static void UpdateStatistics(User user)
        {
            Console.WriteLine("Статистика обновлена");
        }
        
        // LEVEL 4

        Extensions.OnIteration += message => Console.WriteLine($"EVENT: {message}");

        List<string> names = new List<string>
        {
            "Брайн",
            "Макс",
            "Тоха"
        };

        names.ForEachWithIndex((name, index) =>
        {
            Console.WriteLine($"{index + 1}. {name}");
        });
        
        // SECURITY SYSTEM

        Sensor sensor = new Sensor();
        Siren siren = new Siren();
        Logger logger = new Logger();
            
        sensor.OnAlert += siren.Activate;
        sensor.OnAlert += logger.Log;
            
        logger.Logs.CollectionChanged += OnLogsChanged;
            
        sensor.Trigger("Движение в секторе A");
        sensor.Trigger("Критично: вскрытие двери");

        Console.WriteLine("\nКритичные сообщения:");

        AnalyzeLog(
            logger.Logs,
            log => log.Contains("Критично"));

        static void OnLogsChanged(
            object sender,
            NotifyCollectionChangedEventArgs e)
        {
            if (e.Action ==
                NotifyCollectionChangedAction.Add)
            {
                Console.WriteLine(
                    "Запись добавлена в базу данных");
            }
        }

        static void AnalyzeLog<T>(
            IEnumerable<T> logs,
            Predicate<T> filter)
        {
            foreach (var log in logs)
            {
                if (filter(log))
                {
                    Console.WriteLine(log);
                }
            }
        }
    }

}
