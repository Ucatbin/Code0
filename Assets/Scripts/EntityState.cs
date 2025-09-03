using UnityEngine;
public abstract class EntityState
{
    // Necessary parameter
    protected EntityContoller _thisEntity;
    protected StateMachine _stateMachine;
    protected string _stateName;
    public int Priority;
    private PlayerController player;

    public EntityState(EntityContoller entity, StateMachine stateMachine, int priority, string stateName)
    {
        _thisEntity = entity;
        _stateMachine = stateMachine;
        Priority = priority;
        _stateName = stateName;
    }

    protected EntityState(PlayerController player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        _stateMachine = stateMachine;
        _stateName = stateName;
    }

    public abstract void Enter();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void Exit();
}