using UnityEngine;

public class Player_FallState : Player_AirState
{
    public Player_FallState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName){}

    public override void Enter()
    { 
        // Control gravity to make it easier to control movement
        _player.Rb.gravityScale = _player.FallGravity;
    }
    
    public override void PhysicsUpdate()
    {

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void Exit()
    {
        
    }
}