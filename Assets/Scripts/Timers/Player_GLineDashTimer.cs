using UnityEngine;

public class Player_GLineDashTimer : BaseTimer
{
    [SerializeField] GrappingHook _grappingHook;
    protected override void OnTimerStart()
    {
        _grappingHook.CanUseGLineDash = false;
    }
    protected override void OnTimerEnd()
    {
        _grappingHook.CanUseGLineDash = true;
    }
}