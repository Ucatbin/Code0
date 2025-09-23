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
    public bool WallDected;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _groundCheckDist = 0.03f;
    [SerializeField] Transform _groundCheckPoint;
    [SerializeField] float groundCheckWidth = 0.32f;
    [SerializeField] Transform _wallCheckPoint;
    [SerializeField] float _wallCheckDist = 0.1f;
    [SerializeField] LayerMask _wallLayer;

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
        Vector2 leftPos = _groundCheckPoint.position - _groundCheckPoint.right * groundCheckWidth / 2;
        Vector2 centerPos = _groundCheckPoint.position;
        Vector2 rightPos = _groundCheckPoint.position + _groundCheckPoint.right * groundCheckWidth / 2;

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
        bool wallHit = Physics2D.Raycast(_wallCheckPoint.position, Vector2.right, _wallCheckDist * _player.FacingDir, _wallLayer);

        Debug.DrawRay(_wallCheckPoint.position, Vector2.right * _wallCheckDist * _player.FacingDir, Color.yellow);

        WallDected = wallHit;
    }
}