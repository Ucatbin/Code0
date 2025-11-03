using UnityEngine;

namespace ThisGame.EntitySystem
{
    [CreateAssetMenu(fileName = "EntityData", menuName = "Game/EntitySystem/New EntityData")]
    public class EntityData : ScriptableObject
    {
        public string EntityName;
        [HideInInspector] public int EntityHash;
        public float MaxHelth;
        public float BaseSpeed;

        void OnValidate()
        {
            EntityHash = EntityName.GetHashCode();
        }
    }
}