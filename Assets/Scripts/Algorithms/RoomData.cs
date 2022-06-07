using UnityEngine;

namespace Algorithms
{
    public class RoomData
    {
        public Cell Cell { get; private set; }
        public bool IsBomb { get; private set; }

        public RoomData(Cell cell, bool isBomb)
        {
            Cell = cell;
            IsBomb = isBomb;
        }

        public void SetBomb()
        {
            IsBomb = true;
        }

        public Vector2Int GetRoomPosition()
        {
            return Cell.Position;
        }
    }
}