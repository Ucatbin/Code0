using ThisGame.Entity.StateMachineSystem;

namespace ThisGame.Entity.SkillSystem
{
    public abstract class SkillModel
    {
        protected bool _isUnlocked;
        public bool IsUnlocked => _isUnlocked;
        protected int _currentLevel;
        public int CurrentLevel => _currentLevel;
        protected float _currentCoolDown;
        public float CurrentCoolDown => _currentCoolDown;
        protected int _currentCharges;
        public int CurrentCharges => _currentCharges;
        protected bool _isReady;
        public bool IsReady => _isReady;

        // Dependency
        SkillData _data;
        public SkillData Data => _data;
        public SkillModel(SkillData data)
        {
            _data = data;
            _currentCharges = _data.MaxCharges;
            _isReady = true;
        }
    
        public void Unlock() => _isUnlocked = true;
        public virtual void HandleSkillButtonPressed(P_SkillPressed e) { }
        public virtual void HandleSkillButtonReleased(P_SkillReleased e) { }
        public virtual void ExecuteSkill(P_SkillExecute e) { }
        public virtual void ExecuteSkill() { }
        protected virtual void ConsumeResources(SkillData data)
        {
            _isReady = false;
            _currentCharges -= _data.MaxCharges == -1 ? 0 : 1;
        }
        public virtual void StartCoolDown() { }
    }
}