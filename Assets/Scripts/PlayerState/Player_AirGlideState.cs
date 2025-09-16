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

        float enterSpeed = _player.PlayerItem.TargetSpeed.x;
        _targetAirDamping = enterSpeed * 2.5f;
        _player.IsJumping = false;
    }
    
    public override void PhysicsUpdate()
    {
        _maxAirVelocityX =
            _player.InputSys.MoveInput.x *
            _player.PlayerItem.Property.MaxAirMoveSpeed;

        if (_player.InputSys.MoveInput.x != 0f)
        {
            if (Mathf.Abs(_player.PlayerItem.TargetSpeed.x) <= _player.PlayerItem.Property.MaxAirSpeed)
                _player.PlayerItem.TargetSpeed.x = Mathf.MoveTowards(
                    _player.PlayerItem.TargetSpeed.x,
                    _maxAirVelocityX,
                    _player.PlayerItem.Property.AirAccel * Time.fixedDeltaTime
                );
            else
                _player.PlayerItem.TargetSpeed.x = Mathf.MoveTowards(
                    _player.PlayerItem.TargetSpeed.x,
                    _maxAirVelocityX,
                    _player.PlayerItem.Property.AirDamping * Time.fixedDeltaTime
                );
        }
        else
            _player.PlayerItem.TargetSpeed.x = Mathf.MoveTowards(
                _player.PlayerItem.TargetSpeed.x,
                0,
                _player.PlayerItem.Property.AirDamping / _targetAirDamping * Time.fixedDeltaTime
            );

        _player.Rb.linearVelocity = new Vector2(
            _player.PlayerItem.TargetSpeed.x,
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