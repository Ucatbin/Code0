using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPropertySO", menuName = "Game/Player/Property")]
public class PlayerPropertySO : ScriptableObject
{
    [Header("Health")]
    public int MaxHealth = 100;
    [Header("MOVEMENT")]
    [Tooltip("Max speed can input add on ground")]
    public float MaxGroundMoveSpeed = 10f;
    [Tooltip("Acceration when move on the ground")]
    public float GroundAccel = 60f;
    [Tooltip("Damping when move on the ground")]
    public float GroundDamping = 72f;
    public float WallSlideSpeed = 0.2f;
    [Space(5)]
    [Tooltip("Max speed in air")]
    public float MaxAirMoveSpeed = 10f;
    [Tooltip("Acceration when move in the air")]
    public float AirAccel = 40f;
    [Tooltip("Damping when move in the air")]
    public float AirDamping = 24f;
    public float AirGlideDamping = 4f;

    [Header("GRAVITY")]
    [Tooltip("Max gravity while gliding"), Range(0f, 10f)]
    public float AirGlideGravity = 4.5f;

}