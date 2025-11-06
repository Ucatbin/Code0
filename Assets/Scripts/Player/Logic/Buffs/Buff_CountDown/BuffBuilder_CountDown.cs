using TMPro;
using UnityEngine;

#region Data
[CreateAssetMenu(fileName = "CountDown_BuffData", menuName = "Game/BuffSys/CountDownBuilder/Data")]
public class BuffDataSO_CountDown : BuffDataSO
{
    [Header("Settings")]
    public float MaxCountDown = 10f;
}
#endregion

#region Item
public class BuffItem_CountDown : BaseBuffItem
{
    public TextMeshProUGUI CountDownDisplay;
    public float CountDownTimer;
    public BuffDataSO_CountDown CountDownData => BuffData as BuffDataSO_CountDown;
    public BuffItem_CountDown(BuffDataSO_CountDown buffData, EntityControllerOld caster, EntityControllerOld target, int curStack, TextMeshProUGUI display) : base(buffData, caster, target, curStack)
    {
        CountDownDisplay = display;
    }
}
#endregion