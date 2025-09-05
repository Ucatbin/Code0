using UnityEngine;

public class StateMachine
{
    public EntityState CurrentState;

    public void InitState(EntityState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public virtual void ChangeState(EntityState nextState, bool force)
    {
        if (!force)
        {
            if (nextState.Priority >= CurrentState.Priority)
            {
                // Debug.Log($"{CurrentState.Priority} Exit: {CurrentState} + {nextState.Priority}Enter: {nextState}");
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
