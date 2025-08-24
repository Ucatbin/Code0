using System;
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

    [Header("关节设置")]
    public float jointDistance = 0.8f;
    public float jointDamping = 0.2f;
    public float jointFrequency = 4.5f;

    bool _isAttached;

    void Awake()
    {
        // Disable grapping hook when awake
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
    }

    void Update()
    {
        if (_player.InputSystem.GrapperTrigger && !_isAttached)
        {
            FireHook();
        }

        if (!_player.InputSystem.GrapperTrigger && _isAttached)
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
        _isAttached = true;

        _distanceJoint.connectedAnchor = _hookPoint;
        _distanceJoint.distance = jointDistance;
        _distanceJoint.enabled = true;

        _lineRenderer.SetPosition(0, _hookPoint);
        _lineRenderer.SetPosition(1, transform.position);
        _lineRenderer.enabled = true;
    }
    void ReleaseHook()
    {
        GrappleEvent.TriggerHookReleased();
        _isAttached = false;
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
    }
}