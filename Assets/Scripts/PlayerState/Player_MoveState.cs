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
            _player.PlayerItem.Property.MaxGroundMoveSpeed;

        float rate = Mathf.Abs(_player.PlayerItem.TargetSpeed.x) <= _player.PlayerItem.Property.MaxGroundSpeed
            ? _player.PlayerItem.Property.GroundAccel * Time.fixedDeltaTime
            : _player.PlayerItem.Property.GroundDamping * Time.fixedDeltaTime;

        _player.PlayerItem.TargetSpeed.x = Mathf.MoveTowards(
            _player.PlayerItem.TargetSpeed.x,
            _maxGroundVelocityX,
            rate
        );

        _player.Rb.linearVelocity = new Vector2(
            _player.PlayerItem.TargetSpeed.x,
            _player.Rb.linearVelocity.y
        );
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // If InputX == 0f, enter IdleState
        if (_player.InputSys.MoveInput.x == 0f)
            _stateMachine.ChangeState(_player.PlayerItem.State.IdleState, true);
    }

    public override void Exit()
    {
        base.Exit();
    }
}