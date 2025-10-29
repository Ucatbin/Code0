using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilitySysPresenter<T> where T : CharacterModel
    {
        protected readonly Dictionary<int, AbilityModel> _abilities;
        protected readonly Dictionary<int, AbilityExcution> _excutions;

        // Dependency
        readonly T _charModel;
        readonly EventBus _eventBus;
        public AbilitySysPresenter(EventBus eventBus, T charModel)
        {
            _charModel = charModel;
            _eventBus = eventBus;
            _abilities = new Dictionary<int, AbilityModel>();
        }

        public void RegisterAbility(AbilityData data)
        {
            var model = new AbilityModel(data);
            _abilities[data.AbilityHash] = model;

            model.OnAbilityUpgraded += HandelAbilityUpgraded;
        }
        public void UnregisterAbility(AbilityData data)
        {
            if (_abilities.TryGetValue(data.AbilityHash, out var model))
            {
                _abilities.Remove(data.AbilityHash);

                model.OnAbilityUpgraded -= HandelAbilityUpgraded;
            }
        }

        void HandelAbilityUpgraded(AbilityModel model)
        {

        }
    }
}