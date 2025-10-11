using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill_GrappingHook : PlayerSkill_BaseSkill
{
    [SerializeField] float BreakCoolDown = 2f;
    [Header("NecessaryComponent")]
    [field: SerializeField] public DistanceJoint2D RopeJoint { get; private set; }
    [field: SerializeField] public LineRenderer RopeLine { get; private set; }
    public GameObject HookPoint { get; private set; }   // The point where the hook is attached
    public Vector2 SurfaceNormal { get; private set; }     // The normal of the surface hit    
    public HookPointPool HookPool;

    [Header("GHookAttribute")]
    [field: SerializeField] public float MaxDetectDist { get; private set; } = 15f; // Maximum distance to detect grapple points
    [field: SerializeField] public float MaxLineDist { get; private set; } = 8f;
    [field: SerializeField] public LayerMask CanHookLayer { get; private set; }      // Which layer can the hook attach to
    [SerializeField] float _lineMoveSpeed = 4.5f;
    [SerializeField] float _lineSwingForce = 35f;
    [SerializeField] float _maxSwingSpeed = 12f;
    [SerializeField] float _initPosDuration = 0.25f;
    [SerializeField] float _initLineDuration = 0.2f;

    [Header("Checker")]
    public Collider2D GLineChecker;
    public LayerMask GLineBreakLayer;
    public bool IsHookFinished;

    public PlayerSkill_GrappingHook(PlayerController_Main player) : base(player) { }

    void OnEnable()
    {
        InputEvents.OnGHookPressed += TryUseSkill;
        InputEvents.OnGHookReleased += () =>
        {
            IsInputReset = true;
            CheckLineRelease();
        };
    }
    void OnDisable()
    {
        InputEvents.OnGHookPressed -= TryUseSkill;
        InputEvents.OnGHookReleased -= () =>
        {
            IsInputReset = true;
            CheckLineRelease();
        };
    }

    public override void TryUseSkill()
    {
        if (!_isReady ||
            !IsInputReset ||
            CurrentCharges == 0 ||
            _player.IsBusy
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            _player.transform.position,
            _player.InputSys.MouseDir,
            MaxDetectDist,
            CanHookLayer
        );

        if (hit.collider != null)
        {
            SetHookPoint(hit);
            SkillEvents.TriggerHookAttach();
        }
    }
    public override void CoolDownSkill(float coolDown, string tag)
    {
        TimerManager.Instance.AddTimer(
            coolDown,
            () => { CoolDownSkill(); },
            "PlayerSkillCD"
        );
    }
    public override void TryResetSkill()
    {
        HookPool.Pool.Release(HookPoint);
    }

    public void SetHookPoint(RaycastHit2D hit)
    {
        HookPoint = HookPool.Pool.Get();
        HookPoint.transform.position = hit.point;
        HookPoint.transform.parent = hit.transform;
        SurfaceNormal = hit.normal;
    }
    public void AttachHook()
    {
        IsHookFinished = false;
        RopeLine.enabled = true;
        GLineChecker.enabled = true;

        // If isGrounded, player will move to the hook point first
        if (_player.Checker.IsGrounded && SurfaceNormal.y != 0)
        {
            Vector3 targetPos = new Vector2(HookPoint.transform.position.x, _player.transform.position.y);
            StartCoroutine(MoveToTarget(_player.transform.position, targetPos));
        }
        else
        {
            if (Vector2.Distance(_player.transform.position, HookPoint.transform.position) > MaxLineDist)
                StartCoroutine(SetLineDist());
            else
            {
                SetJoint();
                IsHookFinished = true;
            }
        }
    }

    IEnumerator MoveToTarget(Vector2 startPos, Vector2 targetPos)
    {
        while (Vector2.Distance(targetPos, startPos) <= 0.2f)
        {
            _player.SetTargetVelocityX(_lineMoveSpeed * 1f);
            _player.ApplyMovement();
            SetLineRenderer();
            yield return null;
        }
        _player.Root.position = targetPos;
        SetLineRenderer();
        _player.Rb.AddForce((targetPos - startPos) * 1.5f, ForceMode2D.Impulse);
        GLineChecker.enabled = false;

        if (Vector2.Distance(_player.transform.position, HookPoint.transform.position) > MaxLineDist)
            StartCoroutine(SetLineDist());
        else
        {
            SetJoint();
            IsHookFinished = true;
        }
    }
    IEnumerator SetLineDist()
    {
        SetJoint();
        float elapsedTime = 0f;
        float startDist = RopeJoint.distance;
        while (elapsedTime <= _initLineDuration)
        {
            float t = elapsedTime / _initLineDuration;
            RopeJoint.distance = Mathf.Lerp(startDist, MaxLineDist, t);
            SetLineRenderer();

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        RopeJoint.distance = MaxLineDist;
        SetLineRenderer();
        IsHookFinished = true;
    }
    public void ReleaseGHook()
    {
        HandleDisable();
        CoolDownSkill(SkillCD, "PlayerSkill");
    }
    public void BreakGHook()
    {
        HandleDisable();
        CoolDownSkill(BreakCoolDown, "PlayerSkill");
    }
    void HandleDisable()
    {
        // Let state machine know the player is released
        SkillEvents.TriggerHookRelease();
        _player.IsHooked = false;
        _player.IsPhysicsDriven = false;

        // Disable distance joint and line renderer
        RopeJoint.enabled = false;
        RopeLine.enabled = false;
        HookPool.Pool.Release(HookPoint);
    }
    public void MoveOnLine()
    {
        float inputY = _player.InputSys.MoveInput.y;
        float inputX = _player.InputSys.MoveInput.x;
        if (inputY != 0)
            RopeJoint.distance -= _lineMoveSpeed * inputY * Time.fixedDeltaTime;
        if (inputX != 0 && Mathf.Abs(_player.Rb.linearVelocityX) <= _maxSwingSpeed)
            _player.Rb.AddForce(Vector2.right * inputX * _lineSwingForce);
    }
    void SetJoint()
    {
        RopeJoint.connectedBody = HookPoint.GetComponent<Rigidbody2D>();
        RopeJoint.distance = Vector2.Distance(_player.transform.position, HookPoint.transform.position);
        RopeJoint.enabled = true;
    }
    public void SetLineRenderer()
    {
        RopeLine.SetPosition(0, _player.transform.position);
        RopeLine.SetPosition(1, HookPoint.transform.position);
    }

    void CheckLineRelease()
    {
        if (_player.IsHooked && !_player.IsLineDashing)
            ReleaseGHook();
    }
    public void CheckLineBreak()
    {
        if (GLineChecker.IsTouchingLayers(GLineBreakLayer)
            && !_player.IsLineDashing
            )
            {
                BreakGHook();
                StopCoroutine("MoveToTarget");
            }
    }
}