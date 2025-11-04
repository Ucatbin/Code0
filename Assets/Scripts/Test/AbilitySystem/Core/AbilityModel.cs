using System;
using ThisGame.EntitySystem;
using ThisGame.Events.AbilityEvents;
using UnityEngine;

namespace ThisGame.AbilitySystem
{
    [Serializable]
    public class AbilityModel<TData> : IAbilityModel where TData : AbilityData
    {
        // Info
        public int CurrentLevel { get; set; }
        public int CurrentCharges { get; set; }
        public float CurrentCoolDown { get; set; }
        // State
        public bool IsUnlocked { get; set; }
        public bool IsReady { get; set; }
        public bool IsReset { get; set; }

        // Dependency
        public TData Data;
        public AbilityData AData => Data;
        public void Initialize(AbilityData data)
        {
            Data = data as TData;
            IsReady = true;
            IsReset = true;
            CurrentCharges = Data.MaxCharges;
        }

        // Actions
        public Action<IAbilityModel> OnCoolDownChanged;
        public Action<IAbilityModel> OnAbilityUpgraded;

        // Logics
        public void Upgrade(int deltaLevel)
        {
            CurrentLevel += deltaLevel;
            OnAbilityUpgraded?.Invoke(this);
        }

        public virtual bool CanExecute(IEntityModel entity)
        {
            return IsUnlocked &&
                IsReady &&
                IsReset &&
                CurrentCharges > 0;
        }

        public virtual void Excute(IEntityModel entity)
        {
            ConsumeResources(entity);
            var eventBus = ServiceLocator.Get<IEventBus>();
            eventBus.Publish(new AbilityExecuted(Data.AbilityName));
        }

        public virtual void ConsumeResources(IEntityModel entity)
        {
            CurrentCharges -= Data.MaxCharges == -1 ? 1 : 0;
            IsReady = false;
            IsReset = false;
        }
        public virtual void StartCoolDown()
        {
            TimerManager.Instance.AddTimer(
                Data.CoolDown,
                () => EndExecute(null)
            );
        }
        public virtual void EndExecute(IEntityModel entity)
        {
            var eventBus = ServiceLocator.Get<IEventBus>();
            eventBus.Publish(new AbilityCompleted(Data.AbilityName));
        }
        public virtual void Refresh()
        {
            CurrentCharges += CurrentCharges == Data.MaxCharges ? 0 : Data.MaxCharges;
        }
    }
}