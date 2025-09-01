using System.Net.Security;
using UnityEngine;

public class Player_BaseState : EntityState
{
    protected Player _player;
    public Player_BaseState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
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
