using UnityEngine;

public class Enemy_IdleState : Enemy_GroundState
{
    public Enemy_IdleState(EnemyController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void PhysicsUpdate() { }
    public override void LogicUpdate() { }
    public override void Exit()
    {
        base.Exit();
    }
}