using UnityEngine;

public abstract class EnemyChaseStat : EnemyStat
{
    public EnemyController enemy;

    public override void CheckStat()
    {
        if (enemy.playerInAttackRange)
        {
            OnExit();
            enemy.currentStat = enemy.attackStat;
        }
        else
        {
            if (enemy.playerOutOfRange)
            {
                OnExit();
                enemy.currentStat = enemy.enemyGoBack;
            }
        }
    }
}
