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

        public override void HandleSkillButtonPressed(P_SkillPressed e)
        {
            if (!_isReady || _currentCharges == 0) return;

            InputDir = e.InputDirection;
            var stateChange = new P_SkillStateSwitch()
            {
                SkillState = typeof(P_AttackState)
            };
            EventBus.Publish(stateChange);
        }
        public override void HandleSkillButtonReleased(P_SkillReleased e)
        {
            throw new System.NotImplementedException();
        }
        public override void ExecuteSkill(P_SkillExecute e)
        {

        }
        public override void StartCoolDown()
        {
            TimerManager.Instance.AddTimer(
                Data.CoolDown,
                () =>{
                    _isReady = true;
                }
            );
        }
    }
}