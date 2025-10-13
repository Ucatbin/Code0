using UnityEngine;

[System.Serializable]
public abstract class BaseBuffModifier : ScriptableObject
{
    public abstract void Apply(BuffItem buffInfo);
}