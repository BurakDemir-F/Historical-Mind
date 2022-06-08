using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Algorithms
{
    public class BombPlacer
    {
        private readonly List<Cell> _cells;
        private readonly int _width;
        private readonly int _height;
        private List<RoomData> _roomDataList;
        private List<Cell> _restrictedCells;

        public List<Cell> GoalCellPath { get; private set; }

        public BombPlacer(List<Cell> cells, int width, int height, List<Cell> restrictedCells)
        {
            _cells = cells;
            _width = width;
            _height = height;
            _restrictedCells = restrictedCells;

            CreateRoomData();
            PlaceBombsAndGoalPath();
        }

        private void PlaceBombs()
        {
            for (int i = 0; i < _width * _height / 3; i++)
            {
                var randomIndex = Random.Range(1, _roomDataList.Count);
                var currentCell = _cells[randomIndex];
                if(!currentCell.IsVisited) continue;
                if (_restrictedCells.Contains(currentCell)) continue;

                var neighborCells = currentCell.GetNeighborCells();
                var hasNeighborBomb = false;
                
                foreach (var neighborCell in neighborCells)
                {
                    var index = neighborCell.PositionToIndex(_width);
                    if (_roomDataList[index].IsBomb)
                    {
                        hasNeighborBomb = true;
                        break;
                    }
                }
                if(hasNeighborBomb) continue;
                
                _roomDataList[randomIndex].SetBomb();
            }
        }

        private void PlaceBombs2()
        {
            foreach (var cell in _cells)
            {
                if(_restrictedCells.Contains(cell)) continue;
                if (!cell.IsVisited) _roomDataList[_roomDataList.GetIndex(cell.Position.x, cell.Position.y, _width)].SetBomb();
            }
        }

        private bool IsSafeToPlace()
        {
            return false;
        }
        
        private void PlaceBombsAndGoalPath()
        {
            PlaceBombs2();
            
            // var goalFinder = new GoalFinder(_roomDataList, _cells, _cells[0], _width, _height);
            // GoalCellPath = goalFinder.FindGoalPath(_roomDataList[_roomDataList.GetIndex(1,1,_width)]);

            // for (int counter = 0; counter < _width; counter++)
            // {
            //     var randomIndex = Random.Range(0, _cells.Count);
            //     if (!_cells[randomIndex].IsVisited) continue;
            //     if(GoalCellPath.Contains(_cells[randomIndex])) continue;
            //
            //     _roomDataList[randomIndex].SetBomb();
            // }
        }

        private void CreateRoomData()
        {
            _roomDataList = new List<RoomData>(_cells.Count);
            foreach (var cell in _cells)
            {
                _roomDataList.Add(new RoomData(cell, false));
            }
        }

        public List<RoomData> GetRoomData()
        {
            return _roomDataList;
        }
    }
}

