using UnityEngine;

public class Player_AirGlideState : Player_AirState
{
    float _targetAirDamping;
    
    public Player_AirGlideState(PlayerController entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        float enterSpeed = _player.AttributeSO.TargetVelocity.x;
        _targetAirDamping = enterSpeed * 2.5f;
        _player.IsJumping = false;
    }
    
    public override void PhysicsUpdate()
    {
        _maxAirVelocityX =
            _player.InputSys.MoveInput.x *
            _player.AttributeSO.MaxAirMoveSpeed;

        if (_player.InputSys.MoveInput.x != 0f)
        {
            if (Mathf.Abs(_player.AttributeSO.TargetVelocity.x) <= _player.AttributeSO.MaxAirSpeed)
                _player.AttributeSO.TargetVelocity.x = Mathf.MoveTowards(
                    _player.AttributeSO.TargetVelocity.x,
                    _maxAirVelocityX,
                    _player.AttributeSO.AirAccel * Time.fixedDeltaTime
                );
            else
                _player.AttributeSO.TargetVelocity.x = Mathf.MoveTowards(
                    _player.AttributeSO.TargetVelocity.x,
                    _maxAirVelocityX,
                    _player.AttributeSO.AirDamping * Time.fixedDeltaTime
                );
        }
        else
            _player.AttributeSO.TargetVelocity.x = Mathf.MoveTowards(
                _player.AttributeSO.TargetVelocity.x,
                0,
                _player.AttributeSO.AirDamping / _targetAirDamping * Time.fixedDeltaTime
            );

        _player.Rb.linearVelocity = new Vector2(
            _player.AttributeSO.TargetVelocity.x,
            _player.Rb.linearVelocity.y
        );
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}