using System;
using CodeMonkey.HealthSystemCM;
using ScriptableObjects;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class NpcHealthBehaviour : MonoBehaviour
    {
        [SerializeField] private LivingThing npcData;
        [SerializeField] private NpcInteractableBehaviour interactions;
        private HealthSystem _healthSystem;

        public event Action onNpcDie;
        
        private void Awake()
        {
            _healthSystem = new HealthSystem(npcData.Health);
            _healthSystem.OnDead += NpcDieHandler;
            interactions.OnDamage += DamageHandler;
        }

        private void OnDestroy()
        {
            _healthSystem.OnDead -= NpcDieHandler;
            interactions.OnDamage -= DamageHandler;
        }

        private void DamageHandler()
        {
            _healthSystem.Damage(npcData.Damage);
        }

        private void NpcDieHandler(object sender, EventArgs args)
        {
            onNpcDie?.Invoke();
        }
        
        
    }
}
