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
    [SerializeField] Collider2D _groundChecker;
    [SerializeField] LayerMask _groundLayer;

    [Header("Grapple Check")]
    public Collider2D GLineChecker;
    public LayerMask GLineBreakLayer;

    void Update()
    {
        GroundCheck();
    }

    void GroundCheck()
    {
        IsGrounded = !_player.IsJumping && _groundChecker.IsTouchingLayers(_groundLayer);
    }
}