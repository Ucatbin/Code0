using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GroundLocomotion2D : Locomotion2D
{
    [Tooltip("到达目标X时的容差（米），避免在目标点左右抖动")]
    public float reachToleranceX = 0.08f;

    [Header("可选：用于翻转朝向")]
    public SpriteRenderer spriteRenderer;
    public bool useFlipX = true;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // 地面敌人一般需要重力；如需禁用请在检查器里改 Gravity Scale
    }

    public override void MoveTowards(Vector2 targetPos, float speed)
    {
        if (rb == null) return;

        Vector2 pos = rb.position;
        float dx = targetPos.x - pos.x;

        // 在容差内当作到点，停止X移动，避免来回抖动
        if (Mathf.Abs(dx) <= reachToleranceX)
        {
            Stop();
            return;
        }

        float dir = Mathf.Sign(dx);

        // 只改 X 速度，保留当前 Y（受重力/平台影响）
        Vector2 v = rb.linearVelocity;
        v.x = dir * speed;
        rb.linearVelocity = v;

        FaceTowards(new Vector2(dir, 0f));
    }

    public override void Stop()
    {
        if (rb == null) return;
        Vector2 v = rb.linearVelocity;
        v.x = 0f;
        rb.linearVelocity = v;
    }

    public override void FaceTowards(Vector2 dir)
    {
        // 允许没有渲染器时直接返回
        if (spriteRenderer == null)
        {
            // 也可以选择用缩放翻转：
            // Vector3 s = transform.localScale;
            // s.x = Mathf.Abs(s.x) * (dir.x < 0 ? -1 : 1);
            // transform.localScale = s;
            return;
        }

        if (useFlipX)
        {
            spriteRenderer.flipX = dir.x < 0;
        }
        else
        {
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (dir.x < 0 ? -1 : 1);
            transform.localScale = s;
        }
    }
}
