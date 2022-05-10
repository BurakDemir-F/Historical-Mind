using System.Collections.Generic;
using UnityEngine;

namespace Algorithms
{
    public class Cell : IBackTrace
    {
        public Vector2Int Position { get; private set; }
        public bool IsVisited { get; private set; }
        private NeighborData _neighborData;
        public Cell(Vector2Int position, bool isVisited,NeighborData neighborData)
        {
            Position = position;
            IsVisited = isVisited;
            _neighborData = neighborData;
        }
        public Cell(int positionX, int positionY, bool isVisited, NeighborData neighborData)
        {
            Position = new Vector2Int(positionX, positionY);
            IsVisited = isVisited;
            _neighborData = neighborData;
        }
        public void SetSideNeighbor(int side) => _neighborData.SetNeighborStatus(side);
        public void SetVisited() => IsVisited = true;

        public List<bool> GetNeighborStatuses() => _neighborData.GetNeighborStatuses();
    }
}