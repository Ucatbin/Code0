
namespace ThisGame.Entity.SkillSystem
{
    public class P_SkillController : SkillController
    {
        void Start()
        {
            var skillEntry = SkillManager.Instance.GetSkillEntry(typeof(P_AttackModel));
            var attack = new P_AttackModel(skillEntry.Data as P_AttackData);
            UnlockSkill(attack);
        }
    }
}