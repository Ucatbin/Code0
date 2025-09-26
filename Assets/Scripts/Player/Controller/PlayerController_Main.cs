using UnityEngine;
using Unity.Cinemachine;

public class PlayerController_Main : EntityContoller_Main
{
    [Header("NecessaryComponent")]
    [field: SerializeField] public PlayerController_Checker Checker { get; private set; }
    [field: SerializeField] public PlayerInput InputSys { get; private set; }
    [field: SerializeField] public Camera MainCam { get; private set; }
    [field: SerializeField] public CinemachineCamera Cam { get; private set; }

    [Header("Scriptable Object")]
    public PlayerPropertySO PropertySO;
    public PlayerStateSO StateSO;

    [Header("Controllers")]
    public PlayerController_Visual PlayerVisual;

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
        SkillEvents.OnWallJumpStart += HandleWallJumpStart;
        SkillEvents.OnJumpEnd += HandleJumpEnd;
        SkillEvents.OnHookAttach += HandleHookAtteched;
        SkillEvents.OnHookRelease += HandleHookReleased;
        SkillEvents.OnAttackStart += HandleAttackStart;
        SkillEvents.OnAttackEnd += HandleAttackEnd;
    }
    void OnDisable()
    {
        SkillEvents.OnJumpStart -= HandleJumpStart;
        SkillEvents.OnWallJumpStart -= HandleWallJumpStart;
        SkillEvents.OnJumpEnd -= HandleJumpEnd;
        SkillEvents.OnHookAttach -= HandleHookAtteched;
        SkillEvents.OnHookRelease -= HandleHookReleased;
        SkillEvents.OnAttackStart -= HandleAttackStart;
        SkillEvents.OnAttackEnd -= HandleAttackEnd;
    }

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
            ? FinalGroundSpeed * InputSys.MoveInput.x
            : FinalAirSpeed * InputSys.MoveInput.x;
        float rate = Rb.linearVelocityX <= Mathf.Abs(finalSpeed) ? accel : damping;
        float speedX;

        if (InputSys.MoveInput.x != 0)
            speedX = Mathf.MoveTowards(
                TargetSpeed.x,
                finalSpeed,
                rate
            );
        else
            speedX = Mathf.MoveTowards(
                TargetSpeed.x,
                0,
                damping
            );
        SetTargetSpeed(new Vector2(speedX, TargetSpeed.y));

        if (IsAddingForce)
            SetTargetSpeed(Rb.linearVelocity);
        else
            Rb.linearVelocity = new Vector2(TargetSpeed.x, Rb.linearVelocityY);
    }
    #region Handle Skill Logics
    void HandleJumpStart()
    {
        IsJumping = true;
        _stateMachine.ChangeState(StateSO.JumpState, false);
    }
    void HandleWallJumpStart()
    {
        IsJumping = true;
        _stateMachine.ChangeState(StateSO.WallJumpState, false);
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
}