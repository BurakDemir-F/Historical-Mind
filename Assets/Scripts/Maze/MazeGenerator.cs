using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private MazeBase _depthFirstMaze;
        public List<Cell> GoalCellPath { get; private set; }
        public Cell GoalCell { get; private set; }
        public Cell StartCell { get; private set; }
        
        //[field:SerializeField]public List<Cell> GoalCellPath { get; private set; }
        //public Cell GoalCell => GoalCellPath[^1];

        public GameObject player; // test

        public void InitializeData()
        {
            _size = mazeData.size;
            MazeInfo.InitMazeInfo(_size);
        }
        
        public void GenerateBackTracking()
        {
            var startTime = Time.realtimeSinceStartup;
            _depthFirstMaze = new DepthFirstMaze(_size.x, _size.y);
            var backTracing = new BackTracing(_size.x, _size.y,_depthFirstMaze);
            backTracing.GenerateCellsBasedOnMaze();
            _cells = backTracing.GetCells();
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
            //GoalCellPath = bombPlacer.GoalCellPath;

            var goalFinder = new GoalFinder(_cells, _size.x, _size.y);
            StartCell = goalFinder.FindFirstSafeCell();
            GoalCell = goalFinder.FindGoalCell();
            var aStar = new AStar(_depthFirstMaze,
                new PathMarker(new MapLocation(StartCell.Position.x, StartCell.Position.y), 0, 0, 0, null),
                new PathMarker(new MapLocation(GoalCell.Position.x, GoalCell.Position.y), 0, 0, 0, null));
            
            aStar.PerformAStar();
            if(aStar.IsCompletedSuccessfully)
            {
                print("A star successful");
                GoalCellPath = goalFinder.FindCellsFromPositions(aStar.GetPathCells());
            }

            print($"{(Time.realtimeSinceStartup - startTime) * 1000 } ms - set path and bombs");
            print($"total memory{GC.GetTotalMemory(false) / Mathf.Pow(10,6)}");
        }

        public void GenerateMaze()
        {
            var startTime = Time.realtimeSinceStartup;

            foreach (var roomData in _roomDataList)
            {
                var cell = roomData.Cell;
                //if (!cell.IsVisited) continue;

                var newRoom = Instantiate(mazeRoom, 
                    new Vector3(cell.Position.x * roomOffset, 0f, -cell.Position.y * roomOffset),
                    Quaternion.identity);
                newRoom.transform.SetParent(mazeRoot);
                newRoom.name = $"Room: {cell.Position.x} : {cell.Position.y}";
                newRoom.UpdateRoom(cell.GetNeighborStatuses());
                newRoom.SetRoomPosition(cell.Position);
                newRoom.SetBombRoom(/*roomData.IsBomb*/ cell.IsDangerous);
                MazeInfo.AddRoom(cell.Position,newRoom);
            }

            print($"{(Time.realtimeSinceStartup - startTime) * 1000 } ms - generate maze");
            print($"total memory{GC.GetTotalMemory(false) / Mathf.Pow(10,6)}");

            #region test
            
            var spawnRoom = new Vector2Int(StartCell.Position.x,StartCell.Position.y);
            var isFound = MazeInfo.TryGetRoom(spawnRoom, out mazeRoom);
            if (isFound)
            {
                player = Instantiate(testPlayer);
                var roomPosition = mazeRoom.transform.position;
                player.transform.position = new Vector3(roomPosition.x, 1f, roomPosition.z);
                mazeRoom.OnTriggerEnter(player.GetComponent<Collider>());    
            }
            
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
