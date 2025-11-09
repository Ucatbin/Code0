using Unity.VisualScripting;
using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public class CheckerModel
    {
        protected Transform _checkPoint;
        protected bool _isDetected;
        public bool IsDetected => _isDetected;
        protected CheckerData _data;
        public CheckerData Data => _data;
        public CheckerModel(CheckerData data, Transform checkPoint)
        {
            _data = data;
            _checkPoint = checkPoint;
        }

        public virtual void Check(CheckerData data)
        {
            if (data == null || _checkPoint == null || data.CheckCount <= 0)
                _isDetected = false;
        }
    }
}