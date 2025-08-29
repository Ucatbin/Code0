using System.Collections;
using UnityEngine;

public abstract class BaseTimer : MonoBehaviour
{
    // Necessary parameter
    protected Coroutine _timerCoroutine;
    public float CurrentTimerVal;

    // Basic timer coroutine
    IEnumerator Timer(float targetTime)
    {
        float timer = 0f;
        OnTimerStart();
        while (timer < targetTime)
        {
            timer += Time.deltaTime;
            CurrentTimerVal = timer;
            yield return null;
        }
        OnTimerEnd();
        _timerCoroutine = null;
    }

    // Public timer control voids
    public void StartTimer(float targetTime)
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);

        CurrentTimerVal = 0f;
        _timerCoroutine = StartCoroutine(Timer(targetTime));
    }
    public void StopTimer()
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
        _timerCoroutine = null;
    }

    protected abstract void OnTimerStart();
    protected abstract void OnTimerEnd();
    public bool IsBusy => _timerCoroutine != null;
}