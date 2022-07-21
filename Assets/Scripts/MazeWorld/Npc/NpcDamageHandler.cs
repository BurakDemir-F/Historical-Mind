using System;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class NpcDamageHandler : MonoBehaviour, IDamageable
    {
        [SerializeField] private NpcMotionBehaviour motionBehaviour;

        private void Start()
        {
            motionBehaviour = GetComponent<NpcMotionBehaviour>();
        }

        public event Action<float> OnDamage;
        public void Damage(float damage)
        {
            // if(motionBehaviour.isAttacking) return; how i write this?
            OnDamage?.Invoke(damage);
            InteractionOperations();
        }

        protected virtual void InteractionOperations()
        {
            
        }
    }
}
