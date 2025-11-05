using System.Collections.Generic;
using UnityEngine;
using ThisGame.Events.AbilityEvents;

namespace ThisGame.AbilitySystem
{
    public class AbilityPresenter
    {
        readonly Dictionary<int, IAbilityModel> _abilityModels;
        // Dependency
        public AbilityPresenter()
        {
            _abilityModels = new Dictionary<int, IAbilityModel>();

            var eventBus = ServiceLocator.Get<IEventBus>();
            eventBus.Subscribe<AbilityButtonPressed>(HandleAbilityButtonPressed);
            eventBus.Subscribe<AbilityButtonReleased>(HandleAbilityButtonReleased);
        }

        public void RegisterAbility<TModel, TData>(TData data)
            where TModel : IAbilityModel, new()
            where TData : IAbilityData
        {
            // Debug
            if (_abilityModels.ContainsKey(data.AbilityHash))
            {
                Debug.LogWarning($"{data.AbilityName} already registered!");
                return;
            }

            var model = AbilityFactory.CreateAbility<TModel, TData>(data);
            _abilityModels[data.AbilityHash] = model;
            // TODO:Just for test
            model.IsUnlocked = true;
        }

        void HandleAbilityButtonPressed(AbilityButtonPressed abilityEvent)
        {
            if (_abilityModels.TryGetValue(abilityEvent.AbilityHash, out var model))
            {
                model.Excute(null);
            }
        }
        void HandleAbilityButtonReleased(AbilityButtonReleased abilityEvent)
        {
            if (_abilityModels.TryGetValue(abilityEvent.AbilityHash, out var model))
                model.IsReset = true;
        }

        public T GetAbilityModel<T>(string abilityName) where T : class, IAbilityModel
        {
            if (_abilityModels.TryGetValue(abilityName.GetHashCode(), out var model))
                return model as T;
            return null;
        }
    }
}