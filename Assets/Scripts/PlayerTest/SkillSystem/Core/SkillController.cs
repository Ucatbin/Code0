using UnityEngine;
using ThisGame.Core;
using System.Collections.Generic;
using System;

namespace ThisGame.Entity.SkillSystem
{
    public abstract class SkillController : BaseController
    {
        [SerializeField] protected SkillModelEntry[] _skillEnties;
        protected Dictionary<Type, SkillModel> _models;

        public override void Initialize()
        {
            _models = new Dictionary<Type, SkillModel>();

            RegisterModels();
        }
        public abstract void RegisterModels();

        public T GetSkill<T>() where T : SkillModel
        {
            if (_models.ContainsKey(typeof(T)))
                return _models[typeof(T)] as T;
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