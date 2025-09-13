using UnityEngine;
using Unity.Cinemachine;

public class PlayerController_Main : EntityContoller_Main
{
    [Header("NecessaryComponent")]
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
    [field: SerializeField] public Animator Anim { get; private set; }
    [field: SerializeField] public PlayerController_Checker Checker { get; private set; }
    [field: SerializeField] public PlayerInput InputSys { get; private set; }
    [field: SerializeField] public Camera MainCam { get; private set; }
    [field: SerializeField] public CinemachineCamera Cam { get; private set; }

    [Header("Controllers")]
    public PlayerController_Visual PlayerVisual;

    [Header("StateMark")]
    public int FacingDir = 1;
    public bool IsJumping = false;
    public bool IsAttached = false;
    public bool IsAttacking = false;

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

    #region Handle Skill Logics
    void HandleJumpStart()
    {
        _stateMachine.ChangeState(StateSO.JumpState, false);
        IsJumping = true;
    }
    void HandleJumpEnd()
    {
        _stateMachine.ChangeState(StateSO.AirState, true);
        IsJumping = false;
    }

    void HandleHookAtteched()
    {
        _stateMachine.ChangeState(StateSO.HookedState, true);
        IsAttached = true;
        Checker.GLineChecker.enabled = true;
    }
    void HandleHookReleased()
    {
        _stateMachine.ChangeState(StateSO.AirGlideState, true);
        IsAttached = false;
        Checker.GLineChecker.enabled = false;
    }

    void HandleAttackStart()
    {
        _stateMachine.ChangeState(StateSO.AttackState, false);
        IsAttacking = true;
    }
    void HandleAttackEnd()
    {
        _stateMachine.ChangeState(StateSO.FallState, true);
        IsAttacking = false;
    }
    #endregion
}