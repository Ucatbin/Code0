using UnityEngine;

namespace ThisGame.Entity.MoveSystem
{
    [CreateAssetMenu(fileName = "Move Data", menuName = "ThisGame/Entity/MoveSystem/MoveData")]
    public class MoveData : ScriptableObject
    {
        public float BaseSpeed;
        public float BaseJumpSpeed;
        public float HoldJumpSpeed;
        public float Gravity;
        public float MaxFallSpeed;
        public float RotationSpeed;
        public float Acceleration;
        public float Deceleration;
    }
}