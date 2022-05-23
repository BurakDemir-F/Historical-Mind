using System.Collections;
using System.Collections.Generic;
using Algorithms;
using Maze;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace Utilities
{
    public class RuntimeHelpers : MonoBehaviour
    {
        public void ActivateBombCells()
        {
            var rooms = MazeInfo.GetRooms().Values;
            foreach (var room in rooms)
            {
                room.gameObject.SetActive(room.IsBombRoom);
            }
        }

        public void ActivateAll()
        {
            var rooms = MazeInfo.GetRooms().Values;
            foreach (var room in rooms)
            {
                room.gameObject.SetActive(true);
            }
        }

        public void DeactivateAll()
        {
        
            var rooms = MazeInfo.GetRooms().Values;
            foreach (var room in rooms)
            {
                room.gameObject.SetActive(false);
            }
        }

        public void ShowGoalCell()
        {
            var rooms = MazeInfo.GetRooms();
            var goalRoom = rooms[MazeGenerator.Instance.GoalCell.Position].gameObject;
            goalRoom.SetActive(true);

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = goalRoom.transform.position;
            cube.transform.localScale = Vector3.one * 2;
            var _renderer = cube.GetComponent<Renderer>();
            _renderer.material.color = Color.red;

        }
        
        public void ShowGoalPath()
        {
            var goalCells = MazeGenerator.Instance.GoalCellPath;
            foreach (var cell in goalCells)
            {
                ActivateBasedOnCell(cell);
            }
        }

        public void ActivateBasedOnCell(Cell cell)
        {
            var rooms = MazeInfo.GetRooms();
            rooms[cell.Position].gameObject.SetActive(true);
        }
    }

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
        }
    }
}