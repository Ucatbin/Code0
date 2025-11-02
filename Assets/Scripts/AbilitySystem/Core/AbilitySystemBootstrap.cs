using UnityEngine;

namespace Ucatbin.AbilitySystem
{
    public class AbilitySystemBootstrap : MonoBehaviour
    {
        [SerializeField] AbilityData[] _abilityDataList;
        AbilitySysPresenter _abilityPresenter;

        void Start()
        {
            // Services

            _abilityPresenter = new AbilitySysPresenter();
            RegisterExcutions();
        }

        void RegisterExcutions()
        {
            _abilityPresenter.RegisterAbility<AttackAbilityModel>(_abilityDataList[0], new Plr_JumpExec());
            _abilityPresenter.RegisterAbility<JumpAbilityModel>(_abilityDataList[1], new Plr_AttackExec());
        }
    }
}