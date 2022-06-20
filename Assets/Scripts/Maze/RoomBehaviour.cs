using System;
using System.Collections.Generic;
using MazeWorld;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maze
{
    public class RoomBehaviour : MonoBehaviour, IEquatable<RoomBehaviour>
    {
        /*0 - up, 1 - down, 2 - right, 3 - left*/
        [SerializeField]private GameObject[] walls,doors;
        [SerializeField]private List<bool> testStatuses;
        [field:SerializeField] public Locator Locator { get; private set; }

        private Vector2Int _position;
        //[field:SerializeField]public bool IsBombRoom { get; private set; }
        
        private void OnDestroy()
        {
            MazeInfo.RemoveRoom(_position);
        }

        public void UpdateRoom(IReadOnlyList<bool> doorOpenStatuses)
        {
            if (doorOpenStatuses.Count != walls.Length)
            {
                Debug.Log("Something wrong with door open statuses or wall count");
                return;
            }

            for (var i = 0; i < doorOpenStatuses.Count; i++)
            {
                doors[i].SetActive(doorOpenStatuses[i]);
                walls[i].SetActive(!doorOpenStatuses[i]);
            }
        }

        public void SetRoomPosition(Vector2Int position)
        {
            _position = position;
        }

        public Vector2Int GetRoomPosition()
        {
            return _position;
        }
        
        public void UpdateRoomTest()
        {
            UpdateRoom(testStatuses);
        }

        public bool Equals(RoomBehaviour other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(walls, other.walls) && Equals(doors, other.doors) && Equals(testStatuses, other.testStatuses) && _position.Equals(other._position) && Equals(Locator, other.Locator);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RoomBehaviour) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), walls, doors, testStatuses, _position, Locator);
        }

        public static bool operator ==(RoomBehaviour first, RoomBehaviour second)
        {
            if (first is null)
            {
                return second is null;
            }
            
            return first.Equals(second);
        }

        public static bool operator !=(RoomBehaviour first, RoomBehaviour second)
        {
            return !(first == second);
        }
    }
}
