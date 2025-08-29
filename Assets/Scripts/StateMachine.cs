using UnityEngine;

public class StateMachine
{
    public EntityState CurrentState;

    public void InitState(EntityState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(EntityState nextState)
    {
        Debug.Log($"Exit: {CurrentState} Enter: {nextState}");
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
}
