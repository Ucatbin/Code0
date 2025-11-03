using ThisGame.AbilitySystem;
using ThisGame.EntitySystem;
using ThisGame.Events.AbilityEvents;
using UnityEngine;

public class JumpAbilityModel : AbilityModel<JumpAbilityData>
{
    public override void Excute(EntityModel entity)
    {
        base.Excute(entity);

        if (!CanExecute(entity)) return;
        ConsumeResources(entity);
        var eventBus = ServiceLocator.Get<IEventBus>();
        eventBus.Publish(new JumpExecuteTriggerStart());
    }
    public override void EndExecute(EntityModel entity)
    {
        base.EndExecute(entity);

        var eventBus = ServiceLocator.Get<IEventBus>();
        eventBus.Publish(new JumpExecuteTriggerEnd());
    }
}