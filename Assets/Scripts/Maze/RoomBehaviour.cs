using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class RoomBehaviour : MonoBehaviour
    {
        /*0 - up, 1 - down, 2 - right, 3 - left*/
        [SerializeField]private GameObject[] walls,doors;
        [SerializeField]private List<bool> testStatuses;

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
        
        public void UpdateRoomTest()
        {
            UpdateRoom(testStatuses);
        }
    }
}
