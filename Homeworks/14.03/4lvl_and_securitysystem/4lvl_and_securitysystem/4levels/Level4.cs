using System;
using System.Collections.Generic;

namespace Levels
{
    public static class Extensions
    {
        public static event Action<string> OnIteration;

        public static void ForEachWithIndex<T>(
            this IEnumerable<T> collection,
            Action<T, int> action)
        {
            int index = 0;

            foreach (var item in collection)
            {
                OnIteration?.Invoke($"Обработка индекса {index}");
                action(item, index);
                index++;
            }
        }
    }
}