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
    public BaseBuffModel OnCreat;   // When buff is added
    public BaseBuffModel OnRemove;  // When buff is removed
    public BaseBuffModel OnInvoke;  // When buff should be handle

    public BaseBuffModel OnHit;
    public BaseBuffModel OnBeHurt;
    public BaseBuffModel OnKill;
    public BaseBuffModel OnBekill;
}
