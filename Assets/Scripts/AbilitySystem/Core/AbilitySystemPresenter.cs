using System.Collections.Generic;
using Ucatbin.Events.AbilityEvents;

namespace Ucatbin.AbilitySystem
{
    public class AbilitySysPresenter
    {
        readonly Dictionary<int, AbilityModel> _abilityModels;

        // Dependency
        public AbilitySysPresenter()
        {
            _abilityModels = new Dictionary<int, AbilityModel>();
            RegisterAbilityEvents();
        }

        public void RegisterAbility(AbilityData data, AbilityExecution excution)
        {
            var model = new AbilityModel(data, excution);
            _abilityModels[data.AbilityHash] = model;

            model.OnAbilityUpgraded += HandelAbilityUpgraded;
        }
        public void UnregisterAbility(AbilityData data)
        {
            if (_abilityModels.TryGetValue(data.AbilityHash, out var model))
            {
                _abilityModels.Remove(data.AbilityHash);

                model.OnAbilityUpgraded -= HandelAbilityUpgraded;
            }
        }

        void HandelAbilityUpgraded(AbilityModel model)
        {

        }

        void HandleAbilityInputPressed(AbilityInputTriggerPressed abilityEvent)
        {
            if (_abilityModels.TryGetValue(abilityEvent.AbilityHash, out var model))
                model.Execution.Excute(model, null);
        }
        void HandleAbilityInputReleased(AbilityInputTriggerReleased abilityEvent)
        {
            if (_abilityModels.TryGetValue(abilityEvent.AbilityHash, out var model))
                model.Execution.End(model, null);
        }
        
        void RegisterAbilityEvents()
        {
            var eventBus = ServiceLocator.Get<IEventBus>();

            eventBus.Subscribe<AbilityInputTriggerPressed>(HandleAbilityInputPressed);
            eventBus.Subscribe<AbilityInputTriggerReleased>(HandleAbilityInputReleased);
        }
    }
}