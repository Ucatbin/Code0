using System;
using UnityEngine;

public class Player_BaseState : EntityState
{
    protected PlayerController _player;

    public Player_BaseState(PlayerController entity, StateMachine stateMachine,string stateName) : base(entity, stateMachine, stateName)
    {
        _player = entity;
        Priority = _player.GetStatePriority(GetType());
    }

    public override void Enter() { }
    public override void PhysicsUpdate() { }
    public override void LogicUpdate() { }
    public override void Exit() { }
}
