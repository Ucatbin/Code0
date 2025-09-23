using UnityEngine;

public class StateMachine
{
    public EntityState CurrentState;

    public void InitState(EntityState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public virtual void ChangeState(EntityState nextState, bool forceChange)
    {
        if (!forceChange)
        {
            if (nextState._priority >= CurrentState._priority)
            {
                // Debug.Log($"{CurrentState._priority} Exit: {CurrentState} + {nextState._priority}Enter: {nextState}");
                CurrentState.Exit();
                CurrentState = nextState;
                CurrentState.Enter();
            }
            else return;
        }
        else
        {
            // Debug.Log($"Exit: {CurrentState} Enter: {nextState}");
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }
    }
}
