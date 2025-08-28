using System;
using System.Collections;
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
    [Header("NecessaryComponent")]
    public Player _player; // Reference to the player script
    public Camera MainCam;
    public LineRenderer LineRenderer;
    public DistanceJoint2D DistanceJoint;

    [Header("GHookAttribute")]
    public float MaxDetectDist = 20f; // Maximum distance to detect grapple points
    public LayerMask CanHookLayer; // Which layer can the hook attach to
    public float GrappleCD = 1f; // Cooldown time between grapples
    public float GLineDashCD = 1f; // Cooldown time between grappling line dashes
    [Header("GLineControlAttribute")]
    public float GLineSpeed = 1f;
    public float GLineMaxSpeed = 2f; // Maximum speed to change the length of the grappling line
    public float GLineAcceleration = 1.5f; // Acceleration when holding shift
    public float GLineDamping = 3f;

    [Header("OtherComponent")]
    [HideInInspector] public Vector2 HookPoint; // The point where the hook is attached
    [HideInInspector] public bool CanUseGHook = true;
    [HideInInspector] public bool CanUseGLineDash = true;

    void Awake()
    {
        // Disable grapping hook when awake
        DistanceJoint.enabled = false;
        LineRenderer.enabled = false;
    }

    void Update()
    {
        // Press button when not attached and cooldown is over, shoot the hook
        if (_player.InputSystem.GrapperTrigger && !_player.IsAttached && CanUseGHook)
        {
            FireHook();
        }

    }
    void FireHook()
    {
        // Get mouse position and calculate fire direction
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDir = (mousePos - (Vector2)transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            fireDir,
            MaxDetectDist,
            CanHookLayer
        );

        if (hit.collider != null)
        {
            HookPoint = hit.point;
            AttachHook();
        }
    }
    void AttachHook()
    {
        // Let state machine know the player is attached
        GrappleEvent.TriggerHookAttached();
        _player.IsAttached = true;

        // Set connect point and enable distance joint
        DistanceJoint.distance = Vector2.Distance(transform.position, HookPoint);
        DistanceJoint.connectedAnchor = HookPoint;
        DistanceJoint.enabled = true;

        // Setup line renderer
        LineRenderer.SetPosition(0, HookPoint);
        LineRenderer.SetPosition(1, transform.position);
        LineRenderer.enabled = true;
    }

    public IEnumerator GHookCDTimer(float coolDown)
    {
        float timer = 0f;
        CanUseGHook = false;
        while (timer < coolDown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        CanUseGHook = true;
    }
    public IEnumerator DashCDTimer(float coolDown)
    {
        float timer = 0f;
        CanUseGLineDash = false;
        while (timer < coolDown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        CanUseGLineDash = true;
    }
}