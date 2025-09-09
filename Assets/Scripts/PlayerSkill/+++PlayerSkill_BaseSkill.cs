using UnityEngine;

public abstract class PlayerSkill_BaseSkill :MonoBehaviour
{
    public bool CanUseSkill;
    [SerializeField] protected PlayerController _player;
    [SerializeField] protected PlayerInput _inputSys;
    public int MaxCharges = -1;
    public int CurrentCharges;
    public float SkillCD;

    void Awake()
    {
        CurrentCharges = MaxCharges == -1 ? 0 : MaxCharges;
    }
    public PlayerSkill_BaseSkill(PlayerController player)
    {
        _player = player;
        _inputSys = _player.InputSys;
    }
    public abstract void TryUseSkill();
    public abstract void CoolDownSkill(float coolDown, string tag);
    public abstract void UseSkill();
    public abstract void ResetSkill();
}