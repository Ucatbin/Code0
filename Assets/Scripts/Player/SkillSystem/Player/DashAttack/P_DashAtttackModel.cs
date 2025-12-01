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
        public float DashStartTime;
        public bool IsDashing;

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
            if (IsDashing) return;
            var stateChange = new P_SkillStateSwitch()
            {
                SkillState = typeof(P_AirState)
            };
            EventBus.Publish(stateChange);
        }
        public override void ExecuteSkill(P_SkillExecute e)
        {
            // ConsumeResources(Data);
            
            DashStartTime = e.StartTime;
            IsDashing = true;

            var skillData = Data as P_DashAttackData;
            DashStartPos = e.PlayerPosition;
            DashTargetPos = e.InputDirection;
            DashDuration = skillData.DashAnimationClip.length;
            
            Vector3 toTarget = DashTargetPos - e.PlayerPosition;
            Vector3 dashDirection = toTarget.normalized;
            
            float maxDashDistance = skillData.MaxDashDistance;
            RaycastHit2D hit = Physics2D.Raycast(
                e.PlayerPosition, 
                dashDirection, 
                maxDashDistance, 
                skillData.WallLayerMask
            );
            
            if (hit.collider != null)
            {
                float offset = 0.3f;
                DashTargetPos = (Vector3)hit.point - (dashDirection * offset);
                
                float actualDistance = Vector3.Distance(e.PlayerPosition, DashTargetPos);
                maxDashDistance = Mathf.Min(actualDistance, maxDashDistance);
            }
            else
            {
                if (toTarget.magnitude > maxDashDistance)
                    DashTargetPos = e.PlayerPosition + dashDirection * maxDashDistance;
            }
            
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