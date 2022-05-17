using System;
using Maze;
using UnityEditor;
using UnityEngine;

namespace EditorSpecific
{
    public class TestUtilities : EditorWindow
    {
        [MenuItem ("Window/Test Utilities")]
        public static void  ShowWindow () {
            EditorWindow.GetWindow(typeof(TestUtilities));
        }

        private void OnGUI()
        {
            GUILayout.Label("Test Utilitites", EditorStyles.boldLabel);
            
        }

        private void DebugMazeInfoContent()
        {
            var mazeData = MazeInfo.GetRooms();
            if(mazeData == null)
            {
                Debug.Log("maze data is null");
                return;
            }
            
        }
    }
}
