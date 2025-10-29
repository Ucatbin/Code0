using UnityEngine;

namespace AbilitySystem
{
    public interface IAbilityExcution<T> where T : CharacterModel
    {
        bool CanExcute(AbilityModel ability, T character);
        void Excute(AbilityModel ability, T character);
        void ConsumeResources(AbilityModel ability, T character);
    }
}