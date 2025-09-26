using UnityEngine;

[CreateAssetMenu]
public class EnemyPropertySO : ScriptableObject
{
    [Header("Health")]
    public int MaxHealth = 100;
    [Header("MOVEMENT")]
    public float MaxGroundMoveSpeed = 4f;
    public float MaxAirMoveSpeed = 4f;
    public float Accel = 1f;
    public float Damping = 2f;
    [Header("JUMP")]
    public float JumpForce = 10f;
}
