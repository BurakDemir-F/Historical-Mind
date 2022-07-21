
using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using FileMode = System.IO.FileMode;

namespace Storing
{
    public class BinaryReaderWriter
    {
        private string _path;

        public BinaryReaderWriter(string path)
        {
            _path = path;
        }

        public void Write(string text)
        {
            File.WriteAllTextAsync(_path,text,Encoding.UTF8);
        }

        public string Read()
        {
            var text =File.ReadAllText(_path,Encoding.UTF8); 
            return text;
        }
        

        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        public static string BinaryToString(Byte[] data)
        {
            return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }
     }
}