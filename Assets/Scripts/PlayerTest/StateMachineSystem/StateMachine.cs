using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class StateMachine
    {
        public Dictionary<string, IState> States;
        IState _currentState;
        public IState CurrentState => _currentState;
        
        public void Initialize(IState startState)
        {
            ChangeState(startState);
        }
        public void RegisterState(string stateName, IState state)
        {
            if (States.ContainsKey(stateName))
                throw new InvalidOperationException($"State '{stateName}' is already registered.");
            else
                States.Add(stateName, state);
        }
        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}