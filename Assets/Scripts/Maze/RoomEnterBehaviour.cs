using System;
using UnityEngine;

namespace Maze
{
    public class RoomEnterBehaviour : MonoBehaviour
    {
        [SerializeField] private RoomBehaviour thisRoomBehaviour;
        
        public static event Action<RoomBehaviour,Collider,RoomBehaviour, RoomBehaviour, RoomBehaviour, RoomBehaviour> ONRoomEntered;
        public static event Action<RoomBehaviour, Collider> ONDangerousRoom;
        private void Start()
        {
            ONRoomEntered += RoomEnterHandler;
        }
        private void OnDestroy()
        {
            ONRoomEntered -= RoomEnterHandler;
        }

        private void RoomEnterHandler(RoomBehaviour currentRoom, Collider other, RoomBehaviour up,RoomBehaviour down,RoomBehaviour right, RoomBehaviour left)
        {
            if (!other.CompareTag("Player")) return;

            var roomPos = currentRoom.GetRoomPosition();
            if(roomPos == MazeInfo.currentRoomPosition) return;

            foreach (var room in MazeInfo.GetActiveRooms())
            {
                room.gameObject.SetActive(false);
            }
            
            MazeInfo.RemoveAllActiveRooms();

            currentRoom.gameObject.SetActive(true);
            MazeInfo.currentRoomPosition = roomPos;
            MazeInfo.AddActiveRoom(currentRoom);
            
            var rooms = MazeInfo.GetNeighborRooms(roomPos);
            foreach (var room in rooms)
            {
                room.gameObject.SetActive(true);
                MazeInfo.AddActiveRoom(room);
            }
            
            var roomData = MazeInfo.GetRoomData(currentRoom);
            if(roomData.IsBomb) ONDangerousRoom?.Invoke(currentRoom,other);
        }
        
        public void OnTriggerEnter(Collider other)
        {
            var position = thisRoomBehaviour.GetRoomPosition();
            
            ONRoomEntered?.Invoke(thisRoomBehaviour,other,MazeInfo.GetUpNeighbor(position), MazeInfo.GetDownNeighbor(position),
                MazeInfo.GetRightNeighbor(position), MazeInfo.GetLeftNeighbor(position));
        }
    }
}