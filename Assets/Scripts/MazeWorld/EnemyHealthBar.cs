using CodeMonkey.HealthSystemCM;
using Maze;
using UnityEngine;
using Utilities;

namespace MazeWorld
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private HealthBarUI healthBar;
        
        void Start()
        {
            RoomEnterBehaviour.OnRoomEnteredLight += RoomEnteredHandler;
        }

        private void OnDestroy()
        {
            RoomEnterBehaviour.OnRoomEnteredLight -= RoomEnteredHandler;
        }

        private void RoomEnteredHandler(RoomBehaviour roomBehaviour, Collider col)
        {
            var roomData = MazeInfo.GetRoomData(roomBehaviour);
            if (roomData.GetCreatures().IsEmpty()) return;
            var healthObject = CreatureInfo.GetCreature(roomData).GetComponent<IGetHealthSystem>();
            healthBar.SetHealthSystem(healthObject.GetHealthSystem());
        }
    }
}
