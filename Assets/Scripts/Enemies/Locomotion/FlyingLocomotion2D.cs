using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyingLocomotion2D : Locomotion2D
{
    public SpriteRenderer spriteRenderer;
    public bool useFlipX = true;
    public bool facesRightByDefault = true; // 新增：美术默认朝向

    public EnemyConfig config;

    private Rigidbody2D rb;
    private float _lastSign = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
#if UNITY_600_0_OR_NEWER
        rb.gravityScale = 0f;
#else
        rb.gravityScale = 0f;
#endif
    }

    public override void MoveTowards(Vector2 targetPos, float speed)
    {
        Vector2 dir = (targetPos - (Vector2)transform.position);
        Vector2 vel = dir.normalized * speed;

        if (config != null && config.hoverBobAmplitude > 0f)
        {
            float bob = Mathf.Sin(Time.time * config.hoverBobSpeed) * config.hoverBobAmplitude;
            vel.y += bob;
        }

        SetVelocity(vel);
        FaceTowards(dir);
    }

    public override void Stop()
    {
        SetVelocity(Vector2.zero);
    }

    public override void FaceTowards(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > 0.001f) _lastSign = Mathf.Sign(dir.x);

        bool lookLeft = _lastSign < 0f;
        if (!facesRightByDefault) lookLeft = !lookLeft;

        if (spriteRenderer == null)
        {
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (lookLeft ? -1 : 1);
            transform.localScale = s;
            return;
        }

        if (useFlipX) spriteRenderer.flipX = lookLeft;
        else
        {
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (lookLeft ? -1 : 1);
            transform.localScale = s;
        }
    }

    private void SetVelocity(Vector2 v)
    {
#if UNITY_600_0_OR_NEWER
        rb.linearVelocity = v;
#else
        rb.linearVelocity = v;
#endif
    }
}
