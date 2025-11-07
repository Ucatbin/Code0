using UnityEngine;

namespace ThisGame.Entity.AbilitySystem
{
    [CreateAssetMenu(fileName = "Ability Data", menuName = "ThisGame/Entity/AbilitySystem/AbilityData")]
    public class AbilityData : ScriptableObject
    {
        public int MaxLevel;
        public float CoolDown;
        public int MaxCharges;
        public bool IsUnlocked;
        public bool IsReady;
        public bool IsReleased;
    }
}