using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] Player _player;

    [Header("Ground Check")]
    public bool IsGrounded;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _groundCheckRadius;

    [Header("Edge Checker")]
    public Transform EdgeTransform;
    [SerializeField] Transform _edgeCheck;
    [SerializeField] LayerMask _edgeLayer;
    [SerializeField] float _edgeCheckRadius;


    void Update()
    {
        GroundCheck();
        EdgeCheck();
    }

    void GroundCheck()
    {
        IsGrounded = !_player.IsJumping &&
            Physics2D.OverlapCircle(
                _groundCheck.position,
                _groundCheckRadius,
                _groundLayer
            );
    }
    void EdgeCheck()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(
            _edgeCheck.position,
            _edgeCheckRadius,
            _edgeLayer
        );

        EdgeTransform = hitCollider != null ? hitCollider.transform : null;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);

        Gizmos.color = EdgeTransform? Color.yellow : Color.blue;
        Gizmos.DrawWireSphere(_edgeCheck.position, _edgeCheckRadius);
    }
}