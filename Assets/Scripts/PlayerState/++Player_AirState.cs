using UnityEngine;

public class Player_AirState : Player_BaseState
{
    protected float _maxAirVelocityX;

    public Player_AirState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
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
            _player.PlayerItem.Property.MaxAirMoveSpeed;

        if (_player.InputSys.MoveInput.x != 0f)
        {
            float rate = Mathf.Abs(_player.PlayerItem.TargetSpeed.x) <= _player.PlayerItem.Property.MaxAirSpeed ? _player.PlayerItem.Property.AirAccel * Time.fixedDeltaTime : _player.PlayerItem.Property.AirDamping * Time.fixedDeltaTime;
            _player.PlayerItem.TargetSpeed.x = Mathf.MoveTowards(
                _player.PlayerItem.TargetSpeed.x,
                _maxAirVelocityX,
                rate
            );
        }
        else
            _player.PlayerItem.TargetSpeed.x = Mathf.MoveTowards(
                _player.PlayerItem.TargetSpeed.x,
                0,
                _player.PlayerItem.Property.AirDamping * Time.fixedDeltaTime
            );

        _player.Rb.linearVelocity = new Vector2(
            _player.PlayerItem.TargetSpeed.x,
            _player.Rb.linearVelocity.y
        );
    }
    public override void LogicUpdate()
    {
        // Reset IsJumping to enable ground check, enter fallState
        if (_player.Rb.linearVelocityY < 0f && _stateMachine.CurrentState != _player.PlayerItem.State.FallState)
            _stateMachine.ChangeState(_player.PlayerItem.State.FallState, false);

        // Exit when detect the ground
        if (_player.Checker.IsGrounded && _stateMachine.CurrentState != _player.PlayerItem.State.JumpState)
            _stateMachine.ChangeState(_player.PlayerItem.State.IdleState, true);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
