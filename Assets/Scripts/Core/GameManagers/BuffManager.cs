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
    public static BaseBuffItem CreateBuffItem(BuffDataSO buffData, Character caster, Character target, int stack)
    {
        BaseBuffItem buffItem = new BaseBuffItem(buffData, caster, target, stack);
        return buffItem;
    }
}
