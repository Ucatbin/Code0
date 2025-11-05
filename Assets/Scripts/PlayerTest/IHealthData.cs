using UnityEngine;

namespace ThisGame.Entity.HealthSystem
{
    [CreateAssetMenu(fileName = "Health Data", menuName = "ThisGame/Entity/HealthSystem")]
    public class HealthData : ScriptableObject
    {
        public float MaxHealth;
    }
}