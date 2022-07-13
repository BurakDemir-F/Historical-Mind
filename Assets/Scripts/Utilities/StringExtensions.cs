using System.IO;
using UnityEngine;

namespace Utilities
{
    public static class StringExtensions
    {
        public static string LocateAtPersistentDataPath(this string fileName)
        {
            var pDataPath = Application.persistentDataPath;
            return Path.Combine(pDataPath, fileName);
        }
    }
}