using System;

namespace AbilitySystem
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
        public readonly AbilityData Data;
        public readonly AbilityExcution Excution;
        public AbilityModel(AbilityData data, AbilityExcution excution)
        {
            Data = data;
            Excution = excution;
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