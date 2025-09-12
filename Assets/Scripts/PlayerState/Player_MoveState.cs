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
            _player.AttributeSO.MaxGroundMoveSpeed;

        float rate = Mathf.Abs(_player.AttributeSO.TargetVelocity.x) <= _player.AttributeSO.MaxGroundSpeed ? _player.AttributeSO.GroundAccel * Time.fixedDeltaTime : _player.AttributeSO.GroundDamping * Time.fixedDeltaTime;
        _player.AttributeSO.TargetVelocity.x = Mathf.MoveTowards(
            _player.AttributeSO.TargetVelocity.x,
            _maxGroundVelocityX,
            rate
        );

        _player.Rb.linearVelocity = new Vector2(
            _player.AttributeSO.TargetVelocity.x,
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