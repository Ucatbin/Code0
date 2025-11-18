using ThisGame.Entity.EntitySystem;
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

#region Model
public class BuffItem_CountDown : BaseBuffModel
{
    public TextMeshProUGUI CountDownDisplay;
    public float CountDownTimer;
    public BuffDataSO_CountDown CountDownData => BuffData as BuffDataSO_CountDown;
    public BuffItem_CountDown(BuffDataSO_CountDown buffData, EntityController caster, EntityController target, int curStack, TextMeshProUGUI display) : base(buffData, caster, target, curStack)
    {
        CountDownDisplay = display;
    }
}
#endregion