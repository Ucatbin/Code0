using UnityEngine;

public class EnemyEyeSight : MonoBehaviour
{
    public EnemyController enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            enemy.findPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            enemy.findPlayer = false;
        }
    }
}
