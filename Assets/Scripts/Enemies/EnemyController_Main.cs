using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController_Main : EntityContoller_Main
{
    [Header("Scriptable Object")]
    public EnemyPropertySO PropertySO;
    public EnemyStateSO StateSO;
    public Transform Checkers;

    [Header("StateMark")]
    public bool IsPatroling = false;

    protected override void Awake()
    {
        base.Awake();

        BaseGroundSpeed = PropertySO.MaxGroundMoveSpeed;
        BaseAirSpeed = PropertySO.MaxAirMoveSpeed;
        MaxHealth = PropertySO.MaxHealth;
        CurrentHealth = MaxHealth;

        StateSO.InstanceState(this, _stateMachine);

        _stateMachine.InitState(StateSO.IdleState);
    }

    public override void HandleMovement()
    {
        base.HandleMovement();
    }
}
