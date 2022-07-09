using System;
using DG.Tweening;
using ScriptableObjects;
using StarterAssets;
using UnityEngine;
using Utilities;

namespace MazeWorld.Player
{
    public class PlayerMotionBehaviour : MonoBehaviour
    {
        [SerializeField]private Animator animator;
        [SerializeField] private FirstPersonController character;
        [SerializeField] private PlayerAttackBehaviour attackBehaviour;
        [SerializeField] private LivingThing data;
        [SerializeField] private float cooldown = 1f;
        private static readonly int State = Animator.StringToHash("State");
        
        public event Action AttackMotionStart;
        public event Action AttackMotionEnd;

        private bool _isAttacking = false;
        private bool _isInCoolDown = false;

        private void Start()
        {
            character.OnMove += MovementStartHandler;
            character.OnMoveStop += MovementEndHandler;
            attackBehaviour.OnAttack += AttackHandler;
            // animator.DebugAllClipInfo();
        }

        private void OnDestroy()
        {
            character.OnMove -= MovementStartHandler;
            character.OnMoveStop -= MovementEndHandler;
            attackBehaviour.OnAttack -= AttackHandler;
        }

        private void MovementStartHandler()
        {
            if(_isAttacking) return;
            animator.SetInteger(State,2);
        }

        private void MovementEndHandler()
        {
            if(_isAttacking) return;
            animator.SetInteger(State,1);
        }

        private void AttackHandler()
        {
            if(_isAttacking) return;
            _isAttacking = true;
            animator.SetInteger(State,3);
            AttackMotionStart?.Invoke();
            var attackClipLength = animator.GetClipLengthFromClipIndex(2);
            DOVirtual.DelayedCall(attackClipLength, () =>
            {
                AttackMotionEnd?.Invoke();
                _isAttacking = false;
            });
        }

        #region Test

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                character.OnMove -= MovementStartHandler;
                character.OnMoveStop -= MovementEndHandler;
            }
        }

        #endregion
    }
}
