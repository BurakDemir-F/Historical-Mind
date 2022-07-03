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
        [SerializeField] private NpcDamageHandler npcInteractions;
        private static readonly int State = Animator.StringToHash("State");
        private Coroutine _singleTakeDamageCor;

        public bool isTakingDamage = false;
        public bool isAttacking = false;

        public event Action OnAttackMotionStart;
        public event Action OnAttackMotionEnd;

        public event Action<Motion> OnMotionStateChanged;
        
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
            OnMotionStateChanged?.Invoke(Motion.Attack);
            isAttacking = true;
            OnAttackMotionStart?.Invoke();
            DOVirtual.DelayedCall(animator.GetClipLengthFromClipIndex(2), () =>
            {
                OnAttackMotionEnd?.Invoke();
                isAttacking = false;
            });
        }

        private void OnChaseRangeHandler()
        {
            animator.SetInteger(State,2);
            OnMotionStateChanged?.Invoke(Motion.Walk);
        }

        private void OnPlayerEscapeHandler()
        {
            animator.SetInteger(State,2);
            OnMotionStateChanged?.Invoke(Motion.Walk);
        }

        private void NpcDamageHandler(float damage)
        {
            if (isTakingDamage) return;
            animator.SetInteger(State, 4);
            OnMotionStateChanged?.Invoke(Motion.TakeHit);
            isTakingDamage = true;
            DOVirtual.DelayedCall(animator.GetClipLengthFromClipIndex(3), () => isTakingDamage = false);
        }
        
        private void NpcDieHandler()
        {
            print("npc died");
            animator.SetInteger(State, 5);
            OnMotionStateChanged?.Invoke(Motion.Death);
        }
    }

    public enum Motion
    {
        None,
        Idle,
        Walk,
        Attack,
        TakeHit,
        Death
    }
}
