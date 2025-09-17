using UnityEngine;
using System;

public class EntityContoller_Main : MonoBehaviour
{
    [Header("SO")]
    public PlayerPropertySO PropertySO;
    public PlayerStateSO StateSO;
    [Header("Handler")]
    public BuffHandler BuffHandler;
    
    protected StateMachine _stateMachine;

    protected virtual void Awake()
    {
        _stateMachine = new StateMachine();
    }
    protected virtual void Start()
    {

    }
    protected virtual void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }
    protected virtual void Update()
    {
        _stateMachine.CurrentState.LogicUpdate();
    }
}