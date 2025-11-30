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

        public override void HandleSkillButtonPressed(P_SkillPressed e)
        {
            if (!_isReady || _currentCharges == 0) return;

            ExecuteSkill();
        }
        public override void HandleSkillButtonReleased(P_SkillReleased e)
        {
            throw new System.NotImplementedException();
        }
        public override void ExecuteSkill()
        {
            ConsumeResources(Data);

            var doubleJumpData = Data as P_DoubleJumpData;
            var doubleJump = new JumpExecute()
            {
                JumpType = JumpType.DoubleJump,
                JumpDir = new Vector3(0f, doubleJumpData.JumpSpeed, 0f),
            };
            EventBus.Publish(doubleJump);
        }
        public override void StartCoolDown()
        {

        }

        void HandleGroundStateChanged(GroundCheckChange e)
        {
            if (e.ChangeToGrounded)
            {
                _currentCharges = Data.MaxCharges;
                _isReady = true;
            }
        }
    }
}