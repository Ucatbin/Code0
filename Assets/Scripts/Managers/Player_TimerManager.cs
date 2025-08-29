using UnityEngine;

public class Player_TimerManager : MonoBehaviour
{
    public static Player_TimerManager Instance;

    public Player_GHookTimer GHookTimer;
    public Player_GLineDashTimer GLineDashTimer;
    public Player_JumpTimer JumpTimer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}