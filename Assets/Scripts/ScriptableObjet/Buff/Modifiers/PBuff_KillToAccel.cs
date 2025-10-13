using UnityEngine;

[CreateAssetMenu(fileName = "Permanent BuffModifier", menuName = "Game/BuffSys/BuffModifier/Permanent/KillToAccel")]

public class PKillAccelMD : BaseBuffModifier
{
    public override void Apply(BuffItem buffInfo)
    {
        var buff_speedUp = BuffFactory.CreateBuffItem(BuffManager.Instance.Buff_SpeedUp, buffInfo.Target, buffInfo.Target, 1);
        buffInfo.Target.BuffSys.AddBuff(buff_speedUp);
    }
}
