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
        public AbilityButtonPressed(int abilityHash)
        {
            AbilityHash = abilityHash;
        }
    }
    public struct AbilityButtonReleased
    {
        readonly public int AbilityHash;
        public AbilityButtonReleased(int abilityHash)
        {
            AbilityHash = abilityHash;
        }
    }
    public struct AbilityExecuted
    {
        readonly public int AbilityHash;
        public AbilityExecuted(int abilityHash)
        {
            AbilityHash = abilityHash;
        }
    }
    public struct AbilityCompleted
    {
        readonly public int AbilityHash;
        public AbilityCompleted(int abilityHash)
        {
            AbilityHash = abilityHash;
        }
    }
    public struct AbilityInterrupted
    {
        readonly public int AbilityHash;
        public AbilityInterrupted(int abilityHash)
        {
            AbilityHash = abilityHash;
        }
    }
}