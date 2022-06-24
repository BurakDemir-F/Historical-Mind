using System;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class NpcInteractableBehaviour : MonoBehaviour, IInteractable
    {
        public event Action OnDamage;
        public void Interact()
        {
            Debug.Log("InteractionHappen");
            OnDamage?.Invoke();
            InteractionOperations();
        }

        protected virtual void InteractionOperations()
        {
            
        }
    }
}
