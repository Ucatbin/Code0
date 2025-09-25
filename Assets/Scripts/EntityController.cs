using UnityEngine;
using System;

public class EntityContoller_Main : MonoBehaviour, IMoveable, IDamageable
{
    protected StateMachine _stateMachine;

    [Header("Handler")]
    public BuffHandler BuffHandler;

    [Header("Moveable")]
    public Vector2 TargetSpeed { get; private set; }
    public float BaseGroundSpeed { get; set; }
    public float BaseAirSpeed { get; set; }

    public float AccelMult { get; set; } = 1f;
    public float GroundSpeedMult { get; set; } = 1f;
    public float AirSpeedMult { get; set; } = 1f;
    public float GroundSpeedBonus { get; set; } = 0f;
    public float AirSpeedBonus { get; set; } = 0f;

    public float FinalGroundSpeed => (BaseGroundSpeed + GroundSpeedBonus) * GroundSpeedMult;
    public float FinalAirSpeed => (BaseAirSpeed + AirSpeedBonus) * AirSpeedMult;

    [Header("Damageable")]
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }


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
    public void SetTargetSpeed(Vector2 speed)
    {
        TargetSpeed = speed;
    }

    public void AddSpeed(float speed)
    {
        GroundSpeedBonus += speed;
        AirSpeedBonus += speed;
    }

    public void MultSpeed(float multiplier)
    {
        GroundSpeedMult *= multiplier;
        AirSpeedMult *= multiplier;
    }
    #endregion

    #region  IDamageable
    public void TakeDamage(DamageData damageData)
    {
        throw new NotImplementedException();
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