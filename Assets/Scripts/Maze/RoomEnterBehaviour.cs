using System;
using UnityEngine;

namespace Maze
{
    public class RoomEnterBehaviour : MonoBehaviour
    {
        [SerializeField] private RoomBehaviour thisRoomBehaviour;
        
        public static event Action<RoomBehaviour,Collider,RoomBehaviour, RoomBehaviour, RoomBehaviour, RoomBehaviour> OnRoomEntered;
        public static event Action<RoomBehaviour, Collider> OnRoomEnteredLight;
        private void Start()
        {
            OnRoomEntered += RoomEnterHandler;
        }
        private void OnDestroy()
        {
            OnRoomEntered -= RoomEnterHandler;
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
            
            OnRoomEnteredLight?.Invoke(currentRoom,other);
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player")) return;
            var position = thisRoomBehaviour.GetRoomPosition();
            
            OnRoomEntered?.Invoke(thisRoomBehaviour,other,MazeInfo.GetUpNeighbor(position), MazeInfo.GetDownNeighbor(position),
                MazeInfo.GetRightNeighbor(position), MazeInfo.GetLeftNeighbor(position));
        }
    }
}