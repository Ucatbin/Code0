using UnityEngine;

public class AttackFar : EnemyAttackStat 
{
    public GameObject bulletPrefab;
    public GameObject bulletProducer;
    public float bulletInterval = 0.4f;
    public float bulletTimer = 0.4f;
    public int bulletNum = 3;
    public int curBullet;
    public bool audioPlayed;

    public override void OnExit()
    {
        attackTimer = 0;
        bulletProducer.SetActive(false);
        audioPlayed = false;
        bulletTimer = bulletInterval;
    }

    public override void Tick()
    {
        bulletProducer.SetActive(true);
        if (!isAttacking)
        {
            if (!audioPlayed)
            {
                GetComponent<AudioSource>().Play();
                audioPlayed = true;
            }
            rb.linearVelocity = Vector2.zero;
            attackTimer += Time.fixedDeltaTime;
            float x = 6f * (attackInterval - attackTimer)/attackInterval;
            bulletProducer.transform.localScale = new Vector3(x, bulletProducer.transform.localScale.y, 1);
            if (attackTimer >= attackInterval)
            {
                Attack();
                attackTimer = 0;
            }

            Aim();
        }
        else
        {
            bulletTimer += Time.deltaTime;
            if(curBullet < bulletNum)
            {
                if (bulletTimer >= bulletInterval)
                {
                    Instantiate(bulletPrefab, bulletProducer.transform.position, bulletProducer.transform.rotation);
                    bulletTimer -= bulletInterval;
                    curBullet++;
                }
            }
            else
            {
                isAttacking = false;
                curBullet = 0;
                audioPlayed = false;
                bulletTimer = 0.4f;
            }  
        }
    }

    private void Attack()
    {
        isAttacking = true;
    }

    private void Aim()
    {
        Vector2 dir = ((Vector2)enemy.target.position - (Vector2)bulletProducer.transform.position).normalized;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        float currentAngle = bulletProducer.transform.eulerAngles.z;

        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, 360f * Time.fixedDeltaTime);
        bulletProducer.transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }
}
