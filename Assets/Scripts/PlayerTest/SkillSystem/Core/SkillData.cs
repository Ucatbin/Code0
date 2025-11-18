using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "ThisGame/Entity/SkillSystem/SkillData")]
    public class SkillData : ScriptableObject
    {
        public int MaxLevel;
        public float CoolDown;
        public int MaxCharges;
        public bool IsUnlocked;
    }
}