using UnityEngine;

namespace ThisGame.AbilitySystem
{
    public class AbilitySystemBootstrap : MonoBehaviour
    {
        [SerializeField] IAbilityData[] _abilityDataList;
        public AbilityPresenter AbilityPresenter;

        void Start()
        {
            // Services

            AbilityPresenter = new AbilityPresenter();
            RegisterExcutions();
        }

        void RegisterExcutions()
        {
            AbilityPresenter.RegisterAbility<JumpAbilityModel,JumpAbilityData>((JumpAbilityData)_abilityDataList[0]);
            AbilityPresenter.RegisterAbility<AttackAbilityModel,AttackAbilityData>((AttackAbilityData)_abilityDataList[1]);
        }
    }
}