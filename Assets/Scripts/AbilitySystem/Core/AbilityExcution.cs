
namespace AbilitySystem
{
    public abstract class AbilityExcution : IAbilityExcution
    {
        public virtual bool CanExcute(AbilityModel ability, EntityModel character)
        {
            return ability.IsUnlocked &&
                ability.IsReady &&
                ability.IsReset;
        }

        public abstract void ConsumeResources(AbilityModel ability, EntityModel character);

        public abstract void Excute(AbilityModel ability, EntityModel character);
    }
}