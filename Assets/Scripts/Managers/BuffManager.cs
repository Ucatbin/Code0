using JetBrains.Annotations;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance { get; private set; }

    [field: SerializeField] public BuffDataSO Buff_SpeedUp { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}

public static class BuffFactory
{
    public static BuffItem CreateBuffItem(BuffDataSO buffData, GameObject caster, GameObject target, int stack)
    {
        BuffItem buffItem = new BuffItem(buffData, caster, target, stack);
        return buffItem;
    }
}
