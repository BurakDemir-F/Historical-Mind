using CodeMonkey.HealthSystemCM;
using UnityEngine;

namespace Maze
{
    public static class PlayerInfo 
    {
        public static GameObject PlayerObject { get; private set; }
        public static Transform PlayerTransform { get; private set; }
        public static GameObject PlayerRoot { get; private set; }

        public static Vector3 PlayerPosition => PlayerTransform.position;
        public static void SetPlayer(GameObject player)
        {
            PlayerObject = player;
            PlayerTransform = player.transform;
            SetPlayerRoot();
            SetPlayerHealth();
        }

        private static void SetPlayerHealth()
        {
            var healthSystem = PlayerRoot.GetComponentInChildren<IGetHealthSystem>();
            var healthBar = GameObject.FindWithTag("PlayerHealthBar").GetComponent<HealthBarUI>();
            healthBar.SetHealthSystem(healthSystem.GetHealthSystem());
        }

        private static void SetPlayerRoot()
        {
            PlayerRoot = PlayerTransform.root.gameObject;
        }
    }
}
