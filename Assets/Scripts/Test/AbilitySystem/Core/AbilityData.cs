using UnityEngine;

namespace ThisGame.AbilitySystem
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Game/AbilitySystem/New AbilityData")]
    public class AbilityData : ScriptableObject
    {
        public string AbilityName;
        [HideInInspector] public int AbilityHash;
        public int MaxLevel;
        public float CoolDown;
        public int MaxCharges;
        public Sprite Icon;

        void OnValidate()
        {
            AbilityHash = AbilityName.GetHashCode();
        }
    }
}