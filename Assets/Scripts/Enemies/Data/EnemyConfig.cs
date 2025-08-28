using UnityEngine;

[CreateAssetMenu(menuName = "Game/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("�ƶ�")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;

    [Header("��֪")]
    public float sightRange = 6f;
    [Range(0, 180)] public float sightHalfAngle = 60f;   // ��Ұ�н�һ��
    public LayerMask visionBlockMask;                    // �����ߵĲ㣨ǽ��

    [Header("����")]
    public float attackRange = 1.2f;
    public float attackCooldown = 1.0f;

    [Header("���У���ѡ��")]
    public float hoverBobAmplitude = 0.15f;
    public float hoverBobSpeed = 2.0f;
}
