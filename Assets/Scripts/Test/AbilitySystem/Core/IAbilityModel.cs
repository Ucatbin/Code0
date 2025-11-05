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

        void Initialize(IAbilityData data);

        void Upgrade(int deltaLevel);

        bool CanExecute(IEntityModel entity);
        void Excute(IEntityModel entity);
        
        void ConsumeResources(IEntityModel entity);
        void StartCoolDown();
        void EndExecute(IEntityModel entity);
        void Refresh();
    } 
}