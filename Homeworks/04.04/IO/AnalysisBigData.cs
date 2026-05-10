using System;
using System.IO;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string filePath = "bigdata.txt";
        
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не найден. Сначала запустите код генерации.");
            return;
        }
        
        Stopwatch sw = new Stopwatch();
        sw.Start();

        long totalA = 0;
        const byte targetByte = 65; 
        const int bufferSize = 65536; 
        
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            byte[] buffer = new byte[bufferSize];
            int bytesRead;
            
            while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    if (buffer[i] == targetByte)
                    {
                        totalA++;
                    }
                }
            }
        }

        sw.Stop();
        
        Console.WriteLine($"Найдено символов 'A': {totalA}");
        Console.WriteLine($"Время выполнения: {sw.ElapsedMilliseconds} мс");
        Console.WriteLine($"Потребление RAM: {Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024} МБ");
    }
}