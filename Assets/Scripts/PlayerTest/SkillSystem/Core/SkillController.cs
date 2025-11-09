using UnityEngine;
using ThisGame.Core;
using System.Collections.Generic;
using System;

namespace ThisGame.Entity.SkillSystem
{
    public abstract class SkillController : BaseController
    {
        [SerializeField] protected SkillModelEntry[] _skillEnties;
        protected Dictionary<string, SkillModel> _models;

        public override void Initialize()
        {
            _models = new Dictionary<string, SkillModel>();

            RegisterModels();
        }
        public abstract void RegisterModels();

        public T GetSkill<T>(string skillName) where T : SkillModel
        {
            if (_models.ContainsKey(skillName))
                return _models[skillName] as T;
            else
                return null;
        }
    }
    
    [Serializable]
    public class SkillModelEntry
    {
        public string SkillName;
        public SkillData Data;
    }
}