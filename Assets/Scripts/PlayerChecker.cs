using System;
using UnityEngine;

public static class CheckerEvent
{
}

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] Player _player;

    [Header("Ground Check")]
    public bool IsGrounded;
    [SerializeField] Collider2D _groundCheckCollider;
    [SerializeField] LayerMask _groundLayer;

    [Header("Grapple Check")]
    public Collider2D GrappingLineCheck;
    [SerializeField] LayerMask _grappleLayer;

    void Update()
    {
        GroundCheck();
        GrapLineCheck();
    }

    void GroundCheck()
    {
        IsGrounded = !_player.IsJumping && _groundCheckCollider.IsTouchingLayers(_groundLayer);
    }

    void GrapLineCheck()
    {

    }
}