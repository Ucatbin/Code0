using System.Collections.Generic;
using System.Linq;
using ThisGame.Core;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.HealthSystem;
using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    public class P_AttackView : SkillView
    {
        [SerializeField] PlayerController _player;
        [SerializeField] Collider2D _attackCollider;
        [SerializeField] LayerMask _canHit;
        List<Collider2D> _hitTargets = new List<Collider2D>();

        void OnEnable()
        {
            EventBus.Subscribe<AttackAnimationEvent>(this, HandleAttackAnim);
            EventBus.Subscribe<ViewFlip>(this, HandleFlip);
            EventBus.Subscribe<BeKilled>(this, HandleKill);
        }
        void OnDisable()
        {
            EventBus.UnsubscribeAll(this);
        }

        void Awake()
        {
            if (_attackCollider != null)
                _attackCollider.enabled = false;

            _attackCollider.callbackLayers = _canHit;
        }

        void HandleAttackAnim(AttackAnimationEvent @event)
        {
            switch (@event.AttackEventType)
            {
                case AttackEventType.ColliderEnable:
                    _attackCollider.enabled = true;
                    break;
                case AttackEventType.ColliderDisable:
                    _attackCollider.enabled = false;
                    break;
            }
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (_hitTargets.Contains(other)) return;

            var target = other.GetComponentInChildren<EntityController>();
            if (target == null || target.GetController<HealthController>() == null) return;

            var damageInfo = new DamageInfo()
            {
                DamageSource = _player,
                DamageTarget = target,
                DamageAmount = 10
            };
            target.GetController<HealthController>().Model.TakeDamage(damageInfo);
        }

        void HandleKill(BeKilled @event)
        {
        }
        void HandleFlip(ViewFlip @event)
        {
            var scale = transform.localScale;
            scale.x = @event.FacingDir == 1 ? 1 : -1;
            transform.localScale = scale;
        }
    }
}