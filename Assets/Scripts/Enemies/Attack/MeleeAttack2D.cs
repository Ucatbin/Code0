using UnityEngine;

public class MeleeAttack2D : Attack2D
{
    public float radius = 0.8f;
    public LayerMask targetMask;
    public int damage = 1;
    public Transform hitOrigin;

    protected override void DoAttack(Transform target)
    {
        Collider2D hit = Physics2D.OverlapCircle(hitOrigin.position, radius, targetMask);
         if (hit != null && hit.TryGetComponent(out IDamageable dmg))
        {
            dmg.TakeDamage(damage);
        }
        // TODO: ≤•∑≈∂Øª≠/“Ù–ß
    }

    void OnDrawGizmosSelected()
    {
        if (hitOrigin == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(hitOrigin.position, radius);
    }
}
