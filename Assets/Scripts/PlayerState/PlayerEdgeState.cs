using UnityEngine;

public class Player_EdgeState : Player_BaseState
{
    public Player_EdgeState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName){}

    public override void Enter()
    {
        _player.Rb.gravityScale = 0f;
        _player.Rb.linearVelocity = Vector2.zero;
    }
    
    public override void PhysicsUpdate()
    {

    }
    public override void LogicUpdate()
    {
        if (_player.InputSystem.JumpTrigger)
        {
            _stateMachine.ChangeState(_player.JumpState);
            return;
        }
    }

    public override void Exit()
    {
        
    }
}
