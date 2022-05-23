using System.Collections.Generic;
using UnityEngine;

namespace Algorithms
{
    public class BombPlacer
    {
        private readonly List<Cell> _cells;
        private readonly int _width;
        private readonly int _height;
        private List<RoomData> _roomDataList;
        
        public List<Cell> GoalCellPath { get; private set; }

        public BombPlacer(List<Cell> cells, int width, int height)
        {
            _cells = cells;
            _width = width;
            _height = height;

            CreateRoomData();
            PlaceBombs();
        }

        private void PlaceBombs()
        {
            var goalFinder = new GoalFinder(_cells, _cells[0], _width, _height);
            GoalCellPath = goalFinder.FindGoalPath(_cells[0]);

            for (int counter = 0; counter < _width; counter++)
            {
                var randomIndex = Random.Range(0, _cells.Count);
                if (!_cells[randomIndex].IsVisited) continue;
                if(GoalCellPath.Contains(_cells[randomIndex])) continue;

                _roomDataList[randomIndex].SetBomb();
            }
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

