using System;
using UnityEngine;

public static class CheckerEvent
{
    public static event Action OnGrappleStopped;

    public static void TriggerGrappleStopped() => OnGrappleStopped?.Invoke();
}

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] Player _player;

    [Header("Ground Check")]
    public bool IsGrounded;
    [SerializeField] Collider2D _groundCheckCollider;
    [SerializeField] LayerMask _groundLayer;

    [Header("Grapple Check")]
    public Collider2D GrappleCheckCollider;
    [SerializeField] LayerMask _grappleLayer;

    void Update()
    {
        GroundCheck();
        GrappleCheck();
    }

    void GroundCheck()
    {
        IsGrounded = !_player.IsJumping && _groundCheckCollider.IsTouchingLayers(_groundLayer);
    }

    void GrappleCheck()
    {
        if (_player.IsAttached && GrappleCheckCollider.IsTouchingLayers(_grappleLayer))
            CheckerEvent.TriggerGrappleStopped();
    }
}