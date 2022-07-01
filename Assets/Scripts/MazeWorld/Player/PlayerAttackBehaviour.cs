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
        [SerializeField] private Transform attackTransformStart;
        [SerializeField] private Transform attackTransformEnd;
        [SerializeField] private LayerMask attackLayer;
        [SerializeField] private float attackSphereRadius = 1f;
        [SerializeField] private LivingThing npcData;
        [SerializeField] private Collider thisCollider;
        [SerializeField] private PlayerMotionBehaviour motionBehavior;
        private SpherePhysic _overlapHelper;
        private bool _isAttackMotionPlaying = false;
        
        public event Action OnAttack;
        
        private void Start()
        {
            _overlapHelper = new SpherePhysic(attackTransformEnd,attackSphereRadius, attackLayer);
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
            if(IsAttacking()) return;
            StartCoroutine(CheckHitCor());
            SetAttack(true);
        }

        private void AttackMotionEndHandler()
        {
            StopCoroutine(CheckHitCor());
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
            
            foreach (var hitCollider in hitColliders)
            {
                if(thisCollider == hitCollider) continue;
                var damageObject = hitCollider.GetComponent<IDamageable>();
                if (damageObject == null) continue;
                damageObject.Damage(npcData.Power);
                break;
            }
        }

        private bool IsAttacking()
        {
            return _isAttackMotionPlaying;
        }

        private void SetAttack(bool status)
        {
            _isAttackMotionPlaying = status;
        }
        

        private void OnDrawGizmos()
        {
            _overlapHelper?.DrawGizmo();
        }

        #region Test

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                AttackButtonPressedHandler();
            }
        }

        #endregion
    }
}
