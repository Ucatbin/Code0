using UnityEngine;

namespace ThisGame.Entity.EntitySystem
{
    public class PlayerView : MonoBehaviour
    {
        public Animator Animator;

        public void HandleStateChange(StateChange e)
        {
            Animator.SetBool(e.LastStateAnim, false);
            Animator.SetBool(e.NewStateAnim, true);
        }
        public void FlipSprite(int facingDir)
        {
            var scale = transform.localScale;
            scale.x = facingDir == 1 ? 1 : -1;
            transform.localScale = scale;
        }
    }
}