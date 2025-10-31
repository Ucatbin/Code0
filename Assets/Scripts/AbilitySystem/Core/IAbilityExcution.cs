
namespace Ucatbin.AbilitySystem
{
    public interface IAbilityExcution
    {
        bool CanExecute(AbilityModel ability, EntityModel character);
        void Excute(AbilityModel ability, EntityModel character);
        void ConsumeResources(AbilityModel ability, EntityModel character);
    }
}