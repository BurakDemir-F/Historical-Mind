using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Algorithms;
using CodeMonkey.HealthSystemCM;
using Managers;
using NUnit.Framework;
using Storing;
using UnityEngine;
using UnityEngine.TestTools;
using Utilities;
using System.IO;
using Debug = UnityEngine.Debug;

namespace UnitTests.StoringUnitTests
{
    public class StoringTests
    {
        private static string _fileName = "BurakSaveFile.dat";
        private string _path = _fileName.LocateAtPersistentDataPath();
    
        [Test]
        public void IsFileExistsAfterSave()
        {
            DeleteSaveFile();
            var storingClass = GetNewStoringClass();
            // SaveLoad.Save(storingClass);

            var readerWriter = new BinaryReaderWriter(_path);
            readerWriter.Write(JsonUtility.ToJson(storingClass));
            Assert.IsTrue(File.Exists(_path));
        }

        [Test]
        public void IsFileReadable()
        {
            var readerWriter = new BinaryReaderWriter(_path);
            var result= readerWriter.Read();
            Debug.Log(result);
            var storingObject = JsonUtility.FromJson<StoringClass>(result);
            Assert.IsTrue(storingObject != null);
        }

        [Test]
        public void JsonUtilityTest()
        {
            var storingObject = GetNewStoringClass();

            var jsonString = JsonUtility.ToJson(storingObject);
            var jsonObject = JsonUtility.FromJson<StoringClass>(jsonString);
            Assert.IsTrue(jsonObject != null);
        }
        
        private void DeleteSaveFile()
        {
            File.Delete(_path);
        }

        private StoringClass GetNewStoringClass()
        {
            var random = Random.Range(0, 101);
            var dataList = new List<RoomData>(5);
            var neighborData = new NeighborData(new List<bool>() {true, true, true, true});
            var cell = new Cell(new Vector2Int(random, random), true,neighborData);
            for (int i = 0; i < 5; i++)
            {
                dataList.Add(new RoomData(cell,false));
            }

            return new StoringClass(dataList, new Vector2IntSerializable(random, random),
                new SerializableHealthSystem(random,  random));
        }
    }
}
