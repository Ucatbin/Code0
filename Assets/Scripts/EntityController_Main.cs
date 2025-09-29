using UnityEngine;
using System;

public class EntityContoller_Main : MonoBehaviour, IMoveable, IDamageable
{
    [Header("NecessaryComponent")]
    public CheckerController Checker;
    public Transform Root;
    public Rigidbody2D Rb;
    public Animator Anim;
    protected StateMachine _stateMachine;

    [Header("Handler")]
    public BuffHandler BuffHandler;

    [Header("Moveable")]
    [field: SerializeField] public Vector2 TargetSpeed { get; set; }
    public float BaseGroundSpeed { get; set; }
    public float BaseAirSpeed { get; set; }

    public float AccelMult { get; set; } = 1f;
    public float SpeedMult { get; set; } = 1f;
    public float SpeedBonus { get; set; } = 0f;

    public float FinalGroundSpeed => (BaseGroundSpeed + SpeedBonus) * SpeedMult;
    public float FinalAirSpeed => (BaseAirSpeed + SpeedBonus) * SpeedMult;

    [Header("Damageable")]
    public int MaxHealth { get; set; }
    [field: SerializeField] public int CurrentHealth { get; set; }

    [Header("StateMark")]
    public int FacingDir = 1;
    protected virtual void Awake()
    {
        _stateMachine = new StateMachine();
    }
    protected virtual void Start()
    {

    }
    protected virtual void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();

        HandleMovement();
    }
    protected virtual void Update()
    {
        _stateMachine.CurrentState.LogicUpdate();
    }

    #region IMoveable
    public void SetTargetSpeed(Vector2 speed) => TargetSpeed = speed;
    public void AddSpeed(float speed) => SpeedBonus += speed;
    public void MultSpeed(float multiplier) => SpeedMult *= multiplier;
    public virtual void HandleMovement() {}
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