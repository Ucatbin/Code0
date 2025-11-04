using ThisGame.AbilitySystem;
using UnityEngine;

namespace ThisGame.Events.AbilityEvents
{
    public struct JumpExecuteTriggerStart { }
    public struct JumpExecuteTriggerEnd { }
    public struct Plr_AttackExecTriggerStart { }
    public struct Plr_AttackExecTriggerEnd { }
    

    public struct AbilityButtonPressed
    {
        readonly public int AbilityHash;
        public AbilityButtonPressed(string abilityName)
        {
            AbilityHash = abilityName.GetHashCode();
        }
    }
    public struct AbilityButtonReleased
    {
        readonly public int AbilityHash;
        public AbilityButtonReleased(string abilityName)
        {
            AbilityHash = abilityName.GetHashCode();
        }
    }
    public struct AbilityExecuted
    {
        readonly public int AbilityHash;
        public AbilityExecuted(string abilityName)
        {
            AbilityHash = abilityName.GetHashCode();
        }
    }
    public struct AbilityCompleted
    {
        readonly public int AbilityHash;
        public AbilityCompleted(string abilityName)
        {
            AbilityHash = abilityName.GetHashCode();
        }
    }
    public struct AbilityInterrupted
    {
        readonly public int AbilityHash;
        public AbilityInterrupted(string abilityName)
        {
            AbilityHash = abilityName.GetHashCode();
        }
    }
}