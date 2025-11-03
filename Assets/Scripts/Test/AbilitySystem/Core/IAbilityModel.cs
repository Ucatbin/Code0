using ThisGame.EntitySystem;
using UnityEngine;

namespace ThisGame.AbilitySystem
{
    public interface IAbilityModel
    {
        int CurrentLevel { get; set; }
        int CurrentCharges { get; set; }
        float CurrentCoolDown { get; set; }
        bool IsUnlocked { get; set; }
        bool IsReady { get; set; }
        bool IsReset { get; set; }

        void Initialize(AbilityData data);
        void Upgrade(int deltaLevel);

        bool CanExecute(EntityModel entity);
        void Excute(EntityModel entity);
        
        void ConsumeResources(EntityModel entity);
        void StartCoolDown();
        void EndExecute(EntityModel entity);
        void Refresh();
    } 
}