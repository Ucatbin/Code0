using UnityEngine;

public class StateMachine
{
    public EntityState CurrentState;

    public void InitState(EntityState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(EntityState nextState, bool force)
    {
        if (nextState.Priority <= CurrentState.Priority && !force)
        {
            Debug.Log($"Exit: {CurrentState} Enter: {nextState}");
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }
        else
        {
            Debug.Log($"Exit: {CurrentState} Enter: {nextState}");
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }
    }
}
