/// <summary>
/// 可受伤接口，所有可以被攻击的物体都应该实现此接口
/// </summary>
public interface IDamageable
{
    int CurrentHealth { get; }
    int MaxHealth { get; }
    void TakeDamage(DamageData damageData);
    void TakeHeal();
    void Die();
}

