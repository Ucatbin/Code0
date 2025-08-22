using UnityEngine;

public class Player_GroundState : Player_BaseState
{
    public Player_GroundState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName){}

    public override void Enter()
    {
        _player.Rb.linearVelocityY = 0f;
    }
    
    public override void PhysicsUpdate()
    {
        
    }
    public override void LogicUpdate()
    {
        // Can jump anytime if on ground
        if (_player.InputSystem.JumpTrigger)
        {
            _stateMachine.ChangeState(_player.JumpState);
            return;
        }

        // Enter airState as soon as leave the ground
        if (!_player.IsGrounded)
        {
            _stateMachine.ChangeState(_player.AirState);
            return;
        }
    }

    public override void Exit()
    {
        _player.Rb.linearVelocity = new Vector2(_player.Rb.linearVelocityX, 0f);
    }
}
