using System.Collections.Generic;
using UnityEngine;

namespace Algorithms
{
    public class GoalFinder
    {
        private readonly List<Cell> _cells;
        private readonly Cell _startCell;
        private readonly int _width;
        private readonly int _height;

        public GoalFinder(List<Cell> cells, Cell startCell, int width, int height)
        {
            _cells = cells;
            _startCell = startCell;
            _width = width;
            _height = height;
        }

        public List<Cell> FindGoalPath(Cell startCell )
        {
            var goalPath = new List<Cell> {startCell};

            var neighbors = startCell.GetNeighborCells();
            var randomNeighbor = neighbors[Random.Range(0, neighbors.Count)];
            goalPath.Add(randomNeighbor);
            
            
            while (randomNeighbor != null && CalculateDistance( startCell,randomNeighbor) < _width * 0.9f)
            {
                neighbors = randomNeighbor.GetNeighborCells();
                randomNeighbor = neighbors[Random.Range(0, neighbors.Count)];
                goalPath.Add(randomNeighbor);
            }
            
            return goalPath;
        }

        
        
        private float CalculateDistance(Cell fromCell, Cell toCell)
        {
            return Vector2Int.Distance(fromCell.Position, toCell.Position);
        }
        
        
        
        
    }
}