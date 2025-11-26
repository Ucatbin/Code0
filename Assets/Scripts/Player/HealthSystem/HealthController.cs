using System.ComponentModel;
using ThisGame.Core;
using ThisGame.Entity.EntitySystem;
using UnityEngine;

namespace ThisGame.Entity.HealthSystem
{
    public class HealthController : BaseController
    {
        [SerializeField] GameObject _controllerObj;
        [SerializeField] GameObject _thisEntity;
        [SerializeField] HealthData _data;
        public HealthModel Model;

        void OnEnable()
        {
            EventBus.Subscribe<BeKilled>(this, HandleDie);
        }
        void OnDisable()
        {
            
        }
        public override void Initialize()
        {
            Model = new HealthModel(_data);
        }

        void HandleDie(BeKilled e)
        {
            if (e.TargetEntity == _controllerObj)
            {
                Destroy(_thisEntity);
            }
        }
    }
}