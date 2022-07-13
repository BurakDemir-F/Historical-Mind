using Managers;
using UnityEditor;
using UnityEngine;

namespace EditorSpecific
{
    [CustomEditor(typeof(GameManager))]
    public class GameManagerInspector : Editor
    {
        private GameManager _current;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            _current = target as GameManager;

            if (GUILayout.Button("Start Game"))
            {
                if (_current != null)
                {
                    Debug.Log("it is working i think");
                    //_current.StartGame();
                }
            }
            
            if (GUILayout.Button("Save"))
            {
                if (_current != null)
                {
                    _current.Save();
                }
            }
            
            if (GUILayout.Button("Load"))
            {
                if (_current != null)
                {
                    _current.Load();
                }
            }
        }
        
        
    }
}