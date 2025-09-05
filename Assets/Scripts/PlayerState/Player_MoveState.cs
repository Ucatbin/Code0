using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_MoveState : Player_GroundState
{
    public Player_MoveState(PlayerController entity, StateMachine stateMachine, string stateName) : base(entity, stateMachine, stateName)
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

        if (Mathf.Abs(_player.AttributeSO.TargetVelocity.x) <= _player.AttributeSO.MaxGroundSpeed)
            _player.AttributeSO.TargetVelocity.x = Mathf.MoveTowards(
                _player.AttributeSO.TargetVelocity.x,
                _maxGroundVelocityX,
                _player.AttributeSO.GroundAccel * Time.fixedDeltaTime
            );
        else
            _player.AttributeSO.TargetVelocity.x = Mathf.MoveTowards(
                _player.AttributeSO.TargetVelocity.x,
                _maxGroundVelocityX,
                _player.AttributeSO.GroundDamping * Time.fixedDeltaTime
            );

        _player.Rb.linearVelocity = new Vector2(
            _player.AttributeSO.TargetVelocity.x,
            _player.Rb.linearVelocity.y
        );
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // If InputX == 0f, exit MoveState
        if (_player.InputSys.MoveInput.x == 0f)
            _stateMachine.ChangeState(_player.IdleState, true);
    }

    public override void Exit()
    {
        base.Exit();
    }
}