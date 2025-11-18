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
        BuffEvents.TriggerBuffUpdate(buff.CountDownTimer, buffInfo);

        // TODO: Use event to update display
        var display = Mathf.Max(buff.CountDownTimer, 0);
        buff.CountDownDisplay.text = display.ToString("F1");
    }
}