using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public EnemyStat currentStat;
    [SerializeField] public EnemyData config;
    public Transform origin;
    public Transform left;
    public Transform right;

    [Header("Configs")]
    public Transform target;
    public EnemyMoveRange moveRange;
    public EnemyEyeSight sight;
    public EnemyAttackRange attackRange;
    public EnemyMoveStat moveStat;
    public EnemyChaseStat chaseStat;
    public EnemyAttackStat attackStat;
    public EnemyGoBackStat enemyGoBack;

    [Header("Status")]
    public bool findPlayer;
    public bool playerInAttackRange;
    public bool playerOutOfRange;
    public bool reachOriginPlace;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        moveRange.enemy = this;
        sight.enemy = this;
        attackRange.enemy = this;
        moveStat.enemy = this;
        chaseStat.enemy = this;
        attackStat.enemy = this;
        enemyGoBack.enemy = this;

        attackStat.attackInterval = config.attackInterval;
        attackStat.attackTimer = config.attackInterval / 2f;

        left.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        right.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        origin.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void FixedUpdate()
    {
        currentStat.CheckStat();
        currentStat.Tick();
    }
}
