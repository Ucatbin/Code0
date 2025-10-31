
namespace Ucatbin.AbilitySystem
{
    public abstract class AbilityExecution : IAbilityExcution
    {
        public virtual bool CanExecute(AbilityModel ability, EntityModel entity)
        {
            return ability.IsUnlocked &&
                ability.IsReady &&
                ability.IsReset;
        }

        public abstract void ConsumeResources(AbilityModel ability, EntityModel entity);

        public virtual void Excute(AbilityModel ability, EntityModel entity)
        {
            if (!CanExecute(ability, entity)) return;
            ConsumeResources(ability, entity);
        }
        public abstract void End(AbilityModel ability, EntityModel entity);
    }
}