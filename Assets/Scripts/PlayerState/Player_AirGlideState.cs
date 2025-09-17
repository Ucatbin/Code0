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

        float enterSpeed = _player.RTProperty.TargetSpeed.x;
        _targetAirDamping = enterSpeed * 2.5f;
        _player.IsJumping = false;
    }
    
    public override void PhysicsUpdate()
    {
        if (_player.InputSys.MoveInput.x != 0f)
        {
            if (Mathf.Abs(_player.RTProperty.TargetSpeed.x) <= _player.PropertySO.MaxAirSpeed)
                _player.RTProperty.TargetSpeed.x = Mathf.MoveTowards(
                    _player.RTProperty.TargetSpeed.x,
                    _player.RTProperty.FinalAirSpeed * _player.InputSys.MoveInput.x,
                    _player.PropertySO.AirAccel * Time.fixedDeltaTime
                );
            else
                _player.RTProperty.TargetSpeed.x = Mathf.MoveTowards(
                    _player.RTProperty.TargetSpeed.x,
                    _player.RTProperty.FinalAirSpeed * _player.InputSys.MoveInput.x,
                    _player.PropertySO.AirDamping * Time.fixedDeltaTime
                );
        }
        else
            _player.RTProperty.TargetSpeed.x = Mathf.MoveTowards(
                _player.RTProperty.TargetSpeed.x,
                0,
                _player.PropertySO.AirDamping / _targetAirDamping * Time.fixedDeltaTime
            );

        _player.Rb.linearVelocity = new Vector2(
            _player.RTProperty.TargetSpeed.x,
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