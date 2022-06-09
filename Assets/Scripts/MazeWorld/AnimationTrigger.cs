using System;
using UnityEngine;

namespace MazeWorld
{
    public class AnimationTrigger : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator animator;
        private static readonly int Trigger1 = Animator.StringToHash("Trigger");

        public void Interact()
        {
            animator.SetTrigger(Trigger1);
        }
    }
}
