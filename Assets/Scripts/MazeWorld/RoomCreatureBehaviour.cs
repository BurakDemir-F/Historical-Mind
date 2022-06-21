using Maze;
using UnityEngine;

namespace MazeWorld
{
    public class RoomCreatureBehaviour : MonoBehaviour
    {
        protected RoomBehaviour locatedRoom;

        public void SetRoom(RoomBehaviour room)
        {
            locatedRoom = room;
        }
    }
}
