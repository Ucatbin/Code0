// SimpleShatterer.cs
using UnityEngine;
using System.Collections;

public class SimpleShatterer : MonoBehaviour
{
    [Header("碎裂设置")]
    [SerializeField] private int _fragmentCount = 8;
    [SerializeField] private float _explosionForce = 5f;
    [SerializeField] private float _fadeDuration = 2f;
    [SerializeField] private Sprite[] _fragmentSprites;
    
    public void ShatterOnDeath()
    {
        SpriteRenderer originalRenderer = GetComponentInChildren<SpriteRenderer>();
        if (originalRenderer == null) return;
        
        GameObject fragmentsContainer = new GameObject("Fragments");
        fragmentsContainer.transform.position = transform.position;
        
        for (int i = 0; i < _fragmentCount; i++)
        {
            CreateFragment(fragmentsContainer.transform, originalRenderer, i);
        }
        
        // 隐藏原物体
        originalRenderer.enabled = false;
        var collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;
        
        Destroy(fragmentsContainer, _fadeDuration + 1f);
        Destroy(gameObject, _fadeDuration + 1.1f);
    }
    
    private void CreateFragment(Transform container, SpriteRenderer originalRenderer, int index)
    {
        GameObject fragment = new GameObject($"Fragment_{index}");
        fragment.transform.SetParent(container);
        fragment.transform.position = transform.position + Random.insideUnitSphere * 0.3f;
        
        // 添加SpriteRenderer
        SpriteRenderer fragmentRenderer = fragment.AddComponent<SpriteRenderer>();
        
        if (_fragmentSprites != null && _fragmentSprites.Length > 0)
        {
            fragmentRenderer.sprite = _fragmentSprites[Random.Range(0, _fragmentSprites.Length)];
        }
        else
        {
            fragmentRenderer.sprite = CreateSimpleFragmentSprite();
        }
        
        fragmentRenderer.color = originalRenderer.color;
        fragmentRenderer.sortingOrder = originalRenderer.sortingOrder + 1;
        
        // 只添加Rigidbody2D用于物理运动，不添加碰撞体！
        Rigidbody2D rb = fragment.AddComponent<Rigidbody2D>();
        
        // 设置刚体属性，避免与其他物体交互
        rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        rb.sleepMode = RigidbodySleepMode2D.StartAsleep;
        
        // 爆炸力 - 碎片之间不会碰撞，但会有物理运动
        Vector2 forceDirection = Random.insideUnitCircle.normalized;
        rb.AddForce(forceDirection * Random.Range(_explosionForce * 0.5f, _explosionForce * 1.5f), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-10f, 10f));
        
        // 淡出效果
        StartCoroutine(FadeOutFragment(fragmentRenderer, _fadeDuration));
    }
    
    private Sprite CreateSimpleFragmentSprite()
    {
        Texture2D tex = new Texture2D(16, 16);
        Color[] colors = new Color[16 * 16];
        
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.white;
        }
        
        tex.SetPixels(colors);
        tex.Apply();
        
        return Sprite.Create(tex, new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));
    }
    
    private IEnumerator FadeOutFragment(SpriteRenderer fragmentRenderer, float duration)
    {
        float timer = 0f;
        Color originalColor = fragmentRenderer.color;
        
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / duration);
            fragmentRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        
        Destroy(fragmentRenderer.gameObject);
    }
}