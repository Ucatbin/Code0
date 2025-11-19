using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    [CreateAssetMenu(fileName = "Checker Data", menuName = "ThisGame/Core/CheckerSystem/CheckerData0")]
    public class CheckerData : ScriptableObject
    {
        public LayerMask CheckLayer;
        public int CheckCount;
        public float CheckWidth;

        public Vector2 Direction;
        public float Distance;
        public float CheckRadius;
        public float Angle;
    }
}