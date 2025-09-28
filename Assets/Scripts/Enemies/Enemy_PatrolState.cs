using UnityEngine;

public class Enemy_PatrolState : Enemy_GroundState
{
    public Enemy_PatrolState(EnemyController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.IsPatroling = true;
    }
    public override void PhysicsUpdate() {}
    public override void LogicUpdate()
    {
        if (_enemy.Checker.WallDected)
        {
            _enemy.Root.Rotate(new Vector2(0f, 180f));
            _enemy.FacingDir *= -1;
            _stateMachine.ChangeState(_enemy.StateSO.IdleState, false);
        }
    }
    public override void Exit()
    {
        base.Exit();

        _enemy.IsPatroling = false;
    }
}