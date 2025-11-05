using ThisGame.AbilitySystem;
using UnityEngine;

namespace ThisGame
{
    [CreateAssetMenu(fileName = "EntityConfig", menuName = "Game/Entities/New EntityConfig")]
    public class EntityConfig : ScriptableObject
    {
        public GameObject Prefab;
        public AbilityData[] Abilities;

        public string PrefabPath => $"Prefabs/Entities/{name}";
    }

    // PlayerConfig.cs
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/Entities/New PlayerConfig")]
    public class PlayerConfig : EntityConfig
    {

    }

    // EnemyConfig.cs  
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Game/Entities/New EnemyConfig")]
    public class EnemyConfig : EntityConfig
    {

    }
}