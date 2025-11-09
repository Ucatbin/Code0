using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public class GroundCheckModel : CheckerModel
    {
        bool _wasGrounded;

        public GroundCheckModel(CheckerData data, Transform checkPoint) : base(data, checkPoint)
        {
        }

        public override void Check(CheckerData data)
        {
            base.Check(data);

            Vector2 perpendicular = Vector2.Perpendicular(data.Direction).normalized;
            Vector2 startPoint = (Vector2)_checkPoint.position - perpendicular * data.CheckWidth / 2;
            Vector2 endPoint = (Vector2)_checkPoint.position + perpendicular * data.CheckWidth / 2;

            _wasGrounded = _isDetected;
            _isDetected = true;

            for (int i = 0; i < data.CheckCount; i++)
            {
                float t = data.CheckCount > 1 ? (float)i / (data.CheckCount - 1) : 0.5f;
                Vector2 checkPos = Vector2.Lerp(startPoint, endPoint, t);

                bool hitDetected;

                if (!_isDetected)
                {
                    hitDetected = false;
                }
                else
                {
                    RaycastHit2D hit = Physics2D.Raycast(checkPos, data.Direction, data.Distance, data.CheckLayer);
                    hitDetected = hit.collider != null;
                    _isDetected = hitDetected;
                }

                Debug.DrawRay(checkPos, data.Direction * data.Distance, hitDetected ? Color.green : Color.red);
            }

            if (_wasGrounded != _isDetected)
            {
                var groundStateChanged = new GroundCheckChange()
                {
                    ChangeToGrounded = _isDetected,
                };
                EventBus.Publish(groundStateChanged);
            }
        }
    }
}