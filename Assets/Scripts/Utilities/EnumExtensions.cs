
using System;

namespace Utilities
{
    public static class EnumExtensions 
    {
        public static int ToInt<T>(this T source) where T : IConvertible//enum
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return (int) (IConvertible) source;
        }

        public static int Count<T>(this T source) where T : IConvertible//enum
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return Enum.GetNames(typeof(T)).Length;
        }
    }
}
