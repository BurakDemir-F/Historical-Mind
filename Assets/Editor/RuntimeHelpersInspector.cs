using UnityEditor;
using UnityEngine;

namespace Utilities
{
    [CustomEditor(typeof(RuntimeHelpers))]
    public class RuntimeHelpersInspector : Editor
    {
        private RuntimeHelpers _helpers;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            _helpers = target as RuntimeHelpers;
            if(_helpers == null) return;
        
            if (GUILayout.Button("Activate All"))
            {
                _helpers.ActivateAll();
            }

            if (GUILayout.Button("DeActive All"))
            {
                _helpers.DeactivateAll();
            }
        
            if (GUILayout.Button("Activate Bomb Rooms"))
            {
                _helpers.ActivateBombCells();
            }

            if (GUILayout.Button("Show Goal Path"))
            {
                _helpers.ShowGoalPath();
            }

            if (GUILayout.Button("Show Goal Room"))
            {
                _helpers.ShowGoalCell();
            }

            if (GUILayout.Button("Show Start Room"))
            {
                _helpers.ShowStartCell();
            }
        }
    }
}