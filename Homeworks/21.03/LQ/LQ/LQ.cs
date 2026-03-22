namespace LQ;
using System;
using System.Linq;
using System.Collections.Generic;

public static class LQ1_ArrayShiftExtensions
{
    public static T[] CyclicShiftLeft<T>(this T[] array, int k)
    {
        if (array == null || array.Length == 0)
            return array ?? new T[0];
        
        int n = array.Length;
        int shift = k % n;
        
        if (shift == 0)
            return array.ToArray(); 
        
        return array.Skip(shift)
            .Concat(array.Take(shift))
            .ToArray();
    }
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Point other)
        {
            return X == other.X && Y == other.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}


public static class LQ2_PointExtensions
{
    public static HashSet<Point> GetNeighborhood(this IEnumerable<Point> points)
    {
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };
        
        return points
            .SelectMany(p => Enumerable.Range(0, 8)
                .Select(i => new Point(p.X + dx[i], p.Y + dy[i])))
            .Distinct()
            .ToHashSet();
    }
}

public static class LQ3_StringFilterExtensions
{
    public static IEnumerable<string> FilterStrings(IEnumerable<string> strings)
    {
        return strings.Where(s =>
            s.GroupBy(c => c)
                .All(g => g.Count() <= 2));
    }
}

public class LQ4_WordDictionary
{
    private readonly Dictionary<string, List<string>> _patterns;
    private readonly int _k;
    public LQ4_WordDictionary(IEnumerable<string> words)
    {
        _patterns = new Dictionary<string, List<string>>();
        var wordList = words.ToList();
            
        if (wordList.Count == 0)
        {
            _k = 0;
            return;
        }
            
        _k = wordList[0].Length;
            
        foreach (var word in wordList)
        {
            for (int i = 0; i < _k; i++)
            {
                var pattern = word.Substring(0, i) + "*" + word.Substring(i + 1);
                    
                if (!_patterns.ContainsKey(pattern))
                    _patterns[pattern] = new List<string>();
                    
                _patterns[pattern].Add(word);
            }
        }
    }
        
    public IEnumerable<string> FindSimilar(string query)
    {
        if (query.Length != _k)
            return Enumerable.Empty<string>();
            
        var result = new HashSet<string>();
            
        for (int i = 0; i < _k; i++)
        {
            var pattern = query.Substring(0, i) + "*" + query.Substring(i + 1);
                
            if (_patterns.TryGetValue(pattern, out var matches))
            {
                foreach (var match in matches)
                {
                    if (match != query)
                        result.Add(match);
                }
            }
        }
            
        return result.ToList();
    }
        
    public void Print()
    {
        foreach (var item in _patterns)
        {
            Console.WriteLine(item.Key);
            foreach (var item2 in item.Value)
            {
                Console.WriteLine(item2);
            }
        }
    }
}