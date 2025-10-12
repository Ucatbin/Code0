using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttributeSO", menuName = "Game/Player/Attributes")]
public class PlayerPropertySO : ScriptableObject
{
    [Header("Health")]
    public int MaxHealth = 100;
    [Header("MOVEMENT")]
    [Tooltip("Max speed can input add on ground")]
    public float MaxGroundMoveSpeed = 6f;
    [Tooltip("Acceration when move on the ground")]
    public float GroundAccel = 1f;
    [Tooltip("Damping when move on the ground")]
    public float GroundDamping = 2f;
    [Space(5)]
    [Tooltip("Max speed in air")]
    public float MaxAirMoveSpeed = 8f;
    [Tooltip("Acceration when move in the air")]
    public float AirAccel = 0.8f;
    [Tooltip("Damping when move in the air")]
    public float AirDamping = 0.18f;

    [Header("JUMPSKILL")]
    [Tooltip("Give player a initial force")]
    public float JumpInitPower = 10f;
    [Tooltip("Acceration of jump force")]
    public float JumpHoldSpeed = 10f;
    [Tooltip("Time window that can add force while holding SPACE"), Range(0f, 1f)]
    public float JumpInputWindow = 0.25f;
    public float WallSlideSpeed = 0.2f;
    public float WallJumpPower = 10f;
    public float WallJumpWindow = 0.4f;
    public Vector2 WallJumpDir = new Vector2(0.5f, 1f);
    [Tooltip("Time window that can jump after leave ground"), Range(0f, 1f)]
    public float CoyoteWindow = 0.15f;

    [Header("GRAVITY")]
    [Tooltip("Max gravity while gliding"), Range(0f, 10f)]
    public float AirGlideGravity = 4.5f;
}