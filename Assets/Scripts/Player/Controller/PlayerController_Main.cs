using UnityEngine;
using Unity.Cinemachine;

public class PlayerController_Main : EntityContoller_Main
{
    [Header("NecessaryComponent")]
    public PlayerInput InputSys;
    public Camera MainCam;
    public CinemachineCamera Cam;

    [Header("Scriptable Object")]
    public PlayerPropertySO PropertySO;
    public PlayerStateSO StateSO;

    [Header("Controllers")]
    public PlayerController_Visual Visual;

    [Header("StateMark")]
    public bool IsBusy = true;
    public bool IsJumping = false;
    public bool IsAttached = false;
    public bool IsLineDashing = false;
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
    }
    protected override void Update()
    {
        base.Update();
    }

    public override void HandleMovement()
    {
        // Jump and grapline are controlled by force and horizontal movement is controlled by velocity
        base.HandleMovement();
        
        float accel = Checker.IsGrounded ? PropertySO.GroundAccel : PropertySO.AirAccel;        // Decide to use ground accel or air accel
        float damping = Checker.IsGrounded ? PropertySO.GroundDamping : PropertySO.AirDamping;  // Decide to use ground damping or air damping
        float finalSpeed = Checker.IsGrounded                                                   // Decide to use ground max speed or air max final speed
            ? FinalGroundSpeed * InputSys.MoveInput.x
            : FinalAirSpeed * InputSys.MoveInput.x;
        float delta = Rb.linearVelocityX <= Mathf.Abs(finalSpeed) && InputSys.MoveInput.x != 0  // Decide to use accel or damping
            ? accel
            : damping;

        float speedX = Mathf.MoveTowards(                                                       // Calculate final target speed
            TargetSpeed.x,
            InputSys.MoveInput.x != 0 ? finalSpeed : 0,
            delta
        );
        SetTargetSpeed(new Vector2(speedX, TargetSpeed.y));                                     // Set final target speed

        if (IsAddingForce)
            SetTargetSpeed(Rb.linearVelocity);
        else
            Rb.linearVelocity = new Vector2(TargetSpeed.x, Rb.linearVelocityY);
    }
    #region Handle Skill Logics
    void HandleJumpStart()
    {
        IsJumping = true;
        bool sucess = _stateMachine.ChangeState(StateSO.JumpState, false);
        if (!sucess)
        {
            IsJumping = false;
            Player_SkillManager.Instance.Jump.ResetSkill();
        }
    }
    void HandleWallJumpStart()
    {
        IsJumping = true;
        bool sucess = _stateMachine.ChangeState(StateSO.WallJumpState, false);
        if (!sucess)
        {
            IsJumping = false;
            Player_SkillManager.Instance.Jump.ResetSkill();
        }
    }
    void HandleJumpEnd()
    {
        IsJumping = false;
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