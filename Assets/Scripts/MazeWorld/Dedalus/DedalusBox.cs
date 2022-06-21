using System.Collections.Generic;
using Algorithms;
using Maze;
using UnityEngine;

namespace MazeWorld.Dedalus
{
    public class DedalusBox : RoomObjectBehaviour
    {
        [SerializeField] private List<DedalusEye> _dedalusEyes;
        private void Start()
        {
            RoomEnterBehaviour.OnRoomEntered += RoomEnteredHandler;
            roomObject.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            RoomEnterBehaviour.OnRoomEntered -= RoomEnteredHandler;
        }

        private void RoomEnteredHandler(RoomBehaviour room, Collider col, RoomBehaviour up,RoomBehaviour down,RoomBehaviour right, RoomBehaviour left)
        {
            if (locatedRoom !=room) return;
            roomObject.gameObject.SetActive(true);
            UpdateBox(up,down,right,left);
        }

        private void UpdateBox(RoomBehaviour up,RoomBehaviour down,RoomBehaviour right, RoomBehaviour left)
        {
            if (up != null)
            {
                var roomData = MazeInfo.GetRoomData(up);
                _dedalusEyes[0].ShowCreatures(roomData.GetCreatures());
            }
            if (down != null)
            {
                var roomData = MazeInfo.GetRoomData(down);
                _dedalusEyes[1].ShowCreatures(roomData.GetCreatures());
            }
            if (right != null)
            {
                var roomData = MazeInfo.GetRoomData(right);
                _dedalusEyes[2].ShowCreatures(roomData.GetCreatures());
            }
            if (left != null)
            {
                var roomData = MazeInfo.GetRoomData(left);
                _dedalusEyes[3].ShowCreatures(roomData.GetCreatures());
            }
        }
    }
}
