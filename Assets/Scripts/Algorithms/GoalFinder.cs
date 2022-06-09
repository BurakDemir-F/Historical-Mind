using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Algorithms
{
    public class GoalFinder
    {
        private readonly List<RoomData> _roomDataList;
        private readonly List<Cell> _controlCells;
        private readonly Cell _startCell;
        private readonly int _width;
        private readonly int _height;

        [Obsolete]
        public GoalFinder(List<RoomData> roomDataList, List<Cell> controlControlCells, Cell startCell, int width, int height)
        {
            _roomDataList = roomDataList;
            _controlCells = controlControlCells;
            _startCell = startCell;
            _width = width;
            _height = height;
        }

        public GoalFinder(List<Cell> controlCells, int width, int height)
        {
            _controlCells = controlCells;
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
            while (randomNeighbor != null && CalculateDistance( startRoom.Cell,randomNeighbor) < _width * _height * 0.6f)
            {
                if (++counter >= 3000)
                {
                    Debug.Log("i tried 3000 times");
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

        public List<Cell> FindGoalPath2()
        {
            var startCell = FindFirstSafeCell();
            var neighbors = startCell.GetNeighborCells();
            var randomNeighbor = neighbors[Random.Range(0, neighbors.Count)];

            var safeCounter = 0;
            var pathStack = new Stack<Cell>();
            
            while (++safeCounter < 3000 && CalculateDistance(startCell, randomNeighbor) < _width * _height * 0.6f)
            {
                if (safeCounter == 2999) Debug.Log("i tried 3000 times");
            }

            return null;
        }

        public Cell FindFirstSafeCell()
        {

            foreach (var cell in _controlCells)
            {
                if (!cell.IsDangerous) return cell;
            }

            return _controlCells[0];
        }

        public Cell FindGoalCell()
        {
            for (var index = _controlCells.Count - 1; index >= 0; index--)
            {
                var cell = _controlCells[index];
                if (!cell.IsDangerous) return cell;
            }

            return _controlCells[^1];
        }

        public List<Cell> FindCellsFromPositions(List<Vector2Int> positions)
        {
            var cells = new List<Cell>();
            foreach (var cell in _controlCells)
            {
                foreach (var pos in positions)
                {
                    if(pos.x == cell.Position.x && pos.y == cell.Position.y) cells.Add(cell);
                }
            }

            return cells;
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