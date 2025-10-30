using UnityEngine;

namespace CharacterSystem
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterSystem")]
    public class EntityData : ScriptableObject
    {
        public float MaxHelth;
        public float BaseSpeed;
    }
}