using System.Collections.Generic;
using ThisGame.Entity.EntitySystem;
using UnityEngine;

public class EnemyController : EntityController
{
    [Header("Components")]
    [SerializeField] public EnemyStat currentStat;
    [SerializeField] public EnemyData config;
    public Transform origin;
    public Transform left;
    public Transform right;

    [Header("Configs")]
    public Transform target;
    public EnemyAnimationController enemyAnimationController;
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

    protected override void Awake()
    {
        base.Awake();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Start()
    {
        base.Start();
        enemyAnimationController.enemy = this;
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

    protected override void FixedUpdate()
    {
        currentStat.CheckStat();
        currentStat.Tick();
    }
}
