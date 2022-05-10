using Maze;
using UnityEditor;
using UnityEngine;

namespace EditorSpecific
{
    [CustomEditor(typeof(RoomBehaviour))]
    public class RoomBehaviourInspector : Editor
    {
        private RoomBehaviour _roomBehaviour;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            _roomBehaviour = target as RoomBehaviour;
            
            if (GUILayout.Button("Test Update Room"))
            {
                _roomBehaviour.UpdateRoomTest();
            }
        }
    }
}
