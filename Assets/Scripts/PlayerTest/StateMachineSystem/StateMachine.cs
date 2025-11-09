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
        
        public StateMachine()
        {
            States = new Dictionary<string, IState>();
        }
        public void Initialize(string startStateName)
        {
            ChangeState(startStateName);
        }
        public void RegisterState(string stateName, IState state)
        {
            if (States.ContainsKey(stateName))
                throw new InvalidOperationException($"State '{stateName}' is already registered.");
            else
                States.Add(stateName, state);
        }
        public void ChangeState(string stateName)
        {
            if (_currentState != null && _currentState.GetType().Name == stateName)
                return;
                
            if (States.TryGetValue(stateName, out IState newState))
            {
                Debug.Log($"Exit:'{_currentState}' Enter:'{newState}'");
                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
            }
            else
                throw new ArgumentException($"State '{stateName}' not found.");

        }
    }
}