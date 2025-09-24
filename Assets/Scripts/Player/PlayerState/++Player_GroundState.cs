using Unity.Mathematics;
using UnityEngine;

public class Player_GroundState : Player_BaseState
{
    public Player_GroundState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.Rb.gravityScale = _player.PropertySO.DefaultGravity;
    }
    
    public override void PhysicsUpdate()
    {

    }
    public override void LogicUpdate()
    {
        // Enter airState as soon as leave the ground
        if (!_player.Checker.IsGrounded)
            _stateMachine.ChangeState(_player.StateSO.CoyoteState, false);
    }

    public override void Exit()
    {
        base.Exit();

        _player.Rb.linearVelocity = new Vector2(_player.Rb.linearVelocityX, 0f);
    }
}