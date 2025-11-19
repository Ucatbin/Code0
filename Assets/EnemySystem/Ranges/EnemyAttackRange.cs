using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    public EnemyController enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            enemy.playerInAttackRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            enemy.playerInAttackRange = false;
        }
    }
}
