using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CountDownReset", menuName = "Game/BuffSys/CountDownBuilder/ResetMD")]
public class Modifier_CountDownInit : BaseBuffModifier
{
    public override void Apply(BaseBuffModel buffInfo)
    {
        var buff = buffInfo.ConvertTo<BuffItem_CountDown>();
        buff.CountDownTimer = buff.CountDownData.MaxCountDown;
    }
}