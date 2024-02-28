using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obtained from
/// https://www.jonathanyu.xyz/2020/01/21/unity-cooldown-timer-script-tutorial/
/// </summary>
public class CooldownTimer
{
    public float TimeRemaining { get; private set; }
    public float TotalTime { get; private set; }
    public bool IsRecurring { get; }
    public bool IsActive { get; private set; }
    public int TimesCounted { get; private set; }

    public float TimeElapsed => TotalTime - TimeRemaining;
    public float PercentElapsed => TimeElapsed / TotalTime;
    public bool IsCompleted => TimeRemaining <= 0;

    public delegate void TimerCompleteHandler();

    public event TimerCompleteHandler TimerCompleteEvent;

    public CooldownTimer(float time, bool recurring = false)
    {
        TotalTime = time;
        IsRecurring = recurring;
        TimeRemaining = TotalTime;
    }

    public void Start()
    {
        if (IsActive) { TimesCounted++; }
        TimeRemaining = TotalTime;
        IsActive = true;
        if (TimeRemaining <= 0)
        {
            TimerCompleteEvent?.Invoke();
        }
    }

    public void Start(float time)
    {
        TotalTime = time;
        Start();
    }

    public void Update(float timeDelta)
    {
        if (TimeRemaining > 0 && IsActive)
        {
            TimeRemaining -= timeDelta;
            if (TimeRemaining <= 0)
            {
                if (IsRecurring)
                {
                    TimeRemaining = TotalTime;
                }
                else
                {
                    IsActive = false;
                }

                TimerCompleteEvent?.Invoke();
                TimesCounted++;
            }
        }
    }

    public bool UpdateCheck(float timeDelta)
    {
        if (TimeRemaining > 0 && IsActive)
        {
            TimeRemaining -= timeDelta;
            if (TimeRemaining <= 0)
            {
                if (IsRecurring)
                {
                    TimeRemaining = TotalTime;
                }
                else
                {
                    IsActive = false;
                }

                TimerCompleteEvent?.Invoke();
                TimesCounted++;
            }
            return true;
        }
        return false;
    }

    public void Invoke()
    {
        TimerCompleteEvent?.Invoke();
    }

    public void Pause()
    {
        IsActive = false;
    }

    public void AddTime(float time)
    {
        TimeRemaining += time;
        TotalTime += time;
    }
}
