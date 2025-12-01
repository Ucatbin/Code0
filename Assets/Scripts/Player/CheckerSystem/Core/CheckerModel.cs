using Unity.VisualScripting;
using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public class CheckerModel
    {
        protected bool _isEnabled;
        public bool IsEnabled => _isEnabled;
        protected Transform _checkPoint;
        protected bool _isDetected;
        public bool IsDetected => _isDetected;
        protected CheckerData _data;
        public CheckerData Data => _data;
        public CheckerModel(CheckerData data, Transform checkPoint, bool enabled)
        {
            _data = data;
            _checkPoint = checkPoint;
            _isEnabled = enabled;
        }

        public virtual void Check(CheckerData data)
        {
            if (data == null || _checkPoint == null || data.CheckCount <= 0)
                _isDetected = false;
        }
        public void EnableChecker(bool enable) => _isEnabled = enable;
    }
}