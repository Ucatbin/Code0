using UnityEngine;

public class Player_AirGlideState : Player_AirState
{
    float _targetAirDamping;
    
    public Player_AirGlideState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        float enterSpeed = _player.PropertySO.TargetVelocity.x;
        _targetAirDamping = enterSpeed * 2.5f;
        _player.IsJumping = false;
    }
    
    public override void PhysicsUpdate()
    {
        _maxAirVelocityX =
            _player.InputSys.MoveInput.x *
            _player.PropertySO.MaxAirMoveSpeed;

        if (_player.InputSys.MoveInput.x != 0f)
        {
            if (Mathf.Abs(_player.PropertySO.TargetVelocity.x) <= _player.PropertySO.MaxAirSpeed)
                _player.PropertySO.TargetVelocity.x = Mathf.MoveTowards(
                    _player.PropertySO.TargetVelocity.x,
                    _maxAirVelocityX,
                    _player.PropertySO.AirAccel * Time.fixedDeltaTime
                );
            else
                _player.PropertySO.TargetVelocity.x = Mathf.MoveTowards(
                    _player.PropertySO.TargetVelocity.x,
                    _maxAirVelocityX,
                    _player.PropertySO.AirDamping * Time.fixedDeltaTime
                );
        }
        else
            _player.PropertySO.TargetVelocity.x = Mathf.MoveTowards(
                _player.PropertySO.TargetVelocity.x,
                0,
                _player.PropertySO.AirDamping / _targetAirDamping * Time.fixedDeltaTime
            );

        _player.Rb.linearVelocity = new Vector2(
            _player.PropertySO.TargetVelocity.x,
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