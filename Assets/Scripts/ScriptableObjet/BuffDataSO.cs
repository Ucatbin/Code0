using UnityEngine;

[CreateAssetMenu(fileName = "new BuffData", menuName = "Game/BuffSys/BuffData")]
public class BuffDataSO : ScriptableObject
{
    [Header("Basic Infos")]
    public int Id;
    public string BuffName;
    public string Description;
    public Sprite Icon;
    public int Priority;
    public int MaxStacks;
    public string[] Tags;
    [Header("Time Info")]
    public float Duration;  // The duration of buff
    public float Interval;  // How many tick will buff last
    [Header("Update")]
    public BuffType BuffType;
    public BuffStackType BuffStackType;
    public BuffRemoveType BuffRemoveType;
    [Header("Call Back")]
    public BaseBuffModifier OnCreat;   // When buff is added
    public BaseBuffModifier OnRemove;  // When buff is removed
    public BaseBuffModifier OnInvoke;  // When buff should be handle

    public BaseBuffModifier OnHit;
    public BaseBuffModifier OnBeHurt;
    public BaseBuffModifier OnKill;
    public BaseBuffModifier OnBekill;
}