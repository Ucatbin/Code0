using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public class CheckerController : BaseController
    {
        [System.Serializable]
        public class CheckerModelEntry
        {
            public string CheckerType;
            public CheckerData Data;
            public Transform CheckPoint;
        }
        [SerializeField] CheckerModelEntry[] _checkerEntries;
        Dictionary<string, CheckerModel> _models;

        public override void Initialize()
        {
            _models = new Dictionary<string, CheckerModel>();

            RegisterModels();
        }
        void RegisterModels()
        {
            foreach (var entry in _checkerEntries)
            {
                if (!string.IsNullOrEmpty(entry.CheckerType) && entry.Data != null)
                {
                    var model = CheckerModelFactory.CreateModel(entry.CheckerType, entry.Data, entry.CheckPoint);
                    _models[entry.CheckerType] = model;
                }
            }
        }

        void Update()
        {
            foreach (var model in _models.Values)
            {
                model.Check(model.Data);
            }
        }

        public virtual T GetChecker<T>(string checkerName) where T : CheckerModel
        {
            if (_models.ContainsKey(checkerName))
                return _models[checkerName] as T;
            else
                return null;
        }
    }
}