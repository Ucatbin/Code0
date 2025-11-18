using UnityEngine;

namespace ThisGame.Entity.MoveSystem
{
    [CreateAssetMenu(fileName = "Player MoveData", menuName = "ThisGame/Player/MoveSystem/PlayerMoveData")]

    public class PlayerMoveData : MoveData
    {
        public float JumpInputWindow;
        public float WallSlideSpeed;
        public float WallSlideAcceleration;
        public Vector3 WallJumpDirection;
    }
}