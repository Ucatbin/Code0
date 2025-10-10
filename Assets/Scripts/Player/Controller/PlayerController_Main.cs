using UnityEngine;
using Unity.Cinemachine;

public class PlayerController_Main : EntityContoller_Main
{
    [Header("NecessaryComponent")]
    public PlayerInput InputSys;
    public Camera MainCam;
    public CinemachineCamera Cam;

    [Header("Datas")]
    public PlayerPropertySO PropertySO;
    public PlayerStateSO StateSO;

    [Header("StateMark")]
    public bool IsJumping = false;
    public bool IsAttached = false;
    public bool IsLineDashing = false;
    public bool IsAttacking = false;
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
    }
    protected override void Update()
    {
        base.Update();
    }

    public override void HandleMovement()
    {
        base.HandleMovement();

        if (IsPhysicsDriven || (InputSys.MoveInput.x == 0 && Rb.linearVelocityX == 0))
            return;

        float accel = Checker.IsGrounded ? PropertySO.GroundAccel : PropertySO.AirAccel;        // Decide to use ground accel or air accel
        float damping = Checker.IsGrounded ? PropertySO.GroundDamping : PropertySO.AirDamping;  // Decide to use ground damping or air damping
        float finalSpeed = Checker.IsGrounded                                                   // Decide to use ground max speed or air max final speed
            ? FinalGroundSpeed * InputSys.MoveInput.x
            : FinalAirSpeed * InputSys.MoveInput.x;
        float delta = Rb.linearVelocityX <= Mathf.Abs(finalSpeed) && InputSys.MoveInput.x != 0  // Decide to use accel or damping
            ? accel
            : damping;

        float frameVelocityX = Mathf.MoveTowards(                                                       // Calculate final target speed
            TargetVelocity.x,
            InputSys.MoveInput.x != 0 ? finalSpeed : 0,
            delta * Time.fixedDeltaTime
        );
        SetTargetVelocityX(frameVelocityX);
        ApplyMovement();
    }
    #region Handle Skill Logics
    void HandleJumpStart()
    {
        bool sucess = _stateMachine.ChangeState(StateSO.JumpState, false);
        if (!sucess)
            Player_SkillManager.Instance.Jump.ResetSkill();
    }
    void HandleWallJumpStart()
    {
        bool sucess = _stateMachine.ChangeState(StateSO.WallJumpState, false);
        if (!sucess)
            Player_SkillManager.Instance.Jump.ResetSkill();
    }
    void HandleJumpEnd()
    {
        _stateMachine.ChangeState(StateSO.AirState, true);
    }

    void HandleHookAtteched()
    {
        IsAttached = true;
        bool sucess = _stateMachine.ChangeState(StateSO.HookedState, false);
        if (!sucess)
        {
            IsAttached = false;
            Player_SkillManager.Instance.GrappingHook.ResetSkill();
        }
    }
    void HandleHookReleased()
    {
        IsAttached = false;
        _stateMachine.ChangeState(StateSO.AirGlideState, true);
    }

    void HandleAttackStart()
    {
        IsAttacking = true;
        bool sucess = _stateMachine.ChangeState(StateSO.AttackState, false);
        if (!sucess)
        {
            IsAttacking = false;
            Player_SkillManager.Instance.Attack.ResetSkill();
        }
    }
    void HandleAttackEnd()
    {
        _stateMachine.ChangeState(StateSO.FallState, true);
        IsAttacking = false;
    }
    #endregion
}