using Ucatbin.AbilitySystem;
using Ucatbin.Events.AbilityEvents;
using UnityEngine;

public class Plr_AttackExec : AbilityExecution
{
    public override bool CanExecute(AbilityModel ability, EntityModel entity)
    {
        return base.CanExecute(ability, entity) &&
            !entity.IsBusy;
    }

    public override void ConsumeResources(AbilityModel ability, EntityModel entity)
    {
        throw new System.NotImplementedException();
    }

    public override void Excute(AbilityModel ability, EntityModel entity)
    {
        base.Excute(ability, entity);

        var eventBus = ServiceLocator.Get<IEventBus>();
        eventBus.Publish(new Plr_AttackExecTriggerStart());
    }
    public override void End(AbilityModel ability, EntityModel entity)
    {
        var eventBus = ServiceLocator.Get<IEventBus>();
        eventBus.Publish(new Plr_AttackExecTriggerEnd());
    }
}
