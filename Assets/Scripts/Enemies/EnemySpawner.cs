using UnityEngine;

public class EnemySpawner : ObjectPool
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.childCount == 0)
            Pool.Get();
    }
    void FixedUpdate()
    {
        
    }
}
