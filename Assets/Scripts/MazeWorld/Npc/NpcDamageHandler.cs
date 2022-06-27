using System;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class NpcDamageHandler : MonoBehaviour, IDamageable
    {
        public event Action<float> OnDamage;
        public void Damage(float damage)
        {
            Debug.Log("InteractionHappen");
            OnDamage?.Invoke(damage);
            InteractionOperations();
        }

        protected virtual void InteractionOperations()
        {
            
        }
    }
}
