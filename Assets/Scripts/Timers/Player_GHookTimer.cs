using UnityEngine;

public class Player_GHookTimer : BaseTimer
{
    [SerializeField] GrappingHook _grappingHook;
    protected override void OnTimerStart()
    {
        _grappingHook.CanUseGHook = false;
    }
    protected override void OnTimerEnd()
    {
        _grappingHook.CanUseGHook = true;
    }

}