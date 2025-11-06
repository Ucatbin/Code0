using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.HealthSystem
{
    public class HealthController : BaseController
    {
        [SerializeField] HealthData _data;
        public HealthModel Model;

        public override void Initialize()
        {
            Model = new HealthModel(_data);
        }
    }
}