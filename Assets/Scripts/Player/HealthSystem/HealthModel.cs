using System;
using ThisGame.Core;
using ThisGame.Entity.BuffSystem;
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
        public bool CanHit;
        public HealthModel(HealthData data)
        {
            _data = data;
            _currentHealth = data.MaxHealth;
            CanHit = true;
        }

        public void TakeDamage(DamageInfo info)
        {
            if (!CanHit) return;

            _currentHealth -= info.DamageAmount;
            _currentHealth = Mathf.Max(0, _currentHealth);
            OnHealthChanged?.Invoke(-info.DamageAmount);
            Debug.Log($"{info.DamageTarget} takes {info.DamageAmount} damage from {info.DamageSource}, currentHealth : {_currentHealth}");
            var beHit = new BeHit()
            {
              TargetEntity = info.DamageTarget.gameObject
            };
            EventBus.Publish(new BeHit());
            if (_currentHealth <= 0)
            {
                var beKilled = new BeKilled
                {
                    TargetEntity = info.DamageTarget.gameObject,
                    Killer = info.DamageSource.gameObject
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