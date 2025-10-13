using UnityEngine;

[CreateAssetMenu(fileName = "Permanent BuffModifier", menuName = "Game/BuffSys/BuffModifier/Permanent")]

public class PKillAccelMD : BaseBuffModifier
{
    public override void Apply(BuffItem buffInfo)
    {
        var buff_speedUp = BuffFactory.CreateBuffItem(BuffManager.Instance.Buff_SpeedUp, buffInfo.Target, buffInfo.Target, 1);
        buffInfo.Target.BuffHandler.AddBuff(buff_speedUp);
    }
}
