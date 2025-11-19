using UnityEngine;

public class AttackNear : EnemyAttackStat
{
    float attackTimer;

    public override void OnExit()
    {
        return;
    }

    public override void Tick()
    {
        rb.linearVelocity = Vector2.zero;

        attackTimer += Time.fixedDeltaTime;
        if(attackTimer >= enemy.config.attackInterval)
        {
            Attack();
            attackTimer -= enemy.config.attackInterval;
        }
    }

    void Attack() 
    {

    }
}
