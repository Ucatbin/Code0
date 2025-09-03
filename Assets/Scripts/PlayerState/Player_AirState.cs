using UnityEngine;

public class Player_AirState : Player_BaseState
{
    bool _shouldAddForce;
    float _targetGravity;

    public Player_AirState(PlayerController player, StateMachine stateMachine, int priority, string stateName) : base(player, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_shouldAddForce)
            _player.Rb.AddForce(new Vector2(
                _player.InputSys.MoveInput.x * _player.PlayerSO.AirMoveForce,
                0f
            ), ForceMode2D.Force);
    }
    public override void LogicUpdate()
    {
        ChangeGravityScale();

        // Currently there's nothing fall state should do
        /*
        // Reset IsJumping to enable ground check, enter fallState
        // if (_player.Rb.linearVelocityY <= 0f && _stateMachine.CurrentState != _player.FallState)
        // {
        //     _player.IsJumping = false;
            // _stateMachine.ChangeState(_player.FallState);
        // }
        */

        // If current velocity less than max speed, can add force
        _shouldAddForce = Mathf.Abs(_player.Rb.linearVelocity.x) < _player.PlayerSO.MaxAirSpeed;

        // Exit when detect the ground
        if (_player.Checker.IsGrounded && _player.Rb.linearVelocity.y <= 0f)
            _stateMachine.ChangeState(_player.IdleState, true);
    }

    public override void Exit()
    {

    }
    void ChangeGravityScale()
    {
        Debug.Log(Mathf.Abs(_player.Rb.gravityScale));
        if (_player.IsJumping)
            return;

        if (Mathf.Abs(_player.Rb.linearVelocity.magnitude) < _player.PlayerSO.AirGlideThreshold)
            _targetGravity = _player.PlayerSO.MaxFallGravity;
        else
        {
            _targetGravity = Mathf.Lerp(
                _player.PlayerSO.MaxFallGravity,
                _player.PlayerSO.MinFallGravity,
                Mathf.Abs(_player.Rb.linearVelocity.magnitude) / _player.PlayerSO.MinGravityTrashold);
        }
        _player.Rb.gravityScale = _targetGravity;
    }
}
