using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GroundLocomotion2D : Locomotion2D
{
    public SpriteRenderer spriteRenderer;
    public bool useFlipX = true;

    private Rigidbody2D rb;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }

    public override void MoveTowards(Vector2 targetPos, float speed)
    {
        Vector2 pos = rb.position;
        float dir = Mathf.Sign(targetPos.x - pos.x);
        Vector2 v = rb.linearVelocity;
        v.x = dir * speed;
        rb.linearVelocity = v;

        FaceTowards(new Vector2(dir, 0f));
    }

    public override void Stop()
    {
        Vector2 v = rb.linearVelocity;
        v.x = 0f;
        rb.linearVelocity = v;
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
