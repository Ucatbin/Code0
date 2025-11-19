using System.Collections.Generic;
using UnityEngine;

public class EnemyProducerF : MonoBehaviour
{
    [Header("想生成啥飞行敌人放这儿，想要概率高就多放几个")]
    public List<GameObject> prefabs;   
    [Header("这是生成间隔哈")]
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
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        Vector2 spawnPos = new Vector2(randomX, randomY);

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
