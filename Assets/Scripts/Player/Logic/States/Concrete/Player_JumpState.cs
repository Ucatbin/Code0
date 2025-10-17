using UnityEngine;

public class Player_JumpState : Player_AirState
{
    PlayerSkill_Jump _jumpSkill;

    public Player_JumpState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _jumpSkill = Player_SkillManager.Instance.Jump;

        _player.SetTargetVelocityY(_jumpSkill.JumpInitPower);
        _player.ApplyMovement();

        TimerManager.Instance.AddTimer(
            _jumpSkill.JumpInputWindow,
            () => SkillEvents.TriggerJumpEnd(),
            "JumpStateTimer"
        );
        TimerManager.Instance.AddTimer(
            _jumpSkill.SkillCD,
            () => _jumpSkill.CoolDownSkill(),
            "PlayerSkillGap"
        );

        if (_jumpSkill.CurrentCharges != _jumpSkill.MaxCharges)
            SkillEvents.TriggerJumpEnd();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_player.CheckerSys.IsWallDected)
            SkillEvents.TriggerJumpEnd();
        else
        {
            _player.SetTargetVelocityY(_jumpSkill.JumpHoldSpeed);
            _player.ApplyMovement();
        }        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void Exit()
    {
        base.Exit();

        TimerManager.Instance.CancelTimersWithTag("JumpStateTimer");

        _player.IsBusy = false;
        _player.IsJumping = false;
    }
}