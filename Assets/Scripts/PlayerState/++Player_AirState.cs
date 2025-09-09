using UnityEngine;

public class Player_AirState : Player_BaseState
{
    protected float _maxAirVelocityX;

    public Player_AirState(PlayerController entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (Player_SkillManager.Instance.Jump.FinishJump)
            if (!_player.InputSys.JumpTrigger)
                Player_SkillManager.Instance.Jump.CanUseSkill = true;

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
        // Reset IsJumping to enable ground check, enter fallState
        if (_player.Rb.linearVelocityY < 0f && _stateMachine.CurrentState != _player.StateSO.FallState)
            _stateMachine.ChangeState(_player.StateSO.FallState, false);

        // Exit when detect the ground
        if (_player.Checker.IsGrounded && _stateMachine.CurrentState != _player.StateSO.JumpState)
            _stateMachine.ChangeState(_player.StateSO.IdleState, true);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
