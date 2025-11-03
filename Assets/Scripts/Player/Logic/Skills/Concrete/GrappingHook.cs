using System.Collections;
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
    [SerializeField] float _lineInitSpeedMult = 100f;   // Force add per frame when move to target hookpoint
    [SerializeField] float _initLineLengthDur = 0.2f;         // The duration of setting the line length
    [SerializeField] float _lineStretchSpeed = 4.5f;       // Speed when move on the line
    [SerializeField] float _lineSwingForce = 35f;
    [SerializeField] float _maxSwingSpeed = 12f;

    [Header("Checker")]
    public Collider2D GLineChecker;
    public LayerMask GLineBreakLayer;
    public bool IsHookFinished;

    [Header("Other")]
    bool _releaseEarly = false;

    public PlayerSkill_GrappingHook(PlayerController_Main player) : base(player) { }

    void OnEnable()
    {
        PlayerInputEvents.OnGHookPressed += TryUseSkill;
        PlayerInputEvents.OnGHookReleased += () =>
        {
            IsInputReset = true;
            CheckLineRelease();
        };
    }
    void OnDisable()
    {
        PlayerInputEvents.OnGHookPressed -= TryUseSkill;
        PlayerInputEvents.OnGHookReleased -= () =>
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

    #region Init
    public void AttachHook()
    {
        IsHookFinished = false;
        RopeLine.enabled = true;
        GLineChecker.enabled = true;
        _player.Rb.gravityScale = 4.5f;

        // If isGrounded, player will move to the hook point first
        if (_player.CheckerSys.IsGrounded && SurfaceNormal.y != 0)
        {
            Vector3 targetPos = new Vector2(HookPoint.transform.position.x, _player.transform.position.y);
            StartCoroutine(MoveToTarget(_player.transform.position, targetPos));
        }
        else
        {
            SetJoint();
            if (Vector2.Distance(_player.transform.position, HookPoint.transform.position) > MaxLineDist)
                StartCoroutine(SetLineDist());
            else
            {
                IsHookFinished = true;
                Player_SkillManager.Instance.Jump.CurrentCharges -= 1;
            }
        }
    }
    IEnumerator MoveToTarget(Vector2 startPos, Vector2 targetPos)
    {
        bool isFromLeft = startPos.x < targetPos.x;
        while (Mathf.Abs(_player.transform.position.x - targetPos.x) >= 0.01f)
        {
            bool isLeft = _player.transform.position.x <= targetPos.x;
            if (isLeft != isFromLeft)
                break;

            _player.Rb.AddForce(
                _lineStretchSpeed * (targetPos - startPos).normalized * _lineInitSpeedMult,
                ForceMode2D.Force
                );

            if (_releaseEarly)
            {
                _releaseEarly = false;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
        if (_player.CheckerSys.IsGrounded)
            _player.Root.position = targetPos;
        _player.Rb.linearVelocity *= 0.5f;
        GLineChecker.enabled = false;

        SetJoint();
        if (Vector2.Distance(_player.transform.position, HookPoint.transform.position) > MaxLineDist)
            StartCoroutine(SetLineDist());
        else
        {
            IsHookFinished = true;
            Player_SkillManager.Instance.Jump.CurrentCharges -= 1;
        }
    }
    IEnumerator SetLineDist()
    {
        float elapsedTime = 0f;
        float startDist = RopeJoint.distance;
        while (Mathf.Abs(RopeJoint.distance - MaxLineDist) > 0.05f)
        {
            float t = elapsedTime / _initLineLengthDur;
            RopeJoint.distance = Mathf.Lerp(startDist, MaxLineDist, t);

            elapsedTime += Time.deltaTime;

            if (_releaseEarly)
            {
                _releaseEarly = false;
                yield break;
            }
            yield return null;
        }
        RopeJoint.distance = MaxLineDist;
        IsHookFinished = true;
        Player_SkillManager.Instance.Jump.CurrentCharges -= 1;
    }
    #endregion
    #region Setup components
    public void SetHookPoint(RaycastHit2D hit)
    {
        HookPoint = HookPool.Pool.Get();
        HookPoint.transform.position = hit.point;
        HookPoint.transform.parent = hit.transform;
        SurfaceNormal = hit.normal;
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
    #endregion
    #region Release and break
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
            if (!IsHookFinished)
                _releaseEarly = true;
            BreakGHook();
        }
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
        SkillEvents.TriggerHookRelease();

        HookPool.Pool.Release(HookPoint);
        RopeJoint.enabled = false;
        RopeLine.enabled = false;
    }
    # endregion

    public void MoveOnLine()
    {
        float inputY = _player.InputSys.MoveInput.y;
        float inputX = _player.InputSys.MoveInput.x;
        if (inputY != 0)
            RopeJoint.distance -= _lineStretchSpeed * inputY * Time.fixedDeltaTime;
        if (Mathf.Abs(_player.Rb.linearVelocity.magnitude) <= _maxSwingSpeed &&
            inputX * _player.Rb.linearVelocityX > 0
        )
            _player.Rb.AddForce(Vector2.right * inputX * _lineSwingForce);
    }
}