using UnityEngine;
using System;
using UnityEngine.UIElements;
using Unity.Cinemachine;

public class PlayerController : EntityContoller
{
    [Header("StateMachine")]
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_AirState AirState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_HookedState HookedState { get; private set; }
    public Player_AirGlideState AirGlideState { get; private set; }
    public Player_AttackState AttackState { get; private set; }

    [Header("NecessaryComponent")]
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
    [field: SerializeField] public Animator Anim { get; private set; }
    [field: SerializeField] public PlayerChecker Checker { get; private set; }
    [field: SerializeField] public PlayerInput InputSys { get; private set; }
    [field: SerializeField] public Camera MainCam { get; private set; }
    [field: SerializeField] public CinemachineCamera Cam { get; private set; }
    [field: SerializeField] public Transform Visual { get; private set; }

    [Header("SO")]
    public PlayerAttributeSO AttributeSO;
    public PlayerStatePrioritySO StatePrioritySO;

    [Header("StateMark")]
    public bool IsJumping;
    public bool IsAttached;
    public bool IsAttacking;

    void OnEnable()
    {
        SkillEvents.OnHookAttach += HandleHookAtteched;
        SkillEvents.OnHookRelease += HandleHookReleased;
        SkillEvents.OnAttackStart += HandleAttackStart;
        SkillEvents.OnAttackEnd += HandleAttackEnd;
    }
    void OnDisable()
    {
        SkillEvents.OnHookAttach -= HandleHookAtteched;
        SkillEvents.OnHookRelease -= HandleHookReleased;
        SkillEvents.OnAttackStart -= HandleAttackStart;
        SkillEvents.OnAttackEnd -= HandleAttackEnd;
    }

    protected override void Awake()
    {
        base.Awake();

        // Initialize StateMachine
        IdleState = new Player_IdleState(this, _stateMachine, "Idle");
        MoveState = new Player_MoveState(this, _stateMachine, "Move");
        AirState = new Player_AirState(this, _stateMachine, "Idle");
        JumpState = new Player_JumpState(this, _stateMachine, "Jump");
        FallState = new Player_FallState(this, _stateMachine, "Fall");
        HookedState = new Player_HookedState(this, _stateMachine, "Hooked");
        AirGlideState = new Player_AirGlideState(this, _stateMachine, "AirGlide");
        AttackState = new Player_AttackState(this, _stateMachine, "Attack");

        _stateMachine.InitState(IdleState);
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

    public int GetStatePriority(Type stateType)
    {
        return StatePrioritySO?.GetPriority(stateType) ?? 1;
    }

    #region Handle Skill Logics
    void HandleHookAtteched()
    {
        _stateMachine.ChangeState(HookedState, true);
        IsAttached = true;
        Checker.GLineChecker.enabled = true;
    }
    void HandleHookReleased()
    {
        _stateMachine.ChangeState(AirGlideState, true);
        IsAttached = false;
        Checker.GLineChecker.enabled = false;
    }

    void HandleAttackStart()
    {
        _stateMachine.ChangeState(AttackState, false);
        IsAttacking = true;
    }
    void HandleAttackEnd()
    {
        _stateMachine.ChangeState(FallState, true);
        IsAttacking = false;
    }
    #endregion
}