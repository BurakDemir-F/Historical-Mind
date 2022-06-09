using System;
using System.Collections.Generic;
using UnityEngine;

namespace MazeWorld
{
    public class PlayerRayCaster : MonoBehaviour
    {
        private int _layerMask = 1 << 7;
        private bool _isKeyPressed = false;

        public static Action<IInteractable[]> onHitInteractable;
        public static Action onInteractableKeyPressed;

        private void Update()
        {
            _isKeyPressed = Input.GetKeyDown(KeyCode.P);
        }

        void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hitObject, 10f,
                _layerMask))
            {
                if (!_isKeyPressed) return;
                onInteractableKeyPressed?.Invoke();
                
                var interactable = hitObject.collider.gameObject.GetComponents<IInteractable>();
                if (interactable == null) return;
            
                onHitInteractable?.Invoke(interactable);
                
                foreach (var obj in interactable)
                {
                    obj.Interact();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position,transform.TransformDirection(Vector3.forward));
        }
    }
}
