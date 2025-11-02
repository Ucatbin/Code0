using System;

namespace Ucatbin.AbilitySystem
{
    public class AbilityModel
    {
        // Info
        public int CurrentLevel;
        public int CurrentCharges;
        public float CurrentCoolDown;
        // State
        public bool IsUnlocked;
        public bool IsReady;
        public bool IsReset;

        // Dependency
        AbilityData _data;
        public AbilityData Data => _data;
        AbilityExecution _execution;
        public AbilityExecution Execution => _execution;
        public virtual void Initialize(AbilityData data, AbilityExecution excution)
        {
            _data = data;
            _execution = excution;
        }

        // Actions
        public Action<AbilityModel> OnCoolDownChanged;
        public Action<AbilityModel> OnAbilityUpgraded;

        // Logics
        public void StartCoolDown()
        {
            CurrentCoolDown = Data.CoolDown;
        }
        public void Upgrade(int deltaLevel)
        {
            CurrentLevel += deltaLevel;
            OnAbilityUpgraded?.Invoke(this);
        }
    }
}