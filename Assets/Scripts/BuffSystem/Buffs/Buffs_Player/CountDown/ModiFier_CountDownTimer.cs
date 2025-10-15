using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CountDownTimer", menuName = "Game/BuffSys/CountDownBuilder/TimerMD")]
public class Modifier_CountDownTimer : BaseBuffModifier
{
    public override void Apply(BaseBuffItem buffInfo)
    {
        var buff = buffInfo.ConvertTo<BuffItem_CountDown>();
        buff.CountDownTimer -= 0.1f;
        buff.CountDownDisplay.text = buff.CountDownTimer.ToString("F1");
    }
}