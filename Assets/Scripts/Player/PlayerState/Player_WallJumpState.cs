using UnityEngine;

public class Player_WallJumpState : Player_AirState
{
    PlayerSkill_Jump _jumpSkill;

    public Player_WallJumpState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Initialize
        _jumpSkill = Player_SkillManager.Instance.Jump;
        _player.IsAddingForce = true;
        _player.Rb.gravityScale = _player.PropertySO.RiseGravity;
        _player.RTProperty.TargetSpeed = Vector2.zero;
        _player.Rb.linearVelocity = Vector2.zero;

        // Start jump timer
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

        Vector2 diagonalForce = new Vector2(-_player.FacingDir * _player.PropertySO.WallJumpDir.x, _player.PropertySO.WallJumpDir.y).normalized;
        _player.Rb.AddForce(_player.PropertySO.WallJumpForce * diagonalForce, ForceMode2D.Impulse);
    }
    public override void PhysicsUpdate() { }
    public override void LogicUpdate() { }
    public override void Exit()
    {
        base.Exit();

        _player.IsAddingForce = false;
        _player.RTProperty.TargetSpeed.y = 0f;
    }
}