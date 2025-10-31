using Ucatbin.AbilitySystem;
using UnityEngine;

namespace Ucatbin.Events.AbilityEvents
{
    public struct JumpExcuteTriggerStart { }
    public struct JumpExcuteTriggerEnd { }
    public struct Plr_AttackExecTriggerStart { }
    public struct Plr_AttackExecTriggerEnd { }
    

    public struct AbilityInputTriggerPressed
    {
        string _abilityName;
        readonly public int AbilityHash;
        public AbilityInputTriggerPressed(string abilityName)
        {
            _abilityName = abilityName;
            AbilityHash = abilityName.GetHashCode();
        }
    }
    public struct AbilityInputTriggerReleased
    {
        string _abilityName;
        readonly public int AbilityHash;
        public AbilityInputTriggerReleased(string abilityName)
        {
            _abilityName = abilityName;
            AbilityHash = abilityName.GetHashCode();
        }
    }
}