using UnityEngine;
using ThisGame.Core;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ThisGame.Entity.SkillSystem
{
    public class SkillController : BaseController
    {
        protected Dictionary<Type, SkillModel> _models;
        [SerializeField] protected List<SkillModel> _unlockedSkills;

        public override void Initialize()
        {
            _models = new Dictionary<Type, SkillModel>();
            _unlockedSkills = new List<SkillModel>();

            RegisterModels();
        }
        public virtual void RegisterModels() { }

        public void UnlockSkill<T>(T thisSkill) where T : SkillModel
        {
            if (!thisSkill.IsUnlocked)
            {
                thisSkill.Unlock();
                _unlockedSkills.Add(thisSkill);
            }
            else
                Debug.LogError($"Skill: {thisSkill} already unlocked");
        }
        public T GetSkill<T>() where T : SkillModel
        {
            if (_models.ContainsKey(typeof(T)))
                return _models[typeof(T)] as T;
            else
                return null;
        }
    }
}