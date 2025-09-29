using System;
using UnityEngine;

public class CheckerController : MonoBehaviour, IGroundCheck, IWallCheck
{
    [SerializeField] EntityContoller_Main _entity;

    [Header("Ground Check")]
    [SerializeField] Transform _groundCheckPoint;
    [SerializeField]LayerMask _groundLayer;
    [SerializeField]float _groundCheckDist = 0.03f;
    [SerializeField]float _GroundCheckWidth = 0.32f;
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
    [field:SerializeField] public bool WallDected { get; set; }
    public Transform WallCheckPoint => _wallCheckPoint;
    public LayerMask WallCheckLayer => _wallLayer;
    public float WallCheckDist => _wallCheckDist;
    public float WallCheckWidth => _wallCheckWidth;

    void Update()
    {
        GroundCheck();
        WallCheck();
    }

    public virtual void GroundCheck()
    {
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
        //Vector2 upperPos = _wallCheckPoint.position - _wallCheckPoint.up * _wallCheckWidth / 2;
        Vector2 centerPos = _wallCheckPoint.position;
        //Vector2 lowerPos = _wallCheckPoint.position + _wallCheckPoint.up * _wallCheckWidth / 2;

        // Raycasts
        //bool upperHit = Physics2D.Raycast(upperPos, Vector2.right, _wallCheckDist * _entity.FacingDir, _wallLayer);
        bool centerHit = Physics2D.Raycast(centerPos, Vector2.right, _wallCheckDist * _entity.FacingDir, _wallLayer);
        //bool lowerHit = Physics2D.Raycast(lowerPos, Vector2.right, _wallCheckDist * _entity.FacingDir, _wallLayer);

        // Debug ray
        //Debug.DrawRay(upperPos, Vector2.right * _wallCheckDist, Color.red);
        Debug.DrawRay(centerPos, Vector2.right * _wallCheckDist * _entity.FacingDir, Color.red);
        //Debug.DrawRay(lowerPos, Vector2.right * _wallCheckDist, Color.red);

        WallDected = centerHit;
        //WallDected = upperHit || centerHit || lowerHit;
    }
}