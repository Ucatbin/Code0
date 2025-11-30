using ThisGame.Core;
using ThisGame.Entity.StateMachineSystem;
using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    public class P_DashAttackModel : SkillModel
    {
        public Vector3 DashStartPos;
        public Vector3 DashTargetPos;
        public float DashDuration;

        public P_DashAttackModel(SkillData data) : base(data)
        {
        }

        public override void HandleSkillButtonPressed(P_SkillPressed e)
        {
            if (!_isReady || _currentCharges == 0) return;

            var stateChange = new P_SkillStateSwitch()
            {
                SkillState = typeof(P_DashAttackState)
            };
            EventBus.Publish(stateChange);
        }
        public override void HandleSkillButtonReleased(P_SkillReleased e)
        {
            var stateChange = new P_SkillStateSwitch()
            {
                SkillState = typeof(P_AirState)
            };
            EventBus.Publish(stateChange);
        }
        public override void ExecuteSkill(P_SkillExecute e)
        {
            // ConsumeResources(Data);

            var skillData = Data as P_DashAttackData;
            DashStartPos = e.PlayerPosition;
            DashTargetPos = e.InputDirection;
            DashDuration = skillData.DashAnimationClip.length;
            Vector3 toTarget = DashTargetPos - e.PlayerPosition;
            if (toTarget.magnitude > skillData.MaxDashDistance)
                DashTargetPos = e.PlayerPosition + toTarget.normalized * skillData.MaxDashDistance;
            
            SmoothTime.SetSmoothTimeScale(skillData.NormalTimeScale);
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