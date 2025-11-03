using ThisGame.AbilitySystem;
using UnityEngine;

namespace ThisGame.Events.AbilityEvents
{
    public struct JumpExecuteTriggerStart { }
    public struct JumpExecuteTriggerEnd { }
    public struct Plr_AttackExecTriggerStart { }
    public struct Plr_AttackExecTriggerEnd { }
    

    public struct AbilityInputTriggerPressed
    {
        readonly public int AbilityHash;
        public AbilityInputTriggerPressed(string abilityName)
        {
            AbilityHash = abilityName.GetHashCode();
        }
    }
    public struct AbilityInputTriggerReleased
    {
        readonly public int AbilityHash;
        public AbilityInputTriggerReleased(string abilityName)
        {
            AbilityHash = abilityName.GetHashCode();
        }
    }
}