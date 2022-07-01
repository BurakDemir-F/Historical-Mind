using System;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class NpcDamageHandler : MonoBehaviour, IDamageable
    {
        [SerializeField] private NpcMotionBehaviour motionBehaviour;

        private void OnValidate()
        {
            motionBehaviour = GetComponent<NpcMotionBehaviour>();
        }

        public event Action<float> OnDamage;
        public void Damage(float damage)
        {
            if(motionBehaviour.isAttacking) return;
            Debug.Log("InteractionHappen");
            OnDamage?.Invoke(damage);
            InteractionOperations();
        }

        protected virtual void InteractionOperations()
        {
            
        }
    }
}
