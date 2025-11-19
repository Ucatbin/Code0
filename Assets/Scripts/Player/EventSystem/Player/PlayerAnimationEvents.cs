using ThisGame.Core;
using ThisGame.Entity.EntitySystem;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    Animator _playerAnimator;
    void Start()
    {
        _playerAnimator = _player?.View.Animator;
    }
    public void OnAnimClipComplete()
    {
        _playerAnimator.SetTrigger("EndTrigger");
    }

    public void OnAttackEventInvoke(AttackEventType type)
    {
        var attackAnimEvent = new AttackAnimationEvent
        {
            AttackEventType = type
        };
        EventBus.Publish(attackAnimEvent);
    }
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
