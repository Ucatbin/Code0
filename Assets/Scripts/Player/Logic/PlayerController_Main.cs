using TMPro;
using ThisGame.Events.AbilityEvents;
using UnityEngine;

public class PlayerController_Main : EntityController
{
    [Header("PlayerHandlers")]
    public InputHandler InputSys;

    [Header("PlayerComponents")]
    public Camera MainCam;
    public Collider2D Collider;
    public AnimationCurve GravityCurve;
    [SerializeField] TextMeshProUGUI _countDownDisplay;
    
    [Header("PlayerDatas")]
    public PlayerPropertySO PropertySO;
    public PlayerStateSO StateSO;

    [Header("PlayerStateMarks")]
    public bool IsJumping = false;
    public bool IsCoyoting = false;
    public bool IsHooked = false;
    public bool IsLineDashing = false;
    public bool IsAttacking = false;
    public bool IsWallSliding = false;

    void OnEnable()
    {
        SkillEvents.OnHookAttach += HandleHookAtteched;
        SkillEvents.OnHookRelease += HandleHookReleased;
    }
    void OnDisable()
    {
        SkillEvents.OnHookAttach -= HandleHookAtteched;
        SkillEvents.OnHookRelease -= HandleHookReleased;
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

        var Buff_CountDown = new BuffItem_CountDown(
            BuffManager.Instance.BuffData_CountDown,
            this,
            this,
            1,
            _countDownDisplay);
        BuffSys.AddBuff(Buff_CountDown);

        var eventBus = ServiceLocator.Get<IEventBus>();
        eventBus.Subscribe<JumpExecuteTriggerStart>(HandleJumpStart);
        eventBus.Subscribe<JumpExecuteTriggerEnd>(HandleJumpEnd);
        eventBus.Subscribe<Plr_AttackExecTriggerStart>(HandleAttackStart);
        eventBus.Subscribe<Plr_AttackExecTriggerEnd>(HandleAttackEnd);
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
        float accel = CheckerSys.IsGrounded ? PropertySO.GroundAccel : PropertySO.AirAccel;        // Decide to use ground accel or air accel
        float damping = CheckerSys.IsGrounded ? PropertySO.GroundDamping : PropertySO.AirDamping;  // Decide to use ground damping or air damping
        float finalSpeed = CheckerSys.IsGrounded                                                   // Decide to use ground max speed or air max final speed
            ? FinalGroundSpeed * InputSys.MoveInput.x
            : FinalAirSpeed * InputSys.MoveInput.x;
        float delta = Rb.linearVelocityX <= Mathf.Abs(finalSpeed) && InputSys.MoveInput.x != 0  // Decide to use accel or damping
            ? accel
            : damping;

        float frameVelocityX = Mathf.MoveTowards(                                               // Calculate final target speed
            TargetVelocity.x,
            InputSys.MoveInput.x != 0 ? finalSpeed : 0,
            delta * Time.fixedDeltaTime
        );
        SetTargetVelocityX(frameVelocityX);
        ApplyMovement();
    }
    #region Handle Skill Logics
    // JUMP
    void HandleJumpStart(JumpExecuteTriggerStart jumpEvent)
    {
        var jumpSkill = Player_SkillManager.Instance.Jump;
        _stateMachine.ChangeState(IsWallSliding ? StateSO.WallJumpState : StateSO.JumpState, false);
        IsJumping = true;
        IsBusy = true;
        jumpSkill.ConsumeSkill();
    }
    void HandleJumpEnd(JumpExecuteTriggerEnd jumpEvent)
    {
        if (!CheckerSys.IsGrounded)
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
    void HandleAttackStart(Plr_AttackExecTriggerStart AttackEvent)
    {
        var attackSkill = Player_SkillManager.Instance.Attack;
        _stateMachine.ChangeState(StateSO.AttackState, false);
        IsAttacking = true;
        IsBusy = true;
        attackSkill.ConsumeSkill();
    }
    void HandleAttackEnd(Plr_AttackExecTriggerEnd AttackEvent)
    {
        _stateMachine.ChangeState(StateSO.FallState, true);

        IsAttacking = false;
        IsBusy = false;
    }
    #endregion
}