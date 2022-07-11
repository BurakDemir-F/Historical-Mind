using System.Collections.Generic;

namespace Utilities
{
    public static class ListExtensions
    {
        private static System.Random rng = new System.Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
        
        public static int GetIndex<T>(this IList<T> list,int x, int y, int width)
        {
            return y * width + x;
        }

        public static bool IsEmpty<T>(this IList<T> list)
        {
            return list.Count == 0;
        }

        public static void UniqueAdd<T>(this IList<T> list, T item)
        {
            if (!list.Contains(item)) list.Add(item);
        }
        
        public static void SafeRemove<T>(this IList<T> list, T item)
        {
            if (list.Contains(item)) list.Remove(item);
        }

    }
}
