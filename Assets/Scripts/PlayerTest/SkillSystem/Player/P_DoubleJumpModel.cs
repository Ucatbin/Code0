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
                var doubleJumpData = Data as P_DoubleJumpData;
                var doubleJumpExecute = new P_Skill_DoubleJumpExecute()
                {
                    DoubleJumpSpeed = doubleJumpData.JumpSpeed
                };
                EventBus.Publish(doubleJumpExecute);
                ExecuteSkill(doubleJumpExecute);         
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