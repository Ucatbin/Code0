using System;
using UnityEngine;

public class StateMachineOld
{
    public EntityState CurrentState;

    public void InitState(EntityState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    /// <summary>
    /// Change state and return whether state change is sucess
    /// </summary>
    public virtual bool ChangeState(EntityState nextState, bool forceChange)
    {
        if (!forceChange)
        {
            if (nextState._priority >= CurrentState._priority)
            {
                // Debug.Log($"{CurrentState._priority} Exit: {CurrentState} + {nextState._priority}Enter: {nextState}");
                CurrentState.Exit();
                CurrentState = nextState;
                CurrentState.Enter();
                return true;
            }
            else
                return false;
        }
        else
        {
            // Debug.Log($"Exit: {CurrentState} Enter: {nextState}");
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
            return true;
        }
    }
}
