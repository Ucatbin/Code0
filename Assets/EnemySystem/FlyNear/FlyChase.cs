using UnityEngine;

public class FlyChase : EnemyChaseStat
{
    Vector2 dirction;

    public override void OnExit()
    {
        return;
    }

    public override void Tick()
    {
        dirction = (enemy.target.position + new Vector3(0,2.5f) - transform.position).normalized;
        rb.linearVelocity = dirction * enemy.config.chaseSpeed;
    }

}
