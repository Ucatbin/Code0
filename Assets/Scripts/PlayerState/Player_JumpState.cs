using Unity.Burst.Intrinsics;
using UnityEngine;

public class Player_JumpState : Player_AirState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {
        base.Enter();

        _player.IsJumping = true;
        _timer = _player.JumpWindow;

        _player.Rb.gravityScale = _player.JumpGravity;

        float jumpForce = Mathf.Sqrt(_player.JumpHeight * (_player.Rb.gravityScale * Physics2D.gravity.y) * -2f) * _player.Rb.mass;
        _player.Rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    
    public override void PhysicsUpdate()
    {
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _timer -= Time.deltaTime;
        // Can jump higher if holding Space button
        if (_timer >= 0f &&
            _timer < _player.JumpWindow - _player.JumpDelay &&
            _player.InputSystem.JumpTrigger
        )
            _player.Rb.AddForce(new Vector2(0, _player.JumpHoldForce), ForceMode2D.Impulse);
        else if (_timer < 0f)
            _stateMachine.ChangeState(_player.AirState);
    }

    public override void Exit()
    {

    }
}
