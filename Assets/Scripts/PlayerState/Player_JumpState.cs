using Unity.Burst.Intrinsics;
using UnityEngine;

public class Player_JumpState : Player_AirState
{
    bool _shouldApplyForce;
    float _jumpForce;
    float _jumpHoldForce;

    public Player_JumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {
        // Initialize
        _player.IsJumping = true;
        _timer = _player.JumpWindow;

        // Calculate jump force with jump height
        _jumpForce = Mathf.Sqrt(
            _player.JumpHeight *
            (_player.Rb.gravityScale * Physics2D.gravity.y) *
            -2f
        ) * _player.Rb.mass;
        _jumpHoldForce = Mathf.Sqrt(
            _player.JumpHoldHeight *
            (_player.Rb.gravityScale * Physics2D.gravity.y) *
            -2f
        ) * _player.Rb.mass;

        // Control gravity to make it easier to control movement
        _player.Rb.gravityScale = _player.JumpGravity;

        // Apply base jump once when enter
        _player.Rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Apply jump force
        if (_shouldApplyForce)
            _player.Rb.AddForce(Vector2.up * _jumpHoldForce, ForceMode2D.Force);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timer -= Time.deltaTime;

        // Can jump higher if holding Space button
        float jumpWindowTime = _player.JumpWindow - _player.JumpDelay;
        _shouldApplyForce = _timer >= 0f && 
                        _timer < jumpWindowTime && 
                        _player.InputSystem.JumpTrigger;

            
        // Cant add force after jumpWindow
        if (_timer < 0f)
        {
            _stateMachine.ChangeState(_player.AirState);
            _shouldApplyForce = false;
        }
    }

    public override void Exit()
    {

    }
}
