using UnityEngine;

public class FlyGoBack : EnemyGoBackStat
{
    Vector2 direction;
    public override void OnExit()
    {
        enemy.reachOriginPlace = false;
    }

    public override void Tick()
    {
        direction = (enemy.origin.position - transform.position).normalized;
        rb.linearVelocity = direction * enemy.config.moveSpeed;

        if (Vector2.Distance(transform.position, enemy.origin.position) < 0.1f)
        {
            rb.linearVelocity = Vector2.zero;
            rb.MovePosition(enemy.origin.position);
            enemy.reachOriginPlace = true;
        }
    }

}
