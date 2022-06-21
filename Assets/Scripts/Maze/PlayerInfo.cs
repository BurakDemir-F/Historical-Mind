using UnityEngine;

namespace Maze
{
    public static class PlayerInfo 
    {
        public static GameObject PlayerObject { get; private set; }
        public static Transform PlayerTransform { get; private set; }

        public static Vector3 PlayerPosition => PlayerTransform.position;
        public static void SetPlayer(GameObject player)
        {
            PlayerObject = player;
            PlayerTransform = player.transform;
        }
    }
}
