using UnityEngine;

namespace MazeWorld.Npc
{
    
    // this class need modification for working
    public class CapsuleOverlap
    {
        private readonly Transform _overlapStart;
        private readonly float _capsuleLength;
        private readonly float _capsuleRadius;
        private readonly LayerMask _interactionLayer;
        private  Vector3 _capsuleEndPoint;

        public CapsuleOverlap(Transform overlapStart, float capsuleLength, float capsuleRadius, LayerMask interactionLayer)
        {
            _overlapStart = overlapStart;
            _capsuleLength = capsuleLength;
            _capsuleRadius = capsuleRadius;
            _interactionLayer = interactionLayer;
        }

        public Collider[] Overlap()
        {
            var capsuleEndPoint = GetCapsuleEndPoint();
            return Physics.OverlapCapsule(_overlapStart.position, capsuleEndPoint, _capsuleRadius, _interactionLayer);
        }

        public void DrawGizmo()
        {
            Gizmos.color = Color.white;
            var startPosition = _overlapStart.position;
            Gizmos.DrawSphere(startPosition,_capsuleRadius);

            var endPoint = GetCapsuleEndPoint();
            Gizmos.DrawLine(startPosition,endPoint);
            
            Gizmos.DrawSphere(endPoint,_capsuleRadius);
        }

        private Vector3 GetCapsuleEndPoint()
        {
            _capsuleEndPoint = _overlapStart.position + Vector3.forward * _capsuleLength;
            return _capsuleEndPoint;
        }
    }
}