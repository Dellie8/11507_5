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
            
            // Пример продажи
            MakeSale("Капучино");
            MakeSale("Капучино");
            
            EndShift();
            
            SaveConfig();
        }
        
        static void LoadConfig()
        {
            if (File.Exists(configPath))
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<CoffeeConfig>(json);
                Console.WriteLine("Конфигурация загружена.");
                Console.WriteLine($"Остатки: Вода={config.Ingredients["Вода"]}, " +
                                  $"Молоко={config.Ingredients["Молоко"]}, " +
                                  $"Зерна={config.Ingredients["Зерна"]}");
            }
            else
            {
                config = new CoffeeConfig
                {
                    Prices = new Dictionary<string, int> { { "Капучино", 150 } },
                    Ingredients = new Dictionary<string, int> { { "Вода", 1000 }, { "Молоко", 500 }, { "Зерна", 200 } }
                };
                SaveConfig();
                Console.WriteLine("Создан новый конфиг по умолчанию.");
            }
        }

        static void SaveConfig() 
        {
            File.WriteAllText(configPath, JsonSerializer.Serialize(config, 
                new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("Конфигурация сохранена.");
        }
        
        static bool CheckIngredients(string drinkName)
        {
            int waterNeeded = 50;
            int milkNeeded = 50;
            int beansNeeded = 10;
            
            if (config.Ingredients["Вода"] < waterNeeded)
            {
                Console.WriteLine($"Недостаточно воды. Нужно: {waterNeeded}, есть: {config.Ingredients["Вода"]}");
                return false;
            }
            if (config.Ingredients["Молоко"] < milkNeeded)
            {
                Console.WriteLine($"Недостаточно молока. Нужно: {milkNeeded}, есть: {config.Ingredients["Молоко"]}");
                return false;
            }
            if (config.Ingredients["Зерна"] < beansNeeded)
            {
                Console.WriteLine($"Недостаточно зерен. Нужно: {beansNeeded}, есть: {config.Ingredients["Зерна"]}");
                return false;
            }
            
            return true;
        }
        
        static void ConsumeIngredients(string drinkName)
        {
            config.Ingredients["Вода"] -= 50;
            config.Ingredients["Молоко"] -= 50;
            config.Ingredients["Зерна"] -= 10;
            
            Console.WriteLine($"Ингредиенты списаны. Осталось: Вода={config.Ingredients["Вода"]}, " +
                              $"Молоко={config.Ingredients["Молоко"]}, Зерна={config.Ingredients["Зерна"]}");
        }
        
        static void MakeSale(string drinkName)
        {
            if (!config.Prices.ContainsKey(drinkName))
            {
                Console.WriteLine($"Напиток {drinkName} не найден в конфигурации");
                return;
            }
            
            int price = config.Prices[drinkName];
            
            if (!CheckIngredients(drinkName))
            {
                Console.WriteLine($"Невозможно приготовить {drinkName}: недостаточно ингредиентов");
                return;
            }

            ConsumeIngredients(drinkName);

            string logEntry = $"[{DateTime.Now:yyyy-MM-dd}] Продано: {drinkName}, Цена: {price}";
            File.AppendAllLines(logPath, new[] { logEntry });
            Console.WriteLine($"Продажа зафиксирована: {drinkName} за {price} руб.");
        }

        static void EndShift()
        {
            if (!File.Exists(logPath)) 
            {
                Console.WriteLine("Лог продаж не найден.");
                return;
            }

            int totalRevenue = 0;
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            var lines = File.ReadAllLines(logPath);

            foreach (var line in lines)
            {
                if (line.StartsWith($"[{today}]"))
                {
                    var parts = line.Split("Цена: ");
                    if (parts.Length > 1 && int.TryParse(parts[1], out int price))
                    {
                        totalRevenue += price;
                    }
                }
            }

            var report = new 
            { 
                TotalRevenue = totalRevenue, 
                Date = today,
                RemainingIngredients = config.Ingredients 
            };
            
            string reportFileName = $"report_{DateTime.Now:yyyy_MM_dd}.json";
            
            File.WriteAllText(reportFileName, JsonSerializer.Serialize(report, 
                new JsonSerializerOptions { WriteIndented = true }));
            
            Console.WriteLine($"\nСмена окончена. Отчет {reportFileName} создан.");
            Console.WriteLine($"Итоговая выручка: {totalRevenue} руб.");
            Console.WriteLine($"Остатки ингредиентов: Вода={config.Ingredients["Вода"]}, " +
                              $"Молоко={config.Ingredients["Молоко"]}, Зерна={config.Ingredients["Зерна"]}");
        }
    }
}
