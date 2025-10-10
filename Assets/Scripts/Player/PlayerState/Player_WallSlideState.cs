using UnityEngine;

public class Player_WallSlideState : Player_BaseState
{
    public Player_WallSlideState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.IsWallSliding = true;
    }
    public override void PhysicsUpdate()
    {
        _player.SetTargetVelocityY(_player.PropertySO.WallSlideSpeed);
        _player.ApplyMovement();
    }
    public override void LogicUpdate()
    {
        if (_player.Checker.IsGrounded || !_player.Checker.WallDected || _player.InputSys.MoveInput.x == 0)
            _stateMachine.ChangeState(_player.StateSO.FallState, false);

    }
    public override void Exit()
    {
        base.Exit();

        _player.IsWallSliding = false;
    }
}
