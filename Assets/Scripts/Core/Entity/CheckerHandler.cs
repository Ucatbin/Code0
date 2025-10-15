using System;
using UnityEngine;

public class CheckerHandler : MonoBehaviour, IGroundCheck, IWallCheck, ICeilingCheck
{
    [SerializeField] Character _entity;

    [Header("Ground Check")]
    [SerializeField] Transform _groundCheckPoint;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _groundCheckDist = 0.03f;
    [SerializeField] float _GroundCheckWidth = 0.32f;
    // Iterface
    [field:SerializeField] public bool IsGrounded { get; set; }
    public Transform GroundCheckPoint => _groundCheckPoint;
    public LayerMask GroundLayer => _groundLayer;
    public float GroundCheckDist => _groundCheckDist;
    public float GroundCheckWidth => _GroundCheckWidth;

    [Header("Wall Check")]
    [SerializeField] Transform _wallCheckPoint;
    [SerializeField] LayerMask _wallLayer;
    [SerializeField] float _wallCheckDist = 0.1f;
    [SerializeField] float _wallCheckWidth = 0.32f;
    // Interface
    [field:SerializeField] public bool IsWallDected { get; set; }
    public Transform WallCheckPoint => _wallCheckPoint;
    public LayerMask WallCheckLayer => _wallLayer;
    public float WallCheckDist => _wallCheckDist;
    public float WallCheckWidth => _wallCheckWidth;

    [Header("Ceiling Check")]
    [SerializeField] Transform _ceilingCheckPoint;
    [SerializeField] LayerMask _ceilingLayer;
    [SerializeField] float _ceilingCheckDist;
    // Interface
    [field:SerializeField] public bool IsCeilingDetected { get; set; }
    public Transform CeilingCheckPoint => _ceilingCheckPoint;
    public LayerMask CeilingLayer => _ceilingLayer;
    public float CeilingCheckDist => _ceilingCheckDist;

    void Update()
    {
        GroundCheck();
        WallCheck();
        CeilingCheck();
    }

    public virtual void GroundCheck()
    {
        if (_groundCheckPoint == null)
            return;

        // Add check points
        Vector2 leftPos = GroundCheckPoint.position - GroundCheckPoint.right * GroundCheckWidth / 2;
        Vector2 centerPos = GroundCheckPoint.position;
        Vector2 rightPos = GroundCheckPoint.position + GroundCheckPoint.right * GroundCheckWidth / 2;

        // Raycasts
        bool leftHit = Physics2D.Raycast(leftPos, Vector2.down, GroundCheckDist, GroundLayer);
        bool centerHit = Physics2D.Raycast(centerPos, Vector2.down, GroundCheckDist, GroundLayer);
        bool rightHit = Physics2D.Raycast(rightPos, Vector2.down, GroundCheckDist, GroundLayer);

        // Debug ray
        Debug.DrawRay(leftPos, Vector2.down * GroundCheckDist, Color.red);
        Debug.DrawRay(centerPos, Vector2.down * GroundCheckDist, Color.red);
        Debug.DrawRay(rightPos, Vector2.down * GroundCheckDist, Color.red);

        IsGrounded = leftHit || centerHit || rightHit;
    }

    public virtual void WallCheck()
    {
        if (_wallCheckPoint == null)
            return;

        // Add check points
        Vector2 centerPos = _wallCheckPoint.position;

        // Raycasts
        bool centerHit = Physics2D.Raycast(centerPos, Vector2.right, _wallCheckDist * _entity.FacingDir, _wallLayer);

        // Debug ray
        Debug.DrawRay(centerPos, Vector2.right * _wallCheckDist * _entity.FacingDir, Color.red);

        IsWallDected = centerHit;
    }

    public virtual void CeilingCheck()
    {
        if (_ceilingCheckPoint == null)
            return;

        // Add check points
        Vector2 centerPos = _ceilingCheckPoint.position;

        // Raycasts
        bool centerHit = Physics2D.Raycast(centerPos, Vector2.up, _ceilingCheckDist, _ceilingLayer);

        // Debug ray
        Debug.DrawRay(centerPos, Vector2.up * _ceilingCheckDist, Color.red);

        IsCeilingDetected = centerHit;
    }
}