using System;
using CodeMonkey.HealthSystemCM;
using ScriptableObjects;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class NpcHealth : MonoBehaviour
    {
        [SerializeField] private LivingThing npcData;
        private HealthSystem _healthSystem;

        private void Awake()
        {
            _healthSystem = new HealthSystem(npcData.Health);
            _healthSystem.OnDead += NpcDeadHandler;
        }

        private void Damage(float damage)
        {
            _healthSystem.Damage(damage);
        }

        private void NpcDeadHandler(object sender, EventArgs args)
        {
            print("Npc dead");
        }
    }
}
