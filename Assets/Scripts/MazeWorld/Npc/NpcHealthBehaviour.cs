using System;
using CodeMonkey.HealthSystemCM;
using ScriptableObjects;
using UnityEngine;

namespace MazeWorld.Npc
{
    public class NpcHealthBehaviour : MonoBehaviour,IGetHealthSystem
    {
        [SerializeField] private LivingThing npcData;
        [SerializeField] private NpcDamageHandler interactions;
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

        private void DamageHandler(float damage)
        {
            _healthSystem.Damage(damage);
            print("Npc damage!");
        }

        private void NpcDieHandler(object sender, EventArgs args)
        {
            onNpcDie?.Invoke();
            print("Npc dead!!!");
        }


        public HealthSystem GetHealthSystem()
        {
            return _healthSystem;
        }
    }
}
