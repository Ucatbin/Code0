using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController_Main : Character
{
    [Header("Scriptable Object")]
    public EnemyPropertySO PropertySO;
    public EnemyStateSO StateSO;

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

        int moveDir = IsPatroling ? FacingDir : 0;
        float finalSpeed = Checker.IsGrounded
            ? FinalGroundSpeed * moveDir
            : FinalAirSpeed * moveDir;
        float delta = Rb.linearVelocityX <= Mathf.Abs(finalSpeed) && moveDir != 0
            ? PropertySO.Accel
            : PropertySO.Damping;

        float speedX = Mathf.MoveTowards(
            TargetVelocity.x,
            moveDir != 0 ? finalSpeed : 0,
            delta
        );
        SetTargetVelocity(new Vector2(speedX, TargetVelocity.y));
        Rb.linearVelocity = new Vector2(TargetVelocity.x, Rb.linearVelocityY);
    }
}