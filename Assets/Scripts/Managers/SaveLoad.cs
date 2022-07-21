using Storing;
using UnityEngine;
using Utilities;

namespace Managers
{
    public static class SaveLoad
    {
        private static string _fileName = "BurakSaveFile.dat";
        private static readonly BinaryReaderWriter _binaryReaderWriter =
            new BinaryReaderWriter(_fileName.LocateAtPersistentDataPath());

        public static void Save(StoringClass storingClass)
        {
            _binaryReaderWriter.Write(JsonOperator<StoringClass>.ToJson(storingClass));
        }

        public static StoringClass Load()
        {
            var jsonString = _binaryReaderWriter.Read();
            Debug.Log("json string");
            return JsonOperator<StoringClass>.FromJson(jsonString);
        }
    }
}