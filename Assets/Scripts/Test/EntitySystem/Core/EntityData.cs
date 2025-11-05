using UnityEngine;

namespace ThisGame.EntitySystem
{
    [CreateAssetMenu(fileName = "EntityData", menuName = "Game/EntitySystem/New EntityData")]
    public class EntityData : ScriptableObject, IEntityData
    {
        public string EntityName { get; }

        public int EntityHash { get => EntityName.GetHashCode();  }

        public float MaxHealth { get; }

        public float BaseSpeed { get; }
    }
}