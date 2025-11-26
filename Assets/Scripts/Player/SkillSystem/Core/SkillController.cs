using UnityEngine;
using ThisGame.Core;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ThisGame.Entity.SkillSystem
{
    public class SkillController : BaseController
    {
        [SerializeField] protected List<SkillModel> _unlockedSkills = new List<SkillModel>();

        public override void Initialize()
        {
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
            return _unlockedSkills.OfType<T>().FirstOrDefault();
        }
    }
}