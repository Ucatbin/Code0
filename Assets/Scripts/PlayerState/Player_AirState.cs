using UnityEngine;

public class Player_AirState : Player_BaseState
{
    bool _shouldAddForce;

    public Player_AirState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {

    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_shouldAddForce)
            _player.Rb.AddForce(new Vector2(
                _player.InputSystem.MoveInput.x * _player.AirMoveForce,
                0f
            ), ForceMode2D.Force);
    }
    public override void LogicUpdate()
    {
        // Reset IsJumping to enable ground check, enter fallState
        if (_player.Rb.linearVelocityY <= 0f && _stateMachine.CurrentState != _player.FallState)
        {
            _player.IsJumping = false;
            _stateMachine.ChangeState(_player.FallState);
        }
            
        // If current velocity less than max speed, can add force
        _shouldAddForce = Mathf.Abs(_player.Rb.linearVelocity.x) < _player.MaxAirSpeed;

        // Exit when detect the ground
            if (_player.Checker.IsGrounded)
                _stateMachine.ChangeState(_player.IdleState);
    }

    public override void Exit()
    {
        
    }
}
