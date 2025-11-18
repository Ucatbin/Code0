using System;
using UnityEngine;

public class Player_AirGlideState : Player_AirState
{
    public Player_AirGlideState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.IsPhysicsDriven = true;
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _player.Rb.gravityScale = _player.GravityCurve.Evaluate(Mathf.Abs(_player.Rb.linearVelocity.magnitude));
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _player.ApplyMovement();
        if (Mathf.Abs(_player.Rb.linearVelocityX) < 6)
            _stateMachine.ChangeState(_player.StateSO.FallState, true);

    }

    public override void Exit()
    {
        base.Exit();

        _player.Rb.gravityScale = 0f;
        _player.IsPhysicsDriven = false;
    }
}