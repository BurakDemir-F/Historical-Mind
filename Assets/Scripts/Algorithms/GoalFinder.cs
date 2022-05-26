using System.Collections.Generic;
using UnityEngine;

namespace Algorithms
{
    public class GoalFinder
    {
        private readonly List<RoomData> _roomDataList;
        private readonly List<Cell> _controlCells;
        private readonly Cell _startCell;
        private readonly int _width;
        private readonly int _height;

        public GoalFinder(List<RoomData> roomDataList, List<Cell> controlControlCells, Cell startCell, int width, int height)
        {
            _roomDataList = roomDataList;
            _controlCells = controlControlCells;
            _startCell = startCell;
            _width = width;
            _height = height;
        }

        public List<Cell> FindGoalPath(RoomData startRoom )
        {
            var goalPath = new List<Cell> {startRoom.Cell};

            var neighbors = startRoom.Cell.GetNeighborCells();
            var randomNeighbor = neighbors[Random.Range(0, neighbors.Count)];
            goalPath.Add(randomNeighbor);
            var previousCell = _startCell;

            var counter = 0;
            while (randomNeighbor != null && CalculateDistance( startRoom.Cell,randomNeighbor) < _width * 0.9f)
            {
                if (++counter >= 1000)
                {
                    Debug.Log("i tried 1000 times");
                    break;
                }
                
                neighbors = randomNeighbor.GetNeighborCells();
                randomNeighbor = neighbors[Random.Range(0, neighbors.Count)];
                
                if(IsBombCell(randomNeighbor))
                {
                    randomNeighbor = previousCell;
                    continue;
                }

                goalPath.Add(randomNeighbor);
                previousCell = randomNeighbor;
            }
            
            return goalPath;
        }

        private bool IsBombCell(Cell cell)
        {
            foreach (var room in _roomDataList)
            {
                if (cell == room.Cell)
                {
                    return room.IsBomb;
                }
            }

            return false;
        }
        
        private float CalculateDistance(Cell fromCell, Cell toCell)
        {
            return Vector2Int.Distance(fromCell.Position, toCell.Position);
        }
        
        
        
        
    }
}