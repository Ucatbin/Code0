
namespace AbilitySystem
{
    public interface IAbilityExcution
    {
        bool CanExcute(AbilityModel ability, EntityModel character);
        void Excute(AbilityModel ability, EntityModel character);
        void ConsumeResources(AbilityModel ability, EntityModel character);
    }
}