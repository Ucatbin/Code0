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
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _groundCheckDist= 0.1f;
    [SerializeField] Transform _groundCheckPoint;
    [SerializeField] float groundCheckWidth = 0.4f;
    RaycastHit2D[] _groundHits = new RaycastHit2D[3];

    [Header("Grapple Check")]
    public Collider2D GLineChecker;
    public LayerMask GLineBreakLayer;

    void Update()
    {
        GroundCheck();
    }

    void GroundCheck()
    {
        // IsGrounded = !_player.IsJumping && _groundChecker.IsTouchingLayers(_groundLayer);

        // 计算左右两个检测点的位置
        Vector2 leftPos = _groundCheckPoint.position - _groundCheckPoint.right * groundCheckWidth / 2;
        Vector2 centerPos = _groundCheckPoint.position;
        Vector2 rightPos = _groundCheckPoint.position + _groundCheckPoint.right * groundCheckWidth / 2;

        // 绘制调试射线（仅在Scene视图可见）
        Debug.DrawRay(leftPos, Vector2.down * _groundCheckDist, Color.red);
        Debug.DrawRay(centerPos, Vector2.down * _groundCheckDist, Color.red);
        Debug.DrawRay(rightPos, Vector2.down * _groundCheckDist, Color.red);

        // 发射三条射线
        bool leftHit = Physics2D.Raycast(leftPos, Vector2.down, _groundCheckDist, _groundLayer);
        bool centerHit = Physics2D.Raycast(centerPos, Vector2.down, _groundCheckDist, _groundLayer);
        bool rightHit = Physics2D.Raycast(rightPos, Vector2.down, _groundCheckDist, _groundLayer);

        // 任意一条射线击中地面，则认为角色接地
        IsGrounded = leftHit || centerHit || rightHit;
    }
}