using AbilitySystem;
using UnityEngine;

public class JumpAbilityExcution : AbilityExcution
{
    public override bool CanExcute(AbilityModel ability, EntityModel character)
    {
        return base.CanExcute(ability, character) &&
            !character.IsBusy;
    }
    public override void ConsumeResources(AbilityModel ability, EntityModel character)
    {
        throw new System.NotImplementedException();
    }

    public override void Excute(AbilityModel ability, EntityModel character)
    {
        SkillEvents.TriggerJumpStart();
    }
}