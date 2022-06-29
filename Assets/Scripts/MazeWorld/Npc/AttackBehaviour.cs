using System;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class AttackBehaviour : MonoBehaviour
    {
        public event Action<Vector3> OnHit;

        protected virtual void InvokeHitEvent(Vector3 pos)
        {
            OnHit?.Invoke(pos);
        }
    }
}