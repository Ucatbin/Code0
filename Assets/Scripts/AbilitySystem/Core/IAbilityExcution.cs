
namespace AbilitySystem
{
    public interface IAbilityExcution
    {
        bool CanExcute(AbilityModel ability, CharacterModel character);
        void Excute(AbilityModel ability, CharacterModel character);
        void ConsumeResources(AbilityModel ability, CharacterModel character);
    }
}