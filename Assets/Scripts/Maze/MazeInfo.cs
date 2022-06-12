using System.Collections.Generic;
using System.Linq;
using Algorithms;
using UnityEngine;

namespace Maze
{
    public static class MazeInfo
    {
        public static Vector2Int size = new(10,10);
        public static Vector2Int currentRoomPosition = new (1000,1000);

        private static Dictionary<Vector2Int, RoomBehaviour> _rooms;
        private static readonly List<RoomBehaviour> ActiveRooms = new ();
        private static Dictionary<RoomBehaviour, RoomData> _roomDatas;

        public static void InitMazeInfo(Vector2Int mazeSize)
        {
            size = mazeSize;
            _rooms = new Dictionary<Vector2Int, RoomBehaviour>(size.x * size.y);
            _roomDatas = new Dictionary<RoomBehaviour, RoomData>(size.x * size.y);
        }
        
        public static void AddRoom(Vector2Int position, RoomBehaviour room)
        {
            if(!_rooms.ContainsKey(position))
                _rooms.Add(position,room);
        }

        public static void RemoveRoom(Vector2Int position)
        {
            if (!_rooms.ContainsKey(position)) _rooms.Remove(position);
        }

        public static void AddRoomData(RoomBehaviour roomBehaviour, RoomData data)
        {
            if (!_roomDatas.ContainsKey(roomBehaviour))
            {
                _roomDatas.Add(roomBehaviour,data);
            }
        }
        
        public static void RemoveRoomData(RoomBehaviour roomBehaviour, RoomData data)
        {
            if (_roomDatas.ContainsKey(roomBehaviour))
            {
                _roomDatas.Remove(roomBehaviour);
            }
        }

        public static RoomData GetRoomData(RoomBehaviour roomBehaviour)
        {
            return _roomDatas.ContainsKey(roomBehaviour) ? _roomDatas[roomBehaviour] : null;
        }
        
        public static Dictionary<Vector2Int,RoomBehaviour> GetRooms()
        {
            return _rooms;
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
            if (_rooms.ContainsKey(position))
            {
                room = _rooms[position];
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
            return _rooms.ContainsKey(pos) ? _rooms[pos] : null;

        }
        
        public static RoomBehaviour GetDownNeighbor(Vector2Int position)
        {
            if (position.y >= size.y - 1) return null;
            var pos = new Vector2Int(position.x, position.y + 1);
            return _rooms.ContainsKey(pos) ? _rooms[pos] : null;

        }

        public static RoomBehaviour GetRightNeighbor(Vector2Int position)
        {
            if (position.x >= size.x - 1) return null;
            var pos = new Vector2Int(position.x + 1, position.y);
            return _rooms.ContainsKey(pos) ? _rooms[pos] : null;

        }

        public static RoomBehaviour GetLeftNeighbor(Vector2Int position)
        {
            if (position.x <= 0) return null;
            var pos = new Vector2Int(position.x - 1, position.y);
            return _rooms.ContainsKey(pos) ? _rooms[pos] : null;

        }
    }
    
}
