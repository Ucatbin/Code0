using UnityEngine;

namespace AbilitySystem
{
    public class AbilityExcution : IAbilityExcution<PlayerModel>
    {
        public bool CanExcute(AbilityModel ability, PlayerModel character)
        {
            throw new System.NotImplementedException();
        }

        public void ConsumeResources(AbilityModel ability, PlayerModel character)
        {
            throw new System.NotImplementedException();
        }

        public void Excute(AbilityModel ability, PlayerModel character)
        {
            throw new System.NotImplementedException();
        }
    }
}