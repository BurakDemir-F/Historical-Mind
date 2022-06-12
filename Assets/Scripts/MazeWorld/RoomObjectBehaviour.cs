using Maze;
using UnityEngine;

namespace MazeWorld
{
    public class RoomObjectBehaviour : MonoBehaviour
    {
        [SerializeField] protected GameObject roomObject;
        protected RoomBehaviour locatedRoom;

        public void SetRoom(RoomBehaviour room)
        {
            locatedRoom = room;
        }
    }
}