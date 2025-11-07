using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public class CheckerController : BaseController
    {
        [System.Serializable]
        public class CheckerModelEntry
        {
            public string CheckerName;
            public CheckerData Data;
            public Transform CheckPoint;
        }
        [SerializeField] private CheckerModelEntry[] _modelEntries;
        Dictionary<string, CheckerModel> _models;

        public override void Initialize()
        {
            _models = new Dictionary<string, CheckerModel>();

            foreach (var entry in _modelEntries)
            {
                if (!string.IsNullOrEmpty(entry.CheckerName) && entry.Data != null)
                {
                    var model = new CheckerModel(entry.Data, entry.CheckPoint);
                    _models[entry.CheckerName] = model;
                }
            }
        }
        void Start()
        {
            Initialize();
        }
        void Update()
        {
            foreach (var model in _models.Values)
            {
                model.PerformCheck(model.Data);
            }
        }
    }
}