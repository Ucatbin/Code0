using Unity.Mathematics;
using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultSO", menuName = "Game/Player/Attributes/Player")]
public class PlayerAttributeSO : EntityAttributeSO
{
    [Header("MOVEMENT")]
    public Vector2 TargetVelocity;
    [Tooltip("Max speed on ground")]
    public float MaxGroundSpeed = 10f;
    [Tooltip("Max speed can input add on ground")]
    public float MaxGroundMoveSpeed = 8f;
    [Tooltip("Acceration when move on the ground"), Range(0f, 100f)]
    public float GroundAccel = 10f;
    [Tooltip("Damping when move on the ground"), Range(0f, 100f)]
    public float GroundDamping = 15f;
    [Space(5)]
    [Tooltip("Max speed in air")]
    public float MaxAirSpeed = 20f;
    [Tooltip("Max speed in air")]
    public float MaxAirMoveSpeed = 10f;
    [Tooltip("Move speed can input add in air"), Range(0f, 100f)]
    public float AirAccel = 8f;
    [Tooltip("Damping when move on the ground"), Range(0f, 100f)]
    public float AirDamping = 10f;

    [Header("JUMP")]
    [Tooltip("The height that player can reach")]
    public float JumpHeight = 2.5f;
    [Tooltip("The force add each frame while holding SPACE")]
    public float JumpHoldForce = 5f;
    [Tooltip("Time window that can add force while holding SPACE")]
    public float JumpWindow = 0.3f;
    [Tooltip("Time delay to add additional force")]
    public float JumpDelay = 0.06f;

    [Header("AIR GLIDE")]
    [Tooltip("Speed trashold of min air glide gravity")]
    public float MinGravityTrashold = 30f;
    [Tooltip("Speed trashold of air glide")]
    public float AirGlideThreshold = 16f;

    [Header("GRAVITY")]
    [Tooltip("Gravity on ground"), Range(0f, 10f)]
    public float DefaultGravity = 1f;
    [Tooltip("Gravity while rising"), Range(0f, 10f)]
    public float RiseGravity = 3f;
    [Tooltip("Min gravity while falling"), Range(0f, 10f)]
    public float MinFallGravity = 1f;
    [Tooltip("Max gravity while falling"), Range(0f, 10f)]
    public float MaxFallGravity = 4.5f;
    [Tooltip("Gravity when attacking"), Range(0f, 10f)]
    public float AttackGravity = 0.4f;
}