using UnityEngine;

public abstract class Player_BaseSkill :MonoBehaviour
{
    [SerializeField] protected PlayerController _player;
    [SerializeField] protected PlayerInput _inputSys;
    public float CoolDown;
    public bool CanUseSkill;
    
    public Player_BaseSkill(PlayerController player)
    {
        _player = player;
        _inputSys = _player.InputSys;
    }

    public abstract void CoolDownSkill();
    public abstract void UseSkill();
    public abstract void ResetSkill();
}