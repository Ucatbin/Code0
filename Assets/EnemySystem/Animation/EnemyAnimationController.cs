using UnityEngine;
public class EnemyAnimationController : MonoBehaviour
{
    [Header("Components")]
    public EnemyController enemy;
    public Animator animator;
    public Rigidbody2D rb;

    // 记录上一帧处于哪个状态，用来检测“状态是否变化”
    private EnemyStat lastStat;

    // 方便后面用字符串常量
    private static readonly int IdleHash = Animator.StringToHash("Idle");
    private static readonly int WalkHash = Animator.StringToHash("Walk");
    private static readonly int ChaseHash = Animator.StringToHash("Chase");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HurtHash = Animator.StringToHash("Hurt");
    private static readonly int DieHash = Animator.StringToHash("Die");

    private void Awake()
    {
        if (enemy == null)
            enemy = GetComponent<EnemyController>();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();   // 如果动画在子物体上
    }

    private void Update()
    {
        if (enemy == null || animator == null) return;

        // 1. 检测状态是否发生变化
        if (enemy.currentStat != lastStat)
        {
            OnStateChanged(enemy.currentStat);
            lastStat = enemy.currentStat;
        }

        // 2. 可选：这里顺便做朝向翻转（看向玩家）
        UpdateFacing();
        UpdateLocomotion();
    }

    private void OnStateChanged(EnemyStat newState)
    {
        // 注意：这里用“== enemy.xxxStat”来判断当前是哪个状态
        if (newState == null)
        {
            PlayIdle();
            return;
        }

        if (newState == enemy.moveStat || newState == enemy.enemyGoBack)
        {
            // 巡逻 / 回到原点
            PlayWalk();
        }
        else if (newState == enemy.chaseStat)
        {
            // 追击
            PlayChase();
        }
        else if (newState == enemy.attackStat)
        {
            // 攻击
            PlayAttack();
        }
        else
        {
            // 其他未知状态，默认 Idle
            PlayIdle();
        }
    }

    private void PlayIdle()
    {
        animator.CrossFade(IdleHash, 0f);
    }

    private void PlayWalk()
    {
        animator.CrossFade(WalkHash, 0f);
    }

    private void PlayChase()
    {
        animator.CrossFade(ChaseHash, 0f);
    }

    private void PlayAttack()
    {
        animator.CrossFade(AttackHash, 0f);
    }

    public void PlayHurt()
    {
        animator.CrossFade(HurtHash, 0f);
    }

    public void PlayDie()
    {
        animator.CrossFade(DieHash, 0f);
    }

    /// <summary>
    /// 根据玩家位置翻转敌人朝向（可选）
    /// </summary>

    private void UpdateLocomotion()
    {
        if (rb == null) return;

        // 攻击 / 受伤 / 死亡 等高优先级状态，不参与移动动画
        if (enemy.currentStat == enemy.attackStat)
            return;
        // 未来如果有 Hurt / Die 之类，也在这里提前 return

        float speedX = Mathf.Abs(rb.linearVelocity.x);
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 1）速度几乎为 0：Idle
        if (speedX < 0.1f)
        {
            if (!stateInfo.IsName("Idle"))
            {
                animator.Play(IdleHash);
            }
            return;
        }

        // 2）有速度：根据当前逻辑状态选择 Walk / Chase
        if (enemy.currentStat == enemy.moveStat || enemy.currentStat == enemy.enemyGoBack)
        {
            if (!stateInfo.IsName("Walk"))
            {
                animator.Play(WalkHash);
            }
        }
        else if (enemy.currentStat == enemy.chaseStat)
        {
            if (!stateInfo.IsName("Chase"))
            {
                animator.Play(ChaseHash);
            }
        }
        else
        {
            // 其他状态：保底 Idle
            if (!stateInfo.IsName("Idle"))
            {
                animator.Play(IdleHash);
            }
        }
    }


    private void UpdateFacing()
    {
        if (enemy.target == null) return;

        Vector3 scale = transform.localScale;

        // 【模式 1】当前是攻击状态（例如 AttackFar）：朝向 target
        if (enemy.currentStat == enemy.attackStat)
        {
            float dirToTarget = enemy.target.position.x - transform.position.x;

            if (dirToTarget > 0.05f)
                scale.x = -Mathf.Abs(scale.x);      // 朝右
            else if (dirToTarget < -0.05f)
                scale.x = Mathf.Abs(scale.x);     // 朝左
        }
        else
        {
            // 【模式 2】移动 / 追击状态：按刚体速度方向来转
            bool facedByVelocity = false;

            if (rb != null && Mathf.Abs(rb.linearVelocity.x) > 0.05f) // 或 rb.velocity.x 视你用哪个
            {
                if (rb.linearVelocity.x > 0f)
                    scale.x = -Mathf.Abs(scale.x);      // 向右移动 → 朝右
                else if (rb.linearVelocity.x < 0f)
                    scale.x = Mathf.Abs(scale.x);     // 向左移动 → 朝左

                facedByVelocity = true;
            }

            // 如果速度几乎为 0（站住了），再退回用 target 方向来朝向
            if (!facedByVelocity)
            {
                float dirToTarget = enemy.target.position.x - transform.position.x;

                if (dirToTarget > 0.05f)
                    scale.x = -Mathf.Abs(scale.x);
                else if (dirToTarget < -0.05f)
                    scale.x = Mathf.Abs(scale.x);
            }
        }

        transform.localScale = scale;
    }
}
