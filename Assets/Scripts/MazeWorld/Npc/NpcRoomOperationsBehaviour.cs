using System;
using Maze;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class NpcRoomOperationsBehaviour : RoomCreatureBehaviour
    {
        public event Action OnPlayerRoom;
        public event Action OnPlayerExitRoom;

        private bool _isPlayerInRoom = false;
        private void Start()
        {
            RoomEnterBehaviour.OnRoomEnteredLight += PlayerOnRoomEnteredLightHandler;
        }

        private void OnDestroy()
        {
            RoomEnterBehaviour.OnRoomEnteredLight -= PlayerOnRoomEnteredLightHandler;
        }

        private void PlayerOnRoomEnteredLightHandler(RoomBehaviour room, Collider other)
        {
            if(room != locatedRoom && _isPlayerInRoom)
            {
                _isPlayerInRoom = false;
                OnPlayerExitRoom?.Invoke();
                print("Player left room");
                return;
            }
            
            if(room != locatedRoom) return;
            if(!other.CompareTag("Player")) return;

            OnPlayerRoom?.Invoke();
            _isPlayerInRoom = true;
        }
    }
}
