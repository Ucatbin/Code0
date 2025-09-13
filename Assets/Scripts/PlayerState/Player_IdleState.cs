using UnityEngine;

public class Player_IdleState : Player_GroundState
{
    public Player_IdleState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.PropertySO.TargetVelocity.y = 0f;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.PropertySO.TargetVelocity.x = Mathf.MoveTowards(
            _player.PropertySO.TargetVelocity.x,
            0,
            _player.PropertySO.GroundDamping * Time.fixedDeltaTime
        );

        _player.Rb.linearVelocity = new Vector2(
            _player.PropertySO.TargetVelocity.x,
            _player.Rb.linearVelocity.y
        );
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Change to moveState when have InputX and is not holding jump
        if (_player.InputSys.MoveInput.x != 0f)
            _stateMachine.ChangeState(_player.StateSO.MoveState, false);
    }

    public override void Exit()
    {
        base.Exit();
    }
}