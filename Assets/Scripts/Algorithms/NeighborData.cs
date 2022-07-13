using System.Collections.Generic;
using UnityEngine;

namespace Algorithms
{
    public class NeighborData
    {
        private readonly List<bool> _neighborStatuses;
        private List<Cell> _neighborCells;

        public NeighborData(List<bool> neighborStatuses)
        {
            _neighborStatuses = neighborStatuses;
            _neighborCells = new List<Cell>(4);
        }

        public void SetNeighborStatus(int side)
        {
            if(side >= _neighborStatuses.Count || side < 0)
            {
                Debug.Log("Something going wrong here");
                return;
            }

            _neighborStatuses[side] = true;
        }

        public List<Cell> GetNeighborCells()
        {
            return _neighborCells;
        }

        public void AddNeighborCell(Cell neighbor)
        {
            _neighborCells.Add(neighbor);
        }

        public List<bool> GetNeighborStatuses()
        {
            return _neighborStatuses;
        }

        public SerializableNeighborData ToSerializable()
        {
            return new SerializableNeighborData(this);
        }
    }
}