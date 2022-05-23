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
            if (GUILayout.Button("Start Game"))
            {
                _current = target as GameManager;
                if (_current != null)
                {
                    Debug.Log("it is working i think");
                    //_current.StartGame();
                }
            }
        }
    }
}