using UnityEngine;

namespace ThisGame.AbilitySystem
{
    public class AbilitySystemBootstrap : MonoBehaviour
    {
        [SerializeField] AbilityData[] _abilityDataList;
        public AbilitySysPresenter AbilityPresenter;

        void Start()
        {
            // Services

            AbilityPresenter = new AbilitySysPresenter();
            RegisterExcutions();
        }

        void RegisterExcutions()
        {
            AbilityPresenter.RegisterAbility<JumpAbilityModel>(_abilityDataList[0]);
            AbilityPresenter.RegisterAbility<AttackAbilityModel>(_abilityDataList[1]);
        }
    }
}