using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace CoffeeShopSystem
{
    public class CoffeeConfig
    {
        public Dictionary<string, int> Prices { get; set; }
        public Dictionary<string, int> Ingredients { get; set; }
    }

    class Program
    {
        private static string configPath = "config.json";
        private static string logPath = "sales_history.txt";
        private static CoffeeConfig config;

        static void Main(string[] args)
        {
            LoadConfig();
            
            
            MakeSale("Капучино", 150);
            
            EndShift();
        }
        
        static void LoadConfig()
        {
            if (File.Exists(configPath))
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<CoffeeConfig>(json);
                Console.WriteLine("Конфигурация загружена.");
            }
            else
            {
                config = new CoffeeConfig
                {
                    Prices = new Dictionary<string, int> { { "Капучино", 150 } },
                    Ingredients = new Dictionary<string, int> { { "Вода", 1000 }, { "Молоко", 500 }, { "Зерна", 200 } }
                };
                SaveConfig();
            }
        }

        static void SaveConfig() 
        {
            File.WriteAllText(configPath, JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true }));
        }
        
        static void MakeSale(string drinkName, int price)
        {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd}] Продано: {drinkName}, Цена: {price}";
            File.AppendAllLines(logPath, new[] { logEntry });
            Console.WriteLine($"Продажа зафиксирована: {drinkName}");
        }

        static void EndShift()
        {
            if (!File.Exists(logPath)) return;

            int totalRevenue = 0;
            var lines = File.ReadAllLines(logPath);

            foreach (var line in lines)
            {
                var parts = line.Split("Цена: ");
                if (parts.Length > 1 && int.TryParse(parts[1], out int price))
                {
                    totalRevenue += price;
                }
            }

            var report = new { TotalRevenue = totalRevenue, Date = DateTime.Now.ToString("yyyy-MM-dd") };
            string reportFileName = $"report_{DateTime.Now:yyyy_MM_dd}.json";
            
            File.WriteAllText(reportFileName, JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }));
            
            Console.WriteLine($"Смена окончена. Отчет {reportFileName} создан. Итоговая выручка: {totalRevenue}");
        }
    }
}