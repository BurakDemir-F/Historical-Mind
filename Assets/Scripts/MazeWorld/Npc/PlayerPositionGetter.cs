using Maze;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class PlayerPositionGetter : MonoBehaviour, IGetTargetTransform
    {
        public Transform GetTarget()
        {
            return PlayerInfo.PlayerTransform;
        }
    }
}