using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.StateMachineSystem
{
    public class StateMachine
    {
        Dictionary<Type, IState> _states;
        IState _currentState;
        public IState CurrentState => _currentState;
        
        public StateMachine()
        {
            _states = new Dictionary<Type, IState>();
        }
        public void Initialize<T>() where T : IState
        {
            ChangeState<T>();
        }
        public void RegisterState<T>(IState state) where T : IState
        {
            if (_states.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"State '{state}' is already registered.");
            else
                _states[typeof(T)] = state;
        }
        public void ChangeState<T>() where T : IState
        {
            Type targetType = typeof(T);

            if (_currentState?.GetType() == targetType) return;
                        
            if (_states.TryGetValue(targetType, out IState newState))
            {
                Debug.Log($"Exit:'{_currentState}' Enter:'{newState}'");
                var lastState = _currentState;
                string lastAnimName = (lastState as BaseState)?.AnimName;
                string newAnimName = (newState as BaseState)?.AnimName;

                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
                var StateChange = new StateChange()
                {
                    LastStateAnim = lastAnimName,
                    NeWStateAnim = newAnimName
                };
                EventBus.Publish(StateChange);
            }
            else
            {
                throw new ArgumentException($"State '{targetType.Name}' not found.");
            }
        }
    }
}