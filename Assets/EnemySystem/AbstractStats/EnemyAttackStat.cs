using UnityEngine;

public abstract class EnemyAttackStat : EnemyStat
{
    public EnemyController enemy;
    public bool isAttacking;
    public float attackInterval;
    public float attackTimer;

    public override void CheckStat()
    {
        if (isAttacking) return;
        if (!enemy.playerInAttackRange)
        {
            if (enemy.findPlayer)
            {
                OnExit();
                Debug.Log("atk -> chase");
                enemy.currentStat = enemy.chaseStat;
            }
            else
            {
                OnExit();
                Debug.Log("atk -> back");
                enemy.currentStat = enemy.enemyGoBack;
            }
        }
    }
}
