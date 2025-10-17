using System;

/// <summary>
/// All entity can be hit 
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// Only get form property scriptable object
    /// </summary>
    int MaxHealth { get; }
    /// <summary>
    /// Entity's current health
    /// </summary>
    int CurrentHealth { get; }

    void TakeDamage(DamageData damageData);
    void TakeHeal();
    void BeKilled(DamageData damageData);
}