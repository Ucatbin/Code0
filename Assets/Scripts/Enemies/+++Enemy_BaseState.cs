using UnityEngine;

public class Enemy_Basestate : EntityState
{
    protected EnemyController_Main _enemy;
    public Enemy_Basestate(EnemyController_Main entity, StateMachineOld stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
        _enemy = entity;
    }

    public override void Enter()
    {
        // _enemy.Anim?.SetBool(_stateName, true);
    }
    public override void PhysicsUpdate() {}
    public override void LogicUpdate() {}
    public override void Exit()
    {
        // _enemy.Anim?.SetBool(_stateName, false);
    }
}
