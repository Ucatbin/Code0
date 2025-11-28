using UnityEngine;
using ThisGame.Core;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ThisGame.Entity.SkillSystem
{
    public class SkillController : BaseController
    {
        protected Dictionary<Type, (SkillModel model, SkillEntry entry)> _unlockedSkills = new Dictionary<Type, (SkillModel model, SkillEntry entry)>();
        public override void Initialize()
        {
            RegisterModels();
        }
        public virtual void RegisterModels() { }

        public void UnlockSkill<T>(T model, SkillEntry entry) where T : SkillModel
        {
            if (!model.IsUnlocked)
            {
                model.Unlock();
                _unlockedSkills[typeof(T)] = (model, entry);
            }
            else
                Debug.LogError($"Skill: {model} already unlocked");
        }
        public (T model, SkillEntry entry) GetSkill<T>() where T : SkillModel
        {
            if (_unlockedSkills.TryGetValue(typeof(T), out var skillInfo))
                return (skillInfo.model as T, skillInfo.entry);
            return (null, null);
        }
    }
}