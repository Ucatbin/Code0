using UnityEngine;

namespace AbilitySystem
{
    public class AbilitySystemBootstrap : MonoBehaviour
    {
        [SerializeField] AbilityData[] _abilityDataList;
        [SerializeField] EventBus _eventBus;
        [SerializeField] CharacterModel _charModel;
        AbilitySysPresenter _abilityPresenter;

        void Start()
        {
            _abilityPresenter = new AbilitySysPresenter(_eventBus, _charModel);
            RegisterExcutions();
        }

        void RegisterExcutions()
        {
            _abilityPresenter.RegisterAbility(_abilityDataList[0], new JumpAbilityExcution());
        }
    }
}