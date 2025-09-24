using System;
using UnityEngine;

public static class CheckerEvent
{
}

public class PlayerController_Checker : MonoBehaviour
{
    [SerializeField] PlayerController_Main _player;
    [Header("Ground Check")]
    public bool IsGrounded;
    [SerializeField] Transform _groundCheckPoint;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _groundCheckDist = 0.03f;
    [SerializeField] float _groundCheckWidth = 0.32f;

    [Header("Wall Check")]
    public bool WallDected;
    [SerializeField] Transform _wallCheckPoint;
    [SerializeField] LayerMask _wallLayer;
    [SerializeField] float _wallCheckDist = 0.1f;
    [SerializeField] float _wallCheckWidth = 0.32f;

    [Header("Grapple Check")]
    public Collider2D GLineChecker;
    public LayerMask GLineBreakLayer;

    void Update()
    {
        GroundCheck();
        WallCheck();
    }

    void GroundCheck()
    {
        // Add check points
        Vector2 leftPos = _groundCheckPoint.position - _groundCheckPoint.right * _groundCheckWidth / 2;
        Vector2 centerPos = _groundCheckPoint.position;
        Vector2 rightPos = _groundCheckPoint.position + _groundCheckPoint.right * _groundCheckWidth / 2;

        // Raycasts
        bool leftHit = Physics2D.Raycast(leftPos, Vector2.down, _groundCheckDist, _groundLayer);
        bool centerHit = Physics2D.Raycast(centerPos, Vector2.down, _groundCheckDist, _groundLayer);
        bool rightHit = Physics2D.Raycast(rightPos, Vector2.down, _groundCheckDist, _groundLayer);

        // Debug ray
        Debug.DrawRay(leftPos, Vector2.down * _groundCheckDist, Color.red);
        Debug.DrawRay(centerPos, Vector2.down * _groundCheckDist, Color.red);
        Debug.DrawRay(rightPos, Vector2.down * _groundCheckDist, Color.red);

        IsGrounded = leftHit || centerHit || rightHit;
    }

    void WallCheck()
    {
        Vector2 upperPos = _wallCheckPoint.position - _wallCheckPoint.up * _wallCheckWidth / 2;
        Vector2 centerPos = _wallCheckPoint.position;
        Vector2 lowerPos = _wallCheckPoint.position + _wallCheckPoint.up * _wallCheckWidth / 2;

        // Raycasts
        bool upperHit = Physics2D.Raycast(upperPos, Vector2.right, _wallCheckDist * _player.FacingDir, _wallLayer);
        bool centerHit = Physics2D.Raycast(centerPos, Vector2.right, _wallCheckDist * _player.FacingDir, _wallLayer);
        bool lowerHit = Physics2D.Raycast(lowerPos, Vector2.right, _wallCheckDist * _player.FacingDir, _wallLayer);

        // Debug ray
        Debug.DrawRay(upperPos, Vector2.right * _wallCheckDist, Color.red);
        Debug.DrawRay(centerPos, Vector2.right * _wallCheckDist, Color.red);
        Debug.DrawRay(lowerPos, Vector2.right * _wallCheckDist, Color.red);

        WallDected = upperHit || centerHit || lowerHit;
    }
}