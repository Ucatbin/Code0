using UnityEngine;
using UnityEngine.InputSystem;

public class Player_MoveState : Player_GroundState
{
    bool _shouldAddForce;

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

        if (_shouldAddForce)
            _player.Rb.AddForce(new Vector2(
                _player.InputSys.MoveInput.x * _player.AttributeSO.GroundMoveForce,
                0f
            ), ForceMode2D.Force);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // If current velocity less than max speed, can add force
        _shouldAddForce = Mathf.Abs(_player.Rb.linearVelocity.x) < _player.AttributeSO.MaxGroundSpeed;

        // If InputX == 0f, exit MoveState
        if (_player.InputSys.MoveInput.x == 0f)
            _stateMachine.ChangeState(_player.IdleState, true);
    }

    public override void Exit()
    {
        base.Exit();

        _player.Rb.linearVelocity = _player.Rb.linearVelocity * _player.AttributeSO.GroundDamping;
    }
}