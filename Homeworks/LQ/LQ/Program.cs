using LQ;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== LQ1 ===");
        int[] arr = { 1, 2, 3, 4, 5 };
        Console.WriteLine($"Сдвиг на 2: [{string.Join(", ", LQ1_ArrayShiftExtensions.CyclicShiftLeft(arr, 2))}]");
        
        Console.WriteLine("\n=== LQ2 ===");
        var points = new[] { new Point(0, 0) };
        var neighbors = LQ2_PointExtensions.GetNeighborhood(points);
        Console.WriteLine($"1-окрестность (0,0): {neighbors.Count()} точек");
            
        Console.WriteLine("\n=== LQ3 ===");
        var strings = new[] { "aabbcc", "aaabbb", "abcd" };
        var filtered = LQ3_StringFilterExtensions.FilterStrings(strings);
        Console.WriteLine($"Подходит: {string.Join(", ", filtered)}");
            
        Console.WriteLine("\n=== LQ4 ===");
        var dict = new LQ4_WordDictionary(new[] { "cat", "bat", "car", "hat"});
        var similar = dict.FindSimilar("cat");
        Console.WriteLine($"Похожие на 'cat': {string.Join(", ", similar)}");
    }
}