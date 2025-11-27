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
            var buff = buffInfo.ConvertTo<P_CountDownModel>();
            var data = buff.Data as P_CountDownData;
            buff.CountdownTimer = data.MaxCountDown;

            var updateDisplay = new UpdateCountdownDisplay
            {
                TimerDisplay = data.MaxCountDown
            };
            Core.EventBus.Publish(updateDisplay);
        }
    }
}