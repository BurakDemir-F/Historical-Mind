using System;
using System.Collections.Generic;
using UnityEngine;

namespace MazeWorld
{
    public class PlayerRayCaster : MonoBehaviour
    {
        [SerializeField] private LineRenderer testLineRenderer;
        [SerializeField] private FPController testPlayer;
        
        private int _layerMask = 1 << 7;
        private bool _isKeyPressed = false;

        public static Action<IInteractable> onHitInteractable;
        public static Action onInteractableKeyPressed;

        private Vector3[] testLineRendererPositions;

        private void Start()
        {
            testLineRenderer = Instantiate(testLineRenderer, transform.position, Quaternion.identity);
            testLineRendererPositions = new Vector3[2];
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _isKeyPressed = true;
            }
            
            TestRayCastRenderer();
        }

        private void TestRayCastRenderer()
        {
            var myTransform = transform;
            var startPos = myTransform.position;

            var endPos = startPos + transform.TransformDirection(Vector3.forward) * 3;
            testLineRendererPositions[0] = startPos;
            testLineRendererPositions[1] = endPos;
            testLineRenderer.SetPositions(testLineRendererPositions);
        }

        void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hitObject, 10f,
                _layerMask))
            {
                if (!_isKeyPressed) return;
                _isKeyPressed = false;
                
                onInteractableKeyPressed?.Invoke();
                
                var interactable = hitObject.collider.gameObject.GetComponent<IInteractable>();
                if (interactable == null) return;
            
                onHitInteractable?.Invoke(interactable);
                interactable.Interact();
            }
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawLine(transform.position,transform.TransformDirection(Vector3.forward));
        // }
    }
}
