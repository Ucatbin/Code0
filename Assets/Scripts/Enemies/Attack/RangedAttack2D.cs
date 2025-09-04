using UnityEngine;

public class RangedAttack2D : Attack2D
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 7f;

    protected override void DoAttack(Transform target)
    {
        if (projectilePrefab == null || firePoint == null || target == null) return;

        GameObject go = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        if (go.TryGetComponent<Rigidbody2D>(out var rb))
        {
            Vector2 dir = (target.position - firePoint.position).normalized;
            rb.linearVelocity = dir * projectileSpeed;
        }
        // TODO: ������Ż������˺��ű�
    }
}
