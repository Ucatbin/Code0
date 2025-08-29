using Unity.Burst.Intrinsics;
using UnityEngine;

public class Player_JumpState : Player_AirState
{
    bool _shouldApplyForce;
    float _jumpForce;
    float _jumpHoldForce;
    Player_JumpTimer _timer;

    public Player_JumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {
        // Initialize
        _player.IsJumping = true;
        _timer = Player_TimerManager.Instance.JumpTimer;
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
        _timer.StartTimer(_player.JumpWindow);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Apply additional jump force
        if (_shouldApplyForce)
            _player.Rb.AddForce(Vector2.up * _jumpHoldForce, ForceMode2D.Force);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Can jump higher if holding Space button
        _shouldApplyForce = _timer.CurrentTimerVal >= _player.JumpDelay && 
                        _timer.CurrentTimerVal <= _player.JumpWindow && 
                        _player.InputSystem.JumpTrigger;

        // Cant add force after jumpWindow
        if (_timer.CurrentTimerVal > _player.JumpWindow || !_player.InputSystem.JumpTrigger)
        {
            _stateMachine.ChangeState(_player.AirState);
            _shouldApplyForce = false;
        }
    }

    public override void Exit()
    {
        _player.IsJumping = false;
    }
}
