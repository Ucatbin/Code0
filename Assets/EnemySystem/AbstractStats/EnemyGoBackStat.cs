using UnityEngine;

public abstract class EnemyGoBackStat : EnemyStat
{
    public EnemyController enemy;

    public override void CheckStat()
    {
        if(enemy.reachOriginPlace)
        {
            enemy.currentStat = enemy.moveStat;
            OnExit();
        }
        else if (enemy.findPlayer)
        {
            enemy.currentStat = enemy.chaseStat;
            OnExit();
        }
    }
}
