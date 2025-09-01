using UnityEngine;
using UnityEngine.InputSystem;

public class Player_MoveState : Player_GroundState
{
    bool _shouldAddForce;

    public Player_MoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_shouldAddForce)
            _player.Rb.AddForce(new Vector2(
                _player.InputSys.MoveInput.x * _player.GroundMoveForce,
                0f
            ), ForceMode2D.Force);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // If current velocity less than max speed, can add force
        _shouldAddForce = Mathf.Abs(_player.Rb.linearVelocity.x) < _player.MaxGroundSpeed;


        // If InputX == 0f, exit MoveState
        if (_player.InputSys.MoveInput.x == 0f)
            _stateMachine.ChangeState(_player.IdleState);
    }

    public override void Exit()
    {
        
    }
}