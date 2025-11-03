using ThisGame.AbilitySystem;
using ThisGame.EntitySystem;
using ThisGame.Events.AbilityEvents;
using UnityEngine;

public class AttackAbilityModel : AbilityModel<AttackAbilityData>
{
    public override void Excute(EntityModel entity)
    {
        base.Excute(entity);

        var eventBus = ServiceLocator.Get<IEventBus>();
        eventBus.Publish(new Plr_AttackExecTriggerStart());
    }
    public override void EndExecute(EntityModel entity)
    {
        base.EndExecute(entity);

        var eventBus = ServiceLocator.Get<IEventBus>();
        eventBus.Publish(new Plr_AttackExecTriggerEnd());
    }
}
