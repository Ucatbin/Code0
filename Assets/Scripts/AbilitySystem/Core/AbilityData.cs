using UnityEngine;

namespace Ucatbin.AbilitySystem
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Game/AbilitySystem/New AbilityData")]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] string _abilityName;
        public int AbilityHash;
        public int MaxLevel;
        public float CoolDown;
        public int MaxCharges;
        public Sprite Icon;

        void OnValidate()
        {
            AbilityHash = _abilityName.GetHashCode();
        }
    }
}