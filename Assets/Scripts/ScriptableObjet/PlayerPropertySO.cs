using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttributeSO", menuName = "Game/Player/Attributes")]
public class PlayerPropertySO : ScriptableObject
{
    [Header("MOVEMENT")]
    public Vector2 TargetVelocity;
    [Tooltip("Max speed on ground")]
    public float MaxGroundSpeed = 12f;
    [Tooltip("Max speed can input add on ground")]
    public float MaxGroundMoveSpeed = 6f;
    [Tooltip("Acceration when move on the ground")]
    public float GroundAccel = 30f;
    [Tooltip("Damping when move on the ground")]
    public float GroundDamping = 25f;
    [Space(5)]
    [Tooltip("Max speed in air")]
    public float MaxAirSpeed = 20f;
    [Tooltip("Max speed in air")]
    public float MaxAirMoveSpeed = 8f;
    [Tooltip("Acceration when move in the air")]
    public float AirAccel = 40f;
    [Tooltip("Damping when move in the air")]
    public float AirDamping = 30f;

    [Header("JUMP")]
    [Tooltip("Max speed can input add to jumping")]
    public float MaxJumpSpeed = 12.5f;
    [Tooltip("Give player a initial speed")]
    public float JumpInitSpeed = 2f;
    [Tooltip("Acceration of jump speed")]
    public float JumpAccel = 0.65f;
    [Tooltip("Time window that can add force while holding SPACE"), Range(0f, 1f)]
    public float JumpInputWindow = 0.25f;
    [Tooltip("Time window that can jump after leave ground"), Range(0f, 1f)]
    public float CoyoteWindow = 0.15f;
    public float MaxRaiseSpeed = 20f;
    public float MaxFallSpeed = 25f;

    [Header("GRAVITY")]
    [Tooltip("Gravity on ground"), Range(0f, 10f)]
    public float DefaultGravity = 1f;
    [Tooltip("Gravity while rising"), Range(0f, 10f)]
    public float RiseGravity = 3f;
    [Tooltip("Max gravity while falling"), Range(0f, 10f)]
    public float FallGravity = 4.5f;
    [Tooltip("Gravity when attacking"), Range(0f, 10f)]
    public float AttackGravity = 0.4f;

    private void Reset()
    {
        TargetVelocity = Vector2.zero;
    }

    private void OnEnable()
    {
        TargetVelocity = Vector2.zero;
    }
}