using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    public class P_GrappingHookModel : SkillModel
    {
        public GameObject HookPoint;
        public P_GrappingHookModel(SkillData data) : base(data)
        {
        }

        public override void HandleSkillButtonPressed(ISkillEvent e)
        {
            if (!_isReady || _currentCharges == 0) return;

            if (e is P_Skill_GrappingHookPressed thisSkillEvent)
            {
                var rayDir = (thisSkillEvent.InputDirection - thisSkillEvent.CurrentPosition).normalized;
                var data = Data as P_GrappingHookData;
                RaycastHit2D hit = Physics2D.Raycast(
                    thisSkillEvent.CurrentPosition,
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
                    EventBus.Publish(new P_Skill_GrappingHookExecute());
                }
            }
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
    }
}