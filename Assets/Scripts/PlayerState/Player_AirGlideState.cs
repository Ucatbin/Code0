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

        _player.Rb.gravityScale = _player.PropertySO.AirGlideGravity;
        float enterSpeed = _player.RTProperty.TargetSpeed.x;
        _targetAirDamping = enterSpeed * 2.5f;
        _player.IsJumping = false;
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
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