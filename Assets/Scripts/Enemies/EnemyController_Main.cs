using UnityEngine;

public class EnemyController_Main : EntityContoller_Main
{
    [Header("Scriptable Object")]
    public EnemyPropertySO PropertySO;
    public EnemyStateSO StateSO;

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
