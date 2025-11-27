using ThisGame.Core;
using ThisGame.Entity.BuffSystem;
using UnityEngine;

namespace ThisGame.Entity.HealthSystem
{
    public class HealthController : BaseController
    {
        [SerializeField] GameObject _controllerObj;
        [SerializeField] GameObject _thisEntity;
        [SerializeField] HealthData _data;
        public HealthModel Model;
        [SerializeField] SimpleShatterer _shatterer;

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
                if (_controllerObj != null && _controllerObj.GetComponent<P_BuffController>() != null)
                {
                    var targetBuffController = _controllerObj.GetComponent<P_BuffController>();
                    foreach (var buff in targetBuffController.ActiveBuffs)
                        buff.Data.OnBekill.Apply(buff);
                }
                if (e.Killer != null && e.Killer.GetComponent<BuffController>() != null)
                {
                    var killerBuffController = e.Killer.GetComponent<BuffController>();
                    foreach (var buff in killerBuffController.ActiveBuffs)
                    {
                        buff.Data.OnKill?.Apply(buff);
                    }
                }
                _shatterer.ShatterOnDeath();
            }
        }
    }
}