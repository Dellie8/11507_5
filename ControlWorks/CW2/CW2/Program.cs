namespace CW2;
using System;
using System.Threading;
using System.Reflection;
using System.Collections;

class Program
{
    static void Main()
    {
        Console.WriteLine(" Тестирование AttributeFilter \n");
        
        var user1 = new User { Id = 1, Name = "Alice", Password = "pass123", CreditCardNumber = "4111-1111-1111-1111" };
        var user2 = new User { Id = 2, Name = "Bob", Password = "secret456", CreditCardNumber = "5500-0000-0000-0004" };
        var product = new Product { Id = 1, Name = "Laptop", Price = 999.99m };
        var employee = new Employee { Id = 1, Name = "Charlie", SSN = "123-45-6789", Salary = 75000m };
        
        var objects = new List<object> { user1, product, user2, employee };
        
        var validObjects = AttributeFilter.GetValidObjects(objects);
        
        Console.WriteLine($"Всего объектов: {objects.Count}");
        Console.WriteLine($"Объектов с [Sensitive] свойствами: {validObjects.Count}");
        
        foreach (var obj in validObjects)
        {
            Console.WriteLine($"  - {obj.GetType().Name}");
        }
        
        Console.WriteLine("\n Тестирование ParallelProcessor \n");
        
        var manyObjects = new List<object>();
        for (int i = 0; i < 1000; i++)
        {
            if (i % 3 == 0)
                manyObjects.Add(new User { Id = i, Name = $"User{i}", Password = $"pass{i}", CreditCardNumber = $"card{i}" });
            else if (i % 3 == 1)
                manyObjects.Add(new Product { Id = i, Name = $"Product{i}", Price = i * 10m });
            else
                manyObjects.Add(new Employee { Id = i, Name = $"Emp{i}", SSN = $"{i:000}-{i:00}-{i:0000}", Salary = i * 1000m });
        }
        
        Console.WriteLine("Обычная обработка (без фильтрации):\n");
        ParallelProcessor.ProcessObjects(manyObjects);
        
        Console.WriteLine("\n= Объединенная обработка (с фильтрацией) =\n");
        CombinedProcessor.ProcessWithFilter(manyObjects);
        
        Console.WriteLine("\n= Демонстрация с маленьким списком =\n");
        
        var testObjects = new List<object>
        {
            new User { Id = 1, Name = "John", Password = "pass1", CreditCardNumber = "1234" },
            new Product { Id = 2, Name = "Book", Price = 29.99m },
            new Employee { Id = 3, Name = "Jane", SSN = "987-65-4321", Salary = 50000m },
            new User { Id = 4, Name = "Mike", Password = "pass2", CreditCardNumber = "5678" },
            new Product { Id = 5, Name = "Pen", Price = 1.99m }
        };
        
        CombinedProcessor.ProcessWithFilter(testObjects, 2);
    }
}
