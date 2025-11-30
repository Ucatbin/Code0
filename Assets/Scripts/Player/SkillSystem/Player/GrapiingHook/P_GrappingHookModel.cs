using ThisGame.Core;
using ThisGame.Entity.StateMachineSystem;
using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    public class P_GrappingHookModel : SkillModel
    {
        public DistanceJoint2D Joint;
        public GameObject HookPoint;
        public P_GrappingHookModel(SkillData data, DistanceJoint2D joint) : base(data)
        {
            Joint = joint;
        }

        public override void HandleSkillButtonPressed(P_SkillPressed e)
        {
            if (!_isReady || _currentCharges == 0) return;

            var rayDir = (e.InputDirection - e.PlayerPosition).normalized;
            var data = Data as P_GrappingHookData;
            RaycastHit2D hit = Physics2D.Raycast(
                e.PlayerPosition,
                rayDir,
                data.MaxDetectDist,
                data.CanHookLayer
            );
            if (hit.collider != null)
            {
                if (HookPoint == null)
                {
                    HookPoint = new GameObject("HookPoint");
                    HookPoint.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                }
                HookPoint.transform.SetPositionAndRotation(hit.point, Quaternion.identity);
                HookPoint.transform.parent = hit.transform;
                var stateChange = new P_SkillStateSwitch()
                {
                    SkillState = typeof(P_HookedState)
                };
                EventBus.Publish(stateChange);
            }
        }
        public override void HandleSkillButtonReleased(P_SkillReleased e)
        {
            DisableJoint();
            var stateChange = new P_SkillStateSwitch()
            {
                SkillState = typeof(P_IdleState)
            };
            EventBus.Publish(stateChange);
        }
        public void ControlRope(Vector3 input, Rigidbody2D rb, DistanceJoint2D joint, float deltaTime)
        {
            var data = Data as P_GrappingHookData;
            // Swing
            if (Mathf.Abs(rb.linearVelocity.magnitude) < data.MaxSwingSpeed && input.x != 0f)
                rb.AddForce(new Vector2(input.x * data.SwingForce, 0f), ForceMode2D.Force);
            // Length
            if (input.z != 0f)
                joint.distance -= input.z * data.ExtendSpeed * deltaTime;
        }

        public void EnableJoint(float length)
        {
            Joint.connectedBody = HookPoint.GetComponent<Rigidbody2D>();
            Joint.distance = length;
            Joint.enabled = true;
        }
        public void DisableJoint()
        {
            Joint.enabled = false;
        }
        // public void RopeDash(Vector3 input, Rigidbody2D rb)
        // {
        //     var data = Data as P_GrappingHookData;
        //     Vector2 dashDirection = rb.linearVelocity.normalized;

        //     if (input.x < 0.1f)
        //     {
        //         dashDirection = transform.right; // 或者根据你的玩家朝向调整
        //     }

        //     // 施加瞬间的冲量力
        //     rb.AddForce(dashDirection * data.DashForce, ForceMode2D.Impulse);

        //     EventBus.Publish(new P_Skill_RopeDashExecute());
        // }
    }
}