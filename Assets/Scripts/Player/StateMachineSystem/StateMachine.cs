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
                    NewStateAnim = newAnimName
                };
                EventBus.Publish(StateChange);
            }
            else
            {
                throw new ArgumentException($"State '{targetType.Name}' not found.");
            }
        }
        public void ChangeState(Type stateType)
        {
            if (stateType == null)
                throw new ArgumentNullException(nameof(stateType));
            
            if (!typeof(IState).IsAssignableFrom(stateType))
                throw new ArgumentException($"Type {stateType.Name} must implement IState");

            if (_currentState?.GetType() == stateType) return;
                                
            if (_states.TryGetValue(stateType, out IState newState))
            {
                Debug.Log($"Exit:'{_currentState}' Enter:'{newState}'");
                var lastState = _currentState;
                string lastAnimName = (lastState as BaseState)?.AnimName;
                string newAnimName = (newState as BaseState)?.AnimName;

                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
                
                var stateChange = new StateChange()
                {
                    LastStateAnim = lastAnimName,
                    NewStateAnim = newAnimName
                };
                EventBus.Publish(stateChange);
            }
            else
            {
                throw new ArgumentException($"State '{stateType.Name}' not found in state dictionary.");
            }
        }
    }
}