using System.Collections.Generic;
using System.Linq;
using Algorithms;
using CodeMonkey.HealthSystemCM;
using UnityEngine;

namespace Storing
{
    [System.Serializable]
    public class StoringClass
    {
        public List<SerializableRoomData> roomDataList;
        public Vector2IntSerializable currentRoom;
        public SerializableHealthSystem playerHealth;

        public StoringClass(List<RoomData> roomDataList, Vector2IntSerializable currentRoom, SerializableHealthSystem playerHealth)
        {
            this.roomDataList = (from roomData in roomDataList select roomData.ToSerializable()).ToList() ;
            this.currentRoom = currentRoom;
            this.playerHealth = playerHealth;
        }

        public List<SerializableRoomData> GetRoomDataList()
        {
            return roomDataList;
        }

        public Vector2IntSerializable GetCurrentRoomPosition()
        {
            return currentRoom;
        }

        public SerializableHealthSystem GetHealthSystem()
        {
            return playerHealth;
        }
    }
}