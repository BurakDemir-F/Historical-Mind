using System;
using System.Collections;
using MazeWorld.Npc;
using ScriptableObjects;
using StarterAssets;
using UnityEngine;

namespace MazeWorld.Player
{
    public class PlayerAttackBehaviour : MonoBehaviour
    {
        [SerializeField] private StarterAssetsInputs inputs;
        [SerializeField] private Transform attackTransform;
        [SerializeField] private LayerMask attackLayer;
        [SerializeField] private float attackSphereRadius = 1f;
        [SerializeField] private LivingThing npcData;
        [SerializeField] private Collider thisCollider;
        [SerializeField] private PlayerMotionBehaviour motionBehavior;
        private SpherePhysic _overlapHelper;
        
        public event Action OnAttack;
        
        private void Start()
        {
            _overlapHelper = new SpherePhysic(attackTransform, attackSphereRadius, attackLayer);
            inputs.OnInteractionHappen += AttackButtonPressedHandler;
            motionBehavior.AttackMotionStart += AttackMotionStartedHandler;
            motionBehavior.AttackMotionEnd += AttackMotionEndHandler;
        }

        private void OnDestroy()
        {
            inputs.OnInteractionHappen += AttackButtonPressedHandler;
            motionBehavior.AttackMotionStart -= AttackMotionStartedHandler;
            motionBehavior.AttackMotionEnd -= AttackMotionEndHandler;
        }

        private void AttackButtonPressedHandler()
        {
            OnAttack?.Invoke();
        }

        private void AttackMotionStartedHandler()
        {
            StartCoroutine(CheckHitCor());
        }

        private void AttackMotionEndHandler()
        {
            StopCoroutine(CheckHitCor());
        }
        private IEnumerator CheckHitCor()
        {
            var hitColliders = _overlapHelper.OverlapSphere();
            
            while (hitColliders.Length == 0)
            {
                hitColliders = _overlapHelper.OverlapSphere();
                yield return null;
            }

            if (hitColliders.Length == 0) yield break;

            foreach (var hitCollider in hitColliders)
            {
                if(thisCollider == hitCollider) continue;
                var damageObject = hitCollider.GetComponent<IDamageable>();
                if (damageObject == null) continue;
                damageObject.Damage(npcData.Power);
                break;
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(attackTransform.position,attackSphereRadius);
        }
    }
}
