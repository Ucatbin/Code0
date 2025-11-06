using UnityEngine;
public abstract class EntityState
{
    // Necessary parameter
    protected EntityControllerOld _entity;
    protected StateMachineOld _stateMachine;
    protected string _stateName;
    public int _priority;

    public EntityState(EntityControllerOld entity, StateMachineOld stateMachine, int priority, string stateName)
    {
        _entity = entity;
        _stateMachine = stateMachine;
        _priority = priority;
        _stateName = stateName;
    }
    public abstract void Enter();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void Exit();
}