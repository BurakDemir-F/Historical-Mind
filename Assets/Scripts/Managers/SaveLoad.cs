using Storing;
using Utilities;

namespace Managers
{
    public static class SaveLoad
    {
        private static string _fileName = "BurakSaveFile.dat";
        private static readonly BinaryReaderWriter<StoringClass> _binaryReaderWriter =
            new BinaryReaderWriter<StoringClass>(_fileName.LocateAtPersistentDataPath());

        public static void Save(StoringClass storingClass)
        {
            _binaryReaderWriter.Write(storingClass);
        }

        public static StoringClass Load()
        {
            return _binaryReaderWriter.Read();
        }
    }
}