using System.Threading;
using UnityEngine;

public class Enemy_IdleState : Enemy_GroundState
{
    public Enemy_IdleState(EnemyController_Main entity, StateMachineOld stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        TimerManager.Instance?.AddTimer(
            2f,
            () => _stateMachine.ChangeState(_enemy.StateSO.PatrolState, false),
            "EnemyPatrolWait"
        );
    }
    public override void PhysicsUpdate()
    {
    }
    public override void LogicUpdate()
    {

    }
    public override void Exit()
    {
        base.Exit();
    }
}