using Algorithms;
using Maze;
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
            Debug.Log("",goalRoom);

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
            var room = rooms[cell.Position].gameObject;
            room.SetActive(true);
            
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = room.transform.position;
            cube.transform.localScale = Vector3.one * 2;
            var _renderer = cube.GetComponent<Renderer>();
            _renderer.material.color = Color.red;
            
        }
    }
}