using System;
using StarterAssets;
using UnityEngine;

namespace MazeWorld.Player
{
    public class PlayerInteractionBehaviour : MonoBehaviour
    {
        [SerializeField] private LayerMask interactionMask;
        [SerializeField] private StarterAssetsInputs input; 
        private RayCaster _rayCaster;

        private void Start()
        {
            _rayCaster = new RayCaster(Camera.main, interactionMask);
            _rayCaster.OnHitInteractable += HitInteractableHandler;
            input.OnInteractionHappen += InteractionHandler;
        }

        private void OnDestroy()
        {
            _rayCaster.OnHitInteractable -= HitInteractableHandler;
            input.OnInteractionHappen -= InteractionHandler;
        }

        // if ray hits any related layer of object this function will be called.
        private void HitInteractableHandler(IInteractable interactable)
        {
            interactable.Interact();
        }

        private void InteractionHandler()
        {
            _rayCaster.CastRay();
        }
    }
}
