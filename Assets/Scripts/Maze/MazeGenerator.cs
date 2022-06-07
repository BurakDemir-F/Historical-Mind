using System;
using System.Collections;
using System.Collections.Generic;
using Algorithms;
using DG.Tweening;
using Patterns;
using ScriptableObjects;
using Test;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maze
{
    public class MazeGenerator : Singleton<MazeGenerator>
    {
        [SerializeField] private RoomBehaviour mazeRoom;
        [SerializeField] private float roomOffset = 7f;
        [SerializeField] private MazeData mazeData;
        [SerializeField] private Transform mazeRoot;
        [SerializeField] private GameObject testPlayer; // for testing

        private Vector2Int _size;
        private List<Cell> _cells;
        private List<RoomData> _roomDataList;
        
        [field:SerializeField]public List<Cell> GoalCellPath { get; private set; }
        public Cell GoalCell => GoalCellPath[^1];

        public GameObject player; // test

        public void InitializeData()
        {
            _size = mazeData.size;
            MazeInfo.InitMazeInfo(_size);
        }
        
        public void GenerateBackTracking()
        {
            var startTime = Time.realtimeSinceStartup;
            var backTracing = new BackTracing(_size.x, _size.y);
            _cells = backTracing.GetFullTracedCells();
            print($"{(Time.realtimeSinceStartup - startTime) * 1000 } ms - generate backtracking");
            print($"total memory{GC.GetTotalMemory(false) / Mathf.Pow(10,6)}");
        }

        public void SetBombs()
        {
            var startTime = Time.realtimeSinceStartup;

            var restrictedCells = new List<Cell>();

            for (var i = 0; i < 3; i++)
            {
                restrictedCells.Add(_cells[i * _size.x]);
                restrictedCells.Add(_cells[i * _size.x + 1]);
                restrictedCells.Add(_cells[i * _size.x + 2]);
            }
            
            
            var bombPlacer = new BombPlacer(_cells, _size.x, _size.y, restrictedCells);
            _roomDataList = bombPlacer.GetRoomData();
            GoalCellPath = bombPlacer.GoalCellPath;
            
            print($"{(Time.realtimeSinceStartup - startTime) * 1000 } ms - set bombs");
            print($"total memory{GC.GetTotalMemory(false) / Mathf.Pow(10,6)}");
        }

        public void GenerateMaze()
        {
            var startTime = Time.realtimeSinceStartup;

            foreach (var roomData in _roomDataList)
            {
                var cell = roomData.Cell;
                if (!cell.IsVisited) continue;

                var newRoom = Instantiate(mazeRoom, 
                    new Vector3(cell.Position.x * roomOffset, 0f, -cell.Position.y * roomOffset),
                    Quaternion.identity);
                newRoom.transform.SetParent(mazeRoot);
                newRoom.name = $"Room: {cell.Position.x} : {cell.Position.y}";
                newRoom.UpdateRoom(cell.GetNeighborStatuses());
                newRoom.SetRoomPosition(cell.Position);
                newRoom.SetBombRoom(roomData.IsBomb);
                MazeInfo.AddRoom(cell.Position,newRoom);
            }

            print($"{(Time.realtimeSinceStartup - startTime) * 1000 } ms - generate maze");
            print($"total memory{GC.GetTotalMemory(false) / Mathf.Pow(10,6)}");

            #region test
            
            // player = Instantiate(testPlayer);
            // player.transform.position = new Vector3(0, 1f, 0);
            //
            // if (MazeInfo.TryGetRoom(new Vector2Int(0,0), out mazeRoom))
            // {
            //     mazeRoom.OnTriggerEnter(player.GetComponent<Collider>());    
            // }
            
            //PlayerWalking();
            #endregion
        }

        private void PlayerWalking()
        {
            var tween = DOTween.Sequence();
            var lastRoom = MazeInfo.GetRooms();

            foreach (var room in lastRoom)
            {
                var move = player.transform.DOMove(room.Value.transform.position, 3);
                tween.Append(move);
            }
            
            
            tween.Play();
        }
    }
}
