using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }
    SortedSet<TimerItem> _timerHeap = new SortedSet<TimerItem>();
    float _currentTime;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        _currentTime = Time.fixedTime;

        while (_timerHeap.Count > 0)
        {
            // Get the nearest timer trigger
            TimerItem nextTimer = _timerHeap.Min;
            // If the nearest timer trigger havent come yet, exit the loop
            if (nextTimer.TriggerTime > _currentTime)
                break;

            // Invoke and remove the timer from the heap
            _timerHeap.Remove(nextTimer);
            nextTimer.Callback?.Invoke();

            // If it's a looping timer, reschedule it
            if (nextTimer.IsLoop)
            {
                nextTimer.TriggerTime = _currentTime + nextTimer.Interval;
                _timerHeap.Add(nextTimer);
            }
        }
    }

    // Main internal method to add timers
    void AddTimerInternal(float delayOrTime, Action callback, bool isLoop, float interval, object tag)
    {
        float triggerTime = _currentTime + delayOrTime;

        TimerItem newItem = new TimerItem(triggerTime, callback, isLoop, interval, tag);
        _timerHeap.Add(newItem);
    }

    #region Add Timers
    public void AddTimer(float delay, Action callback, object tag = null)
    {
        AddTimerInternal(delay, callback, false, 0f, tag);
    }
    public void AddLoopTimer(float interval, Action callback, bool immediateFirstCall = false, object tag = null)
    {
        float firstTriggerTime = _currentTime + (immediateFirstCall ? 0f : interval);
        AddTimerInternal(firstTriggerTime, callback, true, interval, tag);
    }
    #endregion

    #region Extend|Set Timers
    public void ExtendTimersWithTag(object tag, float additionalTime)
    {
        List<TimerItem> toUpdate = new List<TimerItem>();
        
        foreach (var timer in _timerHeap)
        {
            if (Equals(timer.Tag, tag) && !timer.IsPaused)
            {
                toUpdate.Add(timer);
            }
        }
        
        foreach (var timer in toUpdate)
        {
            _timerHeap.Remove(timer);
            timer.TriggerTime += additionalTime;
            _timerHeap.Add(timer);
        }
    }

    public void SetTimersWithTag(object tag, float delay)
    {
        List<TimerItem> toUpdate = new List<TimerItem>();
        
        foreach (var timer in _timerHeap)
        {
            if (Equals(timer.Tag, tag) && !timer.IsPaused)
            {
                toUpdate.Add(timer);
            }
        }
        
        foreach (var timer in toUpdate)
        {
            _timerHeap.Remove(timer);
            timer.TriggerTime = _currentTime + delay;
            _timerHeap.Add(timer);
        }
    }
    #endregion

    #region Pause Timers
    public void PauseTimerWithTag(object tag)
    {
        foreach (var timer in _timerHeap)
        {
            if (Equals(timer.Tag, tag) && !timer.IsPaused)
            {
                if (timer.IsPaused) return;

                timer.RemainingTime = timer.TriggerTime - _currentTime;
                timer.IsPaused = true;
                _timerHeap.Remove(timer);
            }
        }
    }
    public void ResumeTimerWithTag(object tag)
    {
        List<TimerItem> toResume = new List<TimerItem>();
        foreach (var timer in _timerHeap)
        {
            if (Equals(timer.Tag, tag) && timer.IsPaused)
                toResume.Add(timer);
        }
        foreach (var timer in toResume)
        {
            if (!timer.IsPaused) return;

            timer.TriggerTime = _currentTime + timer.RemainingTime;
            timer.IsPaused = false;
            _timerHeap.Add(timer);
        }
    }
    public void PauseAllTimers()
    {
        foreach (var timer in _timerHeap)
        {
            if (!timer.IsPaused)
            {
                if (timer.IsPaused) return;

                timer.RemainingTime = timer.TriggerTime - _currentTime;
                timer.IsPaused = true;
                _timerHeap.Remove(timer);
            }
        }
    }
    public void ResumeAllTimers()
    {
        List<TimerItem> toResume = new List<TimerItem>();
        foreach (var timer in _timerHeap)
        {
            if (timer.IsPaused)
                toResume.Add(timer);
        }

        foreach (var timer in toResume)
        {
            if (timer.IsPaused) return;

            timer.RemainingTime = timer.TriggerTime - _currentTime;
            timer.IsPaused = true;
            _timerHeap.Remove(timer);
        }
    }
    #endregion

    # region Clear Timers
    // Clear timers with specific tag
    public void CancelTimersWithTag(object tag)
    {
        List<TimerItem> toRemove = new List<TimerItem>();
        foreach (var timer in _timerHeap)
        {
            if (Equals(timer.Tag, tag))
                toRemove.Add(timer);
        }
        foreach (var timer in toRemove)
            _timerHeap.Remove(timer);
    }
    // Clear all timers
    public void ClearAllTimers()
    {
        _timerHeap.Clear();
    }
    # endregion
}