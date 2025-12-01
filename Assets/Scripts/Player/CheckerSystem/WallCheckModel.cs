using Unity.VisualScripting;
using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public class WallCheckModel : CheckerModel
    {
        bool _wasWalled;

        int _facingDir = 1;

        public WallCheckModel(CheckerData data, Transform checkPoint, bool enabled) : base(data, checkPoint, enabled)
        {
            EventBus.Subscribe<FlipAction>(this, OnFlip);
        }

        void OnFlip(FlipAction flipEvent)
        {
            _facingDir = flipEvent.FacingDir;
        }

        public override void Check(CheckerData data)
        {
            base.Check(data);

            Vector2 perpendicular = Vector2.Perpendicular(data.Direction).normalized;
            Vector2 startPoint = (Vector2)_checkPoint.position - perpendicular * data.CheckWidth / 2;
            Vector2 endPoint = (Vector2)_checkPoint.position + perpendicular * data.CheckWidth / 2;

            _wasWalled = _isDetected;
            _isDetected = true;

            for (int i = 0; i < data.CheckCount; i++)
            {
                float t = data.CheckCount > 1 ? (float)i / (data.CheckCount - 1) : 0.5f;
                Vector2 checkPos = Vector2.Lerp(startPoint, endPoint, t);

                bool hitDetected;

                if (!_isDetected)
                    hitDetected = false;
                else
                {
                    RaycastHit2D hit = Physics2D.Raycast(checkPos, data.Direction * _facingDir, data.CheckDistance, data.CheckLayer);
                    hitDetected = hit.collider != null;
                    _isDetected = hitDetected;
                }

                Debug.DrawRay(checkPos, data.Direction * _facingDir * data.CheckDistance, hitDetected ? Color.green : Color.red);
            }
            
            if (_wasWalled != _isDetected)
            {
                var wallStateChanged = new WallCheckChange()
                {
                    ChangeToWalled = _isDetected,
                };
                EventBus.Publish(wallStateChanged);
            }
        }
    }
}
