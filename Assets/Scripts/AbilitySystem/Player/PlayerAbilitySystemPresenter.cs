using UnityEngine;

namespace AbilitySystem
{
    public class PlayerAbilitySysPresenter : AbilitySysPresenter<PlayerModel>
    {
        public PlayerAbilitySysPresenter(EventBus eventBus, PlayerModel charModel) : base(eventBus, charModel)
        {
        }
    }
}