using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public EnemyConfig config;
    public Locomotion2D locomotion;
    public Vision2D vision;
    public Attack2D attack;
    [Header("巡逻点")]
    public Transform pointA;
    public Transform pointB;
    private Vector2 patrolTarget;

    private enum State { Patrol, Chase, Attack }
    private State state;

    void Start()
    {
        patrolTarget = pointA ? pointA.position : (Vector2)transform.position;
        state = State.Patrol;
    }

    void Update()
    {
        Transform target = null;
        bool hasTarget = (vision != null) && vision.HasTarget(out target);

        switch (state)
        {
            case State.Patrol:
                RunPatrol();
                if (hasTarget) state = State.Chase;
                break;

            case State.Chase:
                if (!hasTarget || target == null) { state = State.Patrol; break; }

                float dist = Vector2.Distance(transform.position, target.position);
                locomotion.MoveTowards(target.position, config.chaseSpeed);

                if (dist <= config.attackRange && attack != null && attack.CanAttack(Time.time))
                    state = State.Attack;
                break;

            case State.Attack:
                if (vision != null && vision.HasTarget(out target))
                {
                    attack.TryAttack(target);
                    float d = Vector2.Distance(transform.position, target.position);
                    state = d <= config.attackRange ? State.Attack : State.Chase;
                }
                else state = State.Patrol;
                break;
        }
    }


    private void RunPatrol()
    {
        if (pointA == null || pointB == null || locomotion == null) return;

        locomotion.MoveTowards(patrolTarget, config.patrolSpeed);

        if (Vector2.Distance(transform.position, patrolTarget) < 0.1f)
            patrolTarget = (patrolTarget == (Vector2)pointA.position) ? (Vector2)pointB.position : (Vector2)pointA.position;
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
