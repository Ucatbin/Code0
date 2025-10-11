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
    public bool IsCoyoting = false;
    public bool IsHooked = false;
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
    // JUMP
    void HandleJumpStart()
    {
        var jumpSkill = Player_SkillManager.Instance.Jump;
        bool sucess = _stateMachine.ChangeState(StateSO.JumpState, false);
        if (sucess)
        {
            IsJumping = true;
            IsBusy = true;

            jumpSkill.ConsumeSkill();
        }
        else
            Player_SkillManager.Instance.Jump.TryResetSkill();
    }
    void HandleWallJumpStart()
    {
        var jumpSkill = Player_SkillManager.Instance.Jump;
        bool sucess = _stateMachine.ChangeState(StateSO.WallJumpState, false);
        if (sucess)
        {
            IsJumping = true;
            IsBusy = true;

            jumpSkill.ConsumeSkill();
        }
        else
            Player_SkillManager.Instance.Jump.TryResetSkill();
    }
    void HandleJumpEnd()
    {
        if (!Checker.IsGrounded)
            _stateMachine.ChangeState(StateSO.AirState, true);
        else
            _stateMachine.ChangeState(StateSO.IdleState, false);

        IsJumping = false;
        IsBusy = false;
    }

    // GRAPPING HOOK
    void HandleHookAtteched()
    {
        var gHookSkill = Player_SkillManager.Instance.GrappingHook;
        bool sucess = _stateMachine.ChangeState(StateSO.HookedState, false);
        if (sucess)
        {
            IsHooked = true;
            IsBusy = true;
            IsPhysicsDriven = true;

            gHookSkill.AttachHook();
            gHookSkill.ConsumeSkill();
        }
        else
            Player_SkillManager.Instance.GrappingHook.TryResetSkill();

    }
    void HandleHookReleased()
    {
        _stateMachine.ChangeState(StateSO.AirGlideState, true);

        IsHooked = false;
        IsBusy = false;
    }

    // ATTACK
    void HandleAttackStart()
    {
        var attackSkill = Player_SkillManager.Instance.Attack;
        bool sucess = _stateMachine.ChangeState(StateSO.AttackState, false);
        if (sucess)
        {
            IsAttacking = true;
            IsBusy = true;

            attackSkill.ConsumeSkill();
        }
        else
            Player_SkillManager.Instance.Attack.TryResetSkill();
    }
    void HandleAttackEnd()
    {
        _stateMachine.ChangeState(StateSO.FallState, true);

        IsAttacking = false;
        IsBusy = false;
    }
    #endregion
}