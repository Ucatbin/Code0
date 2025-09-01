using Unity.Burst.Intrinsics;
using UnityEngine;

public class Player_JumpState : Player_AirState
{
    bool _shouldApplyForce;
    bool _canAddForce;
    float _jumpForce;
    float _jumpHoldForce;

    public Player_JumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {
        // Initialize
        _player.IsJumping = true;
        _player.Rb.gravityScale = _player.JumpGravity;

        // Calculate jump force with jump height
        _jumpForce = Mathf.Sqrt(
            _player.JumpHeight *
            (_player.Rb.gravityScale * Physics2D.gravity.y) *
            -2f
        ) * _player.Rb.mass;
        _jumpHoldForce = Mathf.Sqrt(
            _player.JumpHoldForce *
            (_player.Rb.gravityScale * Physics2D.gravity.y) *
            -2f
        ) * _player.Rb.mass;

        // Apply base jump once when enter
        _player.Rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

        // Start jump timer
        Player_TimerManager.Instance.AddTimer(
            _player.JumpDelay,
            () => { _canAddForce = true; },
            "JumpStateTimer"
        );
        Player_TimerManager.Instance.AddTimer(
            _player.JumpWindow,
            () => StopAddForce(),
            "JumpStateTimer"
        );
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Apply additional jump force
        if (_shouldApplyForce && _canAddForce)
            _player.Rb.AddForce(Vector2.up * _jumpHoldForce, ForceMode2D.Force);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Can jump higher if holding Space button
        _shouldApplyForce = _player.InputSys.JumpTrigger;

        // Cant add force after jumpWindow
        if (!_player.InputSys.JumpTrigger) StopAddForce();
    }

    public override void Exit()
    {
        _player.IsJumping = false;
        Player_TimerManager.Instance.CancelTimersWithTag("JumpStateTimer");
    }
    
    void StopAddForce()
    {
        _stateMachine.ChangeState(_player.AirState);
        _canAddForce = false;
        _shouldApplyForce = false;
    }
}
