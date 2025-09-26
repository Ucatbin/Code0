/// <summary>
/// 可受伤接口，所有可以被攻击的物体都应该实现此接口
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
    void Die();
}