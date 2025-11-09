using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    [CreateAssetMenu(fileName = "Skill Data", menuName = "ThisGame/Entity/SkillSystem/SkillData")]
    public class SkillData : ScriptableObject
    {
        public int MaxLevel;
        public float CoolDown;
        public int MaxCharges;
        public bool IsUnlocked;
    }
}