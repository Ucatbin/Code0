using ThisGame.Entity.MoveSystem;
using UnityEngine;

namespace ThisGame.Entity.AbilitySystem
{
    public class AbilityModel
    {
        int _currentLevel;
        public int CurrentLevel => _currentLevel;
        float _currentCoolDown;
        public float CurrentCoolDown => _currentCoolDown;
        int _currentCharges;
        public int CurrentCharges => _currentCharges;

        // Dependency
        AbilityData _data;
        public AbilityData Data => _data;
        public AbilityModel(AbilityData data)
        {
            _data = data;
        }
    }
}