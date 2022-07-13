using CodeMonkey.HealthSystemCM;
using UnityEngine;

namespace Maze
{
    public static class PlayerInfo 
    {
        public static GameObject PlayerObject { get; private set; }
        public static Transform PlayerTransform { get; private set; }
        public static GameObject PlayerRoot { get; private set; }
        public static HealthSystem PlayerHealth { get; private set; }

        public static Vector3 PlayerPosition => PlayerTransform.position;
        public static void SetPlayer(GameObject player)
        {
            PlayerObject = player;
            PlayerTransform = player.transform;
            SetPlayerRoot();
            //SetPlayerHealth();
        }

        public static void SetPlayerHealth(HealthSystem healthSystem)
        {
            var healthBar = GameObject.FindWithTag("PlayerHealthBar").GetComponent<HealthBarUI>();
            healthBar.SetHealthSystem(healthSystem);
        }

        private static void SetPlayerRoot()
        {
            PlayerRoot = PlayerTransform.root.gameObject;
        }
    }
}
