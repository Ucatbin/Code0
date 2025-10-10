using UnityEngine;
using System;

public class EntityContoller_Main : MonoBehaviour, IMoveable, IDamageable
{
    [Header("NecessaryComponent")]
    public Transform Root;                  // This entity transform
    public CheckerController Checker;       // Entity collision checkers
    public Rigidbody2D Rb;                  // Rigibody
    public Animator Anim;                   // Animator
    public BuffHandler BuffHandler;         // Handle buff system
    protected StateMachine _stateMachine = new StateMachine();   // Statemachine

    [Header("Moveable")]
    [field: SerializeField] public Vector2 TargetVelocity { get; set; }
    public float BaseGroundSpeed { get; set; }
    public float BaseAirSpeed { get; set; }

    public float AccelMult { get; set; } = 1f;
    public float SpeedMult { get; set; } = 1f;
    public float SpeedBonus { get; set; } = 0f;

    [field:SerializeField] public float MaxFallSpeed { get; set; } = 10f;
    [field:SerializeField] public float InAirAccel { get; set; } = 110f;
    [field:SerializeField] public float FallMult { get; set; } = 3f;

    public float FinalGroundSpeed => (BaseGroundSpeed + SpeedBonus) * SpeedMult;
    public float FinalAirSpeed => (BaseAirSpeed + SpeedBonus) * SpeedMult;

    [Header("Damageable")]
    public int MaxHealth { get; set; }
    [field: SerializeField] public int CurrentHealth { get; set; }

    [Header("StateMark")]
    public bool IsBusy = false;
    public bool IsPhysicsDriven = false;
    public int FacingDir = 1;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

    }
    protected virtual void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();

        HandleMovement();
        HandleGravity();
    }
    protected virtual void Update()
    {
        _stateMachine.CurrentState.LogicUpdate();
    }

    #region IMoveable
    public virtual void HandleMovement() { }
    public void SetTargetVelocityX(float speedX) => TargetVelocity = new Vector2(speedX, Rb.linearVelocityY);
    public void SetTargetVelocityY(float speedY) => TargetVelocity = new Vector2(Rb.linearVelocityX, speedY);
    public void SetTargetVelocity(Vector2 speed) => TargetVelocity = speed;
    public void AddVelocity(float speed) => SpeedBonus += speed;
    public void MultVelocity(float multiplier) => SpeedMult *= multiplier;
    public void ApplyMovement()
    {
        if (IsPhysicsDriven)
        {
            SetTargetVelocityX(Rb.linearVelocityX);
            SetTargetVelocityY(Rb.linearVelocityY);
        }
        else
            Rb.linearVelocity = TargetVelocity;
    }
    public void HandleGravity()
    {
        if (IsPhysicsDriven) return;

        if (Checker.IsGrounded && TargetVelocity.y <= 0)
            SetTargetVelocityY(-0.5f);
        else
        {
            float inAirGravity = InAirAccel;
            if (Rb.linearVelocityY < 0)
                inAirGravity *= FallMult;
            float gravity = Mathf.MoveTowards(
                TargetVelocity.y,
                MaxFallSpeed * -1,
                inAirGravity * Time.fixedDeltaTime
            );
            SetTargetVelocityY(gravity);
        }
        ApplyMovement();  
    }
    #endregion

    #region  IDamageable
    public void TakeDamage(DamageData damageData)
    {
        Debug.Log($"{Root.name} take {damageData.DamageAmount} damage from {damageData.Caster}");
        CurrentHealth -= damageData.DamageAmount;
        if (CurrentHealth <= 0)
            Die();
    }
    public void TakeHeal()
    {
        throw new NotImplementedException();
    }
    public void Die()
    {
        Destroy(Root.gameObject);
    }
    #endregion
}