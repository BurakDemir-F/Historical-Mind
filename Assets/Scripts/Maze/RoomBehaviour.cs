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

        public static Action<RoomBehaviour, Collider> onRoomEntered;
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

        public void OnTriggerEnter(Collider other)
        {
            onRoomEntered?.Invoke(this,other);
        }
    }
}
