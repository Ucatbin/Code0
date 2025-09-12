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
        _player.Rb.gravityScale = _player.AttributeSO.RiseGravity;
        _jumpSkill.FinishJump = false;

        // Start jump timer
        Player_TimerManager.Instance.AddTimer(
            _player.AttributeSO.JumpInputWindow,
            () => SkillEvents.TriggerJumpEnd(),
            "JumpStateTimer"
        );

        Player_TimerManager.Instance.AddTimer(
            _jumpSkill.SkillCD,
            () =>{
                if (_jumpSkill.CurrentCharges != 0)
                    _jumpSkill.FinishJump = true;
            },
            "PlayerSkillGap"
        );
        _player.AttributeSO.TargetVelocity.y = _player.AttributeSO.JumpInitSpeed;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        float maxSpeed = Mathf.Abs(_player.AttributeSO.TargetVelocity.y) <= _player.AttributeSO.MaxRaiseSpeed ? _player.AttributeSO.MaxJumpSpeed : _player.AttributeSO.MaxRaiseSpeed;
        _player.AttributeSO.TargetVelocity.y = Mathf.MoveTowards(
            _player.AttributeSO.TargetVelocity.y,
            maxSpeed,
            _player.AttributeSO.JumpAccel
        );

        _player.Rb.linearVelocity = new Vector2(
            _player.Rb.linearVelocityX,
            _player.AttributeSO.TargetVelocity.y
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

        _player.AttributeSO.TargetVelocity.y = 0f;
        Player_TimerManager.Instance.CancelTimersWithTag("JumpStateTimer");
    }
}
