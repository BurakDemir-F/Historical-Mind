using System;
using Algorithms;
using Maze;
using MazeWorld;
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
                room.gameObject.SetActive(MazeInfo.GetRoomData(room).IsBomb);
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

            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        public void ShowGoalCell()
        {
            var rooms = MazeInfo.GetRooms();
            var goalRoom = rooms[MazeGenerator.Instance.GoalCell.Position].gameObject;
            goalRoom.SetActive(true);
            Debug.Log("",goalRoom);
            
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(transform);
            cube.transform.position = goalRoom.transform.position;
            cube.transform.localScale = Vector3.one * 2;
            var _renderer = cube.GetComponent<Renderer>();
            _renderer.material.color = Color.red;
        }

        public void ShowStartCell()
        {
            var rooms = MazeInfo.GetRooms();
            var startRoom = rooms[MazeGenerator.Instance.StartCell.Position].gameObject;
            startRoom.SetActive(true);
            Debug.Log("",startRoom);
            
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(transform);
            cube.transform.position = startRoom.transform.position;
            cube.transform.localScale = Vector3.one * 2;
            var _renderer = cube.GetComponent<Renderer>();
            _renderer.material.color = Color.green;
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
            cube.transform.SetParent(transform);
            cube.transform.position = room.transform.position;
            cube.transform.localScale = Vector3.one * 2;
            var _renderer = cube.GetComponent<Renderer>();
            _renderer.material.color = Color.red;
        }

        public void SendPlayerToGoblin()
        {
            SendPlayerToCreature(MazeWorldCreatures.MazeGoblin);
        }
        
        public void SendPlayerToGuardian()
        {
            SendPlayerToCreature(MazeWorldCreatures.MazeGuardian);
        }
        
        public void SendPlayerToCreature(MazeWorldCreatures creature)
        {
            var player = PlayerInfo.PlayerRoot;
            var mazeData = MazeInfo.GetAllRoomData();
            foreach (var roomData in mazeData)
            {
                var roomCreatures = roomData.Value.GetCreatures();
                if (!roomCreatures.Contains(creature)) continue;
                MazeGenerator.Instance.SpawnPlayer(roomData.Key,player);
                return;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                SendPlayerToGuardian();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                SendPlayerToGoblin();
            }
        }
    }
}