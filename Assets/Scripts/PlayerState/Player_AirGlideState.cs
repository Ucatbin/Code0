using UnityEngine;

public class Player_AirGlideState : Player_AirState
{
    bool _shouldAddForce;

    public Player_AirGlideState(PlayerController entity, StateMachine stateMachine, string stateName) : base(entity, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        _player.IsJumping = false;
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void LogicUpdate()
    {
        // If current velocity less than max speed, can add force
        _shouldAddForce = Mathf.Abs(_player.Rb.linearVelocity.x) < _player.AttributeSO.MaxAirSpeed;

        // Exit when detect the ground
        if (_player.Checker.IsGrounded)
            _stateMachine.ChangeState(_player.IdleState, true);
    }

    public override void Exit()
    {

    }
}