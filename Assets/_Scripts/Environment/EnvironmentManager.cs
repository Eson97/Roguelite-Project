using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : Singleton<EnvironmentManager>
{
    [SerializeField] private DayTimeSO[] _dayTimes;
    [SerializeField] private WeekDaySO[] _weekDays;

    [Header("Debug")]
    [SerializeField] private bool _logOnTimeChanged = false;

    private int _currentDayTimeIndex = 0;
    private int _currentWeekDayIndex = 0;

    public DayTimeSO CurrentDayTime => _dayTimes[_currentDayTimeIndex];
    public WeekDaySO CurrentWeekDay => _weekDays[_currentWeekDayIndex];

    public event Action<DayTimeSO> OnDayTimeChanged;
    public event Action<WeekDaySO> OnWeekDayChanged;

    protected override void Awake()
    {
        base.Awake();

        if (_logOnTimeChanged)
        {
            OnDayTimeChanged += (dayTime) => Debug.Log(dayTime);
            OnWeekDayChanged += (weekDay) => Debug.Log(weekDay);
        }
    }
    private void Start()
    {
        OnDayTimeChanged?.Invoke(CurrentDayTime);
        OnWeekDayChanged?.Invoke(CurrentWeekDay);
    }

    public void AdvanceDayTime(int times)
    {
        for (int i = 0; i < times; i++)
            NextDayTime();
    }
    public void NextDayTime()
    {
        if(CurrentDayTime.Value == DayTimeSO.Values.Night)
        {
            NextWeekDay();
        }
        else
        {
            _currentDayTimeIndex = (++_currentDayTimeIndex) % _dayTimes.Length;
            OnDayTimeChanged?.Invoke(CurrentDayTime);
        }

    }
    public void NextWeekDay()
    {
        _currentDayTimeIndex = 0;
        _currentWeekDayIndex = (++_currentWeekDayIndex) % _weekDays.Length;

        OnDayTimeChanged?.Invoke(CurrentDayTime);
        OnWeekDayChanged?.Invoke(CurrentWeekDay);
    }
}
