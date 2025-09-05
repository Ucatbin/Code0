using UnityEngine;

public class Player_AirState : Player_BaseState
{
    float _targetGravity;
    protected float _maxAirVelocityX;
    public Player_AirState(PlayerController entity, StateMachine stateMachine, string stateName) : base(entity, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _maxAirVelocityX =
            _player.InputSys.MoveInput.x *
            _player.AttributeSO.MaxAirMoveSpeed;

        if (_player.InputSys.MoveInput.x != 0f)
        {
            if (Mathf.Abs(_player.AttributeSO.TargetVelocity.x) <= _player.AttributeSO.MaxAirSpeed)
                _player.AttributeSO.TargetVelocity.x = Mathf.MoveTowards(
                    _player.AttributeSO.TargetVelocity.x,
                    _maxAirVelocityX,
                    _player.AttributeSO.AirAccel * Time.fixedDeltaTime
                );
            else
                _player.AttributeSO.TargetVelocity.x = Mathf.MoveTowards(
                    _player.AttributeSO.TargetVelocity.x,
                    _maxAirVelocityX,
                    _player.AttributeSO.AirDamping * Time.fixedDeltaTime
                );
        }
        else
            _player.AttributeSO.TargetVelocity.x = Mathf.MoveTowards(
                _player.AttributeSO.TargetVelocity.x,
                0,
                _player.AttributeSO.AirDamping * Time.fixedDeltaTime
            );

        _player.Rb.linearVelocity = new Vector2(
            _player.AttributeSO.TargetVelocity.x,
            _player.Rb.linearVelocity.y
        );
    }
    public override void LogicUpdate()
    {
        ChangeGravityScale();

        // Reset IsJumping to enable ground check, enter fallState
        if (_player.Rb.linearVelocityY < 0f && _stateMachine.CurrentState != _player.FallState)
            _stateMachine.ChangeState(_player.FallState, false);

        // Exit when detect the ground
        if (_player.Checker.IsGrounded && _player.Rb.linearVelocity.y <= 0f)
            _stateMachine.ChangeState(_player.IdleState, true);
    }

    public override void Exit()
    {
        base.Exit();
    }
    void ChangeGravityScale()
    {
        if (_player.IsJumping)
            return;

        if (Mathf.Abs(_player.Rb.linearVelocity.magnitude) < _player.AttributeSO.AirGlideThreshold)
            _targetGravity = _player.AttributeSO.MaxFallGravity;
        else
        {
            _targetGravity = Mathf.Lerp(
                _player.AttributeSO.MaxFallGravity,
                _player.AttributeSO.MinFallGravity,
                Mathf.Abs(_player.Rb.linearVelocity.magnitude) / _player.AttributeSO.MinGravityTrashold);
        }
        _player.Rb.gravityScale = _targetGravity;
    }
}
