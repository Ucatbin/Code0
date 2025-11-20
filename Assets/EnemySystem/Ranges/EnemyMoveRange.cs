using UnityEngine;

public class EnemyMoveRange : MonoBehaviour
{
    public EnemyController enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            enemy.playerOutOfRange = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            enemy.playerOutOfRange = true;
        }
    }
}
