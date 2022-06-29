using System;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class EffectBehaviour : MonoBehaviour
    {
        [SerializeField] private AttackBehaviour attackBehaviour;

        private void Start()
        {
            attackBehaviour.OnHit += HitHandler;
        }

        private void OnDestroy()
        {          
            attackBehaviour.OnHit -= HitHandler;
        }

        private void HitHandler(Vector3 position)
        {
            
        }
    }
}