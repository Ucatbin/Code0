using UnityEngine;
using System;
using UnityEngine.UIElements;

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
    public Rigidbody2D Rb;
    public PlayerChecker Checker;
    public PlayerInput InputSys;

    [Header("SO")]
    public PlayerAttributeSO AttributeSO;
    public PlayerStatePrioritySO StatePrioritySO;

    [Header("StateMark")]
    public bool IsJumping;      // Can player add force after jump
    public bool IsAttached;     // Is the grappling hook attached
    public bool IsAttacking;    // Is the player attacking

    void OnEnable()
    {
        GrappleEvent.OnHookAttached += HandleHookAtteched;
        GrappleEvent.OnHookReleased += HandleHookReleased;
    }
    void OnDisable()
    {
        GrappleEvent.OnHookAttached -= HandleHookAtteched;
        GrappleEvent.OnHookReleased -= HandleHookReleased;
    }

    protected override void Awake()
    {
        base.Awake();

        // Initialize StateMachine
        IdleState = new Player_IdleState(this, _stateMachine, "Idle");
        MoveState = new Player_MoveState(this, _stateMachine, "Move");
        AirState = new Player_AirState(this, _stateMachine, "InAir");
        JumpState = new Player_JumpState(this, _stateMachine, "Jump");
        FallState = new Player_FallState(this, _stateMachine, "Fall");
        HookedState = new Player_HookedState(this, _stateMachine, "Hooked");
        AirGlideState = new Player_AirGlideState(this, _stateMachine, "AirGlide");
        AttackState = new Player_AttackState(this, _stateMachine, "Attacking");

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

        HandleAttack();
    }

    public int GetStatePriority(Type stateType)
    {
        return StatePrioritySO?.GetPriority(stateType) ?? 1;
    }

    #region GrappingHook logics
    void HandleHookAtteched()
    {
        // TODO: More check conditions
        if (!IsAttacking)
        {
            _stateMachine.ChangeState(HookedState, true);
            IsAttached = true;
        }
    }
    void HandleHookReleased()
    {
        _stateMachine.ChangeState(AirState, true);
        Player_SkillManager.Instance.GrappingHook.CoolDownSkill();
    }
    #endregion

    #region Attack logics
    void HandleAttack()
    {
        if (!InputSys.AttackTrigger)
            return;

        if (Player_SkillManager.Instance.Attack.CanUseSkill)
        {
            Player_SkillManager.Instance.Attack.UseSkill();
            _stateMachine.ChangeState(AttackState, false);
        }
    }
    #endregion
}