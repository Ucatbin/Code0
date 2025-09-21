using System.Collections;
using UnityEngine;

public abstract class PlayerSkill_BaseSkill : MonoBehaviour
{
    [Tooltip("Whether skill is cooled down")]
    public bool IsReady;
    [Tooltip("Wether button released")]
    public bool CanUse;
    [Tooltip("Get player component")]
    [SerializeField] protected PlayerController_Main _player;
    [Tooltip("Get input")]
    [SerializeField] protected PlayerInput _inputSys;
    [Tooltip("The max charges of this skill, 0 means dont need charges")]
    public int MaxCharges = 0;
    public int CurrentCharges;
    public float SkillCD;

    void Awake()
    {
        CurrentCharges = MaxCharges;
    }
    public PlayerSkill_BaseSkill(PlayerController_Main player)
    {
        _player = player;
        _inputSys = _player.InputSys;
    }
    public abstract void TryUseSkill();
    public abstract void CoolDownSkill(float coolDown, string tag);
    public abstract void UseSkill();
    public abstract void ResetSkill();

    public abstract IEnumerator ButtonReleaseCheck();
}