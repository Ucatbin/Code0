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
    [SerializeField] HookPointPool _pool;

    [Header("GHookAttribute")]
    [field: SerializeField] public float MaxDetectDist { get; private set; } = 15f; // Maximum distance to detect grapple points
    [field: SerializeField] public float MaxLineDist { get; private set; }
    [field: SerializeField] public LayerMask CanHookLayer { get; private set; }      // Which layer can the hook attach to
    [SerializeField] float _lineMoveSpeed = 4.5f;
    [SerializeField] float _lineSwingForce = 20f;
    [SerializeField] float _maxSwingSpeed = 12f;
    [SerializeField] float _initPosDuration = 0.25f;
    [SerializeField] float _initLineDuration = 0.2f;

    public PlayerSkill_GrappingHook(PlayerController_Main player) : base(player) { }

    void Update()
    {
        TryUseSkill();
    }

    public override void TryUseSkill()
    {
        if (!IsReady ||
            CurrentCharges == 0 ||
            !_inputSys.GrapperTrigger ||
            _player.IsAttacking
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        CurrentCharges -= MaxCharges == -1 ? 0 : 1;
        IsReady = false;

        // Get mouse position and calculate fire direction
        RaycastHit2D hit = Physics2D.Raycast(
            _player.transform.position,
            _player.InputSys.MouseDir,
            MaxDetectDist,
            CanHookLayer
        );

        if (hit.collider != null)
        {
            // Set the hook
            HookPoint = _pool.Pool.Get();
            HookPoint.transform.position = hit.point;
            HookPoint.transform.parent = hit.transform;
            SurfaceNormal = hit.normal;
            AttachHook();
        }
        else
            ResetSkill();
    }
    public override void CoolDownSkill(float coolDown, string tag)
    {
        TimerManager.Instance.AddTimer(
            coolDown,
            () => { ResetSkill(); },
            tag
        );
    }
    public override void ResetSkill()
    {
        IsReady = true;
    }
    void AttachHook()
    {
        // Init variables
        _player.IsAddingForce = true;
        _player.Checker.GLineChecker.enabled = true;
        SetLineRenderer();

        // If isGrounded, player will move to the hook point first
        if (_player.Checker.IsGrounded && SurfaceNormal.y != 0)
        {
            Vector3 targetPos = new Vector2(HookPoint.transform.position.x, _player.transform.position.y);
            _player.Rb.AddForce((targetPos - _player.transform.position) * _initPosDuration, ForceMode2D.Impulse);
            StartCoroutine(MoveToTarget(_player.transform.position, targetPos));
        }
        else
        {
            if (Vector2.Distance(_player.transform.position, HookPoint.transform.position) > MaxLineDist)
                StartCoroutine(SetLineDist());
            else
            {
                SetJoint();
                SetLineRenderer();
                SkillEvents.TriggerHookAttach();
            }
        }
    }

    IEnumerator MoveToTarget(Vector2 startPos, Vector2 targetPos)
    {
        float elapsedTime = 0f;
        while (elapsedTime < _initPosDuration)
        {
            float t = elapsedTime / _initPosDuration;
            _player.PlayerRoot.position = Vector2.Lerp(startPos, targetPos, t);
            RopeLine.SetPosition(0, _player.transform.position);
            // break when cant get target position
            if (_player.Checker.GLineChecker.IsTouchingLayers(_player.Checker.GLineBreakLayer))
            {
                BreakGHook();
                yield break;
            }
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _player.PlayerRoot.position = targetPos;
        _player.Rb.AddForce((targetPos - startPos) * 1.5f, ForceMode2D.Impulse);

        if (Vector2.Distance(_player.transform.position, HookPoint.transform.position) > MaxLineDist)
            StartCoroutine(SetLineDist());
        else
        {
            SetJoint();
            SkillEvents.TriggerHookAttach();
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
            RopeLine.SetPosition(0, _player.transform.position);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        RopeJoint.distance = MaxLineDist;
        SkillEvents.TriggerHookAttach();
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
        _player.IsAttached = false;
        _player.IsAddingForce = false;

        _player.Checker.GLineChecker.enabled = false;

        // Disable distance joint and line renderer
        RopeJoint.enabled = false;
        RopeLine.enabled = false;
        _pool.Pool.Release(HookPoint);
    }
    public void MoveOnGLine()
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
    void SetLineRenderer()
    {
        RopeLine.SetPosition(0, _player.transform.position);
        RopeLine.SetPosition(1, HookPoint.transform.position);
        RopeLine.enabled = true;
    }
    public void CheckGLineBreak()
    {
        if (!_player.InputSys.GrapperTrigger && _player.IsAttached)
        {
            ReleaseGHook();
            return;
        }
        if (_player.Checker.GLineChecker.IsTouchingLayers(_player.Checker.GLineBreakLayer))
        {
            BreakGHook();
            return;
        }
    }
}