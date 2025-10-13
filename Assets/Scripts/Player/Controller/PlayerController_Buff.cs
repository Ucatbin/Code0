using UnityEngine;

public class PlayerController_Buff : MonoBehaviour
{
    [SerializeField] PlayerController_Main _player;

    [SerializeField] BuffDataSO PBuff_KillToAccel;

    void Start()
    {
        var P_KillToAccel = BuffFactory.CreateBuffItem(PBuff_KillToAccel, _player, _player, 1);
        _player.BuffHandler.AddBuff(P_KillToAccel);
    }
}
