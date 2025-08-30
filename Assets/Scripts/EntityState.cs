using UnityEngine;

public abstract class EntityState
{
    // Necessary parameter
    protected Entity _thisEntity;
    protected StateMachine _stateMachine;
    protected string _stateName;

    public EntityState(Entity entity, StateMachine stateMachine, string stateName)
    {
        _thisEntity = entity;
        _stateMachine = stateMachine;
        _stateName = stateName;
    }

    public abstract void Enter();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void Exit();
}