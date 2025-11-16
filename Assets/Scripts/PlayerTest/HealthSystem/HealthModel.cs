using System;
using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.HealthSystem
{
    public class HealthModel
    {
        public event Action<float> OnHealthChanged;
        public event Action OnDeath;

        // Dependency
        HealthData _data;
        public HealthData Data => _data;
        float _currentHealth;
        public float CurrentHealth => _currentHealth;
        public HealthModel(HealthData data)
        {
            _data = data;
            _currentHealth = data.MaxHealth;
        }

        public void TakeDamage(DamageInfo info)
        {
            _currentHealth -= info.DamageAmount;
            _currentHealth = Mathf.Max(0, _currentHealth);
            OnHealthChanged?.Invoke(-info.DamageAmount);
            Debug.Log($"{info.DamageTarget} takes {info.DamageAmount} damage from {info.DamageSource}, currentHealth : {_currentHealth}");

            if (_currentHealth <= 0)
            {
                var beKilled = new BeKilled
                {
                    
                };
                EventBus.Publish(beKilled);
                OnDeath?.Invoke();
            }
        }
        public void TakeHeal(float heal)
        {
            if (_currentHealth <= 0) return;

            _currentHealth += heal;
            OnHealthChanged?.Invoke(heal);
        }

    }
} 