using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    public class P_DoubleJumpModel : SkillModel
    {
        public P_DoubleJumpModel(SkillData data) : base(data)
        {
            EventBus.Subscribe<GroundCheckChange>(this, HandleGroundStateChanged);
        }

        public override void HandleSkillButtonPressed(ISkillEvent e)
        {
            if (!_isReady || _currentCharges == 0) return;

            if (e is P_Skill_DoubleJumpPressed thisSkillEvent)
            {
                var doubleJumpPrepare = new P_Skill_DoubleJumpPrepare()
                {
                    Skill = thisSkillEvent.Skill
                };
                EventBus.Publish(doubleJumpPrepare);
                ExecuteSkill(doubleJumpPrepare);         
            }
        }
        public override void ExecuteSkill(ISkillEvent e)
        {
            base.ExecuteSkill(e);
        }
        public override void StartCoolDown()
        {
            base.StartCoolDown();
        }

        void HandleGroundStateChanged(GroundCheckChange e)
        {
            _currentCharges = Data.MaxCharges;
            _isReady = true;
        }
    }
}