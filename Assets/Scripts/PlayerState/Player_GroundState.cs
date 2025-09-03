using UnityEngine;

public class Player_GroundState : Player_BaseState
{
    public Player_GroundState(PlayerController player, StateMachine stateMachine, int priority, string stateName) : base(player, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        _player.Rb.gravityScale = 1f;
    }
    
    public override void PhysicsUpdate()
    {
        
    }
    public override void LogicUpdate()
    {
        // Can jump anytime if on ground
        if (_player.InputSys.JumpTrigger)
        {
            _stateMachine.ChangeState(_player.JumpState, true);
            return;
        }

        // Enter airState as soon as leave the ground
        if (!_player.Checker.IsGrounded)
        {
            _stateMachine.ChangeState(_player.AirState, true);
            return;
        }
    }

    public override void Exit()
    {
        _player.Rb.linearVelocity = new Vector2(_player.Rb.linearVelocityX, 0f);
    }
}
