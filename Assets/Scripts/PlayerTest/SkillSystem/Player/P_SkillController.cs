using ThisGame.Entity.SkillSystem;
using UnityEngine;

public class P_SkillController : SkillController
{
    public override void RegisterModels()
    {
        foreach (var entry in _skillEnties)
        {
            if (!string.IsNullOrEmpty(entry.SkillName) && entry.Data != null)
            {
                SkillModel model = entry.SkillName switch
                {
                    "P_DoubleJump" => new P_DoubleJumpModel(entry.Data),
                    "P_GrappingHook" => new P_GrappingHookModel(entry.Data),
                    _ => null
                };

                if (model != null)
                    _models[entry.SkillName] = model;
            }
        }
    }
}