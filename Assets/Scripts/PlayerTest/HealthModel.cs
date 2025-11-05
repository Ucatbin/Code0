using System;
using ThisGame.Entity.HealthSystem;
using UnityEngine;

public class HealthModel : IModel
{
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    // Dependency
    HealthData _data;
    float _currentHealth;
    public float CurrentHealth => _currentHealth;
    public HealthModel(HealthData data)
    {
        _currentHealth = data.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(0, _currentHealth);
        OnHealthChanged?.Invoke(damage);

        if (_currentHealth <= 0)
            OnDeath?.Invoke();
    }

}
