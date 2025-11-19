using ThisGame.Entity.BuffSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    [CreateAssetMenu(fileName = "P_CountDownReset", menuName = "ThisGame/Player/BuffSystem/CountDown/ResetTimerMD")]
    public class Modifier_CountDownInit : BuffModifier
    {
        public override void Apply(BuffModel buffInfo)
        {
            var buff = buffInfo.ConvertTo<BuffItem_CountDown>();
            buff.CountDownTimer = buff.CountDownData.MaxCountDown;
        }
    }
}