using UnityEngine;

public class Player_SkillManager : MonoBehaviour
{
    public static Player_SkillManager Instance { get; private set; }
    [SerializeField] Player _player;

    [field: SerializeField] public PlayerSkill_GrappingHook GrappingHook { get; private set; }
    [field: SerializeField] public PlayerSkill_GrappingHookDash GrappingHookDash { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}