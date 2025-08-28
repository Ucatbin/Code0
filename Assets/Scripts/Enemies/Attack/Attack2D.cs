using UnityEngine;

public abstract class Attack2D : MonoBehaviour
{
    public EnemyConfig config;
    protected float nextTime;

    public bool CanAttack(float now) => now >= nextTime;

    public void TryAttack(Transform target)
    {
        if (!CanAttack(Time.time)) return;
        DoAttack(target);
        nextTime = Time.time + config.attackCooldown;
    }

    protected abstract void DoAttack(Transform target);
}
