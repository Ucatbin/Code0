using UnityEngine;

public class PlayerBuffHandler : BuffHandler
{
    [SerializeField] BuffDataSO PBuff_KillToAccel;
    [SerializeField] BuffDataSO PBuff_CountDown;
    public float MaxCountDown = 10f;
    public float CountDownTimer;

    void Start()
    {
        CountDownTimer = MaxCountDown;
        
        var P_KillToAccel = BuffFactory.CreateBuffItem(PBuff_KillToAccel, _entity, _entity, 1);
        _entity.BuffSys.AddBuff(P_KillToAccel);
        var P_CountDown = BuffFactory.CreateBuffItem(PBuff_CountDown, _entity, _entity, 1);
        _entity.BuffSys.AddBuff(P_CountDown);
    }
}