using UnityEngine;

public class Player_AirGlideState : Player_AirState
{
    public Player_AirGlideState(PlayerController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.Rb.gravityScale = _player.PropertySO.AirGlideGravity;
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

        _player.Rb.gravityScale = 0f;
        _player.IsPhysicsDriven = false;
    }
}