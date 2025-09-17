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
        _player.Rb.gravityScale = _player.PropertySO.RiseGravity;
        _jumpSkill.FinishJump = false;

        // Start jump timer
        TimerManager.Instance.AddTimer(
            _player.PropertySO.JumpInputWindow,
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
        _player.RTProperty.TargetSpeed.y = _player.PropertySO.JumpInitSpeed;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        float maxSpeed = Mathf.Abs(_player.RTProperty.TargetSpeed.y) <= _player.PropertySO.MaxRaiseSpeed ? _player.PropertySO.MaxJumpSpeed : _player.PropertySO.MaxRaiseSpeed;
        _player.RTProperty.TargetSpeed.y = Mathf.MoveTowards(
            _player.RTProperty.TargetSpeed.y,
            maxSpeed,
            _player.PropertySO.JumpAccel
        );

        _player.Rb.linearVelocity = new Vector2(
            _player.Rb.linearVelocityX,
            _player.RTProperty.TargetSpeed.y
        );
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Cant add force after jumpWindow
        if (!_player.InputSys.JumpTrigger)
            _stateMachine.ChangeState(_player.StateSO.AirState, true);
    }

    public override void Exit()
    {
        base.Exit();

        _player.RTProperty.TargetSpeed.y = 0f;
        TimerManager.Instance.CancelTimersWithTag("JumpStateTimer");
    }
}
