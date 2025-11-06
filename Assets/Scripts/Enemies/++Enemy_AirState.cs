using UnityEngine;

public class Enemy_AirState : Enemy_Basestate
{
    public Enemy_AirState(EnemyController_Main entity, StateMachineOld stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
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
