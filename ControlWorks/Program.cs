namespace Controlwork12sem;
using System;
using System.IO;
using System.Linq;
//1 задание
public static class Utils
{
    public static T FindMin<T>(T[] array) where T : IComparable<T>
    {
        // Реализовать поиск минимального элемента
        if (array == null || array.Length == 0)
        {
            throw new ArgumentException("Массив не должен быть пустым или равняться нулю");
        }

        T min = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i].CompareTo(min) < 0)
            {
                min = array[i];
            }
        }
        return min;
        
    }
}

class Program
{
        static void Main()
        {
            try
            {
                int[] intArray = { 6, 9, 0, 1, 5, 7 };
                int minInt = Utils.FindMin(intArray);
                Console.WriteLine($"Минимум в массиве целых чисел: {minInt}");

                
                double[] doubleArray = { 3.14, 2.1, 1.46851, 27.158, 186.703 };
                double minDouble = Utils.FindMin(doubleArray);
                Console.WriteLine($"Минимум в массиве дробных чисел: {minDouble}");
                
                string[] stringArray = { "disdslf", "qofaodd", "пыкгыкг" };
                string minString = Utils.FindMin(stringArray);
                Console.WriteLine($"Минимальная строка: {minString}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
     
}

//2 задание 

public class TransactionEventArgs : EventArgs
{
    public decimal Amount { get; set; }
    public decimal CurrentBalance { get; set; }
}

public class BankAccount
{
    public decimal Balance { get; private set; }
    public event EventHandler<TransactionEventArgs> InsufficientFunds;

    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            InsufficientFunds?.Invoke(this, new TransactionEventArgs 
            { 
                Amount = amount, 
                CurrentBalance = Balance 
            });
        }
        else
        {
            Balance -= amount;
        }
    }
}


class Program
{
    static void Main()
    {
        decimal Balance = 1000;
        BankAccount myAccount = new BankAccount();
        
        myAccount.InsufficientFunds += (sender, e) =>
        {
            Console.WriteLine($"Ошибка: недостаточно средств! Баланс: {e.CurrentBalance}, Запрос: {e.Amount}");
        };
        
        myAccount.Withdraw(1500);
        
        Console.ReadKey();
    }


}

//3 задание


class Program
{
    static void Main()
    {
        string path = "text.txt";
        if (!File.Exists(path)) return;

        string text = File.ReadAllText(path);

        var result = text
            .Split(new[] { ' ', '.', ',', '!', '?', ';', ':', '-', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(word => word.StartsWith("A", StringComparison.OrdinalIgnoreCase) && word.Length > 3)
            .GroupBy(word => word, StringComparer.OrdinalIgnoreCase)
            .Select(group => new { Word = group.Key, Count = group.Count() })
            .OrderByDescending(x => x.Count)
            .Take(3);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Word}: {item.Count}");
        }
    }
}
