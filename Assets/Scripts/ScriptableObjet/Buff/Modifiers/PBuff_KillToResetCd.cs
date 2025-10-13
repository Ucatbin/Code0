using UnityEngine;

[CreateAssetMenu(fileName = "Permanent BuffModifier", menuName = "Game/BuffSys/BuffModifier/Permanent/ResetCountDown")]

public class PBuff_KillToResetCd : BaseBuffModifier
{
    public override void Apply(BuffItem buffInfo)
    {
        var cdTimer = buffInfo.Target.GetComponent<PlayerBuffHandler>().CountDownTimer;
        cdTimer = buffInfo.Target.GetComponent<PlayerBuffHandler>().MaxCountDown;
    }
}
