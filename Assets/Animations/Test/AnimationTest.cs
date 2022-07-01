using System;
using Maze;
using UnityEngine;
using Utilities;

namespace Animations.Test
{
    public class AnimationTest : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void Start()
        {
            animator.AddAnimationEvent(1,0.5f,Print);
        }

        public void Print()
        {
            print("test");
        }
    }
}
