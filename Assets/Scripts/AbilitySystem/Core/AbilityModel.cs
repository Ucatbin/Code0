using System;

namespace AbilitySystem
{
    public class AbilityModel
    {
        public bool IsUnlocked;
        public bool IsReady;
        public bool IsReset;
        public int CurrentLevel;
        public int CurrentCharges;
        public float CurrentCoolDown;

        // Dependency
        public readonly AbilityData Data;
        public AbilityModel(AbilityData data)
        {
            Data = data;
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