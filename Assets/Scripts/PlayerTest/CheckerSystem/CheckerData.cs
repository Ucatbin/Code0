using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public enum CheckType
    {
        Ray,
        Circle,
        // Sector,
        // Box
    }

    [CreateAssetMenu(fileName = "Checker Data", menuName = "ThisGame/Core/CheckerSystem/CheckerData")]
    public class CheckerData : ScriptableObject
    {
        public CheckType CheckType;
        public LayerMask CheckLayer;
        public int CheckCount;
        public float CheckWidth;

        public Vector2 Direction;
        public float Distance;
        public float CheckRadius;
        public float Angle;
    }
}