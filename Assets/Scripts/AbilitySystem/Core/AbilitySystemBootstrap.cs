using AbilitySystem;
using UnityEngine;

public class AbilitySystemBootstrap<T> : MonoBehaviour where T : CharacterModel
{
    [SerializeField] AbilityData[] _abilityDataList;
    readonly EventBus _eventBus;

    readonly T _charModel;
    AbilitySysPresenter<T> _abilityPresenter;


    void Start()
    {
        _abilityPresenter = new AbilitySysPresenter<T>(_eventBus, _charModel);

        foreach (var a in _abilityDataList)
            _abilityPresenter.RegisterAbility(a);
    }
}
