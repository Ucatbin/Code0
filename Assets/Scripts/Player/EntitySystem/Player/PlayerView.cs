using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.EntitySystem
{
    public class PlayerView : MonoBehaviour
    {
        public Animator Animator;

        void OnEnable()
        {

        }
        void OnDisable()
        {
            EventBus.UnsubscribeAll(this);
        }
        public void HandleStateChange(StateChange @event)
        {
            Animator.SetBool(@event.LastStateAnim, false);
            Animator.SetBool(@event.NewStateAnim, true);
        }
        public void HandleFlip(ViewFlip @event, PlayerController player)
        {
            player.Flip();
            var scale = transform.localScale;
            scale.x = @event.FacingDir == 1 ? 1 : -1;
            transform.localScale = scale;
        }
    }
}