using UnityEngine;

public class Enemy_PatrolState : Enemy_GroundState
{
    public Enemy_PatrolState(EnemyController_Main entity, StateMachine stateMachine, int priority, string stateName) : base(entity, stateMachine, priority, stateName)
    {

    }
}