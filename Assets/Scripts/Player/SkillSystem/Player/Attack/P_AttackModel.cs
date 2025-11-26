using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    public class P_AttackModel : SkillModel
    {
        public Vector3 InputDir;
        public P_AttackModel(SkillData data) : base(data)
        {
        }

        public override void HandleSkillButtonPressed(ISkillEvent e)
        {
            if (!_isReady || _currentCharges == 0) return;

            if (e is P_Skill_AttackPressed thisSkill)
            {
                InputDir = thisSkill.InputDirection;
                var attackExecute = new P_Skill_AttackExecute()
                {
                };
                EventBus.Publish(attackExecute);
                ExecuteSkill(attackExecute);
            }
        }
        public override void ExecuteSkill(ISkillEvent e)
        {
            base.ExecuteSkill(e);
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