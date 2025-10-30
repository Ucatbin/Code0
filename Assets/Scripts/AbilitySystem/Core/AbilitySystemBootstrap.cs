using UnityEngine;

namespace AbilitySystem
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
            Debug.Log(_abilityDataList[0]);


            InputEvents.OnJumpPressed += _abilityPresenter.OnJumpPressed;
        }

        void RegisterExcutions()
        {
            _abilityPresenter.RegisterAbility(_abilityDataList[0], new JumpAbilityExcution());
        }
    }
}