using UnityEngine;

public class Player_DashState : Player_GroundState
{
    public Player_DashState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {
        base.Enter();
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
