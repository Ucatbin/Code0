using UnityEngine;

public class Vision2D : MonoBehaviour
{
    public EnemyConfig config;
    public Transform eye;               // 视线起点
    public LayerMask targetMask;        // 玩家层
    public float memoryTime = 1.5f;     // 失去视线后仍保持仇恨的时间

    private float lastSeenTime = -999f;
    private Transform lastTarget;

    public bool HasTarget(out Transform target)
    {
        target = null;
        Collider2D hit = Physics2D.OverlapCircle(eye.position, config.sightRange, targetMask);
        if (hit != null)
        {
            Vector2 to = (hit.transform.position - eye.position).normalized;
            float angle = Vector2.Angle(eye.right, to); // 以 eye 的右向为前方
            if (angle <= config.sightHalfAngle)
            {
                // 射线判定是否被墙阻挡
                RaycastHit2D block = Physics2D.Raycast(eye.position, to, config.sightRange, config.visionBlockMask);
                if (!block)
                {
                    lastSeenTime = Time.time;
                    lastTarget = hit.transform;
                    target = hit.transform;
                    return true;
                }
            }
        }

        // 记忆时间内仍然认为“有目标”
        if (Time.time - lastSeenTime <= memoryTime && lastTarget != null)
        {
            target = lastTarget;
            return true;
        }
        return false;
    }
}
