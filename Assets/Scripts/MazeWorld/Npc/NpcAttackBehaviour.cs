using System;
using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class NpcAttackBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform attackTransform;
        [SerializeField] private NpcMotionBehaviour motionBehaviour;
        [SerializeField] private LayerMask attackLayer;
        [SerializeField] private float attackSphereRadius = 1f;
        [SerializeField] private LivingThing npcData;
        [SerializeField] private Collider npcCollider;
        private SpherePhysic _overlapHelper;

        private void Start()
        {
            motionBehaviour.OnAttackMotionStart += AttackStartHandler;
            motionBehaviour.OnAttackMotionEnd += AttackEndHandler;
            _overlapHelper = new SpherePhysic(attackTransform, attackSphereRadius, attackLayer);
        }

        private void OnDestroy()
        {
            motionBehaviour.OnAttackMotionStart -= AttackStartHandler;
            motionBehaviour.OnAttackMotionEnd -= AttackEndHandler;
        }

        private void AttackStartHandler()
        {
            StartCoroutine(CheckHitCor());
            print("Attack start");
        }

        private void AttackEndHandler()
        {
            StopCoroutine(CheckHitCor());
            print("Attack end");
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
                if(npcCollider == hitCollider) break;
                var damageObject = hitCollider.GetComponent<IDamageable>();
                if (damageObject == null) continue;
                damageObject.Damage(npcData.Power);
                break;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if(attackTransform == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(attackTransform.position,attackSphereRadius);
        }
    }
}
