using Unity.VisualScripting;
using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    [CreateAssetMenu(fileName = "P_CountDownTimer", menuName = "ThisGame/Player/BuffSystem/CountDown/TimerMD")]
    public class Modifier_CountDownTimer : BuffModifier
    {
        public override void Apply(BuffModel buffInfo)
        {
            var buff = buffInfo.ConvertTo<P_CountDownModel>();
            buff.CountdownTimer -= 0.1f;

            var display = Mathf.Max(buff.CountdownTimer, 0);
            var updateDisplay = new UpdateCountdownDisplay
            {
                TimerDisplay = display
            };
            Core.EventBus.Publish(updateDisplay);
        }
    }
}