using UnityEngine;

public class Player_HookedState : Player_BaseState
{
    public Player_HookedState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {
        _player.Checker.GrappleCheckCollider.enabled = true;
    }

    public override void PhysicsUpdate()
    {

    }
    public override void LogicUpdate()
    {

    }

    public override void Exit()
    {
        _player.Checker.GrappleCheckCollider.enabled = false;
    }
}
