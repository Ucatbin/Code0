using UnityEngine;

public class Player_FallState : Player_AirState
{
    public Player_FallState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {
        // Control gravity to make it easier to control movement
        // _player.Rb.gravityScale = _player.FallGravityMax;
    }

    public override void PhysicsUpdate()
    {

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ChangeGravityScale();
    }

    public override void Exit()
    {

    }
    
    void ChangeGravityScale()
    {
        float targetGravity = _player.FallGravityMax;
        if (_player.Rb.linearVelocityX < _player.AirGlideThreshold)
            targetGravity = _player.FallGravityMax;
        else
        {
            targetGravity = Mathf.Lerp(
                _player.FallGravityMin,
                _player.FallGravityMax,
                (Mathf.Abs(_player.Rb.linearVelocityX) - _player.AirGlideThreshold) / _player.MinGravityTrashold);
        }
        _player.Rb.gravityScale = targetGravity;
    }
}