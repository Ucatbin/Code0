using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    public class P_AttackModel : SkillModel
    {
        public P_AttackModel(SkillData data) : base(data)
        {
        }

        public override void HandleSkillButtonPressed(ISkillEvent e)
        {
            if (!_isReady || _currentCharges == 0) return;

            if (e is P_Skill_AttackPressed thisSkillEvent)
            {
                var attackPrepare = new P_SKill_AttackPrepare();
                EventBus.Publish(attackPrepare);
                ExecuteSkill(attackPrepare);
            }
        }
    }
}