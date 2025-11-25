using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    [CreateAssetMenu(fileName = "DashAttackData", menuName = "ThisGame/Player/SkillSystem/DashAttackData")]
    public class P_DashAttackData : SkillData
    {
        public float SlowTimeScale;
        public float NormalTimeScale;
    }
}