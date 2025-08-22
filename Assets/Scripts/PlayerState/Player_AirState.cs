using UnityEngine;

public class Player_AirState : Player_BaseState
{
    public Player_AirState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {

    }
    
    public override void PhysicsUpdate()
    {

    }
    public override void LogicUpdate()
    {
        // Reset IsJumping to enable ground check, enter fallState
        if (_player.Rb.linearVelocityY <= 0f && _stateMachine.CurrentState != _player.FallState)
        {
            _player.IsJumping = false;
            _stateMachine.ChangeState(_player.FallState);
        }
            
        // Can move slowly when in air
        float moveInputX = _player.InputSystem.MoveInput.x;
        _player.Rb.linearVelocity = new Vector2(
            moveInputX * _player.MoveSpeedAir,
            _player.Rb.linearVelocityY
        );

        // Exit when detect the ground
        if (_player.IsGrounded)
            _stateMachine.ChangeState(_player.IdleState);
    }

    public override void Exit()
    {
        
    }
}
