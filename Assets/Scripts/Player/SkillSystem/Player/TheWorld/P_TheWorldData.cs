using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    [CreateAssetMenu(fileName = "TheWorldData", menuName = "ThisGame/Player/SkillSystem/TheWorldData")]
    public class P_TheWorldData : SkillData
    {
        public float SlowTimeScale;
        public float NormalTimeScale;
    }
}