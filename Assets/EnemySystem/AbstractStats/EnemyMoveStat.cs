using UnityEngine;

public abstract class EnemyMoveStat: EnemyStat
{
    public EnemyController enemy;

    public override void CheckStat()
    {
        if (enemy.findPlayer)
        {
            OnExit();
            enemy.currentStat = enemy.chaseStat;
        }
    }
}
