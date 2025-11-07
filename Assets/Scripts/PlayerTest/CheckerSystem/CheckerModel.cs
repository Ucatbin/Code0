using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public class CheckerModel
    {
        Transform _checkPoint;
        bool _detected;
        public bool IsDetected => _detected;
        CheckerData _data;
        public CheckerData Data => _data;
        public CheckerModel(CheckerData data, Transform checkPoint)
        {
            _data = data;
            _checkPoint = checkPoint;
        }

        public bool PerformCheck(CheckerData data)
        {
            // Debug
            if (data == null || _checkPoint == null) return false;

            return data.CheckType switch
            {
                CheckType.Ray => RayCheck(data),
                CheckType.Circle => CircleCheck(data),
                _ => false
            };
        }

        bool RayCheck(CheckerData data)
        {
            if (data == null || _checkPoint == null || data.CheckCount <= 0)
                return false;

            Vector2 perpendicular = Vector2.Perpendicular(data.Direction).normalized;
            Vector2 startPoint = (Vector2)_checkPoint.position - perpendicular * data.CheckWidth / 2;
            Vector2 endPoint = (Vector2)_checkPoint.position + perpendicular * data.CheckWidth / 2;

            bool allDetected = true;

            for (int i = 0; i < data.CheckCount; i++)
            {
                float t = data.CheckCount > 1 ? (float)i / (data.CheckCount - 1) : 0.5f;
                Vector2 checkPos = Vector2.Lerp(startPoint, endPoint, t);

                bool hitDetected;
                
                if (!allDetected)
                    hitDetected = false;
                else
                {
                    RaycastHit2D hit = Physics2D.Raycast(checkPos, data.Direction, data.Distance, data.CheckLayer);
                    hitDetected = hit.collider != null;
                    allDetected = hitDetected;
                }

                Debug.DrawRay(checkPos, data.Direction * data.Distance, hitDetected ? Color.green : Color.red);
            }

            return allDetected;
        }
        bool CircleCheck(CheckerData data)
        {
            return false;
        }
    }
}