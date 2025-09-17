using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_MoveState : Player_GroundState
{
    public Player_MoveState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        float rate = Mathf.Abs(_player.RTProperty.TargetSpeed.x) <= _player.PropertySO.MaxGroundSpeed
            ? _player.PropertySO.GroundAccel * Time.fixedDeltaTime
            : _player.PropertySO.GroundDamping * Time.fixedDeltaTime;

        _player.RTProperty.TargetSpeed.x = Mathf.MoveTowards(
            _player.RTProperty.TargetSpeed.x,
            _player.RTProperty.FinalGroundSpeed * _player.InputSys.MoveInput.x,
            rate
        );

        _player.Rb.linearVelocity = new Vector2(
            _player.RTProperty.TargetSpeed.x,
            _player.Rb.linearVelocity.y
        );
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // If InputX == 0f, enter IdleState
        if (_player.InputSys.MoveInput.x == 0f)
            _stateMachine.ChangeState(_player.StateSO.IdleState, true);
    }

    public override void Exit()
    {
        base.Exit();
    }
}