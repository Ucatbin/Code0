using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    [SerializeField] List<TimerItem> _timerList = new List<TimerItem>();
    List<TimerItem> _toResume = new List<TimerItem>();

    SortedSet<TimerItem> _timerHeap;

    float _currentTime => SmoothTime.SimulatedTime;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _timerHeap = new SortedSet<TimerItem>(_timerList);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        ProcessTimers();
    }

    void ProcessTimers()
    {
        while (_timerHeap.Count > 0)
        {
            TimerItem nextTimer = _timerHeap.Min;

            if (nextTimer.TriggerTime > _currentTime)
                break;

            _timerHeap.Remove(nextTimer);
            _timerList.Remove(nextTimer);

            nextTimer.Callback?.Invoke();

            if (nextTimer.IsLoop)
            {
                nextTimer.TriggerTime = _currentTime + nextTimer.Interval;
                _timerList.Add(nextTimer);
                _timerHeap.Add(nextTimer);
            }
        }
    }

    void AddTimerInternal(float delay, Action callback, bool isLoop, float interval, object tag)
    {
        float triggerTime = _currentTime + delay;

        TimerItem newItem = new TimerItem(triggerTime, callback, isLoop, interval, tag);

        _timerList.Add(newItem);
        _timerHeap.Add(newItem);
    }

    #region Add Timers
    public void AddTimer(float delay, Action callback, object tag = null)
        => AddTimerInternal(delay, callback, false, 0f, tag);

    public void AddLoopTimer(float interval, Action callback, bool immediateFirstCall = false, object tag = null)
    {
        float firstTrigger = _currentTime + (immediateFirstCall ? 0f : interval);
        AddTimerInternal(firstTrigger, callback, true, interval, tag);
    }
    #endregion

    #region Extend | Set
    public void ExtendTimersWithTag(object tag, float add)
    {
        List<TimerItem> list = new List<TimerItem>();

        foreach (var t in _timerHeap)
            if (Equals(t.Tag, tag) && !t.IsPaused)
                list.Add(t);

        foreach (var t in list)
        {
            _timerHeap.Remove(t);
            _timerList.Remove(t);

            t.TriggerTime += add;

            _timerList.Add(t);
            _timerHeap.Add(t);
        }
    }

    public void SetTimersWithTag(object tag, float delay)
    {
        List<TimerItem> list = new List<TimerItem>();

        foreach (var t in _timerHeap)
            if (Equals(t.Tag, tag) && !t.IsPaused)
                list.Add(t);

        foreach (var t in list)
        {
            _timerHeap.Remove(t);
            _timerList.Remove(t);

            t.TriggerTime = _currentTime + delay;

            _timerList.Add(t);
            _timerHeap.Add(t);
        }
    }
    #endregion

    #region Pause | Resume
    public void PauseTimerWithTag(object tag)
    {
        List<TimerItem> list = new List<TimerItem>();

        foreach (var t in _timerHeap)
            if (Equals(t.Tag, tag) && !t.IsPaused)
                list.Add(t);

        foreach (var timer in list)
        {
            timer.RemainingTime = timer.TriggerTime - _currentTime;
            timer.IsPaused = true;

            _timerHeap.Remove(timer);
            _timerList.Remove(timer);
            _toResume.Add(timer);
        }
    }

    public void ResumeTimerWithTag(object tag)
    {
        List<TimerItem> list = new List<TimerItem>();

        foreach (var t in _toResume)
            if (Equals(t.Tag, tag))
                list.Add(t);

        foreach (var t in list)
        {
            t.TriggerTime = _currentTime + t.RemainingTime;
            t.IsPaused = false;

            _toResume.Remove(t);

            _timerList.Add(t);
            _timerHeap.Add(t);
        }
    }
    #endregion

    #region Clear
    public void CancelTimersWithTag(object tag)
    {
        List<TimerItem> list = new List<TimerItem>();
        foreach (var t in _timerHeap)
            if (Equals(t.Tag, tag))
                list.Add(t);

        foreach (var t in list)
        {
            _timerHeap.Remove(t);
            _timerList.Remove(t);
        }
    }

    public void ClearAllTimers()
    {
        _timerList.Clear();
        _timerHeap.Clear();
        _toResume.Clear();
    }
    #endregion
}
