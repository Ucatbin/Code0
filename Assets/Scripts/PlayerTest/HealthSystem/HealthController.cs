using UnityEngine;

namespace ThisGame.Entity.HealthSystem
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] HealthData _data;
        public HealthModel Model;

        void Initialize()
        {
            Model = new HealthModel(_data);
        }
    }
}