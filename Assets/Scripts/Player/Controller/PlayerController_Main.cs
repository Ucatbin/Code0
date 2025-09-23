using UnityEngine;
using Unity.Cinemachine;

public class PlayerController_Main : EntityContoller_Main, IDamageable
{
    [Header("NecessaryComponent")]
    [field: SerializeField] public Transform PlayerRoot { get; private set; }
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
    [field: SerializeField] public Animator Anim { get; private set; }
    [field: SerializeField] public PlayerController_Checker Checker { get; private set; }
    [field: SerializeField] public PlayerInput InputSys { get; private set; }
    [field: SerializeField] public Camera MainCam { get; private set; }
    [field: SerializeField] public CinemachineCamera Cam { get; private set; }

    [Header("Damageable")]
    public int CurrentHealth { get; private set; }
    public int MaxHealth => PropertySO.MaxHealth;

    [Header("Controllers")]
    public PlayerController_Visual PlayerVisual;
    public RTPropertyController RTProperty;

    [Header("StateMark")]
    public int FacingDir = 1;
    public bool IsJumping = false;
    public bool IsAttached = false;
    public bool IsAttacking = false;
    public bool IsAddingForce = false;
    public bool IsWallSliding = false;

    void OnEnable()
    {
        SkillEvents.OnJumpStart += HandleJumpStart;
        SkillEvents.OnJumpEnd += HandleJumpEnd;
        SkillEvents.OnHookAttach += HandleHookAtteched;
        SkillEvents.OnHookRelease += HandleHookReleased;
        SkillEvents.OnAttackStart += HandleAttackStart;
        SkillEvents.OnAttackEnd += HandleAttackEnd;
    }
    void OnDisable()
    {
        SkillEvents.OnJumpStart -= HandleJumpStart;
        SkillEvents.OnJumpEnd -= HandleJumpEnd;
        SkillEvents.OnHookAttach -= HandleHookAtteched;
        SkillEvents.OnHookRelease -= HandleHookReleased;
        SkillEvents.OnAttackStart -= HandleAttackStart;
        SkillEvents.OnAttackEnd -= HandleAttackEnd;
    }

    protected override void Awake()
    {
        base.Awake();

        StateSO.InstanceState(this, _stateMachine);

        _stateMachine.InitState(StateSO.IdleState);

        RTProperty.Init(PropertySO);

        CurrentHealth = MaxHealth;
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        HandleMovement();
    }
    protected override void Update()
    {
        base.Update();
    }

    void HandleMovement()
    {
        // Jump and grapline are controlled by force and horizontal movement is controlled by velocity
        float accel = Checker.IsGrounded ? PropertySO.GroundAccel : PropertySO.AirAccel;
        float damping = Checker.IsGrounded ? PropertySO.GroundDamping : PropertySO.AirDamping;
        float finalSpeed = Checker.IsGrounded
            ? RTProperty.FinalGroundSpeed * InputSys.MoveInput.x
            : RTProperty.FinalAirSpeed * InputSys.MoveInput.x;
        float rate = Rb.linearVelocityX <= Mathf.Abs(finalSpeed) ? accel : damping;

        if (InputSys.MoveInput.x != 0)
            RTProperty.TargetSpeed.x = Mathf.MoveTowards(
                RTProperty.TargetSpeed.x,
                finalSpeed,
                rate
            );
        else
            RTProperty.TargetSpeed.x = Mathf.MoveTowards(
                RTProperty.TargetSpeed.x,
                0,
                damping
            );

        if (IsAddingForce)
            RTProperty.TargetSpeed = Rb.linearVelocity;
        else
            Rb.linearVelocity = new Vector2(RTProperty.TargetSpeed.x, Rb.linearVelocityY);
    }

    #region Handle Skill Logics
    void HandleJumpStart()
    {
        IsJumping = true;
        _stateMachine.ChangeState(StateSO.JumpState, false);
    }
    void HandleJumpEnd()
    {
        IsJumping = false;
        _stateMachine.ChangeState(StateSO.AirState, true);
    }

    void HandleHookAtteched()
    {
        IsAttached = true;
        _stateMachine.ChangeState(StateSO.HookedState, true);
    }
    void HandleHookReleased()
    {
        IsAttached = false;
        _stateMachine.ChangeState(StateSO.AirGlideState, true);
    }

    void HandleAttackStart()
    {
        IsAttacking = true;
        _stateMachine.ChangeState(StateSO.AttackState, false);
    }
    void HandleAttackEnd()
    {
        IsAttacking = false;
        _stateMachine.ChangeState(StateSO.FallState, true);
    }

    #endregion
    #region DamageInfo
    public void TakeDamage(DamageData damageData)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void TakeHeal()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}