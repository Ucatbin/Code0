using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public EnemyConfig config;
    public Locomotion2D locomotion;
    public Vision2D vision;
    public Attack2D attack;
    public Rigidbody2D rd;

    [Header("巡逻点")]
    public Transform pointA;
    public Transform pointB;

    [SerializeField] private float switchCooldown = 0.1f;
    private float switchCooldownTimer = 0f;
    private Vector2 patrolTarget;

    private enum State { Patrol, Chase, Attack }
    private State state;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        // 以“当前更近的巡逻点”为基准，然后把目标设成“另一个点”，保证一开局就会动
        if (pointA && pointB)
        {
            ChooseNearestPatrolPoint();
            patrolTarget = (patrolTarget == (Vector2)pointA.position)
                ? (Vector2)pointB.position
                : (Vector2)pointA.position;
        }
        else
        {
            patrolTarget = transform.position;
        }

        state = State.Patrol;
    }

    void Update()
    {
        // 视野（可为空）
        Transform target = null;
        bool hasTarget = (vision != null) && vision.HasTarget(out target);

        if (switchCooldownTimer > 0f) switchCooldownTimer -= Time.deltaTime;

        switch (state)
        {
            case State.Patrol:
                RunPatrol();
                if (hasTarget) state = State.Chase;
                break;

            case State.Chase:
                if (!hasTarget || target == null)
                {
                    state = State.Patrol;
                    ChooseNearestPatrolPoint();
                    break;
                }

                float chaseSpeed = config ? config.chaseSpeed : 3f;
                locomotion?.MoveTowards(target.position, chaseSpeed);

                if (attack != null)
                {
                    float d = Vector2.Distance(locomotion.transform.position, target.position);
                    if (d <= (config ? config.attackRange : 1f) && attack.CanAttack(Time.time))
                        state = State.Attack;
                }
                break;

            case State.Attack:
                if (vision != null && vision.HasTarget(out target))
                {
                    attack?.TryAttack(target);
                    float d = Vector2.Distance(locomotion.transform.position, target.position);
                    state = (d <= (config ? config.attackRange : 1f)) ? State.Attack : State.Chase;
                }
                else
                {
                    state = State.Patrol;
                    ChooseNearestPatrolPoint();
                }
                break;
        }
    }

    private void RunPatrol()
    {
        if (locomotion == null || pointA == null || pointB == null) return;

        // 用“被移动的刚体”的位置
        Vector2 pos = locomotion.transform.position;

        // ——关键改动：地面敌人只看 X，把目标点投影到当前 Y——
        bool isGround = locomotion is GroundLocomotion2D;
        Vector2 target = patrolTarget;
        if (isGround) target.y = pos.y;

        // 到点判定：地面用 |Δx|，飞行用 2D 距离
        float xTol = (locomotion as GroundLocomotion2D)?.reachToleranceX ?? 0.1f;
        bool reached = isGround
            ? Mathf.Abs(pos.x - target.x) <= Mathf.Max(0.05f, xTol)
            : Vector2.Distance(pos, target) <= 0.1f;

        if (reached && switchCooldownTimer <= 0f)
        {
            patrolTarget = (patrolTarget == (Vector2)pointA.position)
                ? (Vector2)pointB.position
                : (Vector2)pointA.position;

            locomotion.Stop();
            switchCooldownTimer = switchCooldown;
            return; // 本帧不再移动
        }

        float speed = config ? config.patrolSpeed : 2f;
        locomotion.MoveTowards(target, speed);
    }

    private void ChooseNearestPatrolPoint()
    {
        if (pointA == null || pointB == null) return;

        Vector2 pos = locomotion ? (Vector2)locomotion.transform.position
                                 : (Vector2)transform.position;

        float dA = Vector2.Distance(pos, pointA.position);
        float dB = Vector2.Distance(pos, pointB.position);
        patrolTarget = (dA <= dB) ? (Vector2)pointA.position : (Vector2)pointB.position;
    }

    void OnValidate()
    {
        if (locomotion != null && locomotion.transform != transform)
        {
            Debug.LogWarning($"{name}: EnemyBrain与Locomotion2D不在同一物体上，将以Locomotion的transform作为位置来源。");
        }
    }

    void OnDrawGizmos()
    {
        if (pointA && pointB)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawSphere(pointA.position, 0.06f);
            Gizmos.DrawSphere(pointB.position, 0.06f);
        }
    }
}
