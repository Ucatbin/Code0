using UnityEngine;

public class Player_WallJumpState : Player_AirState
{
    PlayerSkill_Jump _jumpSkill;
    Vector2 _wallJumpDir;

    public Player_WallJumpState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _jumpSkill = Player_SkillManager.Instance.Jump;

        _player.IsBusy = true;
        _player.IsJumping = true;

        TimerManager.Instance.AddTimer(
            _player.PropertySO.WallJumpWindow,
            () => SkillEvents.TriggerJumpEnd(),
            "JumpStateTimer"
        );
        TimerManager.Instance.AddTimer(
            _jumpSkill.SkillCD,
            () => _jumpSkill.CoolDownSkill(),
            "PlayerSkillGap"
        );

        _wallJumpDir = new Vector2(-_player.FacingDir * _player.PropertySO.WallJumpDir.x, _player.PropertySO.WallJumpDir.y).normalized;
        _jumpSkill.ConsumeSkill();
    }
    public override void PhysicsUpdate()
    {
        _player.SetTargetVelocity(_player.PropertySO.WallJumpPower * _wallJumpDir);
        _player.ApplyMovement();
    }
    public override void LogicUpdate() { }
    public override void Exit()
    {
        base.Exit();

        _player.IsBusy = false;
        _player.IsJumping = false;

        TimerManager.Instance.CancelTimersWithTag("JumpStateTimer");
    }
}