using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    [CreateAssetMenu(fileName = "AttackData", menuName = "ThisGame/Player/SkillSystem/AttackData")]
    public class P_AttackData : SkillData
    {
        public float AttackDuration;
    }
}