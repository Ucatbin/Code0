using UnityEngine;

namespace ThisGame.Entity.MoveSystem
{
    [CreateAssetMenu(fileName = "Player MoveData", menuName = "ThisGame/Player/MoveSystem/MoveData")]

    public class PlayerMoveData : MoveData
    {
        public float JumpInputWindow;
        public float WallSlideSpeed;
        public Vector3 WallJumpDirection;
    }
}