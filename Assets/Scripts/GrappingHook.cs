using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class GrappleEvent
{
    public static event Action OnHookAttached;
    public static event Action OnHookReleased;

    public static void TriggerHookAttached() => OnHookAttached?.Invoke();
    public static void TriggerHookReleased() => OnHookReleased?.Invoke();
}

public class GrappingHook : MonoBehaviour
{
    [Header("Necessary Component")]
    public Player _player;
    [SerializeField] Camera _mainCam;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] DistanceJoint2D _distanceJoint;

    [Header("Grapper Attribute")]
    [SerializeField] float _maxDetectDist = 20f;
    Vector2 _hookPoint;
    [SerializeField] LayerMask _hookLayer;
    [SerializeField] float _grappleCD = 5f;

    float _timer;

    void OnEnable()
    {
        CheckerEvent.OnGrappleStopped += ReleaseHook;
    }
    void OnDisable()
    {
        CheckerEvent.OnGrappleStopped -= ReleaseHook;
    }

    void Awake()
    {
        // Disable grapping hook when awake
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
    }

    void FixedUpdate()
    {
        if (!_player.IsAttached)
            return;
        // 检测从玩家到钩点之间是否有地面层障碍物
        RaycastHit2D[] hit = Physics2D.RaycastAll(
            transform.position,
            _hookPoint - (Vector2)transform.position,
            _distanceJoint.distance,
            _hookLayer
        );
        // 如果检测到障碍物，断开连接
        if (hit.Count() > 2)
        {
            ReleaseHook();
        }
    }
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_player.InputSystem.GrapperTrigger && !_player.IsAttached && _timer < 0f)
        {
            FireHook();
        }

        if (!_player.InputSystem.GrapperTrigger && _player.IsAttached)
        {
            ReleaseHook();
        }

        // Update line renderer position
        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }
    void FireHook()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDir = (mousePos - (Vector2)transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            fireDir,
            _maxDetectDist,
            _hookLayer
        );

        if (hit.collider != null)
        {
            _hookPoint = hit.point;
            AttachHook();
        }
    }
    void AttachHook()
    {
        GrappleEvent.TriggerHookAttached();
        _player.IsAttached = true;

        _distanceJoint.connectedAnchor = _hookPoint;
        _distanceJoint.enabled = true;

        _lineRenderer.SetPosition(0, _hookPoint);
        _lineRenderer.SetPosition(1, transform.position);
        _lineRenderer.enabled = true;
    }
    void ReleaseHook()
    {
        GrappleEvent.TriggerHookReleased();
        _player.IsAttached = false;
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
        _timer = _grappleCD;
    }
}