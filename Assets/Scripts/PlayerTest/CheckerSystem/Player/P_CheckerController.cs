using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public class P_CheckerController : CheckerController
    {
        public override void RegisterModels()
        {
            foreach (var entry in _checkerEntries)
            {
                if (!string.IsNullOrEmpty(entry.CheckerName) && entry.Data != null)
                {
                    CheckerModel model = entry.CheckerName switch
                    {
                        "GroundCheck" => new GroundCheckModel(entry.Data, entry.CheckPoint),
                        "WallCheck" => new WallCheckModel(entry.Data, entry.CheckPoint),
                        _ => null
                    };

                    if (model != null)
                        _models[model.GetType()] = model;
                }
            }
        }
    }
}