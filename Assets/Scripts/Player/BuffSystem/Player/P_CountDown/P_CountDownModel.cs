using System.Diagnostics;
using ThisGame.Core;
using ThisGame.Entity.EntitySystem;
using TMPro;

namespace ThisGame.Entity.BuffSystem
{
    public class P_CountDownModel : BuffModel
    {
        public float CountdownTimer;
        // Dependency
        TextMeshProUGUI _countdownDisplay;
        public P_CountDownModel(P_CountDownData data, EntityController source, EntityController target, TextMeshProUGUI display) : base(data, source, target)
        {
            _countdownDisplay = display;
            CountdownTimer = data.MaxCountDown;
            EventBus.Subscribe<UpdateCountdownDisplay>(this, HandleUpdateDisplay);
        }

        void HandleUpdateDisplay(UpdateCountdownDisplay @event)
        {
            _countdownDisplay.text = @event.TimerDisplay.ToString("F1");
        }
    }
}