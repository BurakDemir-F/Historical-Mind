using System;
using System.Collections;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class NpcAttackBehaviour : AttackBehaviour
    {
        [SerializeField] private Transform attackTransform;
        [SerializeField] private NpcMotionBehaviour motionBehaviour;
        [SerializeField] private LayerMask attackLayer;
        [SerializeField] private float attackSphereRadius = 1f;
        [SerializeField] private LivingThing npcData;
        [SerializeField] private Collider thisCollider;
        private SpherePhysic _overlapHelper;

        private bool _isAttacking = false;

        private void Start()
        {
            motionBehaviour.OnAttackMotionStart += AttackStartHandler;
            motionBehaviour.OnAttackMotionEnd += AttackEndHandler;
            motionBehaviour.OnMotionStateChanged += MotionStateChangedHandler;
            _overlapHelper = new SpherePhysic(attackTransform, attackSphereRadius, attackLayer);
        }

        private void OnDestroy()
        {
            motionBehaviour.OnAttackMotionStart -= AttackStartHandler;
            motionBehaviour.OnAttackMotionEnd -= AttackEndHandler;
            motionBehaviour.OnMotionStateChanged -= MotionStateChangedHandler;
        }

        private void AttackStartHandler()
        {
            if (IsAttacking()) return;
            StartCoroutine(CheckHitCor());
            print("Attack start");
            SetAttack(true);
        }

        private void AttackEndHandler()
        {
            if (!IsAttacking()) return;
            StopCoroutine(CheckHitCor());
            print("Attack end");
            SetAttack(false);
        }

        private void MotionStateChangedHandler(Motion motion)
        {
            if(motion == Motion.Attack) return;
            if (!IsAttacking()) return;
            StopCoroutine(CheckHitCor());
            print("Motion Changed Attack End");
            SetAttack(false);
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
                InvokeHitEvent(hitCollider.transform.position);
                break;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if(attackTransform == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(attackTransform.position,attackSphereRadius);
        }

        private bool IsAttacking()
        {
            return _isAttacking;
        }

        private void SetAttack(bool status)
        {
            _isAttacking = status;
        }
    }
}
