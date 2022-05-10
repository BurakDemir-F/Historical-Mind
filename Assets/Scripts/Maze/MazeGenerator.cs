using System;
using System.Collections.Generic;
using Algorithms;
using Test;
using Unity.Mathematics;
using UnityEngine;

namespace Maze
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2Int size;
        [SerializeField] private RoomBehaviour mazeRoom;
        [SerializeField] private float roomOffset = 6f;
        
        private List<Cell> _cells;

        private void Start()
        {
            var backTracing = new BackTracing(size.x, size.y);
            _cells = backTracing.GetCells();
            
            GenerateMaze(_cells);
        }

        private void GenerateMaze(List<Cell> cells)
        {
            var test = gameObject.AddComponent<BackTracingTest>();
            test.CreateTestVisuals(cells,size,roomOffset);
            
            foreach (var cell in cells)
            {
                if (!cell.IsVisited) continue;

                var newRoom = Instantiate(mazeRoom, 
                    new Vector3(cell.Position.x * roomOffset, 0f, -cell.Position.y * roomOffset),
                    Quaternion.identity);
                
                newRoom.UpdateRoom(cell.GetNeighborStatuses());
            }
        }


        public class MazeCell : IBackTrace
        { 
            public Vector2Int Position { get; private set; }
            public bool IsVisited { get; private set; }
        }
    }
}
