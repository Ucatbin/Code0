using UnityEngine;
using System;
using UnityEngine.UIElements;
using Unity.Cinemachine;

public class PlayerController : EntityContoller
{
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
    public PlayerStateSO StateSO;

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