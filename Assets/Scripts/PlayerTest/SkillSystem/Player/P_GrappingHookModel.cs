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
                var data = Data as P_GrappingHookData;
                RaycastHit2D hit = Physics2D.Raycast(
                    thisSkillEvent.CurrentPosition,
                    thisSkillEvent.InputDirection,
                    data.MaxDetectDist,
                    data.CanHookLayer
                );
                if (hit.collider != null)
                {
                    HookPoint = new GameObject("HookPoint");
                    HookPoint.transform.SetPositionAndRotation(hit.point, Quaternion.identity);
                    HookPoint.transform.parent = hit.transform;
                    HookPoint.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

                    var grappingHookPrepare = new P_Skill_GrappingHookPrepare()
                    {
                        TargetPosition = new Vector3(hit.point.x, thisSkillEvent.CurrentPosition.y, 0f)
                    };
                    Debug.Log(HookPoint);
                    EventBus.Publish(grappingHookPrepare);
                }
            }
        }
    }
}