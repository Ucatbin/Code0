using UnityEngine;

namespace ThisGame.AbilitySystem
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Game/AbilitySystem/New AbilityData")]
    public class AbilityData : ScriptableObject, IAbilityData
    {
        public string AbilityName { get; }
        public int AbilityHash { get => AbilityName.GetHashCode(); }
        public int MaxLevel { get; }
        public float CoolDown { get; }
        public int MaxCharges { get; }
        public Sprite Icon { get; }
    }
}