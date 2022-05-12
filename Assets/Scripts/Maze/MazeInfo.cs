using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public static class MazeInfo
    {
        public static Vector2Int size = new(10,10);
        public static Vector2Int currentRoomPosition = new (1000,1000);

        private static readonly Dictionary<Vector2Int, RoomBehaviour> Rooms = new();
        private static readonly List<RoomBehaviour> ActiveRooms = new ();
        
        public static void AddRoom(Vector2Int position, RoomBehaviour room)
        {
            if(!Rooms.ContainsKey(position))
                Rooms.Add(position,room);
        }

        public static void RemoveRoom(Vector2Int position)
        {
            if (!Rooms.ContainsKey(position)) Rooms.Remove(position);
        }

        public static Dictionary<Vector2Int,RoomBehaviour> GetRooms()
        {
            return Rooms;
        }

        public static List<RoomBehaviour> GetActiveRooms()
        {
            return ActiveRooms;
        }
        public static void AddActiveRoom(RoomBehaviour room)
        {
            if (!ActiveRooms.Contains(room))
            {
                ActiveRooms.Add(room);
            }
        }

        public static void RemoveAllActiveRooms()
        {
            ActiveRooms.RemoveRange(0,ActiveRooms.Count);
        }
        
        public static void RemoveActiveRoom(RoomBehaviour room)
        {
            if (ActiveRooms.Contains(room))
            {
                ActiveRooms.Remove(room);
            }
        }

        public static bool TryGetRoom(Vector2Int position, out RoomBehaviour room)
        {
            if (Rooms.ContainsKey(position))
            {
                room = Rooms[position];
                return true;
            }

            room = null;
            return false;
        }

        public static List<RoomBehaviour> GetNeighborRooms(Vector2Int position)
        {
            var rooms = new List<RoomBehaviour>();

            var upNeighbor = GetUpNeighbor(position);
            var downNeighbor = GetDownNeighbor(position);
            var leftNeighbor = GetLeftNeighbor(position);
            var rightNeighbor = GetRightNeighbor(position);

            if(upNeighbor) rooms.Add(upNeighbor);
            if(downNeighbor) rooms.Add(downNeighbor);
            if(leftNeighbor) rooms.Add(leftNeighbor);
            if(rightNeighbor) rooms.Add(rightNeighbor);

            return rooms;
        }

        public static RoomBehaviour GetUpNeighbor(Vector2Int position)
        {
            if (position.y <= 0) return null;
            var pos = new Vector2Int(position.x, position.y - 1);
            return Rooms.ContainsKey(pos) ? Rooms[pos] : null;

        }
        
        public static RoomBehaviour GetDownNeighbor(Vector2Int position)
        {
            if (position.y >= size.y - 1) return null;
            var pos = new Vector2Int(position.x, position.y + 1);
            return Rooms.ContainsKey(pos) ? Rooms[pos] : null;

        }

        public static RoomBehaviour GetRightNeighbor(Vector2Int position)
        {
            if (position.x >= size.x - 1) return null;
            var pos = new Vector2Int(position.x + 1, position.y);
            return Rooms.ContainsKey(pos) ? Rooms[pos] : null;

        }

        public static RoomBehaviour GetLeftNeighbor(Vector2Int position)
        {
            if (position.x <= 0) return null;
            var pos = new Vector2Int(position.x - 1, position.y);
            return Rooms.ContainsKey(pos) ? Rooms[pos] : null;

        }
    }
    
}
