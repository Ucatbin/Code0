using UnityEngine;

namespace ThisGame.EntitySystem
{
    [CreateAssetMenu(fileName = "EntityData", menuName = "Game/EntitySystem/New EntityData")]
    public class EntityData : ScriptableObject
    {
        public float MaxHelth;
        public float BaseSpeed;
    }
}