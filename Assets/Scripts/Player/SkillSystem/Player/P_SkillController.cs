
namespace ThisGame.Entity.SkillSystem
{
    public class P_SkillController : SkillController
    {
        void Start()
        {
            var attackSkillEntry = SkillManager.Instance.GetSkillEntry(typeof(P_AttackModel));
            var attack = new P_AttackModel(attackSkillEntry.Data as P_AttackData);
            UnlockSkill(attack);

            var doubleJumpSkillEntry = SkillManager.Instance.GetSkillEntry(typeof(P_DoubleJumpModel));
            var doubleJump = new P_DoubleJumpModel(doubleJumpSkillEntry.Data as P_DoubleJumpData);
            UnlockSkill(doubleJump);

            var grappingHookSkillEntry = SkillManager.Instance.GetSkillEntry(typeof(P_GrappingHookModel));
            var grappingHook = new P_GrappingHookModel(grappingHookSkillEntry.Data as P_GrappingHookData);
            UnlockSkill(grappingHook);

            var dashAttackSkillEntry = SkillManager.Instance.GetSkillEntry(typeof(P_DashAttackModel));
            var dashAttack = new P_DashAttackModel(dashAttackSkillEntry.Data as P_DashAttackData);
            UnlockSkill(dashAttack);
        }
    }
}