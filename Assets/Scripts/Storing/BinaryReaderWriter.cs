
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Storing
{
    public class BinaryReaderWriter<T>
    {
        private string _path;
        private BinaryFormatter _binaryFormatter;

        public BinaryReaderWriter(string path)
        {
            _path = path;
            _binaryFormatter = new BinaryFormatter();
        }

        public void Write(T serializableObject)
        {
            var writeFileStream = new FileStream(_path,FileMode.Create);
            try
            {
                _binaryFormatter.Serialize(writeFileStream, serializableObject);
            }
            catch (SerializationException e)
            {
                Debug.Log("Error on serialization" + e);
            }
            finally
            {
                writeFileStream.Close();
            }
        }

        public T Read()
        {
            var readFileStream = new FileStream(_path, FileMode.Open);
            try
            {
                return (T)_binaryFormatter.Deserialize(readFileStream);
            }
            catch (SerializationException e)
            {
                Debug.Log("Error on serialization" + e);
            }
            finally
            {
                readFileStream.Close();
            }

            return default;
        }
     }
}