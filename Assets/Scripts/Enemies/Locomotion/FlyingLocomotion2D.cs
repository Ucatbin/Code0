using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyingLocomotion2D : Locomotion2D
{
    public SpriteRenderer spriteRenderer;
    public bool useFlipX = true;
    public EnemyConfig config; // ������ͣ���

    private Rigidbody2D rb;
    private float baseY;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        baseY = transform.position.y;
    }

    public override void MoveTowards(Vector2 targetPos, float speed)
    {
        Vector2 dir = (targetPos - rb.position);
        Vector2 vel = dir.normalized * speed;

        // ��΢���¸������ɹص���
        if (config != null && config.hoverBobAmplitude > 0f)
        {
            float bob = Mathf.Sin(Time.time * config.hoverBobSpeed) * config.hoverBobAmplitude;
            vel.y += bob;
        }

        rb.linearVelocity = vel;
        FaceTowards(dir);
    }

    public override void Stop()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public override void FaceTowards(Vector2 dir)
    {
        if (spriteRenderer == null) return;
        if (useFlipX) spriteRenderer.flipX = dir.x < 0;
        else
        {
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (dir.x < 0 ? -1 : 1);
            transform.localScale = s;
        }
    }
}
