using UnityEngine;

public class Player_IdleState : Player_GroundState
{
    public Player_IdleState(PlayerController player, StateMachine stateMachine, int priority, string stateName) : base(player, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Change to moveState when have InputX and is not holding jump
        if (_player.InputSys.MoveInput.x != 0f)
        {
            if (_player.IsJumping)
                return;
            _stateMachine.ChangeState(_player.MoveState, true);
        }
    }

    public override void Exit()
    {
        
    }
}