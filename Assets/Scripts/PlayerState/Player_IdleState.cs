using UnityEngine;

public class Player_IdleState : Player_GroundState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName){}

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

        // Change to moveState when have InputX and is not holding jump
        if (_player.InputSystem.MoveInput.x != 0f && !_player.InputSystem.JumpTrigger)
            _stateMachine.ChangeState(_player.MoveState);
    }

    public override void Exit()
    {
        
    }
}