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

        _maxGroundVelocityX =
            _player.InputSys.MoveInput.x *
            _player.PropertySO.MaxGroundMoveSpeed;

        float rate = Mathf.Abs(_player.PropertySO.TargetVelocity.x) <= _player.PropertySO.MaxGroundSpeed ? _player.PropertySO.GroundAccel * Time.fixedDeltaTime : _player.PropertySO.GroundDamping * Time.fixedDeltaTime;
        _player.PropertySO.TargetVelocity.x = Mathf.MoveTowards(
            _player.PropertySO.TargetVelocity.x,
            _maxGroundVelocityX,
            rate
        );

        _player.Rb.linearVelocity = new Vector2(
            _player.PropertySO.TargetVelocity.x,
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