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
            RoomEnterBehaviour.ONDangerousRoom += PlayerOnDangerousRoomHandler;
        }

        private void OnDestroy()
        {
            RoomEnterBehaviour.ONDangerousRoom -= PlayerOnDangerousRoomHandler;
        }

        private void PlayerOnDangerousRoomHandler(RoomBehaviour room, Collider other)
        {
            if(room != locatedRoom && _isPlayerInRoom)
            {
                _isPlayerInRoom = false;
                OnPlayerExitRoom?.Invoke();
                return;
            }
            
            if(room != locatedRoom) return;
            if(!other.CompareTag("Player")) return;

            OnPlayerRoom?.Invoke();
            _isPlayerInRoom = true;
        }
    }
}
