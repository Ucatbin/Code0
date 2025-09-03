using UnityEngine;
using System;

public class EntityContoller : MonoBehaviour
{
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