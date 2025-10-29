using System;
using System.Collections.Generic;

namespace AbilitySystem
{
    public class AbilitySysPresenter
    {
        readonly Dictionary<int, AbilityModel> _abilityModels;
        readonly Dictionary<int, AbilityExcution> _abilityexcutions;

        // Dependency
        readonly CharacterModel _charModel;
        readonly EventBus _eventBus;
        public AbilitySysPresenter(EventBus eventBus, CharacterModel charModel)
        {
            _charModel = charModel;
            _eventBus = eventBus;
            _abilityModels = new Dictionary<int, AbilityModel>();
            _abilityexcutions = new Dictionary<int, AbilityExcution>();
        }

        public void RegisterAbility(AbilityData data, AbilityExcution excution)
        {
            var model = new AbilityModel(data);
            _abilityModels[data.AbilityHash] = model;
            _abilityexcutions[data.AbilityHash] = excution;

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
    }
}