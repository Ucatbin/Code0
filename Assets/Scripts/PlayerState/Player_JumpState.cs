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

        // Initialize
        _jumpSkill = Player_SkillManager.Instance.Jump;
        _player.Rb.gravityScale = _player.PlayerItem.Property.RiseGravity;
        _jumpSkill.FinishJump = false;

        // Start jump timer
        TimerManager.Instance.AddTimer(
            _player.PlayerItem.Property.JumpInputWindow,
            () => SkillEvents.TriggerJumpEnd(),
            "JumpStateTimer"
        );

        TimerManager.Instance.AddTimer(
            _jumpSkill.SkillCD,
            () =>{
                if (_jumpSkill.CurrentCharges != 0)
                    _jumpSkill.FinishJump = true;
            },
            "PlayerSkillGap"
        );
        _player.PlayerItem.TargetSpeed.y = _player.PlayerItem.Property.JumpInitSpeed;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        float maxSpeed = Mathf.Abs(_player.PlayerItem.TargetSpeed.y) <= _player.PlayerItem.Property.MaxRaiseSpeed ? _player.PlayerItem.Property.MaxJumpSpeed : _player.PlayerItem.Property.MaxRaiseSpeed;
        _player.PlayerItem.TargetSpeed.y = Mathf.MoveTowards(
            _player.PlayerItem.TargetSpeed.y,
            maxSpeed,
            _player.PlayerItem.Property.JumpAccel
        );

        _player.Rb.linearVelocity = new Vector2(
            _player.Rb.linearVelocityX,
            _player.PlayerItem.TargetSpeed.y
        );
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Cant add force after jumpWindow
        if (!_player.InputSys.JumpTrigger)
            _stateMachine.ChangeState(_player.PlayerItem.State.AirState, true);
    }

    public override void Exit()
    {
        base.Exit();

        _player.PlayerItem.TargetSpeed.y = 0f;
        TimerManager.Instance.CancelTimersWithTag("JumpStateTimer");
    }
}
