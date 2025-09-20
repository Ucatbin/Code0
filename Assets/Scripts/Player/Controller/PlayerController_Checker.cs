using System;
using UnityEngine;

public static class CheckerEvent
{
}

public class PlayerController_Checker : MonoBehaviour
{
    [Header("Ground Check")]
    public bool IsGrounded;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _groundCheckDist= 0.03f;
    [SerializeField] Transform _groundCheckPoint;
    [SerializeField] float groundCheckWidth = 0.32f;

    [Header("Grapple Check")]
    public Collider2D GLineChecker;
    public LayerMask GLineBreakLayer;

    void Update()
    {
        GroundCheck();
    }

    void GroundCheck()
    {
        // Add check points
        Vector2 leftPos = _groundCheckPoint.position - _groundCheckPoint.right * groundCheckWidth / 2;
        Vector2 centerPos = _groundCheckPoint.position;
        Vector2 rightPos = _groundCheckPoint.position + _groundCheckPoint.right * groundCheckWidth / 2;

        // Debug ray
        Debug.DrawRay(leftPos, Vector2.down * _groundCheckDist, Color.red);
        Debug.DrawRay(centerPos, Vector2.down * _groundCheckDist, Color.red);
        Debug.DrawRay(rightPos, Vector2.down * _groundCheckDist, Color.red);

        // Raycasts
        bool leftHit = Physics2D.Raycast(leftPos, Vector2.down, _groundCheckDist, _groundLayer);
        bool centerHit = Physics2D.Raycast(centerPos, Vector2.down, _groundCheckDist, _groundLayer);
        bool rightHit = Physics2D.Raycast(rightPos, Vector2.down, _groundCheckDist, _groundLayer);

        IsGrounded = leftHit || centerHit || rightHit;
    }
}