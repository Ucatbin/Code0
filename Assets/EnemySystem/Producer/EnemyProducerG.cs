using System.Collections.Generic;
using UnityEngine;

public class EnemyProducerG : MonoBehaviour
{
    public List<GameObject> prefabs;
    public float spawnInterval = 5f;

    private float timer;
    private BoxCollider2D box;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnPrefab();
        }
    }

    void SpawnPrefab()
    {
        if (prefabs == null || prefabs.Count == 0)
        {
            Debug.LogWarning("Spawner：未设置可生成的预制体！");
            return;
        }

        GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];

        Vector2 center = box.bounds.center;
        Vector2 size = box.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float fixedY = center.y; 
        Vector2 spawnPos = new Vector2(randomX, fixedY);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        BoxCollider2D b = GetComponent<BoxCollider2D>();
        if (b != null)
            Gizmos.DrawWireCube(b.bounds.center, b.bounds.size);
    }
}
