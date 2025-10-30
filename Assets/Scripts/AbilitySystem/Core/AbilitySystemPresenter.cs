using System;
using System.Collections.Generic;

namespace AbilitySystem
{
    public class AbilitySysPresenter
    {
        readonly Dictionary<int, AbilityModel> _abilityModels;
        readonly Dictionary<int, AbilityExcution> _abilityexcutions;

        // Dependency
        public AbilitySysPresenter()
        {
            _abilityModels = new Dictionary<int, AbilityModel>();
            _abilityexcutions = new Dictionary<int, AbilityExcution>();
        }

        public void RegisterAbility(AbilityData data, AbilityExcution excution)
        {
            var model = new AbilityModel(data, excution);
            _abilityModels[data.AbilityHash] = model;
            _abilityexcutions[data.AbilityHash] = excution;

            model.OnAbilityUpgraded += HandelAbilityUpgraded;
        }
        public void UnregisterAbility(AbilityData data)
        {
            if (_abilityModels.TryGetValue(data.AbilityHash, out var model))
            {
                _abilityModels.Remove(data.AbilityHash);
                _abilityexcutions.Remove(data.AbilityHash);

                model.OnAbilityUpgraded -= HandelAbilityUpgraded;
            }
        }

        void HandelAbilityUpgraded(AbilityModel model)
        {

        }

        public void OnJumpPressed()
        {
            int abilityId = "Jump".GetHashCode();
            
            if (_abilityexcutions.TryGetValue(abilityId, out var execution) && 
                _abilityModels.TryGetValue(abilityId, out var model))
            {
                execution.Excute(model, null);
            }
        }
    }
}