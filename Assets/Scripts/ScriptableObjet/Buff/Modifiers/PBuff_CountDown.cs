using UnityEngine;

[CreateAssetMenu(fileName = "Permanent BuffModifier", menuName = "Game/BuffSys/BuffModifier/Permanent/CountDown")]

public class PBuff_CountDown : BaseBuffModifier
{
    PlayerBuffHandler _handler;
    public override void Apply(BuffItem buffInfo)
    {
        _handler = buffInfo.Target.gameObject.GetComponentInChildren<PlayerBuffHandler>();
        _handler.CountDownTimer -= 0.1f;
    }
}
