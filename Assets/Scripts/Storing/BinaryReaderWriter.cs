
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Storing
{
    public class BinaryReaderWriter<T>
    {
        private string _path;
        private FileStream _fileStream;
        private BinaryFormatter _binaryFormatter;

        public BinaryReaderWriter(string path)
        {
            _path = path;
            _fileStream = new FileStream(path,FileMode.OpenOrCreate);
            _binaryFormatter = new BinaryFormatter();
        }

        public void Write(T serializableObject)
        {
            try
            {
                _binaryFormatter.Serialize(_fileStream, serializableObject);
            }
            catch (SerializationException e)
            {
                Debug.Log("Error on serialization" + e);
            }
            finally
            {
                _fileStream.Dispose();
            }
        }

        public T Read()
        {
            try
            {
                return (T)_binaryFormatter.Deserialize(_fileStream);
            }
            catch (SerializationException e)
            {
                Debug.Log("Error on serialization" + e);
            }
            finally
            {
                _fileStream.Dispose();
            }

            return default;
        }
     }
}