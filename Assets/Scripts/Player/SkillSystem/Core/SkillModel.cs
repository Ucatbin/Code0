using System;
using ThisGame.Core;
using ThisGame.Entity.MoveSystem;
using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    public class SkillModel
    {
        protected bool _isUnlocked;
        public bool IsUnlocked => _isUnlocked;
        protected int _currentLevel;
        public int CurrentLevel => _currentLevel;
        protected float _currentCoolDown;
        public float CurrentCoolDown => _currentCoolDown;
        protected int _currentCharges;
        public int CurrentCharges => _currentCharges;
        protected bool _isReady;
        public bool IsReady => _isReady;

        // Dependency
        SkillData _data;
        public SkillData Data => _data;
        public SkillModel(SkillData data)
        {
            _data = data;
            _currentCharges = _data.MaxCharges;
            _isReady = true;
        }
    
        public virtual void HandleSkillButtonPressed(ISkillEvent e) {}
        public virtual void ExecuteSkill(ISkillEvent e)
        {
            ConsumeResources(_data);
        }
        protected virtual void ConsumeResources(SkillData data)
        {
            _isReady = false;
            _currentCharges -= _data.MaxCharges == -1 ? 0 : 1;
        }
        public virtual void StartCoolDown()
        {
        }
        public void Unlock() => _isUnlocked = true;
    }
}