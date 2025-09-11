using UnityEngine;
public enum BuffUpdateType
{
    Add,
    Replace,
    None
}
public enum BuffRemoveType
{
    Reduce,
    Clear
}

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
    public float Duration;
    public float Interval;
    [Header("Update")]
    public BuffUpdateType BuffUpdateT;
    public BuffRemoveType BuffRemoveT;
    [Header("Call Back")]
    public BaseBuffModel OnCreat;
    public BaseBuffModel OnRemove;
    public BaseBuffModel OnTick;

    public BaseBuffModel OnHit;
    public BaseBuffModel OnBeHurt;
    public BaseBuffModel OnKill;
    public BaseBuffModel OnBekill;
}
