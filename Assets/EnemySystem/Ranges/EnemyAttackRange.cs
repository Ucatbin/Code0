using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    public EnemyController enemy;

private void OnTriggerEnter2D(Collider2D collision)
    {
        // 使用Tag而不是Layer来检测玩家，避免攻击碰撞体干扰
        if (collision.CompareTag("Player"))
        {
            enemy.playerInAttackRange = true;
        }
    }

private void OnTriggerExit2D(Collider2D collision)
    {
        // 使用Tag而不是Layer来检测玩家，避免攻击碰撞体干扰
        if (collision.CompareTag("Player"))
        {
            enemy.playerInAttackRange = false;
        }
    }
}
