using Ucatbin.AbilitySystem;
using Ucatbin.Events.AbilityEvents;

public class Plr_JumpExec : AbilityExecution
{
    public override bool CanExecute(AbilityModel ability, EntityModel entity)
    {
        return base.CanExecute(ability, entity) &&
            !entity.IsBusy;
    }
    public override void ConsumeResources(AbilityModel ability, EntityModel entity)
    {
        // ability.CurrentCharges--;
    }

    public override void Excute(AbilityModel ability, EntityModel entity)
    {
        base.Excute(ability, entity);

        var eventBus = ServiceLocator.Get<IEventBus>();
        eventBus.Publish(new JumpExcuteTriggerStart());
    }
    public override void End(AbilityModel ability, EntityModel entity)
    {
        var eventBus = ServiceLocator.Get<IEventBus>();
        eventBus.Publish(new JumpExcuteTriggerEnd());
    }
}