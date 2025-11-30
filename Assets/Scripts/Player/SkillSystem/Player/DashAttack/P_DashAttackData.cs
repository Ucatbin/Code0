using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    [CreateAssetMenu(fileName = "DashAttackData", menuName = "ThisGame/Player/SkillSystem/DashAttackData")]
    public class P_DashAttackData : SkillData
    {

        [Header("Dash Settings")]
        public float MaxDashDistance = 10f;
        public AnimationClip DashAnimationClip;

        [Header("Time Scale Settings")]
        public float SlowTimeScale;
        public float NormalTimeScale;
    }
}