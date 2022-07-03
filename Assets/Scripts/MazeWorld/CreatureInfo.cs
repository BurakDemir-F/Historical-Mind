using System.Collections.Generic;
using Algorithms;
using Maze;
using MazeWorld.Npc;

namespace MazeWorld
{
    public static class CreatureInfo
    {
        private static Dictionary<RoomData, NpcBehaviour> _creatures;

        public static void InitCreatureInfo(int capacity)
        {
            _creatures = new Dictionary<RoomData, NpcBehaviour>(capacity);
        }

        public static void AddCreature(RoomData data, NpcBehaviour npc)
        {
            _creatures.Add(data,npc);
        }
        
        public static void RemoveCreature(RoomData data, NpcBehaviour npc)
        {
            _creatures.Remove(data);
        }

        public static NpcBehaviour GetCreature(RoomData data)
        {
            return _creatures[data];
        }

        public static NpcBehaviour GetCreature(RoomBehaviour room)
        {
            var roomData = MazeInfo.GetRoomData(room);
            return _creatures[roomData];
        }
    }
}
