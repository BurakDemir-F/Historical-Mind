using System;
using System.IO;
using System.Text;
using UnityEngine;
using Utilities;

namespace Log
{
    public class LogWriter : MonoBehaviour
    {
        private string _path;
        private StringBuilder _stringBuilder;
        
        private void Awake()
        {
            _stringBuilder = new StringBuilder();
            _path = "GameLog.txt".LocateAtPersistentDataPath();
            File.Delete(_path);
        }

        private void OnEnable()
        {
            Application.logMessageReceived += LogReceivedHandler;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= LogReceivedHandler;
        }
        
        private void LogReceivedHandler(string condition, string stacktrace, LogType type)
        {
            _stringBuilder.Append("condition: ");
            _stringBuilder.Append(condition);
            _stringBuilder.Append("stackTrace: ");
            _stringBuilder.Append(stacktrace);
            _stringBuilder.AppendLine();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SaveLogsToFile();
            }
        }

        private void SaveLogsToFile()
        {
            File.WriteAllText(_path,_stringBuilder.ToString());
        }
    }
}
