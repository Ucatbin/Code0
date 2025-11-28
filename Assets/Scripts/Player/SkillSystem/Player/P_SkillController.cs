

namespace ThisGame.Entity.SkillSystem
{
    public class P_SkillController : SkillController
    {
        public override void RegisterModels()
        {
            base.RegisterModels();

            var attackSkillEntry = SkillManager.Instance.GetSkillEntry(typeof(P_AttackModel));
            var attack = new P_AttackModel(attackSkillEntry.Data as P_AttackData);
            UnlockSkill(attack, attackSkillEntry);

            var doubleJumpSkillEntry = SkillManager.Instance.GetSkillEntry(typeof(P_DoubleJumpModel));
            var doubleJump = new P_DoubleJumpModel(doubleJumpSkillEntry.Data as P_DoubleJumpData);
            UnlockSkill(doubleJump, doubleJumpSkillEntry);

            var grappingHookSkillEntry = SkillManager.Instance.GetSkillEntry(typeof(P_GrappingHookModel));
            var grappingHook = new P_GrappingHookModel(grappingHookSkillEntry.Data as P_GrappingHookData);
            UnlockSkill(grappingHook, grappingHookSkillEntry);

            var dashAttackSkillEntry = SkillManager.Instance.GetSkillEntry(typeof(P_DashAttackModel));
            var dashAttack = new P_DashAttackModel(dashAttackSkillEntry.Data as P_DashAttackData);
            UnlockSkill(dashAttack, dashAttackSkillEntry);
        }
    }
}