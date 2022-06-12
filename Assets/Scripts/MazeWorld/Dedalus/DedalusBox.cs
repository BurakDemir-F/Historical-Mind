using Algorithms;
using Maze;
using UnityEngine;

namespace MazeWorld.Dedalus
{
    public class DedalusBox : RoomObjectBehaviour
    {
        private void Start()
        {
            RoomEnterBehaviour.onRoomEntered += RoomEnteredHandler;
            roomObject.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            RoomEnterBehaviour.onRoomEntered -= RoomEnteredHandler;
        }

        private void RoomEnteredHandler(RoomBehaviour room, Collider col, RoomBehaviour up,RoomBehaviour down,RoomBehaviour right, RoomBehaviour left)
        {
            if (!locatedRoom ==room) return;
            roomObject.gameObject.SetActive(true);
            UpdateBox();
        }

        private void UpdateBox()
        {
            
        }
    }
}
