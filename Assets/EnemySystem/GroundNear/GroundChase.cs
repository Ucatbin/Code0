using UnityEngine;

public class GroundChase : EnemyChaseStat
{
    float dirction;
    public override void OnExit()
    {
        return;
    }

    public override void Tick()
    {
        float dir = enemy.target.position.x - transform.position.x;
        if (dir >= 0)
        {
            dirction = 1;
        }
        else{
            dirction = -1;
        }

        rb.linearVelocityX = dirction * enemy.config.chaseSpeed;
    }
}
