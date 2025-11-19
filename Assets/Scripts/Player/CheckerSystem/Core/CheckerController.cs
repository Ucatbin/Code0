using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public abstract class CheckerController : BaseController
    {
        [SerializeField] protected CheckerModelEntry[] _checkerEntries;
        protected Dictionary<Type, CheckerModel> _models;

        public override void Initialize()
        {
            _models = new Dictionary<Type, CheckerModel>();

            RegisterModels();
        }
        public abstract void RegisterModels();

        protected void Update()
        {
            foreach (var model in _models.Values)
            {
                model.Check(model.Data);
            }
        }

        public T GetChecker<T>() where T : CheckerModel
        {
            if (_models.ContainsKey(typeof(T)))
                return _models[typeof(T)] as T;
            else
                return null;
        }
    }
    [Serializable]
    public class CheckerModelEntry
    {
        public string CheckerName;
        public CheckerData Data;
        public Transform CheckPoint;
    }
}