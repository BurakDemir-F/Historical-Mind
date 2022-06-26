using System;
using CodeMonkey.HealthSystemCM;
using ScriptableObjects;
using UnityEngine;

namespace MazeWorld.CreatureBehaviour
{
    public class HealthBehaviour : MonoBehaviour,IGetHealthSystem
    {
        [SerializeField] private LivingThing data;
        private HealthSystem _healthSystem;

        public event Action<float> OnDamage;
        public event Action OnDie;
        private void Awake()
        {
            _healthSystem = new HealthSystem(data.Health);
            _healthSystem.OnDead += PlayerDieHandler;
        }

        private void OnDestroy()
        {
            _healthSystem.OnDead -= PlayerDieHandler;
        }

        public void Damage(float damage)
        {
            print($"CurrentHealth: {_healthSystem.GetHealth()}");
            _healthSystem.Damage(damage);
            print($"AfterDamage: {_healthSystem.GetHealth()}");
            OnDamage?.Invoke(damage);
        }

        protected virtual void PlayerDieHandler(object sender, EventArgs args)
        {
            OnDie?.Invoke();
        }

        public HealthSystem GetHealthSystem()
        {
            return _healthSystem;
        }
    }
}
