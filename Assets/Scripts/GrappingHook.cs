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
    [Header("Necessary Component")]
    public Player _player; // Reference to the player script
    public Camera MainCam;
    public LineRenderer LineRenderer;
    public DistanceJoint2D _distanceJoint;

    [Header("Grapper Attribute")]
    public float ScrollSpeed = 1f;
    public float MaxDetectDist = 20f; // Maximum distance to detect grapple points
    public Vector2 HookPoint; // The point where the hook is attached
    public LayerMask CanHookLayer; // Which layer can the hook attach to
    public float GrappleCD = 2f; // Cooldown time between grapples

    [Header("Other Component")]
    public bool CanUseGrapple = true;

    void Awake()
    {
        // Disable grapping hook when awake
        _distanceJoint.enabled = false;
        LineRenderer.enabled = false;
    }

    void Update()
    {
        // Press button when not attached and cooldown is over, shoot the hook
        if (_player.InputSystem.GrapperTrigger && !_player.IsAttached && CanUseGrapple)
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
        _distanceJoint.distance = Vector2.Distance(transform.position, HookPoint);
        _distanceJoint.connectedAnchor = HookPoint;
        _distanceJoint.enabled = true;

        // Setup line renderer
        LineRenderer.SetPosition(0, HookPoint);
        LineRenderer.SetPosition(1, transform.position);
        LineRenderer.enabled = true;
    }

    public IEnumerator CDTimer(float coolDown)
    {
        float timer = 0f;
        _player.GrappingHook.CanUseGrapple = false;
        while (timer < coolDown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _player.GrappingHook.CanUseGrapple = true;
    }
}