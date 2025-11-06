using UnityEngine;
using System;
using System.Collections.Generic;

public class EntityControllerOld : MonoBehaviour, IMoveable, IDamageable
{
    [Header("BaseComponents")]
    public Transform Root;                  // This entity transform
    public Rigidbody2D Rb;                  // Rigibody
    public Animator Anim;                   // Animator
    protected StateMachineOld _stateMachine = new StateMachineOld();   // Statemachine

    [Header("BaseHandlers")]
    public CheckerHandler CheckerSys;       // Entity collision checkers
    public BuffHandler BuffSys;             // Handle buff system

    [Header("BaseStateMarks")]
    public bool IsBusy = false;
    public bool IsPhysicsDriven = false;
    public int FacingDir = 1;

    protected virtual void Awake() {}
    protected virtual void Start() {}

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
    [Header("Moveable")]
    [field: SerializeField] public Vector2 TargetVelocity { get; set; }
    public float BaseGroundSpeed { get; set; }
    public float BaseAirSpeed { get; set; }

    public float AccelMult { get; set; } = 1f;
    public float SpeedMult { get; set; } = 1f;
    public float SpeedBonus { get; set; } = 0f;

    public float FinalGroundSpeed => (BaseGroundSpeed + SpeedBonus) * SpeedMult;
    public float FinalAirSpeed => (BaseAirSpeed + SpeedBonus) * SpeedMult;

    [field:SerializeField] public List<Transform> ShouldFlip { get; set; } = new List<Transform>();

    public void SetTargetVelocityX(float speedX) => TargetVelocity = new Vector2(speedX, Rb.linearVelocityY);
    public void SetTargetVelocityY(float speedY) => TargetVelocity = new Vector2(Rb.linearVelocityX, speedY);
    public void SetTargetVelocity(Vector2 speed) => TargetVelocity = speed;
    public void AddVelocity(float speed) => SpeedBonus += speed;
    public void MultVelocity(float multiplier) => SpeedMult *= multiplier;
    public void ApplyMovement()
    {
        if (IsPhysicsDriven)    // Update targetVelocity if drive by physics
            SetTargetVelocity(Rb.linearVelocity);
        else                    // Set velocity
            Rb.linearVelocity = TargetVelocity;

        // Handle flip
        if (Rb.linearVelocityX * FacingDir < 0)
            HandleFlip();

        // Prevent stuck on ceiling
        if (CheckerSys.IsCeilingDetected && Rb.linearVelocityY > 0)
        {
            Rb.linearVelocityY = 0;
            SetTargetVelocityY(0);
        }

        if (CheckerSys.IsWallDected && Rb.linearVelocityX * FacingDir > 0)
        {
            Rb.linearVelocityX = 0;
            SetTargetVelocityX(0);
        }

        var maxSpeed = CheckerSys.IsGrounded ? WorldManager.Instance.GlobalVar.MaxGroundSpeed : WorldManager.Instance.GlobalVar.MaxAirSpeed;
        if (Mathf.Abs(Rb.linearVelocityX) > maxSpeed)
        {
            Rb.linearVelocityX = maxSpeed * Rb.linearVelocityX > 0 ? 1 : -1;
            return;
        }
    }
    public virtual void HandleMovement()
    {
    }
    public void HandleGravity()
    {
        if (CheckerSys.IsGrounded && TargetVelocity.y <= 0)
            SetTargetVelocityY(-1.5f);
            
        if (IsPhysicsDriven) return;

        else
        {
            var globalGravity = WorldManager.Instance.GlobalVar;
            float inAirGravity = globalGravity.FallDamping;
            if (Rb.linearVelocityY < 0)
                inAirGravity *= globalGravity.FallMult;
            float gravity = Mathf.MoveTowards(
                TargetVelocity.y,
                globalGravity.MaxFallSpeed * -1,
                inAirGravity * Time.fixedDeltaTime
            );
            SetTargetVelocityY(gravity);
        }
        ApplyMovement();
    }

    public virtual void HandleFlip()
    {
        if (ShouldFlip != null)
            foreach (var f in ShouldFlip)
                f.Rotate(new Vector2(0f, 180f));
        FacingDir *= -1;
    }
    #endregion

    #region  IDamageable
    [Header("Damageable")]
    public int MaxHealth { get; set; }
    [field: SerializeField] public int CurrentHealth { get; set; }

    public void TakeDamage(DamageData damageData)
    {
        damageData.HandleHit();

        var buffsToProcess = BuffSys.BuffHeap;
        foreach (var buffInfo in buffsToProcess)
            buffInfo?.BuffData.OnBeHit?.Apply(buffInfo);

        Debug.Log($"{Root.name} take {damageData.DamageAmount} damage from {damageData.Caster}");
        CurrentHealth -= damageData.DamageAmount;
        if (CurrentHealth <= 0)
            BeKilled(damageData);
    }
    public void TakeHeal()
    {
        CurrentHealth += 1;
    }
    public void BeKilled(DamageData damageData)
    {
        damageData.HandleKill();

        var buffsToProcess = BuffSys.BuffHeap;
        foreach (var buffInfo in buffsToProcess)
            buffInfo.BuffData.OnBekill.Apply(buffInfo);         
        Destroy(Root.gameObject);
    }
    #endregion
}