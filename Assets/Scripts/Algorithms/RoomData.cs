using System.Collections.Generic;
using MazeWorld;
using UnityEngine;

namespace Algorithms
{
    public class RoomData
    {
        public Cell Cell { get; private set; }
        public bool IsBomb { get; private set; }

        private List<MazeWorldCreatures> _creatures; 

        public RoomData(Cell cell, bool isBomb)
        {
            Cell = cell;
            IsBomb = isBomb;
            _creatures = new List<MazeWorldCreatures>();
        }

        public void SetBomb()
        {
            IsBomb = true;
        }

        public Vector2Int GetRoomPosition()
        {
            return Cell.Position;
        }

        public List<MazeWorldCreatures> GetCreatures()
        {
            return _creatures;
        }

        public void AddCreature(MazeWorldCreatures creature)
        {
            if(!_creatures.Contains(creature)) _creatures.Add(creature);
        }
        
        public void RemoveCreature(MazeWorldCreatures creature)
        {
            if(_creatures.Contains(creature)) _creatures.Remove(creature);
        }

    }
}