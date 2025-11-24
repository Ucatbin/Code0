using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    public class P_TheWorldModel : SkillModel
    {
        public P_TheWorldModel(SkillData data) : base(data)
        {
        }

        public override void HandleSkillButtonPressed(ISkillEvent e)
        {
            if (!_isReady || _currentCharges == 0) return;

            if (e is P_Skill_TheWorldPressed thisSkill)
            {
                var skillExecute = new P_Skill_TheWorldExecuted()
                {

                };
                EventBus.Publish(skillExecute);
                ExecuteSkill(skillExecute);
            }
        }
        public override void ExecuteSkill(ISkillEvent e)
        {
            base.ExecuteSkill(e);

            _isReady = true;
        }
        public override void StartCoolDown()
        {
            base.StartCoolDown();

            TimerManager.Instance.AddTimer(
                Data.CoolDown,
                () =>{
                    _isReady = true;
                }
            );
        }
    }
}