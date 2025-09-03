using Unity.Mathematics;
using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultSO", menuName = "Game/Player/Attributes/Player")]
public class PlayerAttributeSO : EntityAttributeSO
{
    [Header("MOVEMENT")]
    [Tooltip("Move speed on ground"), Range(1f, 100f)]
    public float GroundMoveForce = 20f;
    [Tooltip("Move speed in air"), Range(1f, 100f)]
    public float AirMoveForce = 10f;
    [Tooltip("If speed already larger than max speed, can't add force")]
    public float MaxGroundSpeed = 8f;
    [Tooltip("If speed already larger than max speed, can't add force")]
    public float MaxAirSpeed = 10f;

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
}