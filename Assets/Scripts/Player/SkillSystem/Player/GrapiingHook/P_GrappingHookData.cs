using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    [CreateAssetMenu(fileName = "GrappingHookData", menuName = "ThisGame/Player/SkillSystem/GrappingHookData")]
    public class P_GrappingHookData : SkillData
    {
        [Header("Check useable")]
        public float MaxDetectDist;
        public LayerMask CanHookLayer;
        public float MaxLineDist;
        public float MinLineDist;
        public float DeltaSpeed;

        [Header("Movement")]
        public float SwingForce;
        public float MaxSwingSpeed;
        public float ExtendSpeed;

            [Header("自动缩绳设置")]
        public bool EnableAutoShortenRope = true;
        public float MinGroundClearance = 0.15f;       // 最小离地高度
        public float RopeShortenSpeed = 25f;           // 绳长缩短速度
        public float RopeExtendSpeed = 15f;            // 绳长恢复速度
        public float MinRopeLength = 1.5f;             // 最小绳长
        public float MaxRopeLength = 10f;              // 最大绳长
        public float GroundDetectAhead = 1.5f;         // 地面检测预判距离
        public LayerMask GroundLayerMask;              // 地面层级
    }
}