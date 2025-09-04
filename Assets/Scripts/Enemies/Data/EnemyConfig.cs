using UnityEngine;

[CreateAssetMenu(menuName = "Game/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("移动")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;

    [Header("感知")]
    public float sightRange = 6f;
    [Range(0, 180)] public float sightHalfAngle = 60f;   // 视野夹角一半
    public LayerMask visionBlockMask;                    // 堵视线的层（墙）

    [Header("攻击")]
    public float attackRange = 1.2f;
    public float attackCooldown = 1.0f;

    [Header("飞行（可选）")]
    public float hoverBobAmplitude = 0.15f;
    public float hoverBobSpeed = 2.0f;
}
