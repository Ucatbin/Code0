using System;

public class TimerItem : IComparable<TimerItem>
{
    public float TriggerTime; // Absolute trigger time on the timeline
    public Action Callback;   // Action to callback when the timer triggers
    public bool IsLoop;       // Is it a looping timer
    public float Interval;    // Interval for looping timers
    public object Tag;        // Tag for identification
    public float RemainingTime; // Remaining time when paused
    public bool IsPaused;     // Is the timer paused

    public TimerItem(
        float triggerTime,
        Action callback,
        bool isLoop = false,
        float interval = 0f,
        object tag = null
    )
    {
        TriggerTime = triggerTime;
        Callback = callback;
        IsLoop = isLoop;
        Interval = interval;
        Tag = tag;
    }
    public int CompareTo(TimerItem other)
    {
        if (other == null) return 1;
        return TriggerTime.CompareTo(other.TriggerTime);
    }
}
