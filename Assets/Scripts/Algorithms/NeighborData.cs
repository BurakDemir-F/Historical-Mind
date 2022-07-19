using System.Collections.Generic;
using Storing;
using UnityEngine;

namespace Algorithms
{
    public class NeighborData
    {
        private readonly List<bool> _neighborStatuses;
        private List<Cell> _neighborCells;
        private List<SerializableCell> _serializableNeighborCells;

        public NeighborData(List<bool> neighborStatuses)
        {
            _neighborStatuses = neighborStatuses;
            _neighborCells = new List<Cell>(4);
            _serializableNeighborCells = new List<SerializableCell>(4);
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
            _serializableNeighborCells.Add(new SerializableCell(ToSerializable(),
                new Vector2IntSerializable(neighbor.Position.x, neighbor.Position.y), neighbor.IsVisited,
                neighbor.IsDangerous));
        }

        public List<bool> GetNeighborStatuses()
        {
            return _neighborStatuses;
        }

        public SerializableNeighborData ToSerializable()
        {
            return new SerializableNeighborData(this,_serializableNeighborCells);
        }
    }
}