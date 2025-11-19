using ThisGame.Entity.EntitySystem;
using UnityEngine;

namespace ThisGame.Core
{
    public struct DamageInfo
    {
        public EntityController DamageSource;
        public EntityController DamageTarget;
        public float DamageAmount;
        public Vector2 HitDirection;
    }
}