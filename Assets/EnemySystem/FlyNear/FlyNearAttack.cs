using System.Collections;
using UnityEngine;

public class FlyNearAttack : EnemyAttackStat
{
    public Vector2 startPos;
    public Vector2 targetPos;
    public float dashDistance;
    public bool goBack;

    public override void OnExit()
    {
        attackTimer = attackInterval/2;
    }

    public override void Tick()
    {
        if (!isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            attackTimer += Time.fixedDeltaTime;
            if (attackTimer >= attackInterval)
            {
                Attack();
            }
        }
        else
        {
            if(!goBack)
            {
                float distance = Vector2.Distance(startPos, transform.position);
                if (distance <= dashDistance)
                {
                    rb.linearVelocity = 24 * (targetPos - startPos).normalized;
                }
                else
                {
                    goBack = true;
                }
            }
            else
            {
                float distance = Vector2.Distance(targetPos, transform.position);
                if (distance <= dashDistance)
                {
                    rb.linearVelocity = 10 * (startPos - targetPos).normalized;
                }
                else
                {
                    isAttacking = false;
                    goBack = false;
                }
            }
        }
    }

    void Attack()
    {
        goBack = false;
        attackTimer = 0;
        isAttacking = true;
        startPos = transform.position;
        targetPos = enemy.target.position + new Vector3(0,1.5f,0);
        dashDistance = Vector2.Distance(targetPos, startPos);
    }

}


