using UnityEngine;
using System;


public class EntityContoller_Main : MonoBehaviour, IMoveable, IDamageable
{
    [Header("NecessaryComponent")]
    protected StateMachine _stateMachine;
    [field: SerializeField] public Transform Root { get; private set; }
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
    [field: SerializeField] public Animator Anim { get; private set; }

    [Header("Handler")]
    public BuffHandler BuffHandler;

    [Header("Moveable")]
    [field: SerializeField] public Vector2 TargetSpeed { get; private set; }
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
    }

    public void TakeHeal()
    {
        throw new NotImplementedException();
    }

    public void Die()
    {
        throw new NotImplementedException();
    }
    #endregion
}