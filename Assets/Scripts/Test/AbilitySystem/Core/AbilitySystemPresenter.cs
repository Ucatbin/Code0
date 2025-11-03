using System.Collections.Generic;
using UnityEngine;
using ThisGame.Events.AbilityEvents;
using System;
using ThisGame.EntitySystem;

namespace ThisGame.AbilitySystem
{
    public class AbilitySysPresenter
    {
        readonly Dictionary<int, IAbilityModel> _abilityModels;
        // Dependency
        public AbilitySysPresenter()
        {
            _abilityModels = new Dictionary<int, IAbilityModel>();

            var eventBus = ServiceLocator.Get<IEventBus>();
            eventBus.Subscribe<AbilityInputTriggerPressed>(HandleAbilityInputPressed);
            eventBus.Subscribe<AbilityInputTriggerReleased>(HandleAbilityInputReleased);
        }

        public void RegisterAbility<TModel>(AbilityData data)
            where TModel : IAbilityModel, new()
        {
            // Debug
            if (_abilityModels.ContainsKey(data.AbilityHash))
            {
                Debug.LogWarning($"{data.AbilityName} already registered!");
                return;
            }

            var model = new TModel();
            model.Initialize(data);
            _abilityModels[data.AbilityHash] = model;
            model.IsUnlocked = true;
        }
        public void UnregisterAbility(AbilityData data)
        {
            if (_abilityModels.TryGetValue(data.AbilityHash, out var model))
            {
                _abilityModels.Remove(data.AbilityHash);
            }
        }

        void HandleAbilityInputPressed(AbilityInputTriggerPressed abilityEvent)
        {
            if (_abilityModels.TryGetValue(abilityEvent.AbilityHash, out var model))
            {
                model.Excute(null);
            }
        }
        void HandleAbilityInputReleased(AbilityInputTriggerReleased abilityEvent)
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