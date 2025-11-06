using UnityEngine;

public class Player_WallJumpState : Player_AirState
{
    PlayerSkill_Jump _jumpSkill;
    Vector2 _wallJumpDir;

    public Player_WallJumpState(PlayerController_Main entity, StateMachineOld stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _jumpSkill = Player_SkillManager.Instance.Jump;

        TimerManager.Instance.AddTimer(
            _jumpSkill.WallJumpWindow,
            () => SkillEvents.TriggerJumpEnd(),
            "JumpStateTimer"
        );
        TimerManager.Instance.AddTimer(
            _jumpSkill.SkillCD,
            () => _jumpSkill.CoolDownSkill(),
            "PlayerSkillGap"
        );

        _player.HandleFlip();
        _wallJumpDir = new Vector2(-_jumpSkill.WallJumpDir.x, _jumpSkill.WallJumpDir.y).normalized;
    }
    public override void PhysicsUpdate()
    {
        _player.SetTargetVelocity(_jumpSkill.WallJumpPower * _wallJumpDir);
        _player.ApplyMovement();
    }
    public override void LogicUpdate() { }
    public override void Exit()
    {
        base.Exit();

        TimerManager.Instance.CancelTimersWithTag("JumpStateTimer");

        _player.IsBusy = false;
        _player.IsJumping = false;
    }
}