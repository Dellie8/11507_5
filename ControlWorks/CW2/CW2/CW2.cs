namespace CW2;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

//1 задача
[AttributeUsage(AttributeTargets.Property)]
public class SensitiveAttribute : Attribute
{
    public SensitiveAttribute() { }
}

public class AttributeFilter
{
    public static List<object> GetValidObjects(List<object> objects)
    {
        var result = new List<object>();
        
        foreach (var obj in objects)
        {
            if (obj == null) continue;
            
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            bool hasSensitiveProperty = properties.Any(prop => 
                Attribute.IsDefined(prop, typeof(SensitiveAttribute)));
            
            if (hasSensitiveProperty)
            {
                result.Add(obj);
            }
        }
        
        return result;
    }
}


//2 задача
public class ParallelProcessor
{
    public static void ProcessObjects(List<object> objects, int chunkCount = 4)
    {
        if (objects == null || objects.Count == 0)
            return;
        
        int chunkSize = (int)Math.Ceiling(objects.Count / (double)chunkCount);
        
        Parallel.For(0, chunkCount, i =>
        {
            int start = i * chunkSize;
            int end = Math.Min(start + chunkSize, objects.Count);
            
            int processedCount = 0;
            
            for (int j = start; j < end; j++)
            {
                Type objType = objects[j]?.GetType();
                processedCount++;
            }
            
            Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, " +
                              $"Обработано объектов: {processedCount}");
        });
    }
}



// классы-тесты
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [Sensitive]
    public string Password { get; set; }
    
    [Sensitive]
    public string CreditCardNumber { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [Sensitive]
    public string SSN { get; set; } 
    
    [Sensitive]
    public decimal Salary { get; set; }
}

//3 задание
public class CombinedProcessor
{
    public static void ProcessWithFilter(List<object> objects, int chunkCount = 4)
    {
        if (objects == null || objects.Count == 0)
            return;
        
        int chunkSize = (int)Math.Ceiling(objects.Count / (double)chunkCount);
        
        Parallel.For(0, chunkCount, i =>
        {
            int start = i * chunkSize;
            int end = Math.Min(start + chunkSize, objects.Count);
            
            var chunk = objects.Skip(start).Take(end - start).ToList();
            
            var validObjects = AttributeFilter.GetValidObjects(chunk);
            
            Console.WriteLine($"\n= Поток {Thread.CurrentThread.ManagedThreadId} =");
            Console.WriteLine($"Получено объектов в чанке: {chunk.Count}");
            Console.WriteLine($"Объектов с [Sensitive] атрибутами: {validObjects.Count}");
            
            foreach (var obj in validObjects)
            {
                Console.WriteLine($"  - Объект типа: {obj.GetType().Name}");
                
                var properties = obj.GetType().GetProperties();
                var sensitiveProps = properties.Where(p => 
                    Attribute.IsDefined(p, typeof(SensitiveAttribute))).ToList();
                
                if (sensitiveProps.Any())
                {
                    Console.WriteLine($"Sensitive свойства: {string.Join(", ", sensitiveProps.Select(p => p.Name))}");
                }
            }
        });
    }
}
