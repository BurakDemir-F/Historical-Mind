using UnityEngine;

namespace MazeWorld.Npc
{
    public class SpherePhysic
    {
        private readonly Transform _overlapTransform;
        private readonly float _radius;
        private readonly LayerMask _interactionLayer;

        public SpherePhysic(Transform overlapTransform, float radius, LayerMask interactionLayer)
        {
            _overlapTransform = overlapTransform;
            _radius = radius;
            _interactionLayer = interactionLayer;
        }

        public Collider[] OverlapSphere()
        {
            return Physics.OverlapSphere(_overlapTransform.position, _radius, _interactionLayer);
        }
    }
}