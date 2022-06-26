using MazeWorld.Npc;
using UnityEngine;

namespace MazeWorld.Player
{
    public class PlayerDamageBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerHealthBehaviour healthBehaviour;
        public void Damage(float damage)
        {
            healthBehaviour.Damage(damage);
        }
    }
}
