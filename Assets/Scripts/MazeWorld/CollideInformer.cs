using System;
using System.Collections.Generic;
using UnityEngine;

namespace MazeWorld
{
    public class CollideInformer : MonoBehaviour, ICollide
    {
        private IInteractable[] _allInteractable;

        private void OnEnable()
        {
            _allInteractable = GetComponents<IInteractable>();
        }

        public void OnTriggerEnter(Collider col)
        {
            foreach (var obj in _allInteractable)
            {
                obj.Interact();
            }
        }
    }
}