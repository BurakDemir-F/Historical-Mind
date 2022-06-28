using UnityEngine;

namespace MazeWorld.Npc
{
    
    // this class need modification for working
    public class CapsuleOverlap
    {
        private readonly Transform _overlapStart;
        private readonly Transform _overlapEnd;
        private readonly float _capsuleRadius;
        private readonly LayerMask _interactionLayer;
        private  Vector3 _capsuleEndPoint;

        public CapsuleOverlap(Transform overlapStart,Transform overlapEnd, float capsuleRadius, LayerMask interactionLayer)
        {
            _overlapStart = overlapStart;
            _overlapEnd = overlapEnd;
            _capsuleRadius = capsuleRadius;
            _interactionLayer = interactionLayer;
        }

        public Collider[] Overlap()
        {
            return Physics.OverlapCapsule(_overlapStart.position, _overlapEnd.position, _capsuleRadius, _interactionLayer);
        }

        public void DrawGizmo()
        {
            Gizmos.color = Color.white;
            var startPosition = _overlapStart.position;
            var endPoint = _overlapEnd.position;
            
            Gizmos.DrawSphere(startPosition,_capsuleRadius);
            Gizmos.DrawLine(startPosition,endPoint);
            Gizmos.DrawSphere(endPoint,_capsuleRadius);
        }
    }
}