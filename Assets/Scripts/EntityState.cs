using UnityEngine;
public abstract class EntityState
{
    // Necessary parameter
    protected EntityContoller _entity;
    protected StateMachine _stateMachine;
    protected string _stateName;
    public int Priority;

    public EntityState(EntityContoller entity, StateMachine stateMachine, string stateName)
    {
        _entity = entity;
        _stateMachine = stateMachine;
        _stateName = stateName;
    }
    public abstract void Enter();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void Exit();
}