using System;
using UnityEngine;

public class Player_BaseState : EntityState
{
    protected PlayerController_Main _player;

    public Player_BaseState(PlayerController_Main entity, StateMachineOld stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
        _player = entity;
    }

    public override void Enter()
    {
        _player.Anim?.SetBool(_stateName, true);
    }
    public override void PhysicsUpdate() { }
    public override void LogicUpdate() { }
    public override void Exit()
    {
        _player.Anim?.SetBool(_stateName, false);
    }
}