using System.Net.Security;
using UnityEngine;

public class Player_BaseState : EntityState
{
    protected PlayerController _player;

    public Player_BaseState(PlayerController player, StateMachine stateMachine, int priority, string stateName) : base(player, stateMachine, priority, stateName)
    {
        _player = player;
    }

    public override void Enter()
    {
        
    }
    
    public override void PhysicsUpdate()
    {

    }
    public override void LogicUpdate()
    {
        
    }

    public override void Exit()
    {
        
    }
}
