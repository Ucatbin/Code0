using UnityEngine;
using ThisGame.Core;

namespace ThisGame.Entity.AbilitySystem
{
    public class AbilityController : BaseController
    {
        [SerializeField] AbilityData _data;
        public AbilityModel Model;

        public override void Initialize()
        {
            Model = new AbilityModel(_data);
        }
    }
}