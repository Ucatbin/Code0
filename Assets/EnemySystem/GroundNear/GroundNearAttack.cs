using UnityEngine;

public class GroundNearAttack : EnemyAttackStat
{
    private float attackDuration = 0.5f;
    private bool attackStart;
    private float attackAnimTimer;
    private Color originColor;
    public override void OnExit()
    {
        attackTimer = 0;
        isAttacking = false;
        attackAnimTimer = 0;
    }

    private void Start()
    {
        originColor = GetComponent<SpriteRenderer>().color;
    }

    public override void Tick()
    {
        
        if (!isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            attackTimer += Time.fixedDeltaTime;
            if(attackTimer >= attackInterval)
            {
                Attack();
                attackTimer = 0;
            }
        }
        else
        {
            attackAnimTimer += Time.fixedDeltaTime;
            GetComponent<SpriteRenderer>().color = Color.red;

            if(attackAnimTimer >= attackDuration)
            {
                attackAnimTimer -= attackDuration;
                GetComponent<SpriteRenderer>().color = originColor;
                isAttacking = false;
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;
        GetComponent<AudioSource>().Play();
    }

    
}
