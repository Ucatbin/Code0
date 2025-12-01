using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public class GHookCheckModel : CheckerModel
    {
        public GHookCheckModel(CheckerData data, Transform checkPoint, bool enabled) : base(data, checkPoint, enabled)
        {
        }

        public override void Check(CheckerData data)
        {
            base.Check(data);

            Vector2 perpendicular = Vector2.Perpendicular(data.Direction).normalized;
            Vector2 startPoint = (Vector2)_checkPoint.position - perpendicular * data.CheckWidth / 2;
            Vector2 endPoint = (Vector2)_checkPoint.position + perpendicular * data.CheckWidth / 2;

            _isDetected = false;
            for (int i = 0; i < data.CheckCount; i++)
            {
                float t = data.CheckCount > 1 ? (float)i / (data.CheckCount - 1) : 0.5f;
                Vector2 checkPos = Vector2.Lerp(startPoint, endPoint, t);

                bool hitDetected;

                RaycastHit2D hit = Physics2D.Raycast(checkPos, data.Direction, data.CheckDistance, data.CheckLayer);
                hitDetected = hit.collider != null;
                
                if (hitDetected)
                    _isDetected = true;

                Debug.DrawRay(checkPos, data.Direction * data.CheckDistance, hitDetected ? Color.green : Color.red);
            }
        }
    }
}
