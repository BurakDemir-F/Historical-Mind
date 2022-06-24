using System;
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
        private int _attack = Motion.Attack.ToInt();
        private static readonly int State = Animator.StringToHash("State");

        private void Start()
        {
            npcBehaviour.OnAttackRange += OnAttackRangeHandler;
            npcBehaviour.OnChaseRange += OnChaseRangeHandler;
            npcBehaviour.OnPlayerEscape += OnPlayerEscapeHandler;
            healthBehaviour.onNpcDie += NpcDieHandler;
        }

        private void OnDestroy()
        {
            npcBehaviour.OnAttackRange -= OnAttackRangeHandler;
            npcBehaviour.OnChaseRange -= OnChaseRangeHandler;
            npcBehaviour.OnPlayerEscape -= OnPlayerEscapeHandler;
            healthBehaviour.onNpcDie -= NpcDieHandler;
        }

       // private void OnEnable() => animator.SetInteger(State,1); // unsolved mystery here.

        private void OnAttackRangeHandler()
        {
            animator.SetInteger(State,3);
        }

        private void OnChaseRangeHandler()
        {
            animator.SetInteger(State,2);
        }

        private void OnPlayerEscapeHandler()
        {
            animator.SetInteger(State,2);
        }

        private void NpcDieHandler()
        {
            //npc died.
            print("npc died");
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
