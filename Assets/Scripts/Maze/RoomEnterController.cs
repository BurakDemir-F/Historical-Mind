using System;
using UnityEngine;

namespace Maze
{
    public class RoomEnterController : MonoBehaviour
    {
        private void Start()
        {
            RoomBehaviour.onRoomEntered += RoomEnterHandler;
        }
        private void OnDestroy()
        {
            RoomBehaviour.onRoomEntered -= RoomEnterHandler;
        }

        private void RoomEnterHandler(RoomBehaviour currentRoom, Collider other)
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
        }
    }
}