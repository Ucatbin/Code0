using UnityEngine;

public class Player_AirState : Player_BaseState
{
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

        if (_player.InputSys.MoveInput.x != 0f)
        {
            float rate = Mathf.Abs(_player.RTProperty.TargetSpeed.x) <= _player.PropertySO.MaxAirSpeed ? _player.PropertySO.AirAccel * Time.fixedDeltaTime : _player.PropertySO.AirDamping * Time.fixedDeltaTime;
            _player.RTProperty.TargetSpeed.x = Mathf.MoveTowards(
                _player.RTProperty.TargetSpeed.x,
                _player.RTProperty.FinalAirSpeed * _player.InputSys.MoveInput.x,
                rate
            );
        }
        else
            _player.RTProperty.TargetSpeed.x = Mathf.MoveTowards(
                _player.RTProperty.TargetSpeed.x,
                0,
                _player.PropertySO.AirDamping * Time.fixedDeltaTime
            );

        _player.Rb.linearVelocity = new Vector2(
            _player.RTProperty.TargetSpeed.x,
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
