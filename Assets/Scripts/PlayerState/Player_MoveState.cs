using UnityEngine;
using UnityEngine.InputSystem;

public class Player_MoveState : Player_GroundState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName){}

    public override void Enter()
    {
        
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Get InputX and set velocity
        float moveInputX = _player.InputSystem.MoveInput.x;
        _player.Rb.linearVelocity = new Vector2(
            moveInputX * _player.MoveSpeed,
            _player.Rb.linearVelocityY
        );

        // If InputX == 0f, exit MoveState
        if (_player.InputSystem.MoveInput.x == 0f)
            _stateMachine.ChangeState(_player.IdleState);
    }

    public override void Exit()
    {
        
    }
}