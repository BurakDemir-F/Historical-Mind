using System;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class RoomBehaviour : MonoBehaviour
    {
        /*0 - up, 1 - down, 2 - right, 3 - left*/
        [SerializeField]private GameObject[] walls,doors;
        [SerializeField]private List<bool> testStatuses;

        private Vector2Int _position;
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
        
        public void UpdateRoomTest()
        {
            UpdateRoom(testStatuses);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if(_position == MazeInfo.currentRoomPosition) return;
            //if(MazeInfo.GetActiveRooms().Contains(this)) return;

            foreach (var room in MazeInfo.GetActiveRooms())
            {
                room.gameObject.SetActive(false);
            }
            MazeInfo.RemoveAllActiveRooms();

            gameObject.SetActive(true);
            MazeInfo.currentRoomPosition = _position;
            MazeInfo.AddActiveRoom(this);
            
            var rooms = MazeInfo.GetNeighborRooms(_position);
            foreach (var room in rooms)
            {
                room.gameObject.SetActive(true);
                MazeInfo.AddActiveRoom(room);
            }
        }
    }
}
