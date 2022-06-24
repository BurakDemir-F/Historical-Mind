using System;
using UnityEngine;

namespace MazeWorld.Player
{
    public class RayCaster
    {
        private readonly Camera _cam;
        private readonly LayerMask _interactableLayer;

        public event Action<IInteractable> OnHitInteractable;

        public RayCaster(Camera cam, LayerMask interactableLayer)
        {
            _cam = cam;
            _interactableLayer = interactableLayer;
        }
        public void CastRay()
        {
            var screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            var ray = _cam.ScreenPointToRay(screenCenter);
            
            if(!Physics.Raycast(ray,out RaycastHit raycastHit,30f,_interactableLayer)) return;
            
            var interactable = raycastHit.transform.GetComponent<IInteractable>();
            if(interactable != null)
                OnHitInteractable?.Invoke(raycastHit.transform.GetComponent<IInteractable>());
        }
    }
}
