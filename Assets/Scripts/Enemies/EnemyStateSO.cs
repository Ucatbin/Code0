using UnityEngine;

[CreateAssetMenu]
public class EnemyStateSO : ScriptableObject
{
    [Header("STATEMACHINE")]
    public Enemy_IdleState IdleState { get; private set; }
    public Enemy_PatrolState PatrolState { get; private set; }

    [Header("STATES PRIORITY")]
    [Min(0)] public int IdlePriority = 1;
    [Min(0)] public int PatrolPriority = 1;

    public void InstanceState(EnemyController_Main enemy, StateMachineOld stateMachine)
    {
        IdleState = new Enemy_IdleState(enemy, stateMachine, IdlePriority, "Idle");
        PatrolState = new Enemy_PatrolState(enemy, stateMachine, PatrolPriority, "Move");
    }
}