using UnityEngine;

public class EntityStatePriorityCfgSO : ScriptableObject
{
    public virtual int GetPriority(System.Type stateType)
    {
        return 1;
    }
}