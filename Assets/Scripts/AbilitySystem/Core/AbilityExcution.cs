
namespace AbilitySystem
{
    public class AbilityExcution : IAbilityExcution
    {
        public virtual bool CanExcute(AbilityModel ability, CharacterModel character)
        {
            return ability.IsUnlocked &&
                ability.IsReady &&
                ability.IsReset;
        }

        public virtual void ConsumeResources(AbilityModel ability, CharacterModel character)
        {
            if (!CanExcute(ability, character)) return;
            ConsumeResources(ability, character);
        }

        public virtual void Excute(AbilityModel ability, CharacterModel character)
        {
            
        }
    }
}