using TMPro;
using UnityEngine;

public class PlayerBuffHandler : BuffHandler
{
    [SerializeField] BuffDataSO_CountDown PBuff_CountDown;
    [SerializeField] TextMeshProUGUI _countDownDisplay;

    void Start()
    {
        var P_CountDown = new BuffItem_CountDown(PBuff_CountDown, _entity, _entity, 1, _countDownDisplay);
        _entity.BuffSys.AddBuff(P_CountDown);
    }
}