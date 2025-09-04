using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GroundLocomotion2D : Locomotion2D
{
    [Tooltip("到达目标X时的容差（米），避免在目标点左右抖动")]
    public float reachToleranceX = 0.08f;

    [Header("可选：用于翻转朝向")]
    public SpriteRenderer spriteRenderer;
    public bool useFlipX = true;

    [Tooltip("原画是否默认面向右？(大多数像素画默认朝右)")]
    public bool facesRightByDefault = true;

    private Rigidbody2D rb;
    private float _lastSign = 1f; // 记住上次朝向（+1 右，-1 左）

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void MoveTowards(Vector2 targetPos, float speed)
    {
        if (rb == null) return;

        Vector2 pos = rb.position;
        float dx = targetPos.x - pos.x;

        if (Mathf.Abs(dx) <= reachToleranceX)
        {
            Stop();
            return;
        }

        float dir = Mathf.Sign(dx);

        // 只改 X 速度，保留当前 Y（受重力/平台影响）
        Vector2 v = GetVelocity();
        v.x = dir * speed;
        SetVelocity(v);

        FaceTowards(new Vector2(dir, 0f));
    }

    public override void Stop()
    {
        if (rb == null) return;
        Vector2 v = GetVelocity();
        v.x = 0f;
        SetVelocity(v);
    }

    public override void FaceTowards(Vector2 dir)
    {
        // x 很小就沿用上次朝向，避免抖动
        if (Mathf.Abs(dir.x) > 0.001f) _lastSign = Mathf.Sign(dir.x);

        bool lookLeft = _lastSign < 0f;

        // 如果原画默认面向左，则取反含义
        if (!facesRightByDefault) lookLeft = !lookLeft;

        if (spriteRenderer == null)
        {
            // 用缩放翻转的兜底方案
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (lookLeft ? -1 : 1);
            transform.localScale = s;
            return;
        }

        if (useFlipX)
        {
            spriteRenderer.flipX = lookLeft;
        }
        else
        {
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (lookLeft ? -1 : 1);
            transform.localScale = s;
        }
    }

    // --- 兼容层：统一处理 linearVelocity / velocity ---
    private Vector2 GetVelocity()
    {
#if UNITY_600_0_OR_NEWER
        return rb.linearVelocity;
#else
        return rb.linearVelocity;
#endif
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
