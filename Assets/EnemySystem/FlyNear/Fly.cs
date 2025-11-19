using UnityEngine;

public class Fly : EnemyMoveStat
{
    float previousDir = 1;
    float direction = 1;
    float idleTimer;
    bool idleStart;

    public override void OnExit()
    {
        direction = 1;
        previousDir = direction;
        idleTimer = 0;
        idleStart = false;
    }

    public override void Tick()
    {
        float clampX = Mathf.Clamp(transform.position.x, enemy.left.position.x, enemy.right.position.x);
        transform.position = new Vector2(clampX, transform.position.y);
        if(transform.position.x == enemy.left.position.x && direction == -1)
        {
            previousDir = -1;
            direction = 0;
            idleStart = true;
        }
        if (transform.position.x == enemy.right.position.x && direction == 1)
        {
            previousDir = 1;
            direction = 0;
            idleStart = true;
        }

        if (idleStart)
        {
            idleTimer += Time.fixedDeltaTime;
            if(idleTimer >= 3)
            {
                direction = -previousDir;
                idleTimer = 0;
                idleStart = false;
            }
        }

        rb.linearVelocityX = direction * enemy.config.moveSpeed;
    }
}
