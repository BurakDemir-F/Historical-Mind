using System;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class NpcCombatBehaviour : MonoBehaviour
    {
        [SerializeField]private Vector3 sharpObjectPosition;
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(sharpObjectPosition,1f);
        }
    }
}