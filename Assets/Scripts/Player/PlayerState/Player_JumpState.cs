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

        _player.IsBusy = true;
        _player.IsJumping = true;

        _player.SetTargetVelocityY(_player.PropertySO.JumpInitPower);
        _player.ApplyMovement();

        TimerManager.Instance.AddTimer(
            _player.PropertySO.JumpInputWindow,
            () => SkillEvents.TriggerJumpEnd(),
            "JumpStateTimer"
        );
        TimerManager.Instance.AddTimer(
            _jumpSkill.SkillCD,
            () => _jumpSkill.ResetSkill(),
            "PlayerSkillGap"
        );

        _jumpSkill.ConsumeSkill();

        if (_jumpSkill.CurrentCharges != _jumpSkill.MaxCharges - 1)
            SkillEvents.TriggerJumpEnd();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetTargetVelocityY(_player.PropertySO.JumpHoldPower);
        _player.ApplyMovement();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Cant add force after jumpWindow
        if (!_player.InputSys.JumpTrigger)
            SkillEvents.TriggerJumpEnd();
    }

    public override void Exit()
    {
        base.Exit();
        
        _player.IsBusy = false;
        _player.IsJumping = false;
        
        TimerManager.Instance.CancelTimersWithTag("JumpStateTimer");
    }
}