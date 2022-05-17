using System;
using System.Collections;
using System.Collections.Generic;
using Algorithms;
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

        private Vector2Int _size;
        private List<Cell> _cells;
        
        public void InitializeData()
        {
            _size = mazeData.size;
            MazeInfo.InitMazeInfo(_size);
            
            GenerateBackTracking();
        }
        
        private void GenerateBackTracking()
        {
            var startTime = Time.realtimeSinceStartup;
            var backTracing = new BackTracing(_size.x, _size.y);
            _cells = backTracing.GetCells();
            print($"{(Time.realtimeSinceStartup - startTime) * 1000 } ms - generate backtracking");
            print($"total memory{GC.GetTotalMemory(false) / Mathf.Pow(10,6)}");
        }

        public void GenerateMaze()
        {
            var startTime = Time.realtimeSinceStartup;

            foreach (var cell in _cells)
            {
                if (!cell.IsVisited) continue;

                var newRoom = Instantiate(mazeRoom, 
                    new Vector3(cell.Position.x * roomOffset, 0f, -cell.Position.y * roomOffset),
                    Quaternion.identity);
                newRoom.transform.SetParent(mazeRoot);
                newRoom.name = $"Room: {cell.Position.x} : {cell.Position.y}";
                newRoom.UpdateRoom(cell.GetNeighborStatuses());
                newRoom.SetRoomPosition(cell.Position);
                MazeInfo.AddRoom(cell.Position,newRoom);
            }

            print($"{(Time.realtimeSinceStartup - startTime) * 1000 } ms - generate maze");
            print($"total memory{GC.GetTotalMemory(false) / Mathf.Pow(10,6)}");

            #region test

            var player = new GameObject("Player");
            player.AddComponent<BasicPlayer>();
            player.tag = "Player";
            player.AddComponent<BoxCollider>();
            var rigidBody = player.AddComponent<Rigidbody>();
            rigidBody.useGravity = false;
            player.transform.position = new Vector3(0, 2f, 0);
            
            if (MazeInfo.TryGetRoom(new Vector2Int(0,0), out mazeRoom))
            {
                mazeRoom.OnTriggerEnter(player.GetComponent<BoxCollider>());    
            }
            
            var cam = Camera.main;
            cam.transform.position = new Vector3(0, 1f, -0.5f);
            cam.transform.SetParent(player.transform);
            cam.transform.rotation = Quaternion.Euler(Vector3.zero);
            #endregion

        }
    }
}
