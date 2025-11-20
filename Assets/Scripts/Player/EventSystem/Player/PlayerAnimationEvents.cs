using ThisGame.Core;
using ThisGame.Entity.EntitySystem;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    #region Universal
    public void OnAnimClipComplete()
    {
        _animator.SetTrigger("EndTrigger");
    }
    #endregion

    #region Attack
    public void OnAttackEventInvoke(AttackEventType type)
    {
        var attackAnimEvent = new AttackAnimationEvent
        {
            AttackEventType = type
        };
        EventBus.Publish(attackAnimEvent);
    }
    #endregion
}
    public enum AttackEventType
    {
        ColliderEnable,
        ColliderDisable
    }
    public struct AttackAnimationEvent
    {
        public AttackEventType AttackEventType;
    }