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
    }
}