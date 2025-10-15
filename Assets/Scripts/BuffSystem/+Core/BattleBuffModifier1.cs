using UnityEngine;

[System.Serializable]
public abstract class BattleBuffModifier : ScriptableObject
{
    public abstract void Apply(BaseBuffItem buffInfo, DamageData damageInfo);
}