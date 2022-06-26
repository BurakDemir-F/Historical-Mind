using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace MazeWorld.Npc
{
    public class NpcMotionBehaviour : MonoBehaviour
    {
        [SerializeField] private NpcBehaviour npcBehaviour;
        [SerializeField] private Animator animator;
        [SerializeField] private NpcHealthBehaviour healthBehaviour;
        [SerializeField] private NpcInteractableBehaviour npcInteractions;
        private static readonly int State = Animator.StringToHash("State");
        private Coroutine _singleTakeDamageCor;

        public bool isTakingDamage = false;
        public bool isAttacking = false;

        public event Action OnAttackMotionStart;
        public event Action OnAttackMotionEnd;
        
        private void Start()
        {
            npcBehaviour.OnAttackRange += OnAttackRangeHandler;
            npcBehaviour.OnChaseRange += OnChaseRangeHandler;
            npcBehaviour.OnPlayerEscape += OnPlayerEscapeHandler;
            healthBehaviour.onNpcDie += NpcDieHandler;
            npcInteractions.OnDamage += NpcDamageHandler;
        }

        private void OnDestroy()
        {
            npcBehaviour.OnAttackRange -= OnAttackRangeHandler;
            npcBehaviour.OnChaseRange -= OnChaseRangeHandler;
            npcBehaviour.OnPlayerEscape -= OnPlayerEscapeHandler;
            healthBehaviour.onNpcDie -= NpcDieHandler;
            npcInteractions.OnDamage -= NpcDamageHandler;
        }

       // private void OnEnable() => animator.SetInteger(State,1); // unsolved mystery here.

        private void OnAttackRangeHandler()
        {
            if(isAttacking) return;
            animator.SetInteger(State,3);
            isAttacking = true;
            OnAttackMotionStart?.Invoke();
            DOVirtual.DelayedCall(GetCurrentMoveLength(), () =>
            {
                OnAttackMotionEnd?.Invoke();
                isAttacking = false;
            });
        }

        private void OnChaseRangeHandler()
        {
            animator.SetInteger(State,2);
        }

        private void OnPlayerEscapeHandler()
        {
            animator.SetInteger(State,2);
        }

        private void NpcDamageHandler()
        {
            if (isTakingDamage) return;
            animator.SetInteger(State, 4);
            isTakingDamage = true;
            DOVirtual.DelayedCall(GetCurrentMoveLength(), () => isTakingDamage = false);
        }
        
        private void NpcDieHandler()
        {
            //npc died.
            print("npc died");
        }
        
        private float GetCurrentMoveLength()
        {
            var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            return clipInfo[0].clip.length;
        }
    }

    public enum Motion
    {
        None,
        Idle,
        Walk,
        Attack
    }
}
