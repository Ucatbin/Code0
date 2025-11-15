using UnityEngine;

namespace ThisGame.Entity.EntitySystem
{
    public class PlayerView : MonoBehaviour
    {
        public Animator Animator;

        public void HandleStateChange(StateChange @event)
        {
            Animator.SetBool(@event.LastStateAnim, false);
            Animator.SetBool(@event.NewStateAnim, true);
        }
        public void FlipSprite(int facingDir)
        {
            var scale = transform.localScale;
            scale.x = facingDir == 1 ? 1 : -1;
            transform.localScale = scale;
        }
    }
}