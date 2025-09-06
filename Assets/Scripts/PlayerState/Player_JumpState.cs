using UnityEngine;

public class Player_JumpState : Player_AirState
{
    public Player_JumpState(PlayerController entity, StateMachine stateMachine, string stateName) : base(entity, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Initialize
        _player.IsJumping = true;
        _player.Rb.gravityScale = _player.AttributeSO.RiseGravity;

        // Start jump timer
        Player_TimerManager.Instance.AddTimer(
            _player.AttributeSO.JumpWindow,
            () => _stateMachine.ChangeState(_player.AirState, true),
            "JumpStateTimer"
        );

        _player.AttributeSO.TargetVelocity.y = _player.AttributeSO.JumpInitSpeed;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (Mathf.Abs(_player.AttributeSO.TargetVelocity.y) <= _player.AttributeSO.MaxRaiseSpeed)
            _player.AttributeSO.TargetVelocity.y = Mathf.MoveTowards(
                _player.AttributeSO.TargetVelocity.y,
                _player.AttributeSO.MaxJumpSpeed,
                _player.AttributeSO.JumpAccel
            );
        else
            _player.AttributeSO.TargetVelocity.y = Mathf.MoveTowards(
                _player.AttributeSO.TargetVelocity.y,
                _player.AttributeSO.MaxRaiseSpeed,
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
            _stateMachine.ChangeState(_player.AirState, true);
    }

    public override void Exit()
    {
        base.Exit();

        _player.AttributeSO.TargetVelocity.y = 0f;
        _player.IsJumping = false;
        Player_TimerManager.Instance.CancelTimersWithTag("JumpStateTimer");
    }
}
