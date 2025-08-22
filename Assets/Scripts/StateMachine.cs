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
        CurrentState.Exit();
        CurrentState = nextState;
        Debug.Log($"Enter: " + CurrentState);
        CurrentState.Enter();
    }
}
