using UnityEngine;

public class Player_DashState : Player_GroundState
{
    public Player_DashState(PlayerController entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

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
        base.Exit();
    }
}
