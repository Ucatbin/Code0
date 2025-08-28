using UnityEngine;

public class Vision2D : MonoBehaviour
{
    public EnemyConfig config;
    public Transform eye;               // �������
    public LayerMask targetMask;        // ��Ҳ�
    public float memoryTime = 1.5f;     // ʧȥ���ߺ��Ա��ֳ�޵�ʱ��

    private float lastSeenTime = -999f;
    private Transform lastTarget;

    public bool HasTarget(out Transform target)
    {
        target = null;
        Collider2D hit = Physics2D.OverlapCircle(eye.position, config.sightRange, targetMask);
        if (hit != null)
        {
            Vector2 to = (hit.transform.position - eye.position).normalized;
            float angle = Vector2.Angle(eye.right, to); // �� eye ������Ϊǰ��
            if (angle <= config.sightHalfAngle)
            {
                // �����ж��Ƿ�ǽ�赲
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

        // ����ʱ������Ȼ��Ϊ����Ŀ�ꡱ
        if (Time.time - lastSeenTime <= memoryTime && lastTarget != null)
        {
            target = lastTarget;
            return true;
        }
        return false;
    }
}
